using System.Globalization;
using System.Collections.Generic;
using System;
using System.IO;

namespace SlutProject
{
    public class Child
    {
        MasterGameControl controller;
        protected Random rand = new Random();
        public string Name { get; private set; }
        public int HP { get; protected set; }

        protected float maxHP;
        public float Energy { get; set; }
        public float XpMultiplier { get; protected set; }
        public int Level { get; private set; }
        public int Xp { get; private set; }
        public float XpThreshold { get; private set; }
        public string Alignment { get; protected set; }
        public bool IsWild { get; set; }
        public Child(MasterGameControl c, bool wild)
        {
            controller = c;
            Level = 1;
            Name = File.ReadAllLines(@"names.txt")[rand.Next(308)];
            XpThreshold = 10;
            IsWild = wild;
            Energy = 10; //make super drain energy should be ezpz
        }
        public virtual void SuperAttack(Child oC, Player p, Child self)
        {

        }
        public virtual int Attack()
        {
            return 0;
        }
        public void AddXP(int xpValue)
        {
            Xp += (int)Math.Round(xpValue * XpMultiplier);
            System.Console.WriteLine($"{Name} earned {xpValue} experience!");
            if (Xp >= XpThreshold)
            {
                LevelUpEvent();
            }
        }
        private void LevelUpEvent()
        {
            System.Console.WriteLine($"{Name} leveled up!");
            Level += 1;
            Xp = 0;
            XpThreshold *= 1.5f;
            Energy = 10;
            maxHP *= 1.2f;
        }
        public void Punishment()
        {
            System.Console.WriteLine($"You punished {Name}. They cried but maybe they learnt a lesson!");
            AddXP(rand.Next(3) + 1);
            Hurt(rand.Next(2) + 1);
        }
        public void Hurt(int hurtValue)
        {
            HP -= hurtValue;
            if (IsWild)
            {
                System.Console.WriteLine($"Opposing {Name} took {hurtValue} damage! It now has {HP} health left.");
            }
            else
            {
                System.Console.WriteLine($"Your {Name} took {hurtValue} damage! It now has {HP} health left.");
            }
            Key.Press();
            if (HP <= 0)
            {
                controller.ChildDeathEvent(this);
            }
        }
        public void Recover(int recoverAmount)
        {
            HP += recoverAmount;
            if (HP > maxHP) { HP = (int)maxHP; }
            System.Console.WriteLine($"Healed {Name} for {recoverAmount} HP");
        }
        
    }
}
