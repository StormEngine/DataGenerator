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
    /// Testing CosineGenerator functionality by loading SQL express DB via single Insert operations
    /// </summary>
    class CosineGeneratorSingleSqlLoader
    {

        #region PRIVATE INSTANCE FIELDS
        private CosineGenerator _cosGen;  // The cosine generator
        private TimeSpan _interval;       // The interval at which the next cosine is to be generated
        private volatile bool _continueGeneration; // sync tool for stopping continuous cosine generation
        private SqlCommand _sqlCmdInsert; // SQL insert command
        private SqlConnection _sqlConn;   // SQL connection            
        #endregion

        #region PRIVATE CLASS FIELDS
        private static string connStr = @"Data Source=SHEPARD\SQLEXP_2008_TEST;Initial Catalog=tempdb;Integrated Security=True; Asynchronous Processing=true";
        private static object _lock = new object(); // locking object for keeping log operations together
        private static int _workerCount = 0; // Thread counter
        
        #endregion

        #region PUBLIC CONSTRUCTORS
        /// <summary>
        /// Default Constructor.
        /// Initializes a new instance of the <see cref="CosineGeneratorSingleSqlLoader"/> class.
        /// Cosines will be generated at 10 second intervals.
        /// </summary>
        public CosineGeneratorSingleSqlLoader()
        {
            WorkerCount++; // increase the number of working cosine generators

            CosineGenerator = new CosineGenerator();
            Interval = new TimeSpan(0,0,10);
            ContinueGeneration = true;            

            // Setup the parameters for the sql insert command
            _sqlCmdInsert = new SqlCommand();            
            _sqlCmdInsert.Parameters.Add("StartingOrSeedAngle", SqlDbType.Float);
            _sqlCmdInsert.Parameters.Add("AngleRotation", SqlDbType.Float);
            _sqlCmdInsert.Parameters.Add("CurrentAngle", SqlDbType.Float);
            _sqlCmdInsert.Parameters.Add("CosineOfCurrentAngle", SqlDbType.Float);
            _sqlCmdInsert.Parameters.Add("TimeOfCosineOfCurrentAngle", SqlDbType.DateTime2);
            _sqlCmdInsert.Parameters.Add("IntervalAtWhichCosineIsTaken", SqlDbType.BigInt);

            _sqlConn = null;
        } // END public CosineGeneratorThread()

        /// <summary>
        /// Initializes a new instance of the <see cref="CosineGeneratorSingleSqlLoader"/> class.
        /// </summary>
        /// <param name="cosGen">A CosineGenerator. If null then default CosineGenerator instance is used</param>
        /// <param name="interval">A TimeSpan.  If null then 10 second interval is used</param>
        public CosineGeneratorSingleSqlLoader(CosineGenerator cosGen, TimeSpan interval)
        {
            WorkerCount++;  // increase the number of working cosine generators

            CosineGenerator = (cosGen == null) ? new CosineGenerator() : cosGen;
            Interval = (interval == null)? new TimeSpan(0,0,10): interval;
            ContinueGeneration = true;

            // Setup the parameters for the sql insert command
            _sqlCmdInsert = new SqlCommand();            
            _sqlCmdInsert.Parameters.Add("StartingOrSeedAngle", SqlDbType.Float);
            _sqlCmdInsert.Parameters.Add("AngleRotation", SqlDbType.Float);
            _sqlCmdInsert.Parameters.Add("CurrentAngle", SqlDbType.Float);
            _sqlCmdInsert.Parameters.Add("CosineOfCurrentAngle", SqlDbType.Float);
            _sqlCmdInsert.Parameters.Add("TimeOfCosineOfCurrentAngle", SqlDbType.DateTime2);
            _sqlCmdInsert.Parameters.Add("IntervalAtWhichCosineIsTaken", SqlDbType.BigInt);

            _sqlConn = null;

        } // END public CosineGeneratorWorker(CosineGenerator cosGen, TimeSpan interval)
        #endregion
        
        #region PUBLIC METHODS
        /// <summary>
        /// Generates cosine data and pushes the data to SQL express one insert at a time.
        /// </summary>
        public void GenerateAndPushToDB()
        {
            int lineNum = 1;            
            double CosOfCurrentAngleCopy;

            lock (Locker)
            {
                Debug.WriteLine("STARTING:  {0}{1}", ToString(), Environment.NewLine, null);
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
                    using (_sqlConn = new SqlConnection(connStr))
                    {
                        try
                        {
                            CosOfCurrentAngleCopy = CosineGenerator.Next();

                            SqlCommand nxtSqlInsertCmnd = new SqlCommand();

                            // setup new insert parameters
                            _sqlCmdInsert.Parameters["StartingOrSeedAngle"].Value = CosineGenerator.StartingAngle;
                            _sqlCmdInsert.Parameters["AngleRotation"].Value = CosineGenerator.AngleRotation;
                            _sqlCmdInsert.Parameters["CurrentAngle"].Value = CosineGenerator.CurrentAngle;
                            _sqlCmdInsert.Parameters["CosineOfCurrentAngle"].Value = CosOfCurrentAngleCopy;
                            _sqlCmdInsert.Parameters["TimeOfCosineOfCurrentAngle"].Value = DateTime.Now;
                            _sqlCmdInsert.Parameters["IntervalAtWhichCosineIsTaken"].Value = Interval.Ticks;

                            // setup insert SQL command
                            _sqlCmdInsert.CommandText = "INSERT INTO CosineTest2 (StartingOrSeedAngle,AngleRotation,CurrentAngle,CosineOfCurrentAngle,TimeOfCosineOfCurrentAngle,IntervalAtWhichCosineIsTaken) VALUES(@StartingOrSeedAngle,@AngleRotation,@CurrentAngle,@CosineOfCurrentAngle,@TimeOfCosineOfCurrentAngle,@IntervalAtWhichCosineIsTaken)";
                            _sqlCmdInsert.CommandType = CommandType.Text;
                            _sqlCmdInsert.Connection = _sqlConn;

                            _sqlConn.Open();

                            _sqlCmdInsert.ExecuteNonQuery();

                            //Debug output
                            Debug.WriteLine("{0}. Managed Thread ID: {1}", lineNum++, System.Threading.Thread.CurrentThread.ManagedThreadId, null);
                            Debug.WriteLine("Current Time: {0}", System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fffffff tt", CultureInfo.CurrentUICulture), null);
                            Debug.WriteLine("Starting Angle: {0}", CosineGenerator.StartingAngle, null);
                            Debug.WriteLine("Angle Rotation: {0}", CosineGenerator.AngleRotation, null);
                            Debug.WriteLine("Current Angle: {0}", CosineGenerator.CurrentAngle, null);
                            Debug.WriteLine("Cosine of Current Angle: {0}", CosOfCurrentAngleCopy, null);
                            Debug.WriteLine(Environment.NewLine);
                        }

                        catch (Exception e)
                        {
                            Debug.WriteLine(e.ToString() + e.StackTrace);
                        }  // END catch (Exc

                        finally
                        {
                            // make sure that the connection closes if something goes wrong.
                            _sqlConn.Close();
                            _sqlConn.Dispose();                            
                        }

                    } // END using (_sqlConn = new SqlConnection(connStr))

                } // END lock(Locker)

                System.Threading.Thread.Sleep(Interval);

            } // END while(true)

            lock (Locker)
            {
                Debug.WriteLine("ENDING:  {0}{1}", ToString(), Environment.NewLine, null);
                Debug.WriteLine(Environment.NewLine);
            } // END lock(Locker)

        } // END public void GenerateAndPushToDB()



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
                //"Worker Method: public void Generate()" + Environment.NewLine +
                   "HashCode = " + GetHashCode().ToString();
        } // END public static int WorkerCount
        #endregion
       

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets a value indicating whether to continue cosine generation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [continue generation]; otherwise, <c>false</c>.
        /// </value>
        public bool ContinueGeneration
        {
            get { return _continueGeneration; }
            set { _continueGeneration = value; }
        } // public bool ContinueGeneration
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

        /// <summary>
        /// Gets or sets the cosine generator.
        /// </summary>
        /// <value>
        /// The cosine generator.
        /// </value>
        private CosineGenerator CosineGenerator
        {
            get { return _cosGen; }
            set { _cosGen = value; }
        } // END private CosineGenerator CosineGenerator

        /// <summary>
        /// Gets or sets the locker.
        /// </summary>
        /// <value>
        /// The locker.
        /// </value>
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
            private set {_workerCount = value;}
        } // END public static int WorkerCount
        #endregion
    }  // END class CosineGeneratorSingleSqlLoader

} // END namespace DataGeneratorTest
