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
    /// Runs through all IDataGenerator and CosineGenerator functionality tests and logs results.
    /// Log results to Console.Out, log file, and SQL DB.
    /// </summary>
    class MainDataGeneratorTest
    {
        /// <summary>
        /// Main entry point into all data generator tests.
        /// </summary>
        static void Main()
        {            
            try
            {
                // Setup and initialize the neccessary resources for DebugHelper
                DebugHelper.Init();

                DebugHelper.WriteStart("MainDataGeneratorTest::Main()");

                // Tests the functionality of CosineGenerator polymorphically: via IDataGenerator
                IDataGeneratorPolymorphTest.RunAllTests();

                // Tests the functionality of CosineGenrator
                // Supplied TimeSpan controls the interval at which CosineGenerator
                // generates the value of the next Cosine.
                CosineGeneratorTest.RunAllTests(new TimeSpan(0, 0, 0, 15));

                // Tests CosineGenerator by bulk loading SQL express DB with via multiple threads
                // TimeSpan specified the amount of time alloted for loading the SQL DB
                CosineGeneratorBulkSqlLoaderThreadTest.BulkSqlLoaderDataGeneratorThreadTest(new TimeSpan(0, 30, 0));

                // Tests CosineGenerator by bulk loading SQL express DB with via multiple threads
                // TimeSpan specified the amount of time alloted for loading the SQL DB
                CosineGeneratorSingleSqlLoaderThreadTest.SingleSqlLoaderDataGeneratorThreadTest(new TimeSpan(0, 30, 0));

                DebugHelper.WriteEnd("MainDataGeneratorTest::Main()");

                // Release DebugHelper resources
                DebugHelper.Cleanup();

            } // END try

            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            } // END catch (Exception ex)

            finally
            {
                DebugHelper.Cleanup();
            } // END finally

        }  // END static void Main()

    } // END class MainDataGeneratorTest

} // END namespace DataGeneratorTest
