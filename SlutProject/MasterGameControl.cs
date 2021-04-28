using System;

namespace SlutProject
{
    public class MasterGameControl
    {
        Player player;
        public MasterGameControl(Player p)
        {
            player = p;
        }
        public void ChildDeathEvent(Child c)
        {
            System.Console.WriteLine($"{c.Name} has suffered great damage and succumbed to their grim injuries.");
            player.Children.Remove(c);
            //Console.ReadKey();
        }

        public void PunishChildren()
        {
            if (player.Children.Count >= 1)
            {
                for (int i = 0; i < player.Children.Count; i++)
                {
                    System.Console.WriteLine(player.Children.Count);
                    player.Children[i].Punishment();
                    Console.ReadKey();
                }
            }
            else
            {
                System.Console.WriteLine("You have no children left.");
                System.Console.WriteLine("press any key to continue");
                Console.ReadKey();
            }
        }
        public void TempName(Room current)
        {
            switch (player.Selection(current.GetActions(), "Select an Action", true))
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
                        else if (current == Room.rooms["BattleRoom"])
                        {

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
        private void InfoBoard()
        {
            System.Console.WriteLine("[INFO BOARD]");
            System.Console.WriteLine("Please keep your children on a leash or inside their cage at all times!");
            System.Console.WriteLine("If your children are suffering severe damage you can buy band aids at the store.");
            System.Console.WriteLine("Unjustified child attacks are not tolerated and the aggressor will be put down!");
            Console.ReadKey();
            TempName(Room.rooms["Start"]);
        }
        private void Shop(Room current)
        {
            switch (player.Selection(Item.availableItems, "Choose item", true))
            {
                case 0:
                    {
                        if (player.CheckBalance(10))    //this is very bad and lazy coding. Too bad :)
                        {        
                            player.Inventory.Add(new BandAid());
                            player.RemoveCash(10);
                            System.Console.WriteLine("Bought a band aid");
                            Console.ReadKey();
                        }
                        else{
                            System.Console.WriteLine("Insufficient cash");
                            Console.ReadKey();
                            TempName(Room.rooms["Shop"]);
                        }
                        break;
                    }
                case 1:
                    {
                        TempName(Room.rooms["Shop"]); //this is not pretty either.
                        break;
                    }
            }
        }
    }
}