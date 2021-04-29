using System;

namespace SlutProject
{
    public class StartRoom : Room
    {
        
        public override string[] GetChoices()
        {
            choices.Clear();
            choices.Add("Shop");
            choices.Add("Battle");
            choices.Add("Stay");
            return base.GetChoices();
        }
        public override string[] GetActions()
        {
            Actions = new string[]{
                "Check Info Board",
                "Back"
            };
            return Actions;
        }
    }
}
