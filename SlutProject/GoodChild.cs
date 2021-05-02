using System;

namespace SlutProject
{
    public class GoodChild : Child
    {
        public GoodChild(MasterGameControl c, bool wild) : base(c, wild)
        {
            XpMultiplier = 1.5f;
            Alignment = "Good.";
            HP = 20;
            maxHP = HP;
        }
        public override void SuperAttack(Child oC, Player p, Child self)
        {
            int returningValue = 0;
            for (int i = 0; i < 2; i++)
            {
                returningValue += rand.Next(6, 13);
            }
            if (IsWild)
            {
                System.Console.WriteLine($"opposing {Name} drop kicked your {oC.Name}");
            }
            else
            {
                System.Console.WriteLine($"your {Name} drop kicked the opposing {oC.Name}");
            }
            Console.ReadKey();
            oC.Hurt(returningValue);
            Energy--;
        }
        public override int Attack()
        {
            int returningValue = 0;
            for (int i = 0; i < 3; i++)
            {
                returningValue += rand.Next(1, 5);
            }
            return returningValue;
        }
    }
}
