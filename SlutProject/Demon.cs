using System;

namespace SlutProject
{
    public class Demon : BadChild //this one does not need to be a child of GoodChild and would work perfectly as a child of Child but this opens up the possibility of making the class more advanced. It also looks more structured
    {
        public Demon(MasterGameControl c, bool wild) : base(c, wild)
        {
            XpMultiplier = 3.0f;
            Alignment = "A Demon!";
            HP = 45;
            maxHP = HP;
        }
        public override void SuperAttack(Child oC, Player p, Child self)    //overrides the super attack method and damages the enemy by 30% of their current hp
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
        public override int Attack()    //overrides attack as per usual :))))))))))))))
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
