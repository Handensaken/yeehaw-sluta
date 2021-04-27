using System;

namespace SlutProject
{
    public class Hero : GoodChild
    {
        public Hero(){
            XpMultiplier = 3.0f;
            Alignment = "A Hero!";
        }
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
