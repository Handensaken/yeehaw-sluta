using System;

namespace SlutProject
{
    public class Demon : BadChild
    {
        public Demon(MasterGameControl c) : base(c)
        {
            XpMultiplier = 3.0f;
            Alignment = "A Demon!";
            HP = 45;
            maxHP = HP;
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
