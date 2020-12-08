
using System;

namespace app
{
    class AI
    {
        public static int[] skillsTotal = new int[4];
        private int inputsCount = 4;
        public int foodSkill = 0;
        public int attackSkill = 0;
        public int defSkill = 0;
        public float energy = 10;
        public float age = 0;
        private Genome genome;
        private NN nn;
        public void Init(Genome g)
        {
            genome = g;
            //TODO color
            //Color col = new Color(0.1f, 0.1f, 0.25f, 1f);
            float size = 0.75f;
            for (int i = 0; i < Genome.skillCount; i++)
            {
                skillsTotal[g.skills[i]]++;
                if (g.skills[i] == 0)
                {
                    foodSkill++;
                    //col.g += 0.2f;
                }
                else if (g.skills[i] == 1)
                {
                    attackSkill++;
                   // col.r += 0.25f;
                }
                else if (g.skills[i] == 2)
                {
                    defSkill++;
                    //col.b += 0.25f;
                }
                else if (g.skills[i] == 3)
                {
                    size += 0.5f;
                }
            }
            nn = new NN(inputsCount, 8, 4);
            for (int i = 0; i < inputsCount; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    nn.layers[0].weights[i, j] = genome.weights[i + j * inputsCount];
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    nn.layers[1].weights[i, j] = genome.weights[i + j * 8 + inputsCount * 8];
                }
            }
        }
        public void Kill()
        {
            for (int i = 0; i < Genome.skillCount; i++)
            {
                skillsTotal[genome.skills[i]]--;
            }
            //TODO: 
            //Destroy(gameObject);
        }
        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90);
            age += Time.deltaTime;
        }

        void FixedUpdate()
        {
            float vision = 5f + attackSkill;
            float[] inputs = new float[inputsCount];
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, vision);

            // количество соседних объектов
            float[] neighboursCount = new float[4];

            // вектара к центрам масс еды, красного, зеленого и синего
            Vector3[] vectors = new Vector3[4];
            for (int i = 0; i < 4; i++)
            {
                neighboursCount[i] = 0;
                vectors[i] = new Vector3(0f, 0f, 0f);
            }
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject == gameObject) continue;
                if (colliders[i].gameObject.name == "food")
                {
                    neighboursCount[0]++;
                    vectors[0] += colliders[i].gameObject.transform.position - transform.position;
                }
                else if (colliders[i].gameObject.name == "bacterium")
                {
                    AI ai = colliders[i].gameObject.GetComponent<AI>();
                    neighboursCount[1] += ai.attackSkill / 3f;
                    vectors[1] += (colliders[i].gameObject.transform.position - transform.position) * ai.attackSkill;
                    neighboursCount[2] += ai.foodSkill / 3f;
                    vectors[2] += (colliders[i].gameObject.transform.position - transform.position) * ai.foodSkill;
                    neighboursCount[3] += ai.defSkill / 3f;
                    vectors[3] += (colliders[i].gameObject.transform.position - transform.position) * ai.defSkill;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (neighboursCount[i] > 0)
                {
                    vectors[i] /= neighboursCount[i] * vision;
                    inputs[i] = vectors[i].magnitude;
                }
                else
                {
                    inputs[i] = 0f;
                }
            }

            float[] outputs = nn.FeedForward(inputs);
            Vector2 target = new Vector2(0, 0);
            for (int i = 0; i < 4; i++)
            {
                if (neighboursCount[i] > 0)
                {
                    Vector2 dir = new Vector2(vectors[i].x, vectors[i].y);
                    dir.Normalize();
                    target += dir * outputs[i];
                }
            }
            if (target.magnitude > 1f) target.Normalize();
            Vector2 velocity = rb.velocity;
            velocity += target * (0.25f + attackSkill * 0.05f);
            velocity *= 0.98f;
            rb.velocity = velocity;
            float antibiotics = 1f;
            // концентрация антибиотиков
            // if(transform.position.x < -39) antibiotics = 4;
            // else if(transform.position.x < -20) antibiotics = 3;
            // else if(transform.position.x < -1) antibiotics = 2;
            // antibiotics = Mathf.Max(1f, antibiotics - defSkill);
            energy -= Time.deltaTime * antibiotics * antibiotics;
            if (energy < 0f)
            {
                Kill();
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (foodSkill == 0) return;
            if (col.gameObject.name == "food")
            {
                Eat(foodSkill);
                Destroy(col.gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (age < 1f) return;
            if (attackSkill == 0) return;
            if (col.gameObject.name == "bacterium")
            {
                AI ai = col.gameObject.GetComponent<AI>();
                if (ai.age < 1f) return;
                float damage = Mathf.Max(0f, attackSkill - ai.defSkill);
                damage *= 4f;
                damage = Mathf.Min(damage, ai.energy);
                ai.energy -= damage * 1.25f;
                Eat(damage);
                if (ai.energy == 0f) ai.Kill();
            }
        }
        private void Eat(float food)
        {
            energy += food;
            if (energy > 16)
            {
                energy *= 0.5f;
                Genome g = new Genome(genome);
                g.Mutate(0.5f);
                //TODO
                //Creating new instance
                //ai.Init(g);
                //ai.energy = energy;
            }
        }
        static void Main(string[] args)
        {
            NN nn = new NN(4, 8, 4);
            float[] inputs = new float[] { 0.1f, 0.2f, 0.3f, 0.4f};
            float[] ar = nn.FeedForward(inputs);
        }

    }
}
