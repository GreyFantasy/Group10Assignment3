
using System;
using System.IO;
namespace Assignment3;

class Program
{
    static void Main(string [] args)
    {
        //console window size

        Console.SetWindowSize(200, 70);
        Console.SetWindowSize(200, 70);

        //first part, creating the cats with AddFront
        DoublyLinkedList<Animal> animals = new DoublyLinkedList<Animal>(); //Array list Replaced with Doubly Linked List storing cats and snakes under animals

        animals.AddFirst(new Cat("Whiskers", 3, new Position(5, 10, 0), Cat.Breed.Bengal));
        animals.AddFirst(new Cat("Shadow", 5, new Position(12, 8, 0), Cat.Breed.Serval));
        //removed last cat so there is only 2

        //next, adding snakes using AddLast

        animals.AddLast(new Snake("Sly", 3, new Position(7, 10, 3), 1.2, true));
        animals.AddLast(new Snake("Viper", 5, new Position(15, 20, 0), 2.1, true));
        //removed last snake so there is only 2

        //ArrayList<Animal> animals = ArrayList<Animal>.Merge(cats, snakes);

        //testing the print method

        Console.WriteLine("-Print All Forward-"); //updated forward and reverse tostring to work with updated methods in doubly linked list
        Console.WriteLine(animals.ToStringForward());

        Console.WriteLine("-Print All Reverse-"); 
        Console.WriteLine(animals.ToStringReverse()); 

        //Now adding the array of birdCount birds w random positions, (chosen from name and pos rules)

        Random rand = new Random(); //Random num gen
        string[] birdNames = File.ReadAllLines("BirdNames.txt"); //array for bird names, names are taken from the file rather than hard coding them like i did initially

        //added variable for birdcount
        int birdCount = 25;

        Bird[] birds = new Bird[birdCount]; //array size for birds
        bool[] eaten = new bool[birdCount]; //bool of if bird is eaten in list

        for (int i = 0; i < birdCount; i++) //loops ten times; once for each bird in array
        {
            Position birdPos = new Position( rand.Next(0, 101), rand.Next(0, 71), rand.Next(0, 11)); //random starting pos or X:(0 - 100), Y:(0 - 70), Z:(0 - 10)
            birds[i] = new Bird(birdNames[i], rand.Next(1, 6), birdPos); //creates bird with random: Name, Age  ( 1-5), position
        }

        //check birds were made
        Console.WriteLine (" = Birds Made =");
        for (int i = 0; i < birds.Length; i++)
            Console.WriteLine(birds[i]);

        //loop for eating bird sim

        int round = 0; //rounds counter start at zero and count from, till no birds left
        
       
        Console.Clear();
        Console.CursorVisible = false;

        while (true) //runs until broken
        {
            //see if all the birds hav been eaten
            bool allEaten = true; //assume all birds eaten
            for (int i = 0; i < birdCount; i++) if (!eaten[i]) //!eaten[i] confirm if even 1 bird is not eaten, set all eaten to false
            {
                allEaten = false;
                break;
            }

            if (allEaten)
            break;

            round++; //increments the round counter up

            DrawGrid(animals, birds, eaten, round); //calls display for grid

            // bird/snake takes turn
            for (int a = 0; a < animals.GetCount(); a++) //loops through the animals in the list and stores the current in predator
            {
                //DLL replacement for the Array list indexing
                //using GetaAt(), the list is traversed and item at requested position is returned
                Animal? predator = animals.GetAt(a); 

                if (predator == null)
                    continue;

                //look for nearest bird (alive)
                int nearestIndex = -1; //initially assume there is no nearest bird
                double nearestDist = double.MaxValue; //the initial assumed nearest distance is max

                for (int b = 0; b < birdCount; b++) //loop through the birds
                {
                    if (eaten[b]) continue; //skip the eaten birds
                    double dx = birds[b].Position.X - predator.Position.X; //straight line calculation for the distance
                    double dy = birds[b].Position.Y - predator.Position.Y; //Pythagorean theorem using the X and Y values at instance to calculate
                    double dist = Math.Sqrt(dx * dx + dy * dy);
                    if (dist < nearestDist) //if a bird is closer than any other seen thus far then we set it to the nearest
                    {
                        nearestDist = dist;
                        nearestIndex = b;
                    }
                }
                if (nearestIndex == -1) // no more birds
                continue; //skip predators turn (all birds eaten)

                double range = (predator is Cat) ? 8 : 3; //cat range of 8 and snake range 3 (conditional to if it is a cat or snake)
                double speed = (predator is Cat) ? 16 : 14; //cat move speed 16 and snake move speed 14

                if (nearestDist <= range)
                {
                    //Moved the Eating Behavior into Animal Class
                    //If more predators need to be added they can just inherit Eat() rather than needing type checking
                    predator.Eat(birds[nearestIndex]);

                    eaten[nearestIndex] = true;
                }
                else
                {
                    //movement now handled by Animal
                    //MoveTowards() called from Animal with target bird specified
                    predator.MoveTowards(birds[nearestIndex], speed);
                }
            }

            //uneated birds move randomly (like before)

            for (int i = 0; i < birdCount; i++)
            {
                if (!eaten[i])
                birds[i].MoveRandom(animals); //passes list of animal. predators that are close hear bird move
            }
                
        }

        Console.WriteLine($" Every bird was eaten in {round} rounds...");


    }

    static void DrawGrid(DoublyLinkedList<Animal> animals, Bird[] birds, bool[] eaten, int round) //updated DrawGrid to use DLL instead of ArrayList
    {
        Console.SetCursorPosition(0, 0); //go to top left end of each round

        for (int row = 0; row < 70; row++) // loops through each of the rows
        {
            for (int col = 0; col < 100; col++) //loop though each column
            {
                string cell = "  "; // the empty cell default of two spaces

                //check if that is a bird at this position
                for (int b = 0; b < birds.Length; b++)
                {
                    if (!eaten[b] && (int)birds[b].Position.X == col && (int)birds[b].Position.Y == row)
                    cell = "B" + b; // go through birds like B1, B2, B3 etc.

                }

                //now check if there is a predator in this position
                for (int a = 0; a < animals.GetCount(); a++)
                {
                    //same as main predator loop, drawgrid also loops through the passed list, GetAt() returns item at the requested position
                    Animal? predator = animals.GetAt(a); 

                    if (predator == null)
                        continue;

                    if ((int)predator.Position.X == col && (int)predator.Position.Y == row)
                    {
                        if (predator is Cat)
                            cell = "C" + (a + 1); // labels cats at C1, C2, C3
                        else
                            cell = "S" + (a - 2); // snakes labeled as S1, S2, S3 

                    }
                }

                Console.Write(cell); //prints the 2 character wide cell
            }
            Console.WriteLine(); //spacig line after row
        }
        //Console.ReadLine(); //uncomment to go through line by line
        Console.WriteLine($" - Round {round} - ");
        System.Threading.Thread.Sleep(800); // a pause for visibility so you can se each of the frames


    }


}