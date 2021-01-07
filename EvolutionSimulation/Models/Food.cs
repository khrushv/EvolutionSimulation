using System;

public class Food
{
    private double x;
    private double y;
    private double satiety;
    public Food(double x, double y, double satiety=50.0)
    {
        Satiety = satiety;
        X = x;
        Y = y;
    }

    public double Satiety
    {
        get { return satiety; }
        set { satiety = value; }
    }
    public double X
    {
        get { return X; }
        set { X = value; }
    }
    public double Y
    {
        get { return Y; }
        set { Y = value; }
    }
}
