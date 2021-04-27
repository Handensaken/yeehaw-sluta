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
        public int HP { get; set; }
        public float Energy { get; set; }
        public float XpMultiplier { get; protected set; }
        public int Level { get; private set; }
        public int Xp { get; private set; }
        public void AddXP(int value)
        {
            Xp += (int)Math.Round(value*XpMultiplier);
        }
        public Child(){
            Level = 1;
            Name = File.ReadAllLines(@"names.txt")[rand.Next(100)];
        }
        public virtual int SuperAttack(){
            return 0;
        }
    }
}
