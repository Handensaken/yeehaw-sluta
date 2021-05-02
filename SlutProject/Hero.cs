using System;

namespace SlutProject
{
    public class Hero : GoodChild
    {
        public Hero(MasterGameControl c, bool wild) : base(c, wild)
        {
            XpMultiplier = 3.0f;
            Alignment = "A Hero!";
            HP = 35;
            maxHP = HP;
        }
        public override void SuperAttack(Child oC, Player p, Child self)
        {

            if (IsWild)
            {
                System.Console.WriteLine($"opposing {Name} healed for {10 * Level} HP");
                self.Recover(10 * Level);
            }
            else
            {
                foreach (Child c in p.Children)
                {
                    c.Recover(10 * Level);
                }
                System.Console.WriteLine($"your {Name} healed their party for {10 * Level} HP");
            }
            Energy--;
        }
        public override int Attack()
        {
            int returningValue = 0;
            for (int i = 0; i < 6; i++)
            {
                returningValue += rand.Next(1, 5);
            }
            return returningValue;
        }
    }
}
