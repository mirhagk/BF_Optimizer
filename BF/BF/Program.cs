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
        static bool RunTestFile(string fileName, string input, string expectedOutput)
        {
            string code = File.ReadAllText(String.Format("tests\\{0}.bf", fileName));
            return RunTest(fileName, code, input, expectedOutput);
        }
        static bool RunTest(string testName, string code, string input, string expectedOutput)
        {
            var output = new StringWriter();
            Interpreter interpret = new Interpreter(output: output);
            if (input!="")
                interpret.Input=new StringReader(input);
            Console.WriteLine("Running Test Case {0}", testName);
            interpret.Run(code);
            bool passed = expectedOutput == output.ToString();
            Console.WriteLine("\tExpected Output: {0}\n\tActual Output: {1}\n\tPassed: {2}", expectedOutput, output.ToString(), passed);
            return passed;
        }
        static void TestCases()
        {
            RunTest("Loop_Test", "++[>,+.<-]", "ab", "bc");
            RunTest("Dual_Loop_Test", "++[>,>+++++[<+.>-]<<-]", "ab", "bcdefcdefg");
            RunTestFile("hello_world","","Hello World!\n");
        }
        static void Main(string[] args)
        {
            TestCases();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
