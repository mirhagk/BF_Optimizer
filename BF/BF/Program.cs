using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF
{
    class Program
    {
        static void RunTest(string testName)
        {
            string code = File.ReadAllText(String.Format("tests\\{0}.bf",testName));
            Interpreter interpret = new Interpreter(code);
            interpret.Run();
        }
        static void TestCases()
        {
            RunTest("hello_world");
        }
        static void Main(string[] args)
        {
            TestCases();
        }
    }
}
