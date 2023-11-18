using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MiniProject_Stack
{
    class Pair
    {
        public int First, Second;
    }
    class Program
    {
        static bool IsExit(string Inp)
        {
            if (Inp.ToUpper() == "EXIT")
            {
                return true;
            }
            return false;
        }
        static double Calculate(string Phrase)
        {
            char[] Chars = new char[Phrase.Length];
            for (int i = 0; i < Phrase.Length; i++)
            {
                Chars[i] = Phrase[i];
            }
            Stack<string> Temp = new Stack<string>();
            string BigNumber = "";
            foreach (char c in Chars)
            {
                if ((c != '+') && (c != '-') && (c != '*') && (c != '/'))
                {
                    BigNumber = BigNumber + Convert.ToString(c);
                }
                else if (false)
                {
                    //pi , e
                }
                else
                {
                    Temp.Push(BigNumber);
                    BigNumber = "";
                    Temp.Push(Convert.ToString(c));
                }

            }
            return 0;
        }
        static void Error(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static bool IsParenthesesCurrect(string Phrase)
        {
            Stack<string> InputStk = new Stack<string>();
            Stack<string> HalfTwo = new Stack<string>();
            Stack<Pair> Anwer = new Stack<Pair>();
            Stack<int> HalfTwoI = new Stack<int>();
            int n, j;
            for (int i = 0; i < Phrase.Length; i++)
            {
                if (Phrase[i] == '(' || Phrase[i] == ')')
                    InputStk.Push(Convert.ToString(Phrase[i]));
            }
            j = 1;
            n = 2 * InputStk.Count();
            while (n > 0 && InputStk.Count > 0)
            {
                if (HalfTwo.Count() > 0)
                {
                    if (InputStk.Peek() == "(" && HalfTwo.Peek() == ")")
                    {
                        Pair Set = new Pair();
                        Set.First = j;
                        Set.Second = HalfTwoI.Pop();
                        Anwer.Push(Set);
                        HalfTwo.Pop();
                        InputStk.Pop();
                    }
                    else
                    {
                        HalfTwo.Push(InputStk.Pop());
                        HalfTwoI.Push(j);
                    }
                }
                else if (HalfTwo.Count() == 0)
                {
                    HalfTwo.Push(InputStk.Pop());
                    HalfTwoI.Push(j);
                }
                j++;
                n--;
            }
            if (InputStk.Count > 0 || HalfTwo.Count() > 0)
            {
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Seyyedali Hoseynzadeh    4013613025\n\nHello! Welcom to calculator program\n");
            bool Exit = false;
            while (!Exit)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nEnter the mathematical phrase in the currect syntax:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("                                 (Or Enter \"Exit\" to exit program)\n\n");
                string Phrase = Console.ReadLine();
                Exit = IsExit(Phrase);
                if (!Exit)
                {
                    if (IsParenthesesCurrect(Phrase))
                    {
                        Console.Clear();
                        double Result = Calculate(Phrase);
                        Console.WriteLine("\nThe Answer is:  " + Result + "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nPress Enter to claculate another phrase");
                    }
                    else
                    {
                        Error("Error! Parentheses are incorrect!");
                    }
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
