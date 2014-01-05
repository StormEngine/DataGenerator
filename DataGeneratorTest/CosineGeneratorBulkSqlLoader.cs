using System;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using DataGenerator;

/// <summary>
/// Namespace for testing the functionality of IDataGenerator and progeny.
/// Results are loged via System.Console.Out, log files and SQL Express DB
/// </summary>
namespace DataGeneratorTest
{

    /// <summary>
    /// Test CosineGenerator functionality using multiple threads.
    /// Threads are linked to Generate().
    /// </summary>
    public class CosineGeneratorBulkSqlLoader
    {
        #region PRIVATE INSTANCE FIELDS
        private CosineGenerator _cosGen; // The cosine generator
        private TimeSpan _interval; // The interval at which cosines are to be generated
        private volatile bool _continueGeneration; // sync tool for stopping continuous data generation and DB insertion
        #endregion

        #region PRIVATE CLASS FIELDS
        // connection string
        private static string connStr = @"Data Source=SHEPARD\SQLEXP_2008_TEST;Initial Catalog=tempdb;Integrated Security=True; Asynchronous Processing=true";
        
        // object to lock on
        private static object _lock = new object();
        
        // shared buffer for bulk DB loading
        private static DataTable dtBuffer;

        // making sure that the structure fo dtBuffer is setup only once
        private static bool bufferSetup = false;
        
        // counter for the number of working threads
        private static int _workerCount = 0;
        
        // shared SqlBulkCopy for writing to DB in bulk.
        private static SqlBulkCopy sqlBulkCopyBuffer_1 = new SqlBulkCopy(connStr);
        
        #endregion

        #region PUBLIC CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="CosineGeneratorBulkSqlLoader"/> class using
        /// default CosineGenerator and 10 second interval.
        /// </summary>
        public CosineGeneratorBulkSqlLoader()
        {
            WorkerCount++;
            SetupDataTableBuffer(); // setsup the DataTable structure.

            CosineGenerator = new CosineGenerator();
            Interval = new TimeSpan(0, 0, 10);
            ContinueGeneration = true;            
            
        } // END public CosineGeneratorThread()

        public CosineGeneratorBulkSqlLoader(CosineGenerator cosGen, TimeSpan interval)
        {
            WorkerCount++;
            SetupDataTableBuffer();  // setsup the DataTable structure.

            CosineGenerator = (cosGen == null) ? new CosineGenerator() : cosGen;
            Interval = (interval == null) ? new TimeSpan(0, 0, 10) : interval;
            ContinueGeneration = true;
            
        } // END public CosineGeneratorWorker(CosineGenerator cosGen, TimeSpan interval)
        #endregion
        
        #region PUBLIC METHODS

