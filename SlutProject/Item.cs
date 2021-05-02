using System;

namespace SlutProject
{
    public class Item
    {
        public bool Usable { get; protected set; }
        public static string[] availableItems =
        {
          "Band Aid",
          "Net",
          "Nevermind"
        };
        public string Name { get; protected set; }
        public int Cost { get; protected set; }
        public virtual void Effect(Child c, MasterGameControl controller)
        {

        }
    }
}
