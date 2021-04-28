using System;
using System.Collections.Generic;

namespace SlutProject
{
    class Program
    {

        static void Main(string[] args)
        {
            Room.InitializeRooms();
            Player player = new Player();
            ChildSpawner spawner = new ChildSpawner();
            MasterGameControl gameControl = new MasterGameControl(player);
            player.GetController(gameControl);
            Instructions();
            player.ChooseStarter(spawner);  // move outside main playing loop
            while (true)
            {
                player.AddInitialChoices();
               // Console.ReadKey();
            }
        }
        static void Instructions()
        {
            System.Console.WriteLine("In this game you will navigate entirely by selecting an option presented in the form of a multi-choice page.");
            System.Console.WriteLine("To navigate this page you may use the up and down arrows on your keyboard. With enter you select the currently hovered option. Any other keys will do nothing.");
            System.Console.WriteLine("To close this page press enter. Any other button will write text that does not affect the game in nay way.");
            Console.ReadLine();
            Console.Clear();
        }

    }
}
