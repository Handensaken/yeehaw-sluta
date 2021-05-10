using System.Collections.Generic;
using System;

namespace SlutProject
{
    public class ChildSpawner
    {
        public Child Spawner(MasterGameControl controller, bool wild)   //spawns a child and randomizes if they are good or bad. then randomizes 1/100 if they are either a demon or a hero.
        {
            Random rand = new Random();
            switch (rand.Next(2))
            {
                case 0:
                    {
                        if (rand.Next(101) == 100)
                        {
                            return new Demon(controller, wild);
                        }
                        else
                        {
                            return new BadChild(controller, wild);
                        }
                    }
                case 1:
                    {
                        if (rand.Next(101) == 100)
                        {
                            return new Hero(controller, wild);
                        }
                        else
                        {
                            return new GoodChild(controller, wild);
                        }
                    }
                default:
                    {
                        System.Console.WriteLine("shits broken");
                        return null;
                    }
            }
        }
    }
}
