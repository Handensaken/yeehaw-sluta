using System;

namespace SlutProject
{
    public class BandAid : Item
    {
        public BandAid()
        {
            Name = "Band Aid";
            Cost = 10;
            Usable = true;
        }
        private int recovery = 4;
        public override void Effect(Child c)
        {
            c.Recover(recovery);
        }
    }
}
