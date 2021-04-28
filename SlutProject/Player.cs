using System.Runtime.Versioning;
using System;
using System.Collections.Generic;

namespace SlutProject
{
    public class Player
    {
        Room currentRoom = Room.rooms["Battle"];
        public List<Child> Children { get; set; } = new List<Child>();
        private List<string> possibleChoices = new List<string>();

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
            Children.Add(starterChildren[Selection(choices, "Choose one of the following children", true)]);
            System.Console.WriteLine($"you chose {Children[Children.Count - 1].Name}. It is level {Children[Children.Count - 1].Level} and is {Children[Children.Count - 1].Alignment}");
        }

        public void AddInitialChoices()
        {
            possibleChoices.Clear();
            possibleChoices.Add("Move");
            possibleChoices.Add("Punish children");
            possibleChoices.Add("Exit game");
            DecideChoice();
        }
        private void DecideChoice()
        {
            switch (Selection(possibleChoices.ToArray(), "Choose action", true))
            {
                case 0:
                    {
                        System.Console.WriteLine("This one is still in development because I need a selection method that returns a string.");
                        break;
                    }
                case 1:
                    {

                        break;
                    }
                case 2:
                    {
                        Exit();
                        break;
                    }
            }
        }
        private void Exit()
        {
            possibleChoices.Clear();
            possibleChoices.Add("Yes");
            possibleChoices.Add("No");
            switch (Selection(possibleChoices.ToArray(), "Are you sure?", true))
            {
                case 0:
                    {
                        Environment.Exit(1);
                        break;
                    }
                case 1:
                    {
                        AddInitialChoices();
                        break;
                    }
            }
        }

        //you know the usual shebang
        public void PrintChoices(string[] choices, int current, string q)
        {
            System.Console.WriteLine(q);
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
        public int Selection(string[] choices, string q, bool SoI)
        {
            int current = 0;
            while (true)
            {
                Console.Clear();
                PrintChoices(choices, current, q);
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
