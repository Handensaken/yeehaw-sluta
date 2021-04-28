using System.Runtime.Versioning;
using System;
using System.Collections.Generic;

namespace SlutProject
{
    public class Player
    {
        MasterGameControl controller;
        Room currentRoom = Room.rooms["Shop"];
        public List<Child> Children { get; set; } = new List<Child>();
        private List<string> possibleChoices = new List<string>();

        public List<Item> Inventory { get; private set; } = new List<Item>();
        public int Cash { get; private set; }

        public Player()
        {
            Inventory.Clear();
            Cash = 30;
        }
        public void GetController(MasterGameControl control)
        {
            controller = control;
        }
        public void ChooseStarter(ChildSpawner spawner)
        {
            System.Console.WriteLine("Before  you can begin your adventure of weaponizing young children for your own profit and entertainment, you must choose a starting child.");
            System.Console.WriteLine("You will be presented with 3 choices. Press enter to continue");
            Console.ReadLine();
            Child[] starterChildren = new Child[3];
            string[] choices = new string[3];
            for (int i = 0; i < starterChildren.Length; i++)
            {
                starterChildren[i] = spawner.Spawner(controller);
                choices[i] = $"Name: {starterChildren[i].Name} Level: {starterChildren[i].Level}";
            }
            Children.Add(starterChildren[Selection(choices, "Choose one of the following children", true)]);
            System.Console.WriteLine($"you chose {Children[Children.Count - 1].Name}. It is level {Children[Children.Count - 1].Level} and is {Children[Children.Count - 1].Alignment}");
        }

        public void AddInitialChoices()
        {
            possibleChoices.Clear();
            possibleChoices.Add("Stay");
            possibleChoices.Add("Move");
            possibleChoices.Add("Punish children");
            possibleChoices.Add("Exit game");
            DecideChoice();
        }
        public void DecideChoice()
        {
            switch (Selection(possibleChoices.ToArray(), "Choose action", true))
            {
                case 0:
                    {
                        controller.TempName(currentRoom);
                        break;
                    }
                case 1:
                    {
                        System.Console.WriteLine("This one is still in development because I need a selection method that returns a string.");
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        controller.PunishChildren();
                        break;
                    }
                case 3:
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
        public bool CheckBalance(int value)
        {
            if (Cash >= value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveCash(int value)
        {
            Cash -= value;
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