        /// <summary>
        /// Generates cosine values and load them into a DataTable buffer for Bulk copying to SQL
        /// </summary>
        public void Generate()
        {
            int lineNum = 1;
            object[] dtBufferRow = new object[8];

            lock (Locker)
            {
                Debug.WriteLine(DebugHelper.separator);
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine("STARTING:  {0}{1}", ToString(), Environment.NewLine, null);
                Debug.WriteLine(DebugHelper.separator);
                Debug.WriteLine(Environment.NewLine);
            } // END lock(Locker)

            // block each starting thread for 10 sec to sync Debug output            
            System.Threading.Thread.Sleep(new TimeSpan(0, 0, 10));

            while (true)
            {
                if (ContinueGeneration == false)
                    break;
                
                lock (Locker)
                {    
                    // fill dtBufferRow
                    dtBufferRow[0] = null;
                    dtBufferRow[1] = null;
                    dtBufferRow[2] = CosineGenerator.StartingAngle;
                    dtBufferRow[3] = CosineGenerator.AngleRotation;
                    dtBufferRow[4] = CosineGenerator.CurrentAngle;
                    dtBufferRow[5] = CosineGenerator.Next();
                    dtBufferRow[6] = DateTime.Now;
                    dtBufferRow[7] = Interval.Ticks;

                    // add row to dtBuffer
                    dtBuffer.Rows.Add(dtBufferRow);

                    //Debug output
                    Debug.WriteLine("{0}. Managed Thread ID: {1}", lineNum++, System.Threading.Thread.CurrentThread.ManagedThreadId, null);
                    Debug.WriteLine("Time Cosie of CurrentAngle was taken: {0}", ((DateTime)dtBufferRow[6]).ToString("MM/dd/yyyy hh:mm:ss.fffffff tt", CultureInfo.CurrentUICulture), null);
                    Debug.WriteLine("Starting Angle: {0}", (double)dtBufferRow[2], null);
                    Debug.WriteLine("Angle Rotation: {0}", (double)dtBufferRow[3], null);
                    Debug.WriteLine("Current Angle: {0}", (double)dtBufferRow[4], null);
                    Debug.WriteLine("Cosine of Current Angle: {0}", (double)dtBufferRow[5], null);
                    Debug.WriteLine(Environment.NewLine);

                    // Bulk copy every 300 rows
                    if (dtBuffer.Rows.Count >= 300)
                        PushToDB();

                } // END lock(Locker)

                System.Threading.Thread.Sleep(Interval);

            } // END while(true)

            lock (Locker)
            {
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine(DebugHelper.separator);
                Debug.WriteLine("ENDING:  {0}{1}", ToString(), Environment.NewLine, null);
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine(DebugHelper.separator);
            } // END lock(Locker)

        } // END public void Generate()

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Thread ID = " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + Environment.NewLine +
                   CosineGenerator.ToString() + Environment.NewLine +
                   "TimeSpan = " + Interval.Duration().TotalSeconds.ToString() + "sec" + Environment.NewLine +
                   "HashCode = " + GetHashCode().ToString();
        } // END public static int WorkerCount
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Bulk copy DataTable buffer to SQL and clear the buffer.
        /// </summary>
        private void PushToDB()
        {
            DebugHelper.WriteStart("CosineGeneratorBulkSqlLoader::PushToDB()");

            SqlConnection conn = new SqlConnection(connStr);
            sqlBulkCopyBuffer_1.DestinationTableName = "CosineTest2";

            try
            {
                conn.Open();

                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine("Connection opened.");
                Debug.WriteLine(conn.ConnectionString);
                Debug.WriteLine(conn.Database);
                Debug.WriteLine(conn.DataSource);
                Debug.WriteLine(conn.ServerVersion);
                Debug.WriteLine(conn.State);
                Debug.WriteLine(conn.WorkstationId);

                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine(DebugHelper.separator);
                Debug.WriteLine(Environment.NewLine);

                Debug.WriteLine("Performing bulk copy to DB");

                sqlBulkCopyBuffer_1.WriteToServer(dtBuffer);

                Debug.WriteLine("Bulk copy to DB complete");
                Debug.WriteLine(Environment.NewLine);

            } // END try            

            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message + ex.StackTrace);
            } // END catch (SqlException ex)

            finally
            {
                Debug.WriteLine("Closing connection.");
                conn.Close();
                Debug.WriteLine("Connection closed.");
                Debug.WriteLine("Clearing buffer.");
                dtBuffer.Clear();
                Debug.WriteLine("Buffer cleared.");
                Debug.WriteLine(Environment.NewLine);
                DebugHelper.WriteEnd("CosineGeneratorBulkSqlLoader::PushToDB()");
            }  // END finally

        } // END public static void PushToDB()
        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets or sets a value indicating whether [continue generation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [continue generation]; otherwise, <c>false</c>.
        /// </value>
        public bool ContinueGeneration
        {

            get { return _continueGeneration; }
            set { _continueGeneration = value; }

        } // END public bool ContinueGeneration
        #endregion

        #region PRIVATE PROPERTIES
        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        private TimeSpan Interval
        {
            get { return _interval;}
            set { _interval = value; }
        } // END private TimeSpan Interval

        private CosineGenerator CosineGenerator
        {
            get { return _cosGen; }
            set { _cosGen = value; }
        } // END private CosineGenerator CosineGenerator

        private Object Locker
        {
            get { return _lock; }
            set { _lock = value; }
        } // END private Object Locker
        #endregion

        #region PUBLIC STATIC METHODS

        /// <summary>
        /// Gets the worker count.
        /// </summary>
        /// <value>
        /// The worker count.
        /// </value>
        public static int WorkerCount
        {
            get { return _workerCount; }

            // cannot increase worker count without creating a worker first.
            private set {_workerCount = value;} 
        } // END public static int WorkerCount
        #endregion

        #region PRIVATE CLASS METHODS

        /// <summary>
        /// Setups the structure of the data table buffer and make sure that it is only done once.
        /// </summary>
        private static void SetupDataTableBuffer()
        {
            if (!bufferSetup)
            {
                // make sure that columns are setup only once
                bufferSetup = true;

                // get a shared DataTable
                dtBuffer = new DataTable("CosineTest2");

                // setup columns            
                dtBuffer.Columns.Add(new DataColumn("RowId", typeof(int)));
                dtBuffer.Columns.Add(new DataColumn("InsertionTime", typeof(DateTime)));
                dtBuffer.Columns.Add(new DataColumn("StartingOrSeedAngle", typeof(double)));
                dtBuffer.Columns.Add(new DataColumn("AngleRotation", typeof(double)));
                dtBuffer.Columns.Add(new DataColumn("CurrentAngle", typeof(double)));
                dtBuffer.Columns.Add(new DataColumn("CosineOfCurrentAngle", typeof(double)));
                dtBuffer.Columns.Add(new DataColumn("TimeOfCosineOfCurrentAngle", typeof(DateTime)));
                dtBuffer.Columns.Add(new DataColumn("IntervalAtWhichCosineIsTaken", typeof(long)));

            } // END if (!bufferSetup) 
        } // public static void SetupDataTableCosineTest()
        #endregion

    } // END class CosineGeneratorThread
} // END namespace DataGeneratorTest
