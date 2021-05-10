using System.ComponentModel;
using System;
using System.Collections.Generic;
namespace SlutProject
{
    public class Room   //This is a parent that handles everything that is related to all rooms
    {
        protected string[] Actions;
        public string Name { get; protected set; }
        public static Dictionary<string, Room> rooms { get; private set; } = new Dictionary<string, Room>(); 
        //This does not work properly and it behaves like a public list. Even if the set is private you can still add new items you just can't replace it with a new one
        protected List<string> choices = new List<string>();    //This is the choice list of all the possible rooms you can move to
        public static void InitializeRooms() //Adds all neccessary rooms
        {
            rooms.Add("Start", new StartRoom());
            rooms.Add("Battle", new BattleRoom());
            rooms.Add("Shop", new ShopRoom());
        }
        public virtual string[] GetChoices()    //returns choices
        {
            return choices.ToArray();
        }
        //These are both virtual methods meaning they are overwritten by their children
        public virtual string[] GetActions()    //returns actions
        {
            return Actions;
        }
    }
}
