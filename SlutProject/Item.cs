using System;

namespace SlutProject
{
    public class Item
    {
        public static string[] availableItems = 
        {
          "Band Aid",
          "Nevermind"
        };
        public string Name { get; protected set; }
        public int Cost { get; protected set; }
    }
}
