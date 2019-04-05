using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace laboratorium_7
{
    public static class ClassExtension
    {
        public static void PrintThisNicely(this DirectoryInfo aSampleDir)
        {
            Console.WriteLine($"This is a directory named: ${aSampleDir.FullName}");
        }
    }


    public class ASampleClass
    {
        public void SampleMethod()
        {
            var dir = new DirectoryInfo("C:/Users/");
            dir.PrintThisNicely();
        }
    }
}
