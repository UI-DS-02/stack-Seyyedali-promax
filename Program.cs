using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MiniProject_Stack
{
    class Program
    {
        static Stack<string> DuplicateStackStr(Stack<string> First)
        {
            Stack<string> New = new Stack<string>();
            Stack<string> Temp = new Stack<string>();
            while (First.Count() > 0)
            {
                Temp.Push(First.Pop());
            }

            while (Temp.Count() > 0)
            {
                First.Push(Temp.Peek());
                New.Push(Temp.Pop());
            }

            return New;
        } //OK
        static void Error(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Message);
            Console.ForegroundColor = ConsoleColor.White;
        } // OK
        static void MyTest(int Number)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Test: " + Number);
            Console.ForegroundColor = ConsoleColor.White;

        } //OK
        static Stack<string> InverseStackStr(Stack<string> InpFirst)
        {
            Stack<string> First = DuplicateStackStr(InpFirst);
            Stack<string> Result = new Stack<string>();
            while (First.Count() > 0)
            {
                Result.Push(First.Pop());
            }
            return Result;
        }  //OK
        static bool IsExit(string Inp)
        {
            if (Inp.ToUpper() == "EXIT")
            {
                return true;
            }
            return false;
        } //OK
        static Stack<string> SeparatePhrase(string Phrase)
        {
            string[] Chars = new string[Phrase.Length];
            string[] Symbols = new string[8] { "^", "/", "!", "*", "+", "-", "(", ")" };
            for (int i = 0; i < Phrase.Length; i++)
            {
                Chars[i] = Convert.ToString(Phrase[i]);
            }
            //
            Stack<string> Temp = new Stack<string>();
            string BigNumber = "";
            foreach (string c in Chars)
            {
                if (Symbols.Contains(c))
                {
                    if (BigNumber != "")
                    {
                        Temp.Push(BigNumber);
                        BigNumber = "";
                    }
                    Temp.Push(c);
                }
                else if (c == ".")
                {
                    BigNumber = BigNumber + "/";
                }
                else if (c.ToUpper() == "P")
                {
                    if (BigNumber.Length > 0)
                    {
                        Temp.Push(BigNumber);
                    }
                    BigNumber = "P";
                }
                else if (c.ToUpper() == "E")
                {
                    if (BigNumber.Length > 0)
                    {
                        Temp.Push(BigNumber);
                    }
                    Temp.Push("2/7182818");
                    BigNumber = "";
                }
                else if (c.ToUpper() == "I")
                {
                    if (BigNumber.ToUpper() == "P")
                    {
                        Temp.Push("3/141592");
                    }
                    BigNumber = "";
                }
                else
                {
                    BigNumber = BigNumber + c;
                }
            }
            //کنترل عدد آخر
            if (BigNumber.Length > 0)
            { Temp.Push(BigNumber); }
            //
            Stack<string> Result = new Stack<string>();
            while (0 < Temp.Count())
            {
                Result.Push(Temp.Pop());
            }
            return Result;
        } //OK after controling chars
        static Stack<string> FactorielCalc(Stack<string> Inp)
        {
            Stack<string> Phrase = DuplicateStackStr(Inp);
            Stack<string> Temp = new Stack<string>();
            // !
            while (Phrase.Count() > 1)
            {
                Temp.Push(Phrase.Pop());
                if (Phrase.Count() > 0)
                {
                    if (Phrase.Peek() == "!")
                    {
                        Phrase.Pop();
                        Temp.Push(Convert.ToString(Factoriel(Convert.ToDouble(Temp.Pop()))));
                    }
                }
            }
            if (Phrase.Count() == 1)
            { Temp.Push(Phrase.Pop()); }
            Temp = InverseStackStr(Temp);
            Temp = InsertScoreOptions(Temp);
            return Temp;
        }
        static Stack<string> SumMinusCalc (Stack<string> Inp)
        {
            Stack<string> Phrase = DuplicateStackStr(Inp);
            Stack<string> Temp = new Stack<string>();
            while (Phrase.Count()>1)
            {
                Temp.Push(Phrase.Pop());
                if (Phrase.Peek() == "+")
                {
                    Phrase.Pop();
                    Temp.Push(Convert.ToString(Convert.ToDouble(Phrase.Pop()) + Convert.ToDouble(Temp.Pop())));

                }
                else if (Phrase.Peek() == "-")
                {
                    Phrase.Pop();
                    Temp.Push(Convert.ToString(Convert.ToDouble(Temp.Pop()) - Convert.ToDouble(Phrase.Pop())));
                }
            }
            Temp.Push(Phrase.Pop());
            Temp = InverseStackStr(Temp);
            return Temp;
        }
        static bool ControlSyntax(Stack<string> Phrase1)
        {
            Stack<string> Phrase = DuplicateStackStr(Phrase1);
            Stack<string> Words = DuplicateStackStr(Phrase);
            Stack<string> HalfTwo = new Stack<string>();
            string[] Operators = new string[5] { "-", "+", "/", "*", "^" };// - !
            // *,/,...  phrase
            if ((Operators.Contains(Words.Peek()) & Words.Peek() != "+" & Words.Peek() != "-") || Words.Peek() == "!")
            {
                MyTest(1);
                return false;
            }
            HalfTwo.Push(Words.Pop());
            while (Words.Count() > 0)
            {
                // ()
                if (Words.Peek() == ")" & HalfTwo.Peek() == "(")
                {
                    MyTest(2);
                    return false;
                }
                // +-
                if ((Operators.Contains(Words.Peek()) & Operators.Contains(HalfTwo.Peek())) || (Words.Peek() == "!") & HalfTwo.Peek() == "!")
                {
                    MyTest(3);
                    return false;
                }
                // +)
                if (Words.Peek() == ")" & Operators.Contains(HalfTwo.Peek()))
                {
                    MyTest(4);
                    return false;
                }
                // (pi,e) num , num (pi,e) , X! y!
                if (IsNumber(Words.Peek()) & IsNumber(HalfTwo.Peek()))
                {
                    MyTest(5);
                    return false;
                }
                // +!
                if (Words.Peek() == "!" & Operators.Contains(HalfTwo.Peek()))
                {
                    MyTest(8);
                    return false;
                }
                HalfTwo.Push(Words.Pop());
            }
            // phrase +,-,...
            if (Operators.Contains(HalfTwo.Peek()))
            {
                MyTest(6);
                return false;
            }
            return true;
        } //OK
        static double Calculate(Stack<string> PhraseInp)
        {
            double Result = 0;
            Stack<string> Phrase = DuplicateStackStr(PhraseInp);
            Stack<string> Temp = new Stack<string>();
            bool IsStarted = false;
            int PharantheseSize = 0;
            int Pharantheses = 0;
            while (Phrase.Count() > 0)
            {
                Temp.Push(Phrase.Pop());
                if (Temp.Peek() == "(")
                {
                    Pharantheses++;
                    IsStarted = true;
                }
                else if (Temp.Peek() == ")")
                {
                    Pharantheses--;
                }
                if (IsStarted)
                {
                    PharantheseSize++;
                }
                if (Pharantheses == 0 & IsStarted)
                {
                    Stack<string> Temp2 = new Stack<string>();
                    Temp.Pop();
                    for (int i = 0; i < PharantheseSize - 1; i++)
                    {
                        Temp2.Push(Temp.Pop());
                    }
                    Temp.Pop();
                    Temp.Push(Convert.ToString(Calculate(Temp2)));
                    PharantheseSize = 0;
                    IsStarted = false;
                }
            }
            Result = CalculateSimple(InverseStackStr(Temp));
            return Result;
        }
        static double CalculateSimple (Stack<string> Inp)
        {
            double Res = 0;
            Stack<string> Phrase = DuplicateStackStr(Inp);
            return Res;
        }
        static double Factoriel(double x)
        {
            double Result = 1;
            for (int i = 1; i < x + 1; i++)
            {
                Result *= i;
            }
            return Result;
        } 
        static bool ControlPrantheses(string Phrase)
        {
            Stack<string> InputStk = new Stack<string>();
            Stack<string> HalfTwo = new Stack<string>();
            int n;
            for (int i = 0; i < Phrase.Length; i++)
            {
                if (Phrase[i] == '(' || Phrase[i] == ')')
                    InputStk.Push(Convert.ToString(Phrase[i]));
            }
            n = 2 * InputStk.Count();
            while (n > 0 && InputStk.Count > 0)
            {
                if (HalfTwo.Count() > 0)
                {
                    if (InputStk.Peek() == "(" && HalfTwo.Peek() == ")")
                    {
                        HalfTwo.Pop();
                        InputStk.Pop();
                    }
                    else
                    {
                        HalfTwo.Push(InputStk.Pop());
                    }
                }
                else if (HalfTwo.Count() == 0)
                {
                    HalfTwo.Push(InputStk.Pop());
                }
                n--;
            }
            if (InputStk.Count > 0 || HalfTwo.Count() > 0)
            {
                return false;
            }
            return true;
        } //OK
        static bool ControlChars(string Phrase) //OK
        {
            bool Result = true;
            char[] Symbols = new char[9] { '(', ')', '!', '^', '/', '+', '-', '*', '.' };
            char[] Numbers = new char[10] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            for (int i = 0; i < Phrase.Length; i++)
            {
                if (Symbols.Contains(Phrase[i]) || Numbers.Contains(Phrase[i]))
                {
                }
                else if (Phrase[i].ToString().ToLower() == "e")
                {
                }
                else if (Phrase.Length > i + 1 && Phrase[i].ToString().ToLower() + Phrase[i + 1].ToString().ToLower() == "pi")
                {
                    i++;
                }
                else
                {
                    Result = false;
                    break;
                }
            }
            return Result;
        }
        static bool IsNumber(string x)
        {
            double y;
            return double.TryParse(x, out y);
        }
        static Stack<string> InsertScoreOptions(Stack<string> Phrase)
        {
            string[] Ops = new string[2] { "+", "-" };
            Stack<string> Result = new Stack<string>();
            Stack<string> Temp = new Stack<string>();
            // +phrase
            if (Ops.Contains(Phrase.Peek()))
            {
                Phrase.Push("0");
            }
            Temp.Push(Phrase.Pop());
            while (Phrase.Count() > 0)
            {
                // (+
                if (Temp.Peek() == "(" & Ops.Contains(Phrase.Peek()))
                {
                    Temp.Push("0");
                }
                // )( , num( , )num
                if ((Temp.Peek() == ")" & Phrase.Peek() == "(") || (IsNumber(Temp.Peek()) & Phrase.Peek() == "(") || (IsNumber(Phrase.Peek()) & Temp.Peek() == ")"))
                {
                    Temp.Push("*");
                }
                // num! num!
                if (Temp.Peek()=="!"&IsNumber(Phrase.Peek()))
                {
                    Temp.Push("*");
                }
                Temp.Push(Phrase.Pop());
            }
            while (Temp.Count() > 0)
            {
                Result.Push(Temp.Pop());
            }
            return Result;
        }
        static void Main(string[] args)
        {
            //معرفی پروژه 
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Seyyedali Hoseynzadeh    4013613025:     stack project\n\nHello! Welcom to calculator program\n");
            //
            bool Exit = false;
            while (!Exit)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nEnter the mathematical phrase in the currect syntax:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("                                 (Or Enter \"Exit\" to exit program)\n\n");
                string Phrase = Console.ReadLine();
                Exit = IsExit(Phrase);
                if (!Exit && Phrase.Length > 0)
                {
                    if (ControlChars(Phrase))
                    {
                        if (ControlPrantheses(Phrase))
                        {
                            Stack<string> Separated = SeparatePhrase(Phrase);
                            if (ControlSyntax(Separated))
                            {
                                Separated = InsertScoreOptions(Separated);
                                //Console.Clear();
                                while (Separated.Count() > 0)
                                {
                                    Console.WriteLine(Separated.Pop());
                                }
                                double Result = Calculate(Separated);
                                Console.WriteLine("\nThe Answer is:  " + Result + "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nPress Enter to claculate another phrase");
                            }
                            else
                            {
                                Error("Error! incurrect syntax!");
                            }
                        }
                        else
                        {
                            Error("Error! Parentheses are incorrect!");
                        }
                    }
                    else
                    {
                        Error("Error! Invalid characters recognized!");
                    }
                    Console.ReadLine();
                }
                Console.Clear();
            }
        }
    }
}

