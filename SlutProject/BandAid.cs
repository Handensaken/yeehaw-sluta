using System;

namespace SlutProject
{
    public class BandAid : Item
    {
        public BandAid(){
            Name = "Band Aid";
            Cost = 10;
        }
        public int Recovery
        {
            get
            {
                return Recovery;
            }
            private set
            {
                Recovery = 4;
            }
        }
    }
}
