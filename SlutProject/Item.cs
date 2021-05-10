using System;

namespace SlutProject
{
    public class Item
    {
        public bool Usable { get; protected set; }  //bool that describes if an item is usable or not
        public static string[] availableItems = //array that includes the different options when shopping.
        {
          "Band Aid",
          "Net",
          "Nevermind"
        };
        public string Name { get; protected set; }  //name of item
        public int Cost { get; protected set; }   //cost of item
        public virtual void Effect(Child c, MasterGameControl controller)   //sets up a virtual effect method that is later overridden
        {

        }
    }
}
