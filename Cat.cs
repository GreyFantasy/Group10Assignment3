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

    // This modifies SmellList with all animals that are within a 10 unit radius, and 
    // checks for other predators in range
    public void Smell(DoublyLinkedList<Animal> allAnimals, Bird[] birds, bool[] eaten)
    {
        SmellList = new DoublyLinkedList<Animal>();
        // check other predators
        Node<Animal>? curr = allAnimals.Head;
        while (curr != null)
        {
            if (curr.data != this && FindDistance(curr.data) <= 10)
                SmellList.AddLast(curr.data);
            curr = curr.next;
        }

        // check birds
        for (int i = 0; i < birds.Length; i++)
        {
            if (!eaten[i] && FindDistance(birds[i]) <= 10)
                SmellList.AddLast(birds[i]);
        }

    }
// custom ToString class for subclass Cat
    public override string ToString()
    {
        return base.ToString() + $", Breed ={catBreed}";
    }

    
}
