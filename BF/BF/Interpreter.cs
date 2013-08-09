using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF
{
    class Interpreter
    {
        public static string CleanCode(string code)
        {
            return string.Join("", code.Where((x) => "+-<>[],.".Contains(x)));
        }
        string code;
        public Interpreter(string code)
        {
            this.code = CleanCode(code);
        }
        public void Run()
        {
            Stack<int> labels = new Stack<int>();
            for (int i = 0; i < code.Length; i++)
            {
                switch (code[i])
                {
                    case '+':
                        break;
                }
            }
        }
    }
}
