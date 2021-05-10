using System;

namespace SlutProject
{
    public class Key    //This class is used for the shit I couldn't figure out where to put
    {
        public string ReturnString;
        public int ReturnInt;
        //these are used for the selection algorithm so it can be either a string or an int
        public static void Press()  //This static boy just tells the user to press a button and is a glorified console.readkey
        {
            System.Console.WriteLine("Press any button to continue");
            System.Console.WriteLine("");
            Console.ReadKey(true);
        }
    }
}
