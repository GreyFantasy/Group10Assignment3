public class Cat : Animal
{
    public enum Breed
    {
        Abyssinian,
        AmericanWirehair,
        Bengal,
        Himalayan,
        Ocicat,
        Serval
    }

    private Breed catBreed;

    // constructor
    public Cat (string name, int age, Position position, Breed breed) : base(name, age, position) { // Changed constructor type to Position (from string)
        this.catBreed = breed; 
    }
// getters and setters
    public Breed CatBreed
    {
        get {return catBreed;}
        set { catBreed = value;}

    }
// custom ToString class for subclass Cat
    public override string ToString()
    {
        return base.ToString() + $", Breed ={catBreed}";
    }

    
}