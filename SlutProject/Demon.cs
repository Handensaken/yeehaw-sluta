using System;

namespace SlutProject
{
    public class Demon : BadChild
    {
        public Demon(MasterGameControl c, bool wild) : base(c, wild)
        {
            XpMultiplier = 3.0f;
            Alignment = "A Demon!";
            HP = 45;
            maxHP = HP;
        }
        public override void SuperAttack(Child oC, Player p, Child self)
        {
            float damage = (float)oC.HP;
            damage *= 0.3f;
            if (IsWild)
            {
                System.Console.WriteLine($"opposing {Name} flung feces at your {oC.Name}, damaging their soul!");

            }
            else
            {
                System.Console.WriteLine($"your {Name} flung feces at the opposing {oC.Name}, damaging their soul!");

            }
            oC.Hurt((int)damage);
            Energy--;
        }
        public override int Attack()
        {
            int returningValue = 0;
            for (int i = 0; i < 4; i++)
            {
                returningValue += rand.Next(1, 9);
            }
            return returningValue;
        }
    }
}
