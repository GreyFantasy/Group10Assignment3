using System;
namespace Assignment3;
public class Position
{
    private double x;
    private double y;
    private double z;

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

    public double Z
    {
        get { return z; }
        set { z = value; }
    }

    public Position(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public void Move(double dx, double dy, double dz)
    {
        X += dx;
        Y += dy;
        Z += dz;
    }

    public override string ToString()
    {
        return "(" + X.ToString("F1") + ", "
                   + Y.ToString("F1") + ", "
                   + Z.ToString("F1") + ")";
    }
}