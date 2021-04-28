using System.Collections.Generic;
using System;

namespace SlutProject
{
    public class ChildSpawner
    {
        public Child Spawner(MasterGameControl controller)
        {
            Random rand = new Random();
            switch (rand.Next(2))
            {
                case 0:
                    {
                        if (rand.Next(101) == 100)
                        {
                            return new Demon(controller);
                        }
                        else
                        {
                            return new BadChild(controller);
                        }
                    }
                case 1:
                    {
                        if (rand.Next(101) == 100)
                        {
                            return new Hero(controller);
                        }
                        else
                        {
                            return new GoodChild(controller);
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
