using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

/// <summary>
/// Namespace for classes used in debugging IDataGenerator and progeny.
/// </summary>
namespace DataGeneratorTest
{
    /// <summary>
    /// Debug helper class containing methods for writing the beginning and end of testings methods, 
    /// timestamps and for helping setup the debug facility.
    /// </summary>
    class DebugHelper
    {
        public static readonly string separator = new string('=', 90);  // a string for separating text I/O in System.Console.Out and log files.

        /// <summary>
        /// Writes a START and timestamp message at the beginnig of a test.
        /// </summary>
        /// <param name="strMsgStart">Message to write.</param>
        public static void WriteStart(string strMsgStart)
        {
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine(separator);
            Debug.Indent();
            Debug.Write(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fffffff tt  ==>  ", CultureInfo.CurrentUICulture));
            Debug.WriteLine("START ==> {0}", strMsgStart, null);
            Debug.Unindent();
            Debug.WriteLine(Environment.NewLine);
        } // END public static void WriteStart(string strMsgStart)

        /// <summary>
        /// Writes an END and temestamp message at the end of a test.
        /// </summary>
        /// <param name="strMsgEnd">Message to write.</param>
        public static void WriteEnd(string strMsgEnd)
        {
            
            Debug.WriteLine(Environment.NewLine);            
            Debug.Indent();
            Debug.Write(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fffffff tt  ==>  ", CultureInfo.CurrentUICulture));
            Debug.WriteLine("END ==> {0}", strMsgEnd, null);
            Debug.Unindent();
            Debug.WriteLine(separator);
            Debug.WriteLine(Environment.NewLine);
        } // END public static void WriteEnd(string strMsgEnd)

        /// <summary>
        /// Writes a MM/dd/YYYY hh:mm:ss.fffff tttt timestamp without moving to the next line.
        /// </summary>
        public static void Time()
        {
            Debug.Write(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fffffff tt  ==>  ", CultureInfo.CurrentUICulture));
            
        } // END public void Time()

        /// <summary>
        ///  Writes a MM/dd/YYYY hh:mm:ss.fffff tttt timestamp and moves to the next line.
        /// </summary>
        public static void TimeLine()
        {
            Debug.WriteLine(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fffffff tt", CultureInfo.CurrentUICulture));

        } // END public void TimeLine()


        /// <summary>
        /// Adds trace listners (Console.Out and a log file) to debug, configures Debug listner
        /// autoflushing and writes Debug initialization messages to the screen.
        /// </summary>
        public static void Init()
        {
            Debug.AutoFlush = true;

            TextWriterTraceListener stdoutTrLstnr = new TextWriterTraceListener(System.Console.Out);
            string strLogFilePath = GetFilePath("MainDataGeneratorTest");
            TextWriterTraceListener fileTrLstnr = new TextWriterTraceListener(new StreamWriter(strLogFilePath));

            Debug.Listeners.Add(stdoutTrLstnr);
            Debug.Listeners.Add(fileTrLstnr);

            Debug.Indent();
            Debug.WriteLine("DataGenerator DEBUGGING STARTED.");
            Debug.WriteLine("Adding screen and file logging: ");
            Debug.Indent();
            Debug.WriteLine("1. System.Console.Out, and");
            Debug.WriteLine("2. {0}", strLogFilePath, null);
            Debug.Unindent();
            Debug.Write("Turning on AutoFlushing: ");
            Debug.WriteLine("AutoFlush = true.");
            Debug.Unindent();
            Debug.WriteLine(Environment.NewLine);

        } // END private void Init()

        /// <summary>
        /// Removes and closes trace listners added by Init().
        /// </summary>
        public static void Cleanup()
        { Debug.Close(); } // END private static void Wrapup()

        /// <summary>
        /// Returns a path to a log file used in debugging.
        /// </summary>
        /// <param name="fName">Name of log file to create.</param>
        /// <returns>Path to created log file.</returns>
        public static string GetFilePath(string fName)
        {
            string pathToMyDocuments = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            string pathtoLogFiles = @"\dev\vs2010\c#\DataGenerator\DebugLog\";
            string fileName = fName + "_" + System.DateTime.Now.ToFileTime().ToString() + ".txt";

            FileInfo fi = new FileInfo(pathToMyDocuments + pathtoLogFiles + fileName);

            // Create log directory if it does not exist
            if (!Directory.Exists(pathToMyDocuments + pathtoLogFiles))
                Directory.CreateDirectory(pathToMyDocuments + pathtoLogFiles);

            return fi.FullName;
        } // END private static Log GetLog()

    }  // END namespace DataGeneratorTest

} // class DebugHelper
