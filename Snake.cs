using System;

public class Snake : Animal
{
    private double length;
    private bool venomous;

    // constructor
    public Snake(string name, int age, Position position, double length, bool venomous) //changed constructor type to Position
    : base(name, age, position) //removed iD ;it's automatically assigned so its not declared
    {
        this.length = length;
        this.venomous = venomous;
    }

    // setters and getters
    public double Length
    {
        get { return length; }
        set { length = value; }
    }
    public bool Venomous //fixed property type to bool from double
    {
        get { return venomous;}
        set { venomous = value; }
    }

    // custom ToString method for Snake class
    public override String ToString()
    {
        return base.ToString() + $", Length={length:F2}m, Venomous={venomous}";
    }



}