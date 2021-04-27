using System;

namespace SlutProject
{
    public class BadChild : Child
    {
        public BadChild(){
            XpMultiplier = 0.8f;
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
