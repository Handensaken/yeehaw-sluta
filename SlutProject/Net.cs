using System;

namespace SlutProject
{
    public class Net : Item
    {
        public Net(){   //sets the cost of the net
            Cost = 20;
        }
        public override void Effect(Child c, MasterGameControl controller) //overrides the effect method and checks if the child in question is wild, if so it attempts to catch it.
        {

            if (c.IsWild)
            {
                controller.Catch(c);
            }
            else    //This is mainly a debug thing and if it runs I am the big stupido
            {
                System.Console.WriteLine("Child is somehow not wild and I fucked up somewhere");
            }

        }
    }
}
