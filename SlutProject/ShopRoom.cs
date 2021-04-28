using System;

namespace SlutProject
{
    public class ShopRoom : Room
    {
         public override string[] GetChoices()
        {
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
