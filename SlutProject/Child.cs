using System.Globalization;
using System.Collections.Generic;
using System;
using System.IO;

namespace SlutProject
{
    public class Child
    {
        Random rand = new Random();
        public string Name { get; private set; }
        public int HP { get; protected set; }
        public float Energy { get; set; }
        public float XpMultiplier { get; protected set; }
        public int Level { get; private set; }
        public int Xp { get; private set; }
        public string Alignment { get; protected set; }
        public Child()
        {
            Level = 1;
            Name = File.ReadAllLines(@"names.txt")[rand.Next(308) + 1];
        }
        public virtual int SuperAttack()
        {
            return 0;
        }
        public void AddXP(int xpValue)
        {
            Xp += (int)Math.Round(xpValue * XpMultiplier);
            System.Console.WriteLine($"");
        }
        public void Punishment()
        {
            System.Console.WriteLine($"You punished {Name}. They cried but maybe they learnt a lesson!");
            Hurt(rand.Next(2) + 1);
            AddXP(rand.Next(3) + 1);
        }
        public void Hurt(int hurtValue)
        {
            HP -= hurtValue;
            System.Console.WriteLine($"{Name} took {hurtValue} damage! It now has {HP} health left.");
        }
    }
}
