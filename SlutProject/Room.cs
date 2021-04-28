using System.ComponentModel;
using System;
using System.Collections.Generic;
namespace SlutProject
{
    public class Room
    {
        protected string[] Actions;
        public static Dictionary<string, Room> rooms { get; private set; } = new Dictionary<string, Room>();
        protected List<string> choices = new List<string>();
        public static void InitializeRooms()
        {
            rooms.Add("Start", new StartRoom());
            rooms.Add("Battle", new BattleRoom());
            rooms.Add("Shop", new ShopRoom());
        }
        public virtual string[] GetChoices()
        {
            return choices.ToArray();
        }
        public virtual string[] GetActions(){
            return Actions;
        }
    }
}
