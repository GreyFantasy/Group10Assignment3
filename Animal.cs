public class Animal
{
    private static int nextId = 1;

    private int id;
    private string name;
    private int age;
    private Position position;

    public int Id
    {
        get { return id; } // automatically assigned
    }

    public string Name // set name
    {
        get { return name; }
        set { name = value; }

    }
    public int Age
    {
        get { return age; }
        set
        {
            if (value < 0) // if negative value inserted, corrects to 0 to account for error as age cant be negative
            {
                age = 0;
            }
            else
            {
                age = value;
            }
        }
    }

    public Position Position //updating position
    {
        get { return position; }
        set { position = value; }
    }

    public Animal(string name, int age, Position position)
    {
        id = nextId; //assign attributes per ID incrementally
        nextId++;

        Name = name;
        Age = age;
        Position = position;
    }

    public void Move(double dx, double dy, double dz) //check animal movement from postion
    {
        Position.Move(dx, dy, dz);
    }

    public override string ToString() //String for output
    {

        return "ID: " + Id + " Name: " + Name + " Age: " + Age + " Position: " + Position;

    }

    // doubly linkedlist for smell and hear
    public DoublyLinkedList<Animal> SmellList = new DoublyLinkedList<Animal>();
    public DoublyLinkedList<Animal> HearList = new DoublyLinkedList<Animal>();

    // method to find the distance between the animals
    public double FindDistance(Animal other)
    {
        double dx = other.Position.X - Position.X;
        double dy = other.Position.Y - Position.Y;
        double dz = other.Position.Z - Position.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }




}