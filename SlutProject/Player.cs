using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using System;
using System.Collections.Generic;

namespace SlutProject
{
    public class Player
    {

        MasterGameControl controller;
        Room currentRoom = Room.rooms["Start"];
        public List<Child> Children { get; set; } = new List<Child>();
        private List<string> possibleChoices = new List<string>();

        public List<Item> Inventory { get; private set; } = new List<Item>();

        public Dictionary<string, int> inv = new Dictionary<string, int>();
        public int Cash { get; private set; }

        private Child activeChild;

        public bool InBattle { get; set; }  //this shouldn't be public but I can't manage another reasonable way

        public Player()
        {
            Inventory.Clear();
            Cash = 30;
            for (int i = 0; i < Item.availableItems.Length; i++)
            {
                inv.Add(Item.availableItems[i], 0);
            }
        }
        public void GetController(MasterGameControl control)
        {
            controller = control;
        }
        public void ChooseStarter(ChildSpawner spawner)
        {
            System.Console.WriteLine("Before you can begin your adventure of weaponizing young children for your own profit and entertainment, you must choose a starting child.");
            System.Console.WriteLine("You will be presented with 3 choices.");
            Key.Press();
            Child[] starterChildren = new Child[3];
            string[] choices = new string[3];
            for (int i = 0; i < starterChildren.Length; i++)
            {
                starterChildren[i] = spawner.Spawner(controller, false);
                choices[i] = $"Name: {starterChildren[i].Name} Level: {starterChildren[i].Level}";
            }

            Children.Add(starterChildren[Selection(choices, "Choose one of the following children", true).ReturnInt]);
            activeChild = Children[Children.Count - 1];
            System.Console.WriteLine($"you chose {Children[Children.Count - 1].Name}. It is level {Children[Children.Count - 1].Level} and is {Children[Children.Count - 1].Alignment}");
        }
        public void ModifyBalance(int modifier)
        {
            Cash += modifier;
        }
        public void AddInitialChoices()
        {
            possibleChoices.Clear();
            possibleChoices.Add("Children");
            possibleChoices.Add("Inventory");
            possibleChoices.Add("Take room action");
            possibleChoices.Add("Move");
            possibleChoices.Add("Punish children");
            possibleChoices.Add("Exit game");
            DecideChoice();
        }
        public void DecideChoice()
        {
            switch (Selection(possibleChoices.ToArray(), $"Choose action (current room: {currentRoom.Name})", true).ReturnInt)
            {
                case 0:
                    {
                        DisplayChildStats();
                        break;
                    }
                case 1:
                    {
                        SetInventoryItems();
                        break;
                    }
                case 2:
                    {
                        controller.TempName(currentRoom);
                        break;
                    }
                case 3:
                    {
                        currentRoom = controller.GetRoom(currentRoom);
                        break;
                    }
                case 4:
                    {
                        controller.PunishChildren();
                        break;
                    }
                case 5:
                    {
                        Exit();
                        break;
                    }
            }
        }
        private void DisplayChildStats()
        {
            Child displayChild = SelectChild(true);

            if (displayChild == null) { return; }
            System.Console.WriteLine($"Name: {displayChild.Name}");
            System.Console.WriteLine($"HP: {displayChild.HP}");
            System.Console.WriteLine($"Level: {displayChild.Level}");
            System.Console.WriteLine($"XP: {displayChild.Xp}/{displayChild.XpThreshold}");
            System.Console.WriteLine($"Child is {displayChild.Alignment}");
            Key.Press();

        }
        public void SetInventoryItems()
        {
            possibleChoices.Clear();


            foreach (string s in inv.Keys)
            {
                possibleChoices.Add(s);
            }
            possibleChoices.RemoveAt(inv.Count - 1);
            possibleChoices.Add("Back");


            SelectItem();
        }
        private void SelectItem()
        {
            string objectReference = Selection(possibleChoices.ToArray(), "Choose Item", false).ReturnString;
            if (objectReference == "Back")
            {
                if (!InBattle)
                {
                    AddInitialChoices();
                }
                else
                {
                    controller.BattleSelection();
                }
            }
            else if (inv[objectReference] > 0)
            {
                System.Console.WriteLine("item is avaliable");

                possibleChoices.Clear();
                possibleChoices.Add("Yes");
                possibleChoices.Add("No");
                switch (Selection(possibleChoices.ToArray(), "Are you sure?", true).ReturnInt)
                {
                    case 0: //This is not very efficient code but it works. Unfortunate :)
                        {
                            if (objectReference == "Band Aid")
                            {
                                inv[objectReference]--;
                                Item bandAid = new BandAid();
                                bandAid.Effect(SelectChild(false), controller);
                            }
                            else if (objectReference == "Net")
                            {
                                //Only make usable in battle
                                if (InBattle)
                                {
                                    inv[objectReference]--;
                                    Item net = new Net();
                                    net.Effect(controller.oppChild, controller);
                                }
                                else
                                {
                                    System.Console.WriteLine("This item cannot be used in this state");
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("woah shits fucked");
                            }
                            break;
                        }
                    case 1:
                        {
                            SetInventoryItems();
                            return;
                        }
                }
            }
            else
            {
                System.Console.WriteLine("You do not possess this item");
                if (InBattle)
                {
                    Key.Press();
                    controller.BattleSelection();
                    return;
                }
                Key.Press();
            }
        }
        public Child SelectChild(bool optional)
        {
            possibleChoices.Clear();
            for (int i = 0; i < Children.Count; i++)
            {
                possibleChoices.Add(Children[i].Name);
            }
            if (optional)
            {
                possibleChoices.Add("back");
            }
            int index = Selection(possibleChoices.ToArray(), $"Select a child (current active is {activeChild.Name}", true).ReturnInt;
            if (index == Children.Count)
            {
                if (InBattle)
                {
                    return activeChild;
                }
                else
                {
                    AddInitialChoices();
                }
                return null;
            }
            else
            {
                return Children[index];
            }
        }
        private void Exit()
        {
            possibleChoices.Clear();
            possibleChoices.Add("Yes");
            possibleChoices.Add("No");
            switch (Selection(possibleChoices.ToArray(), "Are you sure?", true).ReturnInt)
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
        public Key Selection(string[] choices, string q, bool SoI)
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
                            Key returningKey = new Key();
                            if (SoI)
                            {
                                returningKey.ReturnInt = current;
                            }
                            else
                            {
                                returningKey.ReturnString = choices[current];
                            }
                            return returningKey;
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
