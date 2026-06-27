using System;

public class Bird : Animal
{
    private static Random rand = new Random();

    public Bird(string name, int age, Position position)
        : base(name, age, position)
    {
    }

    // moves the bird randomly within its allowed range, calculates the speed and 
    // informs predators nearby throufgh HearMovement if the speed exceeds their hearing thresholds
    public void MoveRandom()
    {

        // speed 
        double speed;
        double dx = rand.Next(-10, 11);
        double dy = rand.Next(-10, 11);
        double dz = rand.Next(-2, 3);

        Move(dx, dy, dz);


        // keeps the bird within the map
        if (Position.X < 0) Position.X = 0;
        if (Position.X > 100) Position.X = 100;
        if (Position.Y < 0) Position.Y = 0;
        if (Position.Y > 70) Position.Y = 70;
        if (Position.Z < 0) Position.Z = 0;
        if (Position.Z > 10) Position.Z = 10;

        speed = Math.Sqrt(dx * dx + dy * dy + dz * dz);

        // notifies  predators around if the speed exceeds their hearing threshold
        HearMovement(allAnimals, speed);
    }

    // override method to fit class
    public override string ToString()
    {
        return "Bird -> " + base.ToString();
    }


}