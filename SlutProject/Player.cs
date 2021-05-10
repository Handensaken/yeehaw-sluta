using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using System;
using System.Collections.Generic;

namespace SlutProject
{
    public class Player //The purpose of this class is to handle everything related to the player
    {

        MasterGameControl controller;
        Room currentRoom = Room.rooms["Start"];
        public List<Child> Children { get; set; } = new List<Child>();
        private List<string> possibleChoices = new List<string>();

        public List<Item> Inventory { get; private set; } = new List<Item>();

        public Dictionary<string, int> inv = new Dictionary<string, int>();
        public int Cash { get; private set; }

        private Child activeChild;

        //^^These are all just some variables

        public bool InBattle { get; set; }  //this shouldn't be public but I can't manage another reasonable way. It could be done with manual properties but then what is the point

        public Player() //We run the constructor to initiate the player with an empty inventory and some money
        {
            Inventory.Clear();
            Cash = 30;
            for (int i = 0; i < Item.availableItems.Length; i++)
            {
                inv.Add(Item.availableItems[i], 0);
            }
        }
        public void GetController(MasterGameControl control) // a simple controller getter
        {
            controller = control;
        }
        public void ChooseStarter(ChildSpawner spawner) //This method lets the player choose between 3 children and then adds them to the player's children
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
        public void ModifyBalance(int modifier)     //we just edit the balance (haha balance I fall dowwwn)
        {
            Cash += modifier;
        }
        public void AddInitialChoices()     //woah now shit happens. We add some choices. These are the base choices before anything is selected and acts as main choices
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
        public void DecideChoice()  //Here we simply handle the decision through a selection algorithm haha i am broken
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
        private void DisplayChildStats() //Displays a certain child's stats
        {
            Child displayChild = SelectChild(true); //gets child with the select child method

            if (displayChild == null) { return; }   //Because the code runs linearly, some code is not run immedately and runs twice the next time it is run. I do not know why but this bs fixes it
            System.Console.WriteLine($"Name: {displayChild.Name}");
            System.Console.WriteLine($"HP: {displayChild.HP}");
            System.Console.WriteLine($"Level: {displayChild.Level}");
            System.Console.WriteLine($"XP: {displayChild.Xp}/{displayChild.XpThreshold}");
            System.Console.WriteLine($"Child is {displayChild.Alignment}");
            Key.Press();

        }
        public void SetInventoryItems() //Gets the inventory items and prepares them for selection
        {
            possibleChoices.Clear();


            foreach (string s in inv.Keys)
            {
                possibleChoices.Add(s);
            }
            possibleChoices.RemoveAt(inv.Count - 1);    //removes annoying part that I should be able to fix pretty easily but I am lazy
            possibleChoices.Add("Back");


            SelectItem();
        }
        private void SelectItem()   //Handles the selection process for selecting an inventory item
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
            else if (inv[objectReference] > 0)  //This checks if the player in fact possesses the requested item
            {
                System.Console.WriteLine("item is avaliable");

                possibleChoices.Clear();
                possibleChoices.Add("Yes");
                possibleChoices.Add("No");
                switch (Selection(possibleChoices.ToArray(), "Are you sure?", true).ReturnInt)
                {
                    case 0: //This is not very efficient code but it works. Unfortunate bitchass :)
                        {
                            if (objectReference == "Band Aid")
                            {
                                inv[objectReference]--;
                                Item bandAid = new BandAid();
                                bandAid.Effect(SelectChild(false), controller);
                            }
                            else if (objectReference == "Net")
                            {
                                //makes usable only in battle
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
                            else    //This runs only if something is wrong and the player selected something that doesn't exist
                            {
                                System.Console.WriteLine("woah shits fucked");
                            }
                            break;
                        }
                    case 1: //This returns the player to the item selection screen
                        {
                            SetInventoryItems();
                            return;
                        }
                }
            }
            else    //This runs if the player does not have the item. theif fucker try use item not have >:(
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
        public Child SelectChild(bool optional) //This method lets the player select one of their children and returns that child for use wherever needed. When called you specify if its optional or not
        {
            possibleChoices.Clear();
            for (int i = 0; i < Children.Count; i++)
            {
                possibleChoices.Add(Children[i].Name);
            }
            if (optional)   //It's only not optional when your child dies in battle
            {
                possibleChoices.Add("back");
            }
            int index = Selection(possibleChoices.ToArray(), $"Select a child (current active is {activeChild.Name}", true).ReturnInt;
            if (index == Children.Count)   // this lets the player choose not to select a child as long as it's optional. if not it simply returns the chosen child
            {
                if (InBattle)
                {
                    return activeChild;
                }
                else
                {
                    AddInitialChoices();
                }
                return null;    //again, returns null because sometimes code becomes majorly wacko wacko
            }
            else
            {
                return Children[index]; //Gives child
            }
        }
        private void Exit()     //A simple method that asks the player if they want to quit the game and if so quits the game
        {
            possibleChoices.Clear();
            possibleChoices.Add("Yes");
            possibleChoices.Add("No");
            switch (Selection(possibleChoices.ToArray(), "Are you sure?", true).ReturnInt)
            {
                case 0:
                    {
                        Environment.Exit(1);    //AAANND WE OUT
                        break;
                    }
                case 1:
                    {
                        AddInitialChoices();
                        break;
                    }
            }
        }
        public bool CheckBalance(int value) //Checks if the player can afford something
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

        //you know the usual shebang but with a litte twist ;)
        public void PrintChoices(string[] choices, int current, string q)   //The only difference here is that we print the question or instructions
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
        public Key Selection(string[] choices, string q, bool SoI)  //This one is exciting. Here we use the Key class rather than an int or a string
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
                    case ConsoleKey.Enter: //All of the above is still the same shebang but here's some new stuff
                        {
                            Key returningKey = new Key();
                            if (SoI)    //We use a bool that specifies if the requested variable is supposed to be an int or a string. This could be changed to something else than a bool to accomedate multiple return types
                            {
                                returningKey.ReturnInt = current;
                            }
                            else
                            {
                                returningKey.ReturnString = choices[current];
                            }
                            //Then we assign a value to either the int or the string variable of Key.
                            return returningKey;
                        }
                    default:
                        {
                            // If the going gets tough the tough gets going
                        }
                        break;
                }
            }
        }

    }
}
