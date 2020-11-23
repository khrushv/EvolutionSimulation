
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
