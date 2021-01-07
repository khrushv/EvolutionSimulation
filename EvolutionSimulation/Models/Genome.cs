using System;

namespace app
{
    public class Genome
    {
        public static int skillCount = 5;

        public float[] weights;
        public int[] skills;
        public NN nn = new NN(new int[] { 1,1});

        public Genome(int size)
        {
            Random rnd = new Random();
            weights = new float[size];
            skills = new int[skillCount];
            for (int i = 0; i < size; i++)
            {
                weights[i] = nn.NextDoubleRange(-1f, 1f);
            }
        }

        public Genome(Genome a)
        {
            weights = new float[a.weights.Length];
            Array.Copy(a.weights, 0, weights, 0, a.weights.Length);
            skills = new int[skillCount];
            Array.Copy(a.skills, 0, skills, 0, skillCount);
        }

        public void Mutate(float value)
        {
            Random rnd = new Random();
            for (int i = 0; i < weights.Length; i++)
            {
                if (nn.NextDoubleRange(0, 1f) < 0.1) weights[i] += nn.NextDoubleRange( -value, value);
            }
            for (int i = 0; i < skillCount; i++)
            {
                if (nn.NextDoubleRange( 0, 1f) < 0.05)
                {
                    skills[i] = rnd.Next(0, 4);
                }
            }
        }

    }
}