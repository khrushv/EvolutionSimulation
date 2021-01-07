namespace app
{
    public class Layer
    {
        public int size;
        public float[] neurons;
        public float[,] weights;
        public int nextSize;
        public Layer(int size, int nextSize)
        {
            this.size = size;
            neurons = new float[size];
            weights = new float[size, nextSize];
            this.nextSize = nextSize;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Layer;
            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < nextSize; j++)
                {
                    if(weights[i,j] != item.weights[i,j])
                    {
                        return false;
                    }
                }
            }    
            return true;
        }
    }

}