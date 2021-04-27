using System;

namespace SlutProject
{
    public class Demon : BadChild
    {
        public Demon(){
            XpMultiplier = 3.0f;
        }   
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
