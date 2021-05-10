using System;

namespace SlutProject
{
    public class ShopRoom : Room
    {
        public ShopRoom()
        {
            Name = "Shop Room"; //assigns room name
        }
        public override string[] GetChoices()   //Gets room specific movement options
        {
            choices.Clear();
            choices.Add("Start");
            choices.Add("Stay");
            return base.GetChoices();
        }
        public override string[] GetActions()   //Gets room specific actions
        {
            Actions = new string[]{
                "Shop",
                "Go Back"
            };
            return Actions;
        }


    }
}
