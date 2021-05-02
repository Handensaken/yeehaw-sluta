using System;

namespace SlutProject
{
    public class BadChild : Child
    {
        public BadChild(MasterGameControl c, bool wild) : base(c, wild)
        {
            XpMultiplier = 1f;
            Alignment = "Bad.";
            HP = 25;
            maxHP = HP;
        }
        public override void SuperAttack(Child oC, Player p, Child self)
        {
            if (IsWild)
            {
                System.Console.WriteLine($"opposing {Name} bit their opponent");
            }
            else{
                System.Console.WriteLine($"your {Name} bit their opponent");
            }
            oC.Hurt(rand.Next(8, 33));
            Energy--;
        }
        public override int Attack()
        {
            int returningValue = 0;
            for (int i = 0; i < 2; i++)
            {
                returningValue += rand.Next(1, 9);
            }
            return returningValue;
        }
    }
}
