using System;

namespace SlutProject
{
    public class GoodChild : Child
    {
        public GoodChild(MasterGameControl c) : base(c)
        {
            XpMultiplier = 1.5f;
            Alignment = "Good.";
            HP = 20;
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
