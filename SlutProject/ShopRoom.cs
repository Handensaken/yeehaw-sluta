using System;

namespace SlutProject
{
    public class ShopRoom : Room
    {
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
                "Shop",
                "Go Back"
            };
            return Actions;
        }
        

    }
}
