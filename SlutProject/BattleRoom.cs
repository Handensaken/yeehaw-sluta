using System;

namespace SlutProject
{
    public class BattleRoom : Room
    {
         public override string[] GetChoices()
        {
            choices.Add("test2");
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
