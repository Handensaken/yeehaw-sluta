using System;

namespace SlutProject
{
    public class BadChild : Child
    {
        public BadChild(MasterGameControl c, bool wild) : base(c, wild) //Gives the child all their neccessary information 
        {
            XpMultiplier = 1f;
            Alignment = "Bad.";
            HP = 25;
            maxHP = HP;
        }
        public override void SuperAttack(Child oC, Player p, Child self)    //override that describes the child's super attack, damages the enemy and reduces the energy
        {
            if (IsWild) //just checks if the child in question is wild for clarification
            {
                System.Console.WriteLine($"opposing {Name} bit their opponent");    
            }
            else{
                System.Console.WriteLine($"your {Name} bit their opponent");
            }
            oC.Hurt(rand.Next(8, 33));  //damages the enemy
            Energy--;   //reduces energy
        }
        public override int Attack()    //overrides attack and damages the enemy.
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
