using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF
{
    class Interpreter
    {
        public System.IO.TextReader Input{get;set;}
        public System.IO.TextWriter Output { get; set; }
        public System.IO.TextWriter Error { get; set; }
        public int MemorySize { get; set; }
        public static string CleanCode(string code)
        {
            return string.Join("", code.Where((x) => "+-<>[],.".Contains(x)));
        }
        public Interpreter(int memSize = 30000, System.IO.TextReader input = null, System.IO.TextWriter output = null, System.IO.TextWriter error=null)
        {
            Input = input ?? Console.In;
            Output = output ?? Console.Out;
            Error = error ?? Console.Error;
            MemorySize = memSize;
        }
        public void Run(string code, bool debug=false)
        {
            int[] memory = new int[MemorySize];
            int ptr = 0;
            //code = CleanCode(code);
            Stack<int> labels = new Stack<int>();
            for (int i = 0; i < code.Length; i++)
            {
                switch (code[i])
                {
                    case '+':
                        memory[ptr]++;
                        break;
                    case '-':
                        memory[ptr]--;
                        break;
                    case '<':
                        ptr--;
                        break;
                    case '>':
                        ptr++;
                        break;
                    case '[':
                        if (memory[ptr] == 0)
                        {
                            int numToMatch = 1;
                            while(numToMatch>0)
                            {
                                i++;
                                if (code[i] == ']')
                                    numToMatch--;
                                else if (code[i] == '[')
                                    numToMatch++;
                            }
                        }
                        else
                            labels.Push(i);
                        break;
                    case ']':
                        if (labels.Count == 0)
                            Error.WriteLine("ERROR: ] has no matching [ at position {0}", i);
                        if (memory[ptr] != 0)
                            i = labels.Peek();
                        else
                            labels.Pop();
                        break;
                    case ',':
                        memory[ptr] = Input.Read();
                        break;
                    case '.':
                        Output.Write((char)memory[ptr]);
                        break;
                    default://the BF spec states that any non-understood characters should simply be ignored
                        break;
                    case '#':
                        if (debug)
                        {
                            Error.WriteLine("DEBUG: {0}", string.Join(" ", memory.Take(5).Select((x)=>x.ToString())));
                            Console.ReadKey();
                        }
                        break;
                }
            }
        }
    }
}
