using System;
using System.Collections.Generic;

namespace SlutProject
{
    public class Player
    {
        public List<Child> children { get; set; } = new List<Child>();

        public void ChooseStarter(ChildSpawner spawner)
        {
            System.Console.WriteLine("Before  you can begin your adventure of weaponizing young children for your own profit and entertainment, you must choose a starting child.");
            System.Console.WriteLine("You will be presented with 3 choices. Press enter to continue");
            Console.ReadLine();
            Child[] starterChildren = new Child[3];
            string[] choices = new string[3];
            for (int i = 0; i < starterChildren.Length; i++)
            {
                starterChildren[i] = spawner.Spawner();
                choices[i] = $"Name: {starterChildren[i].Name} Level: {starterChildren[i].Level}";
            }
            children.Add(starterChildren[Selection(choices)]);
            System.Console.WriteLine($"you chose {children[children.Count-1].Name}. It is level {children[children.Count-1].Level} and is {children[children.Count-1].Alignment}");
        }
        //you know the usual shebang
        public void PrintChoices(string[] choices, int current)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                if (current == i)
                {
                    System.Console.WriteLine($">  {choices[i]}");
                }
                else
                {
                    System.Console.WriteLine($"  {choices[i]} ");
                }
            }
        }
        public int Selection(string[] choices)
        {
            int current = 0;
            while (true)
            {
                Console.Clear();
                PrintChoices(choices, current);
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            current++;
                            current = current % choices.Length;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        {
                            current--;
                            if (current < 0) { current = choices.Length - 1; }
                            else
                            {
                                current = Math.Abs(current % choices.Length);
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            return current;
                        }
                    default:
                        {
                            // handle everything else
                        }
                        break;
                }
            }
        }

    }
}
