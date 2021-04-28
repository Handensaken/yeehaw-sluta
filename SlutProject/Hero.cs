using System;

namespace SlutProject
{
    public class Hero : GoodChild
    {
        public Hero(MasterGameControl c) : base(c)
        {
            XpMultiplier = 3.0f;
            Alignment = "A Hero!";
            HP = 35;
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
