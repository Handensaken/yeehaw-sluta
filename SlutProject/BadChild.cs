using System;

namespace SlutProject
{
    public class BadChild : Child
    {
        public BadChild(){
            XpMultiplier = 1f;
            Alignment = "Bad.";
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
