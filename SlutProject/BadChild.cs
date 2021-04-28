using System;

namespace SlutProject
{
    public class BadChild : Child
    {
        public BadChild(MasterGameControl c) : base(c) 
        {
            XpMultiplier = 1f;
            Alignment = "Bad.";
            HP = 25;
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
