using System.Diagnostics;
using System.Net.Security;
using System.Linq;
using System;

namespace SlutProject
{
    public class MasterGameControl
    {
        ChildSpawner spawner = new ChildSpawner();
        Player player;
        //sets up variables
        public MasterGameControl(Player p)
        {
            player = p;     //constructor gets the player class
        }
        public void ChildDeathEvent(Child c)    //This one actually handles the death of children
        {
            if (!c.IsWild)  //if they're your children it removes dead children from your list and exits battle if you have run out of kids 
            {
                System.Console.WriteLine($"{c.Name} has suffered great damage and succumbed to their grim injuries.");
                player.Children.Remove(c);
                Key.Press();
                if (player.InBattle && player.Children.Count == 0)
                {
                    player.InBattle = false;
                    System.Console.WriteLine($"All your children are dead");
                }
                else    //if they die and you're in battle you have to switch child
                {
                    SwitchChild(false);
                }
            }
            else    //if the dying child is an enemy you get money and xp. you are also not in battle anymore :=)
            {
                System.Console.WriteLine($"The opposing {c.Name} has been brutally slain");
                System.Console.WriteLine("You won the battle");
                player.InBattle = false;
                player.ModifyBalance(10);
                foreach (Child child in player.Children)
                {
                    child.AddXP(5);
                }
                System.Console.WriteLine("gained 10 moneys");
                Key.Press();
            }
        }

        public void PunishChildren()    //runs the punishment method on the specifid target
        {
            if (player.Children.Count > 0)
            {
                Child target = player.SelectChild(true);
                if (target == null) { return; }
                target.Punishment();
                Key.Press();
            }
            else
            {
                System.Console.WriteLine("You have no children left.");
                System.Console.WriteLine("press any key to continue");
                Key.Press();
            }
        }
        public void TempName(Room current)  //I can't be fucked to change the name :) too bad
        {
            switch (player.Selection(current.GetActions(), "Select an Action", true).ReturnInt) 
            {   //lets you choose room specific action or go back
                case 0:
                    {

                        if (current == Room.rooms["Start"])
                        {
                            //display info board
                            InfoBoard();
                        }
                        else if (current == Room.rooms["Shop"])
                        {
                            Shop(current);
                        }
                        else if (current == Room.rooms["Battle"])
                        {
                            StartBattle();
                            Key.Press();
                        }
                        else
                        {
                            System.Console.WriteLine("null reference exception thrown. You somehow found a new room");
                        }
                        break;
                    }
                case 1:
                    {
                        //go back
                        player.AddInitialChoices();
                        break;
                    }
            }
        }
        private void StartBattle()  //sets up everything in preparation for the battle
        {
            if (player.Children.Count > 0)
            {

                player.InBattle = true;
                Child opponentChild = spawner.Spawner(this, true);
                System.Console.WriteLine($"Engaged Level {opponentChild.Level} {opponentChild.Name} in battle");
                Key.Press();
                BattleSession(opponentChild);
            }
            else    //If you have no children you are yoted back
            {
                TempName(Room.rooms["Battle"]);
            }
        }
        public Child oppChild { get; private set; }
        private Child playerActiveChild;
        private void BattleSession(Child opponent)  //Handles all the running of the battle sequence
        {
            playerActiveChild = player.Children[0];
            oppChild = opponent;
            while (player.InBattle)
            {
                BattleSelection();
                if (oppChild.HP > 0 && player.InBattle)
                {
                    OpponentsTurn(opponent, playerActiveChild);
                }
            }
            //  System.Console.WriteLine("Battle ended");
            //  Key.Press();
            TempName(Room.rooms["Battle"]); //returns us to the action selection screen if the battle ends
        }
        public void BattleSelection() //Handles selection of everything battle related
        {
            string[] battleActions = {  //Sets the choices in battle
                "Attack",
                "Inventory",
                "Children",
                "Run"
            };
            switch (player.Selection(battleActions, $"Select Action (Your {playerActiveChild.Name}'s HP: {playerActiveChild.HP}. Opposing {oppChild.Name}'s HP: {oppChild.HP})", true).ReturnInt)
            {   //lets the player select an action and calls the appropriate method
                case 0:
                    {
                        Attack();
                        break;
                    }
                case 1:
                    {
                        System.Console.WriteLine("Inventory");
                        player.SetInventoryItems();
                        break;
                    }
                case 2:
                    {
                        SwitchChild(true);  //true here defines that the switch is optional
                        break;
                    }
                case 3:
                    {
                        Run();
                        Key.Press();
                        break;
                    }
            }
        }
        private void SwitchChild(bool optional)//Gets the new active child by calling the select child method
        {
            Child pendingChild = player.SelectChild(optional);
            if (playerActiveChild == pendingChild)  //If the selected one is the one already active the payer is just sent back to the selection screen
            {
                BattleSelection();
            }
            else    //Otherwise it switches the new child in
            {
                playerActiveChild = pendingChild;
                System.Console.WriteLine($"Sent in {playerActiveChild.Name}!");
                BattleSelection();
            }

        }
        private void Attack()   //lets the player decide between a normal attack and a super attack or to go back to selection
        {
            string[] attackChoices = { "Attack", "Super Attack", "Go Back" };
            switch (player.Selection(attackChoices, $"Select move (Your {playerActiveChild.Name}'s HP: {playerActiveChild.HP}. Opposing {oppChild.Name}'s HP: {oppChild.HP})", true).ReturnInt)
            {   //The opposing child's hurt method is called with the paramater of the active child's corresponding attack method
                case 0:
                    {
                        oppChild.Hurt(playerActiveChild.Attack());
                        // System.Console.WriteLine("attaku");
                        break;
                    }
                case 1:
                    {
                        playerActiveChild.SuperAttack(oppChild, player, playerActiveChild);
                        //System.Console.WriteLine("supa kicka");
                        break;
                    }
                case 2:
                    {
                        // System.Console.WriteLine("go bakko");
                        BattleSelection();
                        break;
                    }
            }

        }
        private void Run()  //Ends battle
        {
            System.Console.WriteLine("Ran away");
            player.InBattle = false;
        }
        private void OpponentsTurn(Child oC, Child aC)  //Randomizes the opponent's move.
        {
            Random rand = new Random();
            if (rand.Next(2) == 1 && oC.Energy > 0)
            {
                oC.SuperAttack(aC, player, oppChild);
                Key.Press();
            }
            else
            {
                aC.Hurt(oC.Attack());
            }
        }
        private void InfoBoard()    //Prints some info
        {
            System.Console.WriteLine("[INFO BOARD]");
            System.Console.WriteLine("Please keep your children on a leash or inside their cage at all times!");
            System.Console.WriteLine("If your children are suffering severe damage you can buy band aids at the store.");
            System.Console.WriteLine("Unjustified child attacks are not tolerated and the aggressor will be put down!");
            System.Console.WriteLine("This room is connected at either side to a battle room and a shop room. To go from one to the other you must pass through this room.");
            Key.Press();
            TempName(Room.rooms["Start"]);
        }

