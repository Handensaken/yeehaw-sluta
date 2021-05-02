using System;

namespace SlutProject
{
    public class Net : Item
    {
        public override void Effect(Child c, MasterGameControl controller)
        {

            if (c.IsWild)
            {
                controller.Catch(c);
            }
            else
            {
                System.Console.WriteLine("Child is somehow not wild and I fucked up somewhere");
            }

        }
    }
}
