using System;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Data.SqlClient;

using DataGenerator;

/// <summary>
/// Namespace for testing the functionality of IDataGenerator and progeny.
/// Results are loged via System.Console.Out, log files and SQL Express DB
/// </summary>
namespace DataGeneratorTest
{
    /// <summary>
    /// Testing CosineGenerator by loading SQL express using a shared BulkSqlCopy.
    /// </summary>
    public class CosineGeneratorBulkSqlLoaderThreadTest
    {

        // SQL connecitons string
        static string connStr = @"Data Source=SHEPARD\SQLEXP_2008_TEST;Initial Catalog=tempdb;Integrated Security=True";
        
        // SQL clear command
        static string sqlCmdClearCosineTest2 = @"DELETE FROM CosineTest2";

        /// <summary>
        /// Bulks the SQL loader data generator thread test.
        /// </summary>
        /// <param name="ts">The TimeSpan.  The duration for generating and loading cosine data in to SQL Express</param>
        public static void BulkSqlLoaderDataGeneratorThreadTest(TimeSpan ts)
        {
            // Clear CosineTest2 SQL table
            ClearDB();

            DebugHelper.WriteStart("CosineGeneratorBulkSqlLoaderThreadTest::BulkSqlLoaderDataGeneratorThreadTest()");

            // Initialize and Start CosineGeneration Threads
            CosineGeneratorBulkSqlLoader cosGenWorker1 = new CosineGeneratorBulkSqlLoader(new CosineGenerator(0.0D, 1.0D), new TimeSpan(0, 0, 0, 0, 10));
            CosineGeneratorBulkSqlLoader cosGenWorker2 = new CosineGeneratorBulkSqlLoader(new CosineGenerator(30.0D, 5.0D), new TimeSpan(0, 0, 1));
            CosineGeneratorBulkSqlLoader cosGenWorker3 = new CosineGeneratorBulkSqlLoader(new CosineGenerator(60.0D, 10.0D), new TimeSpan(0, 0, 15));
            CosineGeneratorBulkSqlLoader cosGenWorker4 = new CosineGeneratorBulkSqlLoader(new CosineGenerator(90.0D, 15.0D), new TimeSpan(0, 0, 30));
            CosineGeneratorBulkSqlLoader cosGenWorker5 = new CosineGeneratorBulkSqlLoader(new CosineGenerator(120.0D, 20.0D), new TimeSpan(0, 0, 45));

            Thread cosGenWorkerThread1 = new Thread(cosGenWorker1.Generate);
            Thread cosGenWorkerThread2 = new Thread(cosGenWorker2.Generate);
            Thread cosGenWorkerThread3 = new Thread(cosGenWorker3.Generate);
            Thread cosGenWorkerThread4 = new Thread(cosGenWorker4.Generate);
            Thread cosGenWorkerThread5 = new Thread(cosGenWorker5.Generate);

            cosGenWorkerThread1.Name = cosGenWorkerThread1.ToString();
            cosGenWorkerThread2.Name = cosGenWorkerThread2.ToString();
            cosGenWorkerThread3.Name = cosGenWorkerThread3.ToString();
            cosGenWorkerThread4.Name = cosGenWorkerThread4.ToString();
            cosGenWorkerThread5.Name = cosGenWorkerThread5.ToString();

            Debug.WriteLine("Worker Count: {0}{1}", CosineGeneratorBulkSqlLoader.WorkerCount, Environment.NewLine, null);

            DateTime startingDateTime = DateTime.Now;
            DateTime endingDateTime = startingDateTime.Add(ts);

            cosGenWorkerThread1.Start();
            cosGenWorkerThread2.Start();
            cosGenWorkerThread3.Start();
            cosGenWorkerThread4.Start();
            cosGenWorkerThread5.Start();

            while (DateTime.Now.Ticks < endingDateTime.Ticks) ;

            // make sure that threads stop generating and pushing data
            cosGenWorker1.ContinueGeneration = false;
            cosGenWorker2.ContinueGeneration = false;
            cosGenWorker3.ContinueGeneration = false;
            cosGenWorker4.ContinueGeneration = false;
            cosGenWorker5.ContinueGeneration = false;

            cosGenWorkerThread1.Join();
            cosGenWorkerThread2.Join();
            cosGenWorkerThread3.Join();
            cosGenWorkerThread4.Join();
            cosGenWorkerThread5.Join();

            DebugHelper.WriteEnd("CosineGeneratorBulkSqlLoaderThreadTest::BulkSqlLoaderDataGeneratorThreadTest()");

        } // END public static void DataGeneratorThreadTest(TimeSpan ts)

        /// <summary>
        /// Clears CosineTest2 SQL express DB table.
        /// </summary>
        private static void ClearDB()
        {
            DebugHelper.WriteStart("CosineGeneratorBulkSqlLoaderThreadTest::ClearDB()");

            using (SqlCommand clearCmd = new SqlCommand(sqlCmdClearCosineTest2, new SqlConnection(connStr)))
            {

                try
                {
                    clearCmd.Connection.Open();
                    
                    Debug.WriteLine("Connection opened.");
                    Debug.WriteLine(clearCmd.Connection.ConnectionString);
                    Debug.WriteLine(clearCmd.Connection.Database);
                    Debug.WriteLine(clearCmd.Connection.DataSource);
                    Debug.WriteLine(clearCmd.Connection.ServerVersion);
                    Debug.WriteLine(clearCmd.Connection.State);
                    Debug.WriteLine(clearCmd.Connection.WorkstationId);

                    Debug.WriteLine(Environment.NewLine);
                    Debug.WriteLine(DebugHelper.separator);
                    Debug.WriteLine(Environment.NewLine);

                    clearCmd.ExecuteNonQuery();

                } // END try

                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message + ex.StackTrace);
                } // END finally

                finally
                {
                    clearCmd.Connection.Close();
                    Console.WriteLine("Connection closed.");
                } // END finally

            } // END using (SqlCommand clearCmd = new SqlCommand(sqlCmdClearCosineTest2, new SqlConnection(connStr)))

            DebugHelper.WriteEnd("CosineGeneratorBulkSqlLoaderThreadTest::ClearDB()");

        } // END private void ClearDB()
    } // END public class CosineGeneratorBulkSqlLoaderThreadTest
} // namespace DataGeneratorTest
