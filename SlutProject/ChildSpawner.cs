using System.Collections.Generic;
using System;

namespace SlutProject
{
    public class ChildSpawner
    {
        public Child Spawner()
        {
            Random rand = new Random();
            switch (rand.Next(2))
            {
                case 0:
                    {
                        if (rand.Next(101) == 100)
                        {
                            return new Demon();
                        }
                        else
                        {
                            return new BadChild();
                        }
                    }
                case 1:
                    {
                        if (rand.Next(101) == 100)
                        {
                            return new Hero();
                        }
                        else
                        {
                            return new GoodChild();
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
