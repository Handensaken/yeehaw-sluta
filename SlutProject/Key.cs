using System;

namespace SlutProject
{
    public class Key
    {
        public string ReturnString;
        public int ReturnInt;

        public static void Press(){
            System.Console.WriteLine("Press any button to continue");
            System.Console.WriteLine("");
            Console.ReadKey(true);
        }
    }
}
