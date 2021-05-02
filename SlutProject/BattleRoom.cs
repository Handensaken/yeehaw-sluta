using System;

namespace SlutProject
{
    public class BattleRoom : Room
    {
        public BattleRoom()
        {
            Name = "Battle Room";
        }
        public override string[] GetChoices()
        {
            choices.Clear();
            choices.Add("Start");
            choices.Add("Stay");
            return base.GetChoices();
        }
        public override string[] GetActions()
        {
            Actions = new string[]{
                "Begin Battle",
                "Go Back"
            };
            return Actions;
        }
    }
}
