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
            //sets up basic variables
        }
        public override void SuperAttack(Child oC, Player p, Child self)    //overrides the super attack method of Child.cs, describes the attack, damages the enemy and drains energy
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
            oC.Hurt(returningValue);
            Energy--;
        }
        public override int Attack()    //overrides attack method and deals damage
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
