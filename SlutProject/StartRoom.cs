using System;

namespace SlutProject
{
    public class StartRoom : Room
    {
        public override string[] GetChoices()
        {
            choices.Add("test");
            return base.GetChoices();
        }
    }
}
