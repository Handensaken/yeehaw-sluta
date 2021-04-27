using System;

namespace SlutProject
{
    public class GoodChild : Child
    {
        public GoodChild(){
            XpMultiplier = 1.5f;
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
