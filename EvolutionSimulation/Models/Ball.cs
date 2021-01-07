using System;

namespace app
{
    public class Ball
    {
        private double x;
        private double y;
        private double radius = 0;
        public Ball(double radius, double x, double y)
        {
            Radius = radius;
            X = x;
            Y = y;
        }

        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

    }
}