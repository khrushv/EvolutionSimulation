using System;
namespace app
{
    public class NN
    {
        public Layer[] layers;

        public static float NextDoubleRange(System.Random random, double minNumber, double maxNumber)
        {
            return (float)((float)random.NextDouble() * (maxNumber - minNumber) + minNumber);
        }
        public NN(params int[] sizes)
        {
            Random rnd = new Random();
            layers = new Layer[sizes.Length];
            for (int i = 0; i < sizes.Length; i++)
            {
                int nextSize = 0;
                if (i < sizes.Length - 1) nextSize = sizes[i + 1];
                layers[i] = new Layer(sizes[i], nextSize);
                for (int j = 0; j < sizes[i]; j++)
                {
                    for (int k = 0; k < nextSize; k++)
                    {
                        layers[i].weights[j, k] = NN.NextDoubleRange(rnd, -1f, 1f);
                    }
                }
            }
        }

        public float[] FeedForward(float[] inputs)
        {
            Array.Copy(inputs, 0, layers[0].neurons, 0, inputs.Length);
            for (int i = 1; i < layers.Length; i++)
            {
                float min = 0f;
                if (i == layers.Length - 1) min = -1f;
                Layer l = layers[i - 1];
                Layer l1 = layers[i];
                for (int j = 0; j < l1.size; j++)
                {
                    l1.neurons[j] = 0;
                    for (int k = 0; k < l.size; k++)
                    {
                        l1.neurons[j] += l.neurons[k] * l.weights[k, j];
                    }
                    l1.neurons[j] = Math.Min(1f, Math.Max(min, l1.neurons[j]));
                }
            }
            return layers[layers.Length - 1].neurons;
        }

    }
}