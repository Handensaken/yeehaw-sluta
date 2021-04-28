using System;

namespace SlutProject
{
    public class Key<T>
    {
        public T value;
        public void PrintChoices(string[] choices, int current, string q)
        {
            System.Console.WriteLine(q);
            for (int i = 0; i < choices.Length; i++)
            {
                if (current == i)
                {
                    System.Console.WriteLine($">  {choices[i]}");
                }
                else
                {
                    System.Console.WriteLine($"  {choices[i]} ");
                }
            }
        }
        public T Selection(string[] choices, string q, bool SoI)
        {
            int current = 0;
            while (true)
            {
                Console.Clear();
                PrintChoices(choices, current, q);
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            current++;
                            current = current % choices.Length;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        {
                            current--;
                            if (current < 0) { current = choices.Length - 1; }
                            else
                            {
                                current = Math.Abs(current % choices.Length);
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            if (1 == 1)
                            {
                                Key<string> stringKey = new Key<string>();
                                stringKey.value = "test";
                                
                              //  return stringKey.value;
                            }
                            else
                            {

                            }
                            return value;
                        }
                    default:
                        {
                            // handle everything else
                        }
                        break;
                }
            }
        }

    }
}
