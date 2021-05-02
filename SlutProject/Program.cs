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
            Instructions(player);
            player.ChooseStarter(spawner);  
            while (true)
            {
                player.AddInitialChoices();
            }
        }
        static void Instructions(Player player)
        {
            System.Console.WriteLine("In this game you will navigate entirely by selecting an option presented in the form of a multi-choice page.");
            System.Console.WriteLine("To navigate this page you may use the up and down arrows on your keyboard. With enter you select the currently hovered option. Any other keys will do nothing.");
            System.Console.WriteLine("You will be given an example selection to show how the game will work. Remember, arrow keys and enter");
            Key.Press();
            string[] exampleSelection ={"Start the game","Also start the game"};
            player.Selection(exampleSelection, "This is an example selection. Controls are: UpArrow, DownArrow and Enter", true);
            Console.Clear();
        }

    }
}
