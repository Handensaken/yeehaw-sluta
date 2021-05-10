using System;

namespace SlutProject
{
    public class BattleRoom : Room
    {
        public BattleRoom()
        {
            Name = "Battle Room";       //gives room name
        }
        public override string[] GetChoices()   //override that clears the choices and gives the new room specific choices
        {
            choices.Clear();
            choices.Add("Start");
            choices.Add("Stay");
            return base.GetChoices();
        }
        public override string[] GetActions()   //gives the room's specific actions
        {
            Actions = new string[]{
                "Begin Battle",
                "Go Back"
            };
            return Actions;
        }
    }
}
