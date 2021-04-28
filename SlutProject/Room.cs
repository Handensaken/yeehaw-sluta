using System.ComponentModel;
using System;
using System.Collections.Generic;
namespace SlutProject
{
    public class Room
    {
        public static Dictionary<string, Room> rooms {get; private set;} = new Dictionary<string, Room>();
        protected List<string> choices = new List<string>();
        public static void InitializeRooms(){
            rooms.Add("Start", new StartRoom());
            rooms.Add("Battle", new BattleRoom());
            rooms.Add("Shop", new ShopRoom());
        }
        public virtual string[] GetChoices(){
            return choices.ToArray();
        }
    }
}
