using System;
using app;
public class Class1
{
    static void Main(string[] args)
    {
        float best_dist = (float)Math.Sqrt(2);
        DateTimeOffset offset = new DateTimeOffset(2017,
        6, 1, 7, 55, 0, new TimeSpan(-5, 0, 0));
        for (int i = 0; i < 100; i++)
        {
            AI ai = new AI(new Genome(64), 1, 0, 0);
            while (ai.energy != 0)
            {
                if (ai.X < 1.5f && ai.X > 0.5 && ai.Y < 1.5f && ai.Y > 0.5f)
                {
                    Console.WriteLine("energy boost");
                    ai.energy++;
                }
                ai.move(1.0f, 1.0f, offset.ToUnixTimeSeconds(), 2.0f);
                double c1 = Math.Pow(ai.X - 1, 2);
                double c2 = Math.Pow(ai.Y - 1,2);
                double c3 = Math.Sqrt(c1 + c2);
                if (c3 < best_dist)
                    best_dist = (float)c3;
                //Console.WriteLine(ai.X);
                Console.WriteLine(ai.X);
                Console.WriteLine(ai.Y);
                Console.WriteLine("    ");
            }
            Console.WriteLine("--------------------");
        }
        Console.WriteLine(best_dist);

        app.NN nn = new app.NN();
        Console.WriteLine(nn.NextDoubleRange(0f, 1f));
        Console.WriteLine(nn.NextDoubleRange(0f, 1f));
        Console.WriteLine(nn.NextDoubleRange(0f, 1f));
        Console.WriteLine(nn.NextDoubleRange(0f, 1f));

    }
}
