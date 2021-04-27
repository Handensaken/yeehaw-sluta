using System;

namespace SlutProject
{
    public class Hero : GoodChild
    {
        public Hero(){
            XpMultiplier = 3.0f;
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
