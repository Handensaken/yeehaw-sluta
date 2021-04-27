using System;

namespace SlutProject
{
    public class GoodChild : Child
    {
        public GoodChild(){
            XpMultiplier = 1.5f;
            Alignment = "Good.";
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
