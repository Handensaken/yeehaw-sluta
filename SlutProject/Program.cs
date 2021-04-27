using System;
using System.Collections.Generic;

namespace SlutProject
{
    class Program
    {

        static void Main(string[] args)
        {
            ChildSpawner spawner = new ChildSpawner();
            Player player = new Player();
            while (true)
            {
                player.children.Add(spawner.Spawner());
                System.Console.WriteLine(player.children[player.children.Count - 1]);
                Console.ReadKey();

            }
        }
        static void ChooseStarter(Player p, ChildSpawner spawner)
        {
            System.Console.WriteLine("Before  you can begin you adventure och weaponizing young children for your own profit and entertainment you must choose a starting child.");
            System.Console.WriteLine("You will be presented with 3 choices. Press enter to continue");
            Console.ReadLine();
            Child[] starterChildren = new Child[3];
            string[] choices = new string[3];
            for (int i = 0; i < starterChildren.Length; i++)
            {
                starterChildren[i] = spawner.Spawner();
                choices[i] = $"Name: {starterChildren[i].Name} Age: {starterChildren[i].Level}";
            }
        }

    }
}
