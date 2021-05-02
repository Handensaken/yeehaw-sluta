using System;

namespace SlutProject
{
    public class BandAid : Item
    {
        public BandAid()
        {
            /*Name = "Band Aid";
            Usable = true;*/
            Cost = 10;
        }
        private int recovery = 4;
        public override void Effect(Child c, MasterGameControl controller)
        {
            c.Recover(recovery);
        }
    }
}
