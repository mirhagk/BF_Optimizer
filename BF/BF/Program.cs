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
        static bool RunTestFile(string fileName, string input, string expectedOutput, bool debug=false)
        {
            string code = File.ReadAllText(String.Format("tests\\{0}.bf", fileName));
            return RunTest(fileName, code, input, expectedOutput,debug);
        }
        static bool RunTest(string testName, string code, string input, string expectedOutput, bool debug =false)
        {
            var output = new StringWriter();
            Interpreter interpret = new Interpreter(output: output);
            if (input!="")
                interpret.Input=new StringReader(input);
            Console.WriteLine("Running Test Case {0}", testName);
            interpret.Run(code,debug);
            bool passed = expectedOutput == output.ToString();
            Console.WriteLine("\tExpected Output: {0}\n\tActual Output: {1}\n\t", expectedOutput, output.ToString());
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine("Passed: {0}", passed);
            Console.ForegroundColor = previousColor;
            return passed;
        }
        static void TestCases()
        {
            bool result = true;
            result &= RunTest("Loop_Test", "++[>,+.<-]", "ab", "bc");
            result &= RunTest("Dual_Loop_Test", "++[>,>+++++[<+.>-]<<-]", "ab", "bcdefcdefg");
            result &= RunTestFile("hello_world", "", "Hello World!\n");
            result &= RunTestFile("HelloWorld_Variant", "", "Hello World!\n");

            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine("===================");
            if (result)
                Console.WriteLine("All test cases passed\n\n\n");
            else
                Console.WriteLine("One or more test cases failed\n\n\n");
            Console.ForegroundColor = previousColor;
        }
        static void Main(string[] args)
        {
            TestCases();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
