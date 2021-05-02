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
        public MasterGameControl(Player p)
        {
            player = p;
        }
        public void ChildDeathEvent(Child c)
        {
            if (!c.IsWild)
            {
                System.Console.WriteLine($"{c.Name} has suffered great damage and succumbed to their grim injuries.");
                player.Children.Remove(c);
                Key.Press();
                if (player.InBattle && player.Children.Count == 0)
                {
                    player.InBattle = false;
                    System.Console.WriteLine($"All your children are dead");
                }
                else
                {
                    SwitchChild(false);
                }
            }
            else
            {
                System.Console.WriteLine($"The opposing {c.Name} has been brutally slain");
                System.Console.WriteLine("You won the battle");
                player.InBattle = false;
                player.ModifyBalance(10);
                System.Console.WriteLine("gained 10 moneys");
                Key.Press();
            }
        }

        public void PunishChildren()
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
        public void TempName(Room current)
        {
            switch (player.Selection(current.GetActions(), "Select an Action", true).ReturnInt)
            {
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
        private void StartBattle()
        {
            if (player.Children.Count > 0)
            {

                player.InBattle = true;
                Child opponentChild = spawner.Spawner(this, true);
                System.Console.WriteLine($"Engaged Level {opponentChild.Level} {opponentChild.Name} in battle");
                Key.Press();
                BattleSession(opponentChild);
            }
            else
            {
                TempName(Room.rooms["Battle"]);
            }
        }
        public Child oppChild { get; private set; }
        private Child playerActiveChild;
        private void BattleSession(Child opponent)
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
            TempName(Room.rooms["Battle"]);
        }
        public void BattleSelection()
        {
            string[] battleActions = {
                "Attack",
                "Inventory",
                "Children",
                "Run"
            };
            switch (player.Selection(battleActions, $"Select Action (Your {playerActiveChild.Name}'s HP: {playerActiveChild.HP}. Opposing {oppChild.Name}'s HP: {oppChild.HP})", true).ReturnInt)
            {
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
                        SwitchChild(true);
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
        private void SwitchChild(bool optional)
        {
            Child pendingChild = player.SelectChild(optional);
            if (playerActiveChild == pendingChild)
            {
                BattleSelection();
            }
            else
            {
                playerActiveChild = pendingChild;
                System.Console.WriteLine($"Sent in {playerActiveChild.Name}!");
                BattleSelection();
            }

        }
        private void Attack()
        {
            string[] attackChoices = { "Attack", "Super Attack", "Go Back" };
            switch (player.Selection(attackChoices, $"Select move (Your {playerActiveChild.Name}'s HP: {playerActiveChild.HP}. Opposing {oppChild.Name}'s HP: {oppChild.HP})", true).ReturnInt)
            {
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
        private void Run()
        {
            System.Console.WriteLine("Ran away");
            player.InBattle = false;
        }
        private void OpponentsTurn(Child oC, Child aC)
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
        private void InfoBoard()
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
        }

        private void Shop(Room current)
        {
            switch (player.Selection(getNewItemNames(), $"Choose item (Cash: {player.Cash})", true).ReturnInt)
            {
                case 0:
                    {
                        if (player.CheckBalance(10))    //this is very bad and lazy coding. Too bad :)
                        {
                            player.Inventory.Add(new BandAid());

                            player.inv["Band Aid"] += 1;

                            player.RemoveCash(10);
                            System.Console.WriteLine("Bought a band aid");
                            Key.Press();
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
                    {
                        if (player.CheckBalance(20))
                        {

                            player.inv["Net"] += 1;
                            player.RemoveCash(20);
                            System.Console.WriteLine("Bought a net");
                            Key.Press();
                        }
                        else
                        {
                            System.Console.WriteLine("Insufficient cash");
                            Key.Press();
                            TempName(Room.rooms["Shop"]);
                        }
                        break;
                    }
                case 2:
                    {
                        TempName(Room.rooms["Shop"]); //this is not pretty either.
                        break;
                    }
            }
        }
        public Room GetRoom(Room currentRoom)
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
        public void Catch(Child target)
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