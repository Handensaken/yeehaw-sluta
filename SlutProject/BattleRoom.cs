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
    }
}
