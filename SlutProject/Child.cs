using System.Globalization;
using System.Collections.Generic;
using System;
using System.IO;

namespace SlutProject
{
    public class Child
    {
        private MasterGameControl controller;
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
        //^^ sets up a whole bunch of variables
        public Child(MasterGameControl c, bool wild)    //sets up the child
        {
            controller = c;
            Level = 1;
            //^^ this is just simple variable setting
            Name = File.ReadAllLines(@"names.txt")[rand.Next(308)]; //this one uses the IO lib to get a string array from a text file and then assign the name to a random line in said text file
            XpThreshold = 10;
            IsWild = wild;
            Energy = 10; //and the remaining things are also variable setting
        }
        public virtual void SuperAttack(Child oC, Player p, Child self)     
        {
            //Sets up the super attack method to give each subclass their own super attack
        }
        public virtual int Attack()
        {
            return 0;   //sets up attack method so the different subclasses can have unique attacks
        }
        public void AddXP(int xpValue)      //woohoo give xp
        {
            Xp += (int)Math.Round(xpValue * XpMultiplier);
            System.Console.WriteLine($"{Name} earned {xpValue} experience!");
            if (Xp >= XpThreshold)
            {
                LevelUpEvent();
            }
        }
        private void LevelUpEvent() //woohooo level up
        {
            System.Console.WriteLine($"{Name} leveled up!");
            Level += 1;
            Xp = 0;
            XpThreshold *= 1.5f;
            Energy = 10;
            maxHP *= 1.2f;
        }
        public void Punishment()    //this one is for punishing children >:) damages them but gives them xp. 
        {
            System.Console.WriteLine($"You punished {Name}. They cried but maybe they learnt a lesson!");
            AddXP(rand.Next(3) + 1);
            Hurt(rand.Next(2) + 1);
        }
        public void Hurt(int hurtValue) //Hurts the child and kills it if their hp reaches 0
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
        public void Recover(int recoverAmount)  //heals the child for a specified amount
        {
            HP += recoverAmount;
            if (HP > maxHP) { HP = (int)maxHP; }
            System.Console.WriteLine($"Healed {Name} for {recoverAmount} HP");
        }
        
    }
}
