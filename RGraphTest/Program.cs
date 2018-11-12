using System;
using RDotNet;
using System.Collections.Generic;
using System.Linq;

namespace RGraphTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            REngine engine = REngine.GetInstance();
            string fileName;

            //init the R engine            
            REngine.SetEnvironmentVariables();
            engine = REngine.GetInstance();
            engine.Initialize();

            //prepare data
            List<int> size = new List<int>() { 29, 33, 51, 110, 357, 45, 338, 543, 132, 70, 103, 301, 146, 10, 56, 243, 238 };
            List<int> population = new List<int>() { 3162, 11142, 3834, 7305, 81890, 1339, 5414, 65697, 11280, 4589, 320, 60918, 480, 1806, 4267, 63228, 21327 };

            fileName = "C:\\Users\\ofunke\\Desktop\\myplot.png";

            //calculate
            IntegerVector sizeVector = engine.CreateIntegerVector(size);
            engine.SetSymbol("size", sizeVector);

            IntegerVector populationVector = engine.CreateIntegerVector(population);
            engine.SetSymbol("population", populationVector);

            CharacterVector fileNameVector = engine.CreateCharacterVector(new[] { fileName });
            engine.SetSymbol("fileName", fileNameVector);

            engine.Evaluate("reg <- lm(population~size)");
            engine.Evaluate("png(filename=fileName, width=6, height=6, units='in', res=100)");
            engine.Evaluate("plot(population~size)");
            engine.Evaluate("abline(reg)");
            engine.Evaluate("dev.off()");

            //clean up
            engine.Dispose();

            //output
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            */
            StartupParameter rinit = new StartupParameter();
            rinit.Quiet = true;
            rinit.RHome = "C:/Program Files/R/R-3.4.3";
            rinit.Interactive = true;

            REngine.SetEnvironmentVariables();
            // There are several options to initialize the engine, but by default the following suffice:
            REngine engine = REngine.GetInstance();

            // .NET Framework array to R vector.
            NumericVector group1 = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99 });
            engine.SetSymbol("group1", group1);
            // Direct parsing from R script.
            NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98)").AsNumeric();

            // Test difference of mean and get the P-value.
            GenericVector testResult = engine.Evaluate("t.test(group1, group2)").AsList();
            double p = testResult["p.value"].AsNumeric().First();

            Console.WriteLine("Group1: [{0}]", string.Join(", ", group1));
            Console.WriteLine("Group2: [{0}]", string.Join(", ", group2));
            Console.WriteLine("P-value = {0:0.000}", p);

            // you should always dispose of the REngine properly.
            // After disposing of the engine, you cannot reinitialize nor reuse it
            engine.Dispose();

        }
    }
}
