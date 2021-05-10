using System;

namespace SlutProject
{
    public class BandAid : Item
    {
        public BandAid()
        {
            /*Name = "Band Aid";    //this is actually completely useless as I revamped the item system.
            Usable = true;*/
            Cost = 10;  //sets cost
        }
        private int recovery = 4;
        public override void Effect(Child c, MasterGameControl controller)  //this item's effect recovers health by calling the child's Recover method
        {
            c.Recover(recovery);
        }
    }
}
