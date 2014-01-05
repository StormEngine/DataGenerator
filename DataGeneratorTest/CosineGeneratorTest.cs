using System;
using System.Diagnostics;

using DataGenerator;

/// <summary>
/// Namespace for testing the functionality of IDataGenerator and progeny.
/// Results are loged via System.Console.Out, log files and SQL Express DB
/// </summary>
namespace DataGeneratorTest
{
    /// <summary>
    /// Test CosineGenerator functionality.
    /// </summary>
    class CosineGeneratorTest
    {
        /// <summary>
        /// Runs all tests.
        /// </summary>
        /// <param name="ts">The ts.</param>
        public static void RunAllTests(TimeSpan ts)
        {
            DeclarationAndInstantiationTest();
            ToStringTest();
            SeedTest();
            NextTest();
            AngleRotationTest();
            CosineDataGeneratorTest1();
            TimeSpanTest(ts);
        } // END public static void RunAllTests()

        /// <summary>
        /// Test basic declaration and instantion behavior with respect to CosineGenerator
        /// and log results via Debug listners System.Console.Out and log file
        /// </summary>        
        public static void DeclarationAndInstantiationTest()
        {

            DebugHelper.WriteStart("CosineGeneratorTest::DeclarationAndInstantiationTest()");

            Debug.WriteLine("TEST DECLARATIONS");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("TEST: CosineGenerator cosGen;");            

            CosineGenerator cosGen;            

            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("TEST INSTANTIATION");
            Debug.WriteLine(Environment.NewLine);

            try
            {
                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator();");
                cosGen = new CosineGenerator();

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(double.MinValue, 20.0D);");
                cosGen = new CosineGenerator(double.MinValue, 20.0D);

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(double.MaxValue, 20.0D);");
                cosGen = new CosineGenerator(double.MaxValue, 20.0D);

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(double.MinValue, -20.0D);");
                cosGen = new CosineGenerator(double.MinValue, -20.0D);
                
                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(double.MaxValue, -20.0D);");
                cosGen = new CosineGenerator(double.MaxValue, -20.0D);

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(20.0D, double.MinValue);");
                cosGen = new CosineGenerator(20.0D, double.MinValue);

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(20.0D, double.MaxValue);");
                cosGen = new CosineGenerator(20.0D, double.MaxValue);

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(-20.0D, double.MinValue);");
                cosGen = new CosineGenerator(-20.0D, double.MinValue);

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(-20.0D, double.MaxValue);");
                cosGen = new CosineGenerator(-20.0D, double.MaxValue);

            }  // END try

            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            try
            {

                Debug.WriteLine(Environment.NewLine);
                DebugHelper.TimeLine();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(CosineGenerator.DefaultStartingAngle, 0.0D);");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen = new CosineGenerator(CosineGenerator.DefaultStartingAngle, 0.0D);
            }

            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            try
            {
                DebugHelper.TimeLine();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(Double.MaxValue + 1, 0.0D);");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen = new CosineGenerator(Double.MaxValue + 1, 0.0D);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            try
            {
                DebugHelper.TimeLine();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(Double.MinValue - 1, 0.0D);");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen = new CosineGenerator(Double.MinValue - 1, 0.0D);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            try
            {
                DebugHelper.TimeLine();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(Double.NaN, 0.0D);");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen = new CosineGenerator(Double.NaN, 0.0D);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            DebugHelper.WriteEnd("CosineGeneratorTest::DeclarationAndInstantiationTest()");

        } // END public static void InstantiationTest()

        /// <summary>
        /// To the ToString() method and log results via Debug listeners System.Console.Out and log file.
        /// </summary>        
        public static void ToStringTest()
        {
            DebugHelper.WriteStart("CosineGeneratorTest::ToStringTest()");

            CosineGenerator cosGen;
            Random r = new Random(System.DateTime.Now.Millisecond);

            cosGen = new CosineGenerator();
            DebugHelper.Time();
            Debug.WriteLine("TEST: Default Constructor ToString() ==> {0}", cosGen.ToString(), null);

            cosGen = new CosineGenerator(r.NextDouble(), r.NextDouble());
            DebugHelper.Time();
            Debug.WriteLine("TEST: Non-default random values ToString() ==> {0}", cosGen.ToString(), null);

            cosGen = new CosineGenerator(double.MinValue, 20.0D);
            DebugHelper.Time();
            Debug.WriteLine("TEST: Non-default random values ToString() ==> {0}", cosGen.ToString(), null);

            cosGen = new CosineGenerator(double.MaxValue, -20.0D);
            DebugHelper.Time();
            Debug.WriteLine("TEST: Non-default random values:  ToString() ==> {0}", cosGen.ToString(), null);

            cosGen = new CosineGenerator(20.0D, double.MinValue);
            DebugHelper.Time();
            Debug.WriteLine("TEST: Non-default random values ToString() ==> {0}", cosGen.ToString(), null);

            cosGen = new CosineGenerator(-20.0D, double.MaxValue);
            DebugHelper.Time();
            Debug.WriteLine("TEST: Non-default random values:  ToString() ==> {0}", cosGen.ToString(), null);
            Debug.WriteLine(Environment.NewLine);

            int maxIteration = 10;
            Debug.WriteLine("Instantiate CosineGenerator {0} times from [0, {1}] using random values.", maxIteration, maxIteration-1, null);
            Debug.WriteLine(Environment.NewLine);
            
            for (int i = 0; i < maxIteration; i++)
            {
                try
                {
                    cosGen = new CosineGenerator(r.NextDouble(), r.NextDouble());
                    DebugHelper.Time();
                    Debug.WriteLine("{0}.  TEST: Non-default random values ToString() ==> {1}", i, cosGen.ToString(), null);
                    Debug.WriteLine(Environment.NewLine);
                } // END try

                catch (ArgumentOutOfRangeException ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Debug.WriteLine(Environment.NewLine);
                } // END Catch

                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Debug.WriteLine(Environment.NewLine);
                } // END catch

            } // END for (int i = 0; i < 10; i++)

            DebugHelper.WriteEnd("CosineGeneratorTest::ToStringTest()");

        } // END public static void ToStringTest()

        /// <summary>
        /// Test the Seed() Method and log results via Debug listeners System.Console.Out and log file.
        /// </summary>        
        public static void SeedTest()
        {
            DebugHelper.WriteStart("CosineGeneratorTest::SeedTest()");

            CosineGenerator cosGen;
            Random r = new Random();
            double oldSeed, newSeed;
            int seedTestMax = 10;

            cosGen = new CosineGenerator();
            DebugHelper.Time();
            Debug.WriteLine("TEST: DataGenerator with Default seeding ==> {0}", cosGen.ToString(), null);
            Debug.WriteLine(Environment.NewLine);


            Debug.WriteLine("PERFORM {0} RANDOM SEED TESTS", seedTestMax, null);
            Debug.WriteLine(Environment.NewLine);

            for (int i = 0; i < seedTestMax; i++)
            {
                try
                {
                    newSeed = r.NextDouble();
                    oldSeed = cosGen.Seed(r.NextDouble());

                    DebugHelper.TimeLine();
                    Debug.WriteLine("TEST: {0}. DataGenrator change seed", i, null);
                    Debug.WriteLine("oldSeed = {0}", oldSeed, null);
                    Debug.WriteLine("newSeed = {0}", newSeed, null);
                    Debug.WriteLine(Environment.NewLine);
                }  // END try

                catch (ArgumentOutOfRangeException e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(Environment.NewLine);
                } // END catch (ArgumentOutOfRangeException e)

                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(Environment.NewLine);
                } // END catch (Exception e)

            }  // END for (int i = 0; i <= 9; i++)

            try
            {
                DebugHelper.Time();
                Debug.WriteLine("TEST: dg.Seed(Double.Nan)");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen.Seed(Double.NaN);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }

            try
            {
                DebugHelper.Time();
                Debug.WriteLine("TEST: dg.Seed(Double.PositiveInfinity)");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen.Seed(Double.PositiveInfinity);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }

            try
            {
                DebugHelper.Time();
                Debug.WriteLine("TEST: dg.Seed(Double.NegativeInfinity)");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen.Seed(Double.NegativeInfinity);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }

            DebugHelper.WriteEnd("CosineGeneratorTest::SeedTest()");

        }  // END public static void SeedTest()

        /// <summary>
        /// Test the Next() method and log results via System.Console.Out and log file.
        /// </summary>        
        public static void NextTest()
        {
            DebugHelper.WriteStart("CosineGeneratorTest::NextTest()");

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST DEFAULT CONSTRUCTOR CASE: CosineGenerator cosGen = new CosineGenerator();");
            CosineGenerator cosGen = new CosineGenerator();
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE STEP: CosineGenerator cosGen = new CosineGenerator(0.0D, 1.0D);");
            cosGen = new CosineGenerator(0.0D, 1.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE STEP: CosineGenerator cosGen = new CosineGenerator(0.0D,-1.0D);");
            cosGen = new CosineGenerator(0.0D, -1.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE AND POSITIVE STEP: CosineGenerator cosGen = new CosineGenerator(1.0D, 1.0D);");
            cosGen = new CosineGenerator(1.0D, 1.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE AND NEGATIVE STEP: CosineGenerator cosGen = new CosineGenerator(1.0D, -1.0D);");
            cosGen = new CosineGenerator(1.0D, -1.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE AND POSITIVE STEP: CosineGenerator cosGen = new CosineGenerator(-1.0D, 1.0D);");
            cosGen = new CosineGenerator(-1.0D, 1.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE AND NEGATIVE STEP: CosineGenerator cosGen = new CosineGenerator(-1.0D, -1.0D);");
            cosGen = new CosineGenerator(-1.0D, -1.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE STEP > 1: CosineGenerator cosGen = new CosineGenerator(0.0D, 30.0D);");
            cosGen = new CosineGenerator(0.0D, 30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE STEP < -1: CosineGenerator cosGen = new CosineGenerator(0.0D, -30.0D);");
            cosGen = new CosineGenerator(0.0D, -30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND POSITIVE STEP > 1 : CosineGenerator cosGen = new CosineGenerator(30.0D, 30.0D);");
            cosGen = new CosineGenerator(30.0D, 30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND NEGATIVE STEP < -1 : CosineGenerator cosGen = new CosineGenerator(30.0D, -30.0D);");
            cosGen = new CosineGenerator(30.0D, -30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE < -1 AND POSITIVE STEP > 1 : CosineGenerator cosGen = new CosineGenerator(-30.0D, 30.0D);");
            cosGen = new CosineGenerator(-30.0D, 30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE < -1 AND NEGATIVE STEP < -1 : CosineGenerator cosGen = new CosineGenerator(-30.0D, -30.0D);");
            cosGen = new CosineGenerator(-30.0D, -30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND POSITIVE STEP > POSITIVE ANGLE : CosineGenerator cosGen = new CosineGenerator(30.0D, 60.0D);");
            cosGen = new CosineGenerator(30.0D, 60.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND POSITIVE STEP < POSITIVE ANGLE : CosineGenerator cosGen = new CosineGenerator(60.0D, 30.0D);");
            cosGen = new CosineGenerator(60.0D, 30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND ABS(NEGATIVE STEP) < POSITIVE ANGLE : CosineGenerator cosGen = new CosineGenerator(60.0D, -30.0D);");
            cosGen = new CosineGenerator(60.0D, -30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND ABS(NEGATIVE STEP) > POSITIVE ANGLE : CosineGenerator cosGen = new CosineGenerator(30.0D, -60.0D);");
            cosGen = new CosineGenerator(30.0D, -60.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE < -1 AND POSITIVE STEP > ABS(NEGATIVE ANGLE) : CosineGenerator cosGen = new CosineGenerator(-30.0D, 60.0D);");
            cosGen = new CosineGenerator(-30.0D, 60.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE < -1 AND POSITIVE STEP < ABS(NEGATIVE ANGLE) : CosineGenerator cosGen = new CosineGenerator(-60.0D, 30.0D);");
            cosGen = new CosineGenerator(-60.0D, 30.0D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST MAX STARTING ANGLE AND NEGATIVE STEP:  cosGen = new CosineGenerator(double.MaxValue, -1000000.0D);");
            cosGen = new CosineGenerator(double.MaxValue, -(double.MaxValue / 100000000.0D));
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST MIN STARTING ANGLE AND POSITIVE STEP:  cosGen = new CosineGenerator(double.MaxValue, -1000000.0D);");
            cosGen = new CosineGenerator(double.MinValue, (double.MinValue / -100000000.0D));
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST MAX OVERFLOW CASE WITH POSITIVE ANGLE CHANGE:  cosGen = new CosineGenerator(double.MaxValue, 1000000.0D);");
            cosGen = new CosineGenerator(double.MaxValue, double.MaxValue / 100D);
            NextTestHelper(cosGen, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST MAX OVERFLOW CASE WITH NEGATIVE ANGLE CHANGE:  cosGen = new CosineGenerator(double.MaxValue, -100000.0D);");
            cosGen = new CosineGenerator(double.MinValue, double.MinValue / 100D);
            NextTestHelper(cosGen, 10);

            DebugHelper.WriteEnd("CosineGeneratorTest::NextTest()");

        } // END public static void NextTest()

        /// <summary>
        /// Test the AngleRotation property and log results via System.Console.Out and log file.
        /// </summary>        
        public static void AngleRotationTest()
        {
            DebugHelper.WriteStart("CosineGeneratorTest::AngleRotationTest()");

            CosineGenerator cosGen = new CosineGenerator();            

            // Test default AngleRotation
            DebugHelper.Time();
            Debug.WriteLine("TEST: cosGen.AngleRotation");
            Debug.WriteLine("Angle Rotation = {0}", cosGen.AngleRotation);
            
            try
            {
                // Test assinging 0.0D to AngleRotation; should through ArgumentOutOfRangeException
                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen.AngleRotation = 0.0D");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen.AngleRotation = 0.0D;
            } // END try

            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            } // END catch (ArgumentOutOfRangeException e)

            try
            {
                // Test assinging PositiveInfinity to AngleRotation; should through ArgumentOutOfRangeException
                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen.AngleRotation = Double.PositiveInfinity");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen.AngleRotation = Double.PositiveInfinity;
            } // END try
            
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            } // END catch (ArgumentOutOfRangeException e)

            try
            {
                // Test assinging NegativeInfinity to AngleRotation; should through ArgumentOutOfRangeException
                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen.AngleRotation = Double.NegativeInfinity");           
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen.AngleRotation = Double.NegativeInfinity;
            } // try
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            } // END catch (ArgumentOutOfRangeException e)
            
            try
            {
                // Test assinging NaN to AngleRotation; should through ArgumentOutOfRangeException
                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen.AngleRotation = Double.NaN");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                cosGen.AngleRotation = Double.NaN;

            } // try
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            } // END catch (ArgumentOutOfRangeException e)
            
            // attempt random AngleRotation assignements
            Random r = new Random(System.DateTime.Now.Millisecond);
            double r1;
            int maxRotationAssignments = 10;
            
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("Attemp {0} random AngleRotation assignement from [0,{1}]", maxRotationAssignments, maxRotationAssignments - 1, null);
            
            for (int i = 0; i < maxRotationAssignments; i++)
            {
                r1 = r.NextDouble();
                DebugHelper.Time();
                try
                {
                    cosGen.AngleRotation = r1;
                    Debug.WriteLine("TEST: {0}.  Random={1}  cosGenAngleRotation={2}", i,  r1, cosGen.AngleRotation, null);                    
                } // END try
                
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(Environment.NewLine);
                } // END catch (ArgumentOutOfRangeException e)            
            
            } // END for (int i = 0; i < maxRotationAssignments; i++)

            DebugHelper.WriteEnd("CosineGeneratorTest::AngleRotationTest()");

        } // END public static void AngleRotationTest()

        /// <summary>
        /// Test the CurrentAngle property and log results via System.Console.Out and log file.
        /// </summary>        
        public static void CurrentAngleTest()
        {

            DebugHelper.WriteStart("CosineGeneratorTest::CurrentAngleTest()");

            CosineGenerator cosGen = new CosineGenerator();

            int maxCurrentAngleIterations = 10;

            Debug.WriteLine("Attemp {0} random AngleRotation assignement from [0,{1}]", maxCurrentAngleIterations, maxCurrentAngleIterations - 1, null);

            for (int i = 0; i < maxCurrentAngleIterations; i++)
            {
                DebugHelper.Time();
                cosGen.Seed(Convert.ToDouble(i));
                Debug.WriteLine("TEST: {0}.  Current Angle: cosGen.CurrentAngle={1}", i, cosGen.CurrentAngle);                                
            } // END for (int i = 0; i < maxCurrentAngleIterations; i++)

            DebugHelper.WriteEnd("CosineGeneratorTest::CurrentAngleTest()");

        } // END public static void CurrentAngleTest()

        /// <summary>
        /// Iterate through cosines of angles
        /// </summary>
        public static void CosineDataGeneratorTest1()
        {            
            DebugHelper.WriteStart("CosineGeneratorTest::CosineDataGeneratorTest1()");
            
            CosineGenerator cosGen = new CosineGenerator();
            int maxIterations = 10;

            Debug.WriteLine("Attemp {0} iterations from [0,{1}]", maxIterations, maxIterations - 1, null);

            for (int i = 0; i < maxIterations; i++)
            {
                DebugHelper.Time();
                Debug.WriteLine("{0}. cosGen.ToString()={1}", i, cosGen.ToString(), null);
                DebugHelper.Time();
                Debug.WriteLine("{0}. Cosine of current angle={1}", i, cosGen.Next(), null);
                Debug.WriteLine(Environment.NewLine);
            } // END for (int i = 0; i < maxIterations; i++)
            
            DebugHelper.WriteEnd("CosineGeneratorTest::CosineDataGeneratorTest1()");
        }  // END public static void CosineDataGeneratorTest1()

        /// <summary>
        /// Generate cosines of angle at TimeSpan intervals
        /// </summary>
        /// <param name="ts">TimeSpan ts holding the interval for cosine generation</param>
        public static void TimeSpanTest(TimeSpan ts)
        {
            if (ts == null)
                ts = new TimeSpan(0, 0, 1);

            DebugHelper.WriteStart("CosineGeneratorTest::TimeSpanTest()");
            CosineGenerator cosGen = new CosineGenerator();

            int maxIterations = 100;

            Debug.WriteLine("Attemp {0} iterations from [0,{1}]", maxIterations+1, maxIterations, null);
            Debug.WriteLine("Cosines of angles will be generated at following intervals: {0}", ts.ToString(), null);

            for (int i = 0; i <= maxIterations; i++)
            {
                // reverse angle rotation
                if (cosGen.CurrentAngle == maxIterations / 2)
                    cosGen.AngleRotation *= -1;

                // wait to generate value of next cosine of current angle
                System.Threading.Thread.Sleep(ts);
                
                DebugHelper.Time();
                Debug.WriteLine("{0}. Current Angle={1}  Angle Rotation={2}  Cosine of Current Angle={3}", i, cosGen.CurrentAngle, cosGen.AngleRotation, cosGen.Next(), null);

                // Highlight the line where angle rotation is reversed by:
                // adding 1 blank line before the reversal 
                if (i == (maxIterations / 2) - 1)
                    Debug.WriteLine(Environment.NewLine);
                
                // and 1 blank line after the reversal
                if (i == (maxIterations / 2))
                    Debug.WriteLine(Environment.NewLine);

            } // END for (int i = 0; i < maxIterations; i++)

            DebugHelper.WriteEnd("CosineGeneratorTest::TimeSpanTest()");

        } // END public static void DataGeneratorTimeSpanTest(TimeSpan ts)

        #region PRIVATE HELPER METHODS

        /// <summary>
        /// Helper method for NextTest.
        /// Recieves an IDataGenerator<double> object and the number of iteration to perform.
        /// Generates Cosines based on the number of iterations;
        /// </summary>
        /// <param name="dg">an IDATAGenerator dg.</param>
        /// <param name="iterationCount">An int iterationCount with default value of 10.</param>
        private static void NextTestHelper(IDataGenerator<double> dg, int iterationCount=10)
        {
            
            if (iterationCount < 0)
                iterationCount = 10;            

            CosineGenerator cosGen;
            if (dg is CosineGenerator)
                cosGen = dg as CosineGenerator;
            else cosGen = null;

            if (cosGen != null)
            {
                Debug.WriteLine(Environment.NewLine);
                DebugHelper.Time();
                Debug.WriteLine(cosGen.ToString());
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine("About to perform {0} iterations: [0, {1}]", iterationCount, iterationCount - 1, null);
                Debug.WriteLine(Environment.NewLine);
                
                for (int i = 0; i < iterationCount; i++)
                {
                    DebugHelper.Time();
                    Debug.WriteLine("{0}. Cosine of next angle={1}", i, cosGen.Next(), null);
                } // END for (int i = 0; i < 10; i++)

            } // END if (cosGen != null)

            Debug.WriteLine(Environment.NewLine);

        } // END private static void NextTestHelper(, IDataGenerator<double> dg, int iterationCount)

        #endregion
    } // END class CosineGeneratorTest

} // END namespace DataGeneratorTest