        private string[] getNewItemNames(){ //This is very very very ugly and bad, but time is of the essence and I can not be fucked because it works
            string[] newItemNames = Item.availableItems;
            newItemNames[0] += " $10";
            newItemNames[1] += " $20";
            return newItemNames;
            //This is extremely lazy coding but we set the names of the items we are interested in
        }

        private void Shop(Room current) //lets the player choose an item from the shop and then buy it, if the can afford it.
        {
            switch (player.Selection(getNewItemNames(), $"Choose item (Cash: {player.Cash})", true).ReturnInt)
            {   //calls the selection
                case 0:
                    {
                        if (player.CheckBalance(10))    //this is very bad and lazy coding. Too bad :)
                        {
                            player.Inventory.Add(new BandAid());

                            player.inv["Band Aid"] += 1;

                            player.ModifyBalance(-10);
                            System.Console.WriteLine("Bought a band aid");
                            Key.Press();
                            //This adds a band aid and removes cash. 
                        }
                        else
                        {
                            System.Console.WriteLine("Insufficient cash");
                            Key.Press();
                            TempName(Room.rooms["Shop"]);
                        }
                        break;
                    }
                case 1:
                    {   //this one does the same thing but for the net
                        if (player.CheckBalance(20))
                        {

                            player.inv["Net"] += 1;
                            player.ModifyBalance(-20);
                            System.Console.WriteLine("Bought a net");
                            Key.Press();
                        }
                        else
                        {
                            System.Console.WriteLine("Insufficient cash");
                            Key.Press();
                            TempName(Room.rooms["Shop"]);
                        }   //The item system should be class based but I needed more time to make them work neatly so i used a simpler ang uglier method
                        break;
                    }
                case 2:
                    {   //This one returns us to the selection screen
                        TempName(Room.rooms["Shop"]); //this is not pretty either.
                        break;
                    }
            }
        }
        public Room GetRoom(Room currentRoom)   //lets the player move to another room, or stay if they so desire
        {
            string tempString = player.Selection(currentRoom.GetChoices().ToArray(), "Select a room", false).ReturnString;
            if (tempString == "Stay")
            {
                System.Console.WriteLine($"Stayed");
                Key.Press();
                return currentRoom;
            }
            else
            {
                System.Console.WriteLine($"entered {Room.rooms[tempString].Name}");
                Key.Press();
                return Room.rooms[tempString];
            }
        }
        public void Catch(Child target) //tries to catch the target child
        {
            System.Console.WriteLine("Threw net");
            Key.Press();
            Random rand = new Random();
            if (rand.Next(1, 7) == 6)
            {
                target.IsWild = false;
                player.Children.Add(target);
                player.InBattle = false;
                System.Console.WriteLine($"Successfully caught {target.Name}");
            }
            else
            {
                System.Console.WriteLine($"You missed {target.Name} with the net");
            }
            Key.Press();
        }
    }
}