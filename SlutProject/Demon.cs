using System;

namespace SlutProject
{
    public class Demon : BadChild
    {
        public Demon(){
            XpMultiplier = 3.0f;
            Alignment = "A Demon!";
        }   
        public override int SuperAttack()
        {
            return base.SuperAttack();
        }
    }
}
