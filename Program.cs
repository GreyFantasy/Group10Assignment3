//todo
//create arralist of Animals containing 3 cats using the Addfront
// same with 3 snakes but addlast

//merge the two lists
//test  printallforward, print all reverse on the new arraylist

// make an array that has 10 birds at random positions choose from the rules for birds name and positions that is provided.
using System;
using System.ComponentModel;
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

        //Now adding the array of 10 birds w random positions, (chosen from name and pos rules)

        Random rand = new Random(); //Random num gen
        string[] birdNames = { "Tweety", "Zazu", "Iago", "Hula", "Manu", "Couscous", "Roo", "Tookie", "Plucky", "Kiwi" }; //array names for birds

        Bird[] birds = new Bird[10]; //array size for birds

        for (int i = 0; i < 10; i++) //loops ten times; once for each bird in array
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
        bool[] eaten = new bool[10]; // tracks for the birds eaten from total
        
       
        Console.Clear();
        Console.CursorVisible = false;

        while (true) //runs until broken
        {
            //see if all the birds hav been eaten
            bool allEaten = true; //assume all birds eaten
            for (int i = 0; i < 10; i++) if (!eaten[i]) //!eaten[i] confirm if even 1 bird is not eaten, set all eaten to false
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

                for (int b = 0; b < 10; b++) //loop through the birds
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
                    //eating the bird
                    if (predator is Cat) //check if predator is cat
                    ((Cat)predator).Eat(birds[nearestIndex]); //if so then the Cat is eating the closest bird
                    else
                    ((Snake)predator).Eat(birds[nearestIndex]); //otherwise the snake is eating the closest bird

                    eaten[nearestIndex] = true;
                }
                else
                {
                    //if the bird is out of range then go towards nearest bird
                    double dx = birds[nearestIndex].Position.X - predator.Position.X;
                    double dy = birds[nearestIndex].Position.Y - predator.Position.Y;
                    double dist = Math.Sqrt(dx * dx + dy * dy);

                    double moveX = (dx / dist) * speed; //splitting the vector by taking the straight line distance and dividing (x,y)
                    double moveY = (dy / dist) * speed; //by it so the speed applied to x and y result in vector speed of the predator
                    predator.Move(moveX, moveY, 0); //in the exact direction of the nearest bird
                }
            }

            //uneated birds move randomly (like before)

            for (int i = 0; i < 10; i++)
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
                for (int b = 0; b < 10; b++)
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