using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BF
{
    class Program
    {
        static string ReplaceEscapedCharacters(string input)
        {
            return input.Replace("\\n", "\n");
        }
        static bool RunTestFile(string fileName, bool debug=false)
        {
            string code = File.ReadAllText(fileName);
            string expectedOutput = ReplaceEscapedCharacters(Regex.Match(code, @"\[Expected Output:([^\]]*)\]").Groups[1].Value);
            string input = ReplaceEscapedCharacters(Regex.Match(code, @"\[Test Input:([^\]]*)\]").Groups[1].Value);
            string name = Regex.Match(code, @"\[Name:([^\]]*)\]").Groups[1].Value;
            if (string.IsNullOrEmpty(name))
                name = fileName;
            return RunTest(name, code, input, expectedOutput,debug);
        }
        static bool RunTest(string testName, string code, string input, string expectedOutput, bool debug =false)
        {
            var output = new StringWriter();
            Interpreter interpret = new Interpreter(output: output);
            if (input!="")
                interpret.Input=new StringReader(input);
            Console.WriteLine("Running Test Case: {0}", testName);
            interpret.Run(code,debug);
            bool passed = expectedOutput == output.ToString();
            Console.WriteLine("\tExpected Output: {0}\n\tActual Output: {1}", expectedOutput, output.ToString());
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine("\tPassed: {0}", passed);
            Console.ForegroundColor = previousColor;
            return passed;
        }
        static void TestCases()
        {
            bool result = true;
            result &= RunTest("Loop_Test", "++[>,+.<-]", "ab", "bc");
            result &= RunTest("Dual_Loop_Test", "++[>,>+++++[<+.>-]<<-]", "ab", "bcdefcdefg");
            foreach (var file in Directory.EnumerateFiles("tests"))
            {
                result &= RunTestFile(file);
            }

            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
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
