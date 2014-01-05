using System;
using System.IO;
using DataGenerator;
using System.Diagnostics;

/// <summary>
/// Namespace for testing the functionality of IDataGenerator and progeny.
/// Results are loged via System.Console.Out, log files and SQL Express DB
/// </summary>
namespace DataGeneratorTest
{
    /// <summary>
    /// Tests polymorphic functionality of IDataGenerator and CosineGenerator
    /// </summary>
    class IDataGeneratorPolymorphTest
    {

        /// <summary>
        /// Runs all tests.
        /// </summary>
        public static void RunAllTests()
        {
            DeclarationAndInstantiationTest();
            ToStringTest();
            SeedTest();
            NextTest();
            AngleRotationTest();
            CurrentAngleTest();
            DataGeneratorTest1();
        } // END public static void RunAllTests(Log log)
        
        /// <summary>
        /// Test basic declaration and instantion behavior with respect to IDataGenerator
        /// and log results via Debug listners System.Console.Out and log file
        /// </summary>        
        public static void DeclarationAndInstantiationTest()
        {   
            DebugHelper.WriteStart("IDataGeneratorPolymorphTest::DeclarationAndInstantiationTest()");            

            Debug.WriteLine("TEST DECLARATIONS");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("TEST: CosineGenerator cosGen;");
            Debug.WriteLine("TEST: IDataGenerator<double> dg;");

            CosineGenerator cosGen; 
            IDataGenerator<double> dg;

            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("TEST INSTANTIATION");
            Debug.WriteLine(Environment.NewLine);

            try
            {
                DebugHelper.Time();                    
                Debug.WriteLine("TEST: cosGen = new CosineGenerator();");
                cosGen = new CosineGenerator();

                DebugHelper.Time();
                Debug.WriteLine("TEST: dg = cosGen;");
                dg = cosGen;

                DebugHelper.Time();
                Debug.WriteLine("TEST: dg = new CosineGenerator();");
                dg = new CosineGenerator();

                DebugHelper.Time();
                Debug.WriteLine("TEST: dg = new CosineGenerator(double.MinValue, 20.0D);");
                dg = new CosineGenerator(double.MinValue, 20.0D);

                DebugHelper.Time();
                Debug.WriteLine("TEST: dg = new CosineGenerator(double.MaxValue, -20.0D);");
                dg = new CosineGenerator(double.MaxValue, -20.0D);

                DebugHelper.Time();
                Debug.WriteLine("TEST: dg = new CosineGenerator(20.0D, double.MinValue);");
                dg = new CosineGenerator(20.0D, double.MinValue);

                DebugHelper.Time();
                Debug.WriteLine("TEST: dg = new CosineGenerator(20.0D, double.MaxValue);");
                dg = new CosineGenerator(20.0D, double.MaxValue);

                DebugHelper.Time();
                Debug.WriteLine("TEST: dg = new CosineGenerator(-20.0D, double.MinValue);");
                dg = new CosineGenerator(-20.0D, double.MinValue);

                DebugHelper.Time();
                Debug.WriteLine("TEST: dg = new CosineGenerator(-20.0D, double.MaxValue);");
                dg = new CosineGenerator(-20.0D, double.MaxValue);

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(double.MinValue, 20.0D);");
                cosGen = new CosineGenerator(double.MinValue, 20.0D);

                DebugHelper.Time();
                Debug.WriteLine("TEST: cosGen = new CosineGenerator(double.MaxValue, -20.0D);");
                cosGen = new CosineGenerator(double.MaxValue, -20.0D);             

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
                Debug.WriteLine("TEST: dg = new CosineGenerator(CosineGenerator.DefaultStartingAngle, 0.0D);");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                dg = new CosineGenerator(CosineGenerator.DefaultStartingAngle, 0.0D);
            }

            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            try
            {
                DebugHelper.TimeLine();
                Debug.WriteLine("TEST: dg = new CosineGenerator(Double.MaxValue + 1, 0.0D);");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                dg = new CosineGenerator(Double.MaxValue + 1, 0.0D);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            try
            {
                DebugHelper.TimeLine();
                Debug.WriteLine("TEST: dg = new CosineGenerator(Double.MinValue - 1, 0.0D);");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                dg = new CosineGenerator(Double.MinValue - 1, 0.0D);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            try
            {
                DebugHelper.TimeLine();
                Debug.WriteLine("TEST: dg = new CosineGenerator(Double.NaN, 0.0D);");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                dg = new CosineGenerator(Double.NaN, 0.0D);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }  // END catch (ArgumentOutOfRangeException e)

            DebugHelper.WriteEnd("IDataGeneratorPolymorphTest::DeclarationAndInstantiationTest()");

        } // END public static void InstantiationTest(Log log)

        /// <summary>
        /// To the ToString() method and log results via Debug listeners System.Console.Out and log file.
        /// </summary>        
        public static void ToStringTest()
        {
            DebugHelper.WriteStart("IDataGeneratorPolymorphTest::ToStringTest()");
            
            IDataGenerator<double> dg;
            Random r = new Random(System.DateTime.Now.Millisecond);

            try
            {
                dg = new CosineGenerator();
                DebugHelper.Time();
                Debug.WriteLine("TEST: Default Constructor ToString() ==> {0}", dg.ToString(), null);                

                dg = new CosineGenerator(double.MinValue, 20.0D);
                DebugHelper.Time();
                Debug.WriteLine("TEST: Non-default values ({0}, {1}) ToString() ==> {2}",double.MinValue, 20.0D, dg.ToString(), null);

                dg = new CosineGenerator(double.MaxValue, -20.0D);
                DebugHelper.Time();
                Debug.WriteLine("TEST: Non-default values ({0}, {1}) ToString() ==> {2}", double.MaxValue, 20.0D, dg.ToString(), null);

                dg = new CosineGenerator(20, double.MinValue);
                DebugHelper.Time();
                Debug.WriteLine("TEST: Non-default values ({0}, {1}) ToString() ==> {2}", 20.0D, double.MinValue, dg.ToString(), null);

                dg = new CosineGenerator(-20, double.MaxValue);
                DebugHelper.Time();
                Debug.WriteLine("TEST: Non-default values ({0}, {1}) ToString() ==> {2}", -20.0D, double.MaxValue, dg.ToString(), null);

                dg = new CosineGenerator(-20, double.MaxValue);
                DebugHelper.Time();
                Debug.WriteLine("TEST: Non-default values ({0}, {1}) ToString() ==> {2}", -20.0D, double.MaxValue, dg.ToString(), null);

                dg = new CosineGenerator(double.MinValue, double.MaxValue);
                DebugHelper.Time();
                Debug.WriteLine("TEST: Non-default values ({0}, {1}) ToString() ==> {2}", double.MinValue, double.MaxValue, dg.ToString(), null);
                Debug.WriteLine(Environment.NewLine);

                Debug.WriteLine("Instantiate CosineGenerator using random values.");
                for (int i = 0; i < 10; i++)
                {
                    dg = new CosineGenerator(r.NextDouble(), r.NextDouble());
                    DebugHelper.Time();
                    Debug.WriteLine("{0}.  TEST: Non-default random values ToString() ==> {1}", i, dg.ToString(), null);
                    Debug.WriteLine(Environment.NewLine);
                } // END for (int i = 0; i < 10; i++)

            } // END try
            catch (ArgumentOutOfRangeException ar)
            {
                Debug.WriteLine(ar.ToString());
            } // END catch (ArgumentOutOfRangeException ar)

            DebugHelper.WriteEnd("IDataGeneratorPolymorphTest::ToStringTest()");
        } // END public static void ToStringTest()

        /// <summary>
        /// Test the Seed() Method and log results via Debug listeners System.Console.Out and log file.
        /// </summary>        
        public static void SeedTest()
        {
            DebugHelper.WriteStart("IDataGeneratorPolymorphTest::SeedTest()");

            IDataGenerator<double> dg;
            Random r = new Random(System.DateTime.Now.Millisecond << 1);
            double oldSeed, newSeed;
            int seedTestMax = 10;

            dg = new CosineGenerator();
            DebugHelper.Time();
            Debug.WriteLine("TEST: DataGenerator with Default seeding ==> {0}", dg.ToString(), null);
            Debug.WriteLine(Environment.NewLine);          
            

            Debug.WriteLine("PERFORM {0} RANDOM SEED TESTS", seedTestMax, null);
            Debug.WriteLine(Environment.NewLine);

            for (int i = 0; i < seedTestMax; i++)
            {
                try
                {
                    newSeed = r.NextDouble();
                    oldSeed = dg.Seed(r.NextDouble());

                    DebugHelper.TimeLine();
                    Debug.WriteLine("TEST: {0} DataGenerator change seed", i, null);
                    Debug.WriteLine("oldSeed = {0} ==> newSeed = {1}", oldSeed, newSeed, null);                    
                    Debug.WriteLine(Environment.NewLine);
                }  // END try

                catch (ArgumentOutOfRangeException e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(Environment.NewLine);
                } // END catch (ArgumentOutOfRangeException e)

            }  // END for (int i = 0; i <= 9; i++)

            try
            {
                DebugHelper.Time();
                Debug.WriteLine("TEST: dg.Seed(Double.Nan)");
                Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                Debug.WriteLine(Environment.NewLine);
                dg.Seed(Double.NaN);
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
                dg.Seed(Double.PositiveInfinity);
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
                dg.Seed(Double.NegativeInfinity);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(Environment.NewLine);
            }

            DebugHelper.WriteEnd("IDataGeneratorPolymorphTest::SeedTest()");
        } // END public static void SeedTest()

        /// <summary>
        /// Test the Next() method and log results via System.Console.Out and log file.
        /// </summary>        
        public static void NextTest()
        {
            DebugHelper.WriteStart("IDataGeneratorPolymorphTest::NextTest()");

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST DEFAULT CONSTRUCTOR CASE: IDataGenerator<double> dg = new CosineGenerator();");
            IDataGenerator<double> dg = new CosineGenerator();
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE STEP: IDataGenerator<double> dg = new CosineGenerator(0.0D, 1.0D);");
            dg = new CosineGenerator(0.0D, 1.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE STEP: IDataGenerator<double> dg = new CosineGenerator(0.0D,-1.0D);");
            dg = new CosineGenerator(0.0D,-1.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE AND POSITIVE STEP: IDataGenerator<double> dg = new CosineGenerator(1.0D, 1.0D);");
            dg = new CosineGenerator(1.0D, 1.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE AND NEGATIVE STEP: IDataGenerator<double> dg = new CosineGenerator(1.0D, -1.0D);");
            dg = new CosineGenerator(1.0D, -1.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE AND POSITIVE STEP: IDataGenerator<double> dg = new CosineGenerator(-1.0D, 1.0D);");
            dg = new CosineGenerator(-1.0D, 1.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE AND NEGATIVE STEP: IDataGenerator<double> dg = new CosineGenerator(-1.0D, -1.0D);");
            dg = new CosineGenerator(-1.0D, -1.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE STEP > 1: IDataGenerator<double> dg = new CosineGenerator(0.0D, 30.0D);");
            dg = new CosineGenerator(0.0D, 30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE STEP < -1: IDataGenerator<double> dg = new CosineGenerator(0.0D, -30.0D);");
            dg = new CosineGenerator(0.0D, -30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND POSITIVE STEP > 1 : IDataGenerator<double> dg = new CosineGenerator(30.0D, 30.0D);");
            dg = new CosineGenerator(30.0D, 30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND NEGATIVE STEP < -1 : IDataGenerator<double> dg = new CosineGenerator(30.0D, -30.0D);");
            dg = new CosineGenerator(30.0D, -30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE < -1 AND POSITIVE STEP > 1 : IDataGenerator<double> dg = new CosineGenerator(-30.0D, 30.0D);");
            dg = new CosineGenerator(-30.0D, 30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE < -1 AND NEGATIVE STEP < -1 : IDataGenerator<double> dg = new CosineGenerator(-30.0D, -30.0D);");
            dg = new CosineGenerator(-30.0D, -30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND POSITIVE STEP > POSITIVE ANGLE : IDataGenerator<double> dg = new CosineGenerator(30.0D, 60.0D);");
            dg = new CosineGenerator(30.0D, 60.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND POSITIVE STEP < POSITIVE ANGLE : IDataGenerator<double> dg = new CosineGenerator(60.0D, 30.0D);");
            dg = new CosineGenerator(60.0D, 30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND ABS(NEGATIVE STEP) < POSITIVE ANGLE : IDataGenerator<double> dg = new CosineGenerator(60.0D, -30.0D);");
            dg = new CosineGenerator(60.0D, -30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH POSITIVE ANGLE > 1 AND ABS(NEGATIVE STEP) > POSITIVE ANGLE : IDataGenerator<double> dg = new CosineGenerator(30.0D, -60.0D);");
            dg = new CosineGenerator(30.0D, -60.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE < -1 AND POSITIVE STEP > ABS(NEGATIVE ANGLE) : IDataGenerator<double> dg = new CosineGenerator(-30.0D, 60.0D);");
            dg = new CosineGenerator(-30.0D, 60.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST CONSTRUCTOR WITH NEGATIVE ANGLE < -1 AND POSITIVE STEP < ABS(NEGATIVE ANGLE) : IDataGenerator<double> dg = new CosineGenerator(-60.0D, 30.0D);");
            dg = new CosineGenerator(-60.0D, 30.0D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST MAX STARTING ANGLE AND NEGATIVE STEP:  dg = new CosineGenerator(double.MaxValue, -1000000.0D);");
            dg = new CosineGenerator(double.MaxValue, -(double.MaxValue/100000000.0D));
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST MIN STARTING ANGLE AND POSITIVE STEP:  dg = new CosineGenerator(double.MaxValue, -1000000.0D);");
            dg = new CosineGenerator(double.MinValue, (double.MinValue /-100000000.0D));
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST MAX OVERFLOW CASE WITH POSITIVE ANGLE CHANGE:  dg = new CosineGenerator(double.MaxValue, 1000000.0D);");
            dg = new CosineGenerator(double.MaxValue, double.MaxValue / 100D);
            NextTestHelper(dg, 10);

            DebugHelper.TimeLine();
            Debug.WriteLine("TEST MAX OVERFLOW CASE WITH NEGATIVE ANGLE CHANGE:  dg = new CosineGenerator(double.MaxValue, -100000.0D);");
            dg = new CosineGenerator(double.MinValue, double.MinValue / 100D);
            NextTestHelper(dg, 10);           

            DebugHelper.WriteEnd("IDataGeneratorPolymorphTest::NextTest()");
            
        } // END public static void NextTest()

        /// <summary>
        /// Test the AngleRotation property and log results via System.Console.Out and log file.
        /// </summary>                      
        public static void AngleRotationTest()
        {
            DebugHelper.WriteStart("IDataGeneratorPolymorphTest::AngleRotationTest()");
            
            IDataGenerator<double> dg = new CosineGenerator();
            CosineGenerator cosGen;

            if (dg is CosineGenerator)
                cosGen = dg as CosineGenerator;
            else cosGen = null;

            if (cosGen != null)
            {
                // Test default case where cosGen.AngleRotation is set by default constructor
                DebugHelper.Time();
                Debug.WriteLine("TEST: Default cosGen.AngleRotation");
                Debug.WriteLine("Angle Step = {0}",cosGen.AngleRotation, null);
                Debug.WriteLine(Environment.NewLine);

                try
                {
                    // Test assigning 0.0D to AngleRotation which should through an ArgumentOutOfRangeException
                    DebugHelper.Time();
                    Debug.WriteLine("TEST: cosGen.AngleRotation=0 .0D");
                    Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                    Debug.WriteLine(Environment.NewLine);
                    cosGen.AngleRotation = 0.0D;

                } // try
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(Environment.NewLine);
                } // END catch (ArgumentOutOfRangeException e)

                try
                {
                    // Test assigning PositiveInfinity to AngleRotation which should through an ArgumentOutOfRangeException
                    DebugHelper.Time();
                    Debug.WriteLine("TEST: cosGen.AngleRotation = Double.PositiveInfinity");
                    Debug.WriteLine("Should generate ArgumentOutOfRangeException");
                    Debug.WriteLine(Environment.NewLine);
                    cosGen.AngleRotation = Double.PositiveInfinity;

                } // try
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(Environment.NewLine);
                } // END catch (ArgumentOutOfRangeException e)

                try
                {
                    // Test assigning NegativeInfinity to AngleRotation which should through an ArgumentOutOfRangeException
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
                    // Test assigning NaN to AngleRotation which should through an ArgumentOutOfRangeException
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
                Random r = new Random();
                double r1;
                int maxTestCount = 10;
                
                Debug.WriteLine("Run {0} Test random AngleRotation assignements: [0,{1}]", maxTestCount, maxTestCount-1, null);
                Debug.WriteLine(Environment.NewLine);

                for (int i = 0; i < maxTestCount; i++)
                {
                    r1 = r.NextDouble();
                    DebugHelper.Time();
                    try
                    {
                        // r1 could be 0.0
                        cosGen.AngleRotation = r1;
                        Debug.WriteLine("TEST: {0}.  Random = {1}  cosGen.AngleRotation={2}", i, r1, cosGen.AngleRotation);
                    } // end try

                    catch (ArgumentOutOfRangeException e)
                    {
                        Debug.WriteLine(e.ToString());
                        Debug.WriteLine(Environment.NewLine);
                    } // END catch (ArgumentOutOfRangeException e)

                } // END for (int i = 0; i < maxTestCount; i++)

            } // END if (cosGen != null)            

            DebugHelper.WriteEnd("IDataGeneratorPolymorphTest::AngleRotationTest()");

        } // END public static void AngleRotationTest()

        /// <summary>
        /// Test the CurrentAngle property and log results via System.Console.Out and log file.
        /// </summary>        
        public static void CurrentAngleTest()
        {
            DebugHelper.WriteStart("IDataGeneratorPolymorphTest::CurrentAngleTest()");

            IDataGenerator<double> dg = new CosineGenerator();
            CosineGenerator cosGen;
            
            if (dg is CosineGenerator)
                cosGen = dg as CosineGenerator;
            else
                cosGen = null;

            if (cosGen != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    DebugHelper.Time();
                    cosGen.Seed(Convert.ToDouble(i));
                    Debug.WriteLine("TEST {0}.  cosGen.CurrentAngle = {1}", i.ToString(), cosGen.CurrentAngle, null);                                        
                } // END for (int i = 0; i < 10; i++)
                    
            } // END if (cosGen != null)

            DebugHelper.WriteEnd("IDataGeneratorPolymorphTest::CurrentAngleTest()");

        } // END public static void CurrentAngleTest()

        /// <summary>
        /// Generate Cosine data and log to System.Out.Console and log file.
        /// </summary>
        public static void DataGeneratorTest1()
        {
            DebugHelper.WriteStart("IDataGeneratorPolymorphTest::DataGeneratorTest1()");
                      
            IDataGenerator<double> dg = new CosineGenerator();
            int maxIterations = 10;

            Debug.WriteLine("Generate and iterate over {0} cosine values from [0,{1}]", maxIterations, maxIterations - 1, null);
            Debug.WriteLine(Environment.NewLine);

            for (int i = 0; i < maxIterations; i++)
            {
                DebugHelper.Time();
                Debug.WriteLine("{0}. dg.ToString()={1}", i, dg.ToString(), null);
                DebugHelper.Time();
                Debug.WriteLine("{0}. Cosine of current angle: {1}", i, dg.Next(),null);                
                Debug.WriteLine(Environment.NewLine);
            } // END for (int i = 0; i < 10; i++)

            DebugHelper.WriteEnd("IDataGeneratorPolymorphTest::DataGeneratorTest1()");           

        } // END public static void DataGeneratorTest1()

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

            Debug.WriteLine(Environment.NewLine);
            DebugHelper.Time();            
            Debug.WriteLine(dg.ToString());
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("About to perform {0} iterations: [0, {1}]", iterationCount, iterationCount-1, null);
            Debug.WriteLine(Environment.NewLine);
            
            for (int i = 0; i < iterationCount; i++)
            {
                DebugHelper.Time();
                Debug.WriteLine("{0}.  Cosine of next angle = {1}", i, dg.Next(), null);
            } // END for (int i = 0; i < 10; i++)
            
            Debug.WriteLine(Environment.NewLine);

        } // END private static void NextTestHelper(IDataGenerator<double> dg, int iterationCount)
        #endregion

    } // END class IDataGeneratorPolymorphTest
}  // END namespace DataGeneratorTest
