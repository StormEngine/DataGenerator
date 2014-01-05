using System;

/// <summary>
/// Namespace for data generators implementing the IDataGenerator interface.
/// 
/// Corresponds to PART I. data generation in PROJECT_SPECS.
/// </summary>
namespace DataGenerator
{

    /// <summary>
    /// CosineData Generator implements IDataGenerator<double>
    /// Instances of this class generate the Cosine of a supplied starting/seed angle.
    /// The Cosine of the next angle is obtained by taking the starting/seed angle through
    /// a positive or negative angle rotation.
    /// </summary>
    public class CosineGenerator : IDataGenerator<double>
    {
        #region PRIVATE CONSTANTS FIELDS
        // for use in private helper methods CheckAngleRotation(), CheckNanAndInfinity()
        // and other Properties and Constructors 
        private const int PASS = 0;
        private const int FAIL = -1;        
        #endregion

        #region PUBLIC CONSTANT FIELDS
        public const double DefaultStartingAngle = 0.0D;
        public const double DefaultStartingPositiveAngleRotation = 1.0D;
        public const double DefaultStartingNegativeAngleRotation = -1.0D;
        #endregion

        #region PRIVATE FIELDS
        private double _startingAngle;  // staring/seed angle in degrees
        private double _currentAngle;   // the next angle in degrees starting from _angle and increasing/decreasing by _angleRotation
        private double _angleRotation;  // the step to the next angle in degrees
        #endregion

        #region PUBLIC CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="CosineGenerator"/> class.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="angleRotation">The angle rotation.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// angle;public CosineGenerator(double, double): angle must be a non-zero real number.
        /// or
        /// angleRotation;public CosineGenerator(double, double): angleRotation must be a non-zero real number.
        /// </exception>
        public CosineGenerator(double angle = DefaultStartingAngle, double angleRotation=DefaultStartingPositiveAngleRotation)
        {
            // Make sure that angle is not Infinity
            if(CheckNanAndInfinity(angle) == FAIL)
                throw new ArgumentOutOfRangeException("angle", "public CosineGenerator(double, double): angle must be a non-zero real number.");
            else 
                StartingAngle = angle;
            
            // Prevent Next() from generating the same data by disallowing an angle rotation of 0.
            if (CheckAngleRotation(angleRotation) == FAIL)
                throw new ArgumentOutOfRangeException("angleRotation", "public CosineGenerator(double, double): angleRotation must be a non-zero real number.");
            else
                AngleRotation = angleRotation;

            CurrentAngle = StartingAngle;
            
        } // END public CosineGenerator(double angle = 0.0D, double angleRotation=0.01D)
        #endregion

        #region PUBLIC METHODS IMPLEMENTING interface DataGenerator<double>
        
        /// <summary>
        /// Returns the value in degrees of cos(CurrentAngle) and rotates CurrentAngle by AngleRotation
        /// 
        /// If CurrentAngle == NaN or CurrentAngle == Infinity ==> the generator is Reset to
        /// DefaultStartingAngle and DefaultPositiveStep
        /// </summary>
        /// <returns>The Cosine of CurrentAngle</returns>
        public double Next() 
        {
            // get the Cosine of the Current Angle
            double nextCos = ToDegrees(CurrentAngle);

            // Reset the generator if the next CurrentAngle will become Infinite or Nan or exceed inifinity
            if (Double.IsInfinity(CurrentAngle + AngleRotation) || Double.IsNaN(CurrentAngle + AngleRotation))
                Reset();
            else
                CurrentAngle = CurrentAngle + AngleRotation;
            return nextCos;
        }  // END public double Next() 

        // the new starting/seed angle in degrees
        // returns the previous starting/seed angle in degrees
        // if newAngle == NaN or Ininity ==> seed with DefaultStartingAngle
        /// <summary>
        /// Receives a new StartingAngle the value of which may not be NaN of Ininity.
        /// If NaN or Inifity is received than StartingAngle is reset to DefaultStartingAngle        /// 
        /// </summary>
        /// <param name="newAngle">The new angle.</param>
        /// <returns>The value of the previous seed/starting angle</returns>
        public double Seed(double newAngle)
        {
            // save original StartingAngle or Seed
            double previousSeed = StartingAngle;

            if (Double.IsNaN(newAngle) || Double.IsInfinity(newAngle))
            {
                // newAngle is not valid                

                // reset starting angle to DefaultStartingAngle
                StartingAngle = DefaultStartingAngle;

            } // END if (Double.IsNaN(newAngle) || Double.IsInfinity(newAngle))

            else // new Angle is valid
                StartingAngle = newAngle;

            // reset CurrentAngle
            CurrentAngle = StartingAngle;

            return previousSeed;

        } // END public double Seed(double newAngle)
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// The values of StartingAngle, AngleRotation, and CurrentAngle
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Starting Angle:  " + StartingAngle.ToString() + "  Angle Rotation: " + AngleRotation.ToString() + " Current Angle: " + CurrentAngle.ToString();
        }  // END public override string ToString()
        
        #endregion

        #region PUBLIC PROPERTIES

        
        /// <summary>
        /// Gets or sets the angle rotation.
        /// </summary>
        /// <value>
        /// The angle rotation.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value;public double AngleRotation: AngleRotation must be a non-zero real number.</exception>
        public double AngleRotation
        {
            get { return _angleRotation; }
            
            set 
            {
                if (CheckAngleRotation(value) == FAIL)
                    throw new ArgumentOutOfRangeException("value", "public double AngleRotation: AngleRotation must be a non-zero real number.");
                else
                    _angleRotation = value; 
            } // END set
        } // END public double AngleRotation

        
        /// <summary>
        /// Gets the starting angle.
        /// </summary>
        /// <value>
        /// The starting angle.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value;public double StartingAngle private set: StartingAngle must be a non-zero real number.</exception>
        public double StartingAngle
        {
            get { return _startingAngle; }
            
            private set // made private so that Seed() is used to set the StartingAngle
            {
                if (CheckNanAndInfinity(value) == FAIL)
                    throw new ArgumentOutOfRangeException("value", "public double StartingAngle private set: StartingAngle must be a non-zero real number.");
                else
                    _startingAngle = value; 
            } // END private set;
        } // END private double Angle

        
        /// <summary>
        /// Gets the current angle.
        /// </summary>
        /// <value>
        /// The current angle.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value;public double CurrentAngle private set: CurrentAngle must be a non-zero real number.</exception>
        public double CurrentAngle
        {
            get { return _currentAngle; }
            
            private set // made private to prevent rotations without the use of Next();
            {
                if (CheckNanAndInfinity(value) == FAIL)
                    throw new ArgumentOutOfRangeException("value", "public double CurrentAngle private set: CurrentAngle must be a non-zero real number.");
                else
                    _currentAngle = value;                  
            } // END private set;
        } // END private double NextAngle

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Check if AngleRotation is 0.0 Nan or Infinity
        /// </summary>
        /// <param name="angleRotation">The angle rotation.</param>
        /// <returns>Returns PASS angleRotation is valid and FAIL otherise</returns>
        private int CheckAngleRotation(double angleRotation)
        { return (angleRotation == 0.0D || CheckNanAndInfinity(angleRotation) == FAIL) ? FAIL : PASS; }

        /// <summary>
        /// Check if a value is Nan or Infinity
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>Returns PASS if v is not NaN or Inifinity; FAIL otherwise</returns>
        private int CheckNanAndInfinity(double v)
        { return (Double.IsNaN(v) || Double.IsInfinity(v)) ? FAIL : PASS;  }

        /// <summary>
        /// Rest current generator to default values
        /// </summary>
        private void Reset()
        {            
            StartingAngle = DefaultStartingAngle;
            AngleRotation = DefaultStartingPositiveAngleRotation;
            CurrentAngle = StartingAngle;
        } // END private void Reset()

        /// <summary>
        /// Converts radians to degress.
        /// </summary>
        /// <param name="radians">The radians.</param>
        /// <returns>Value in degrees</returns>
        private double ToDegrees(double radians)
        { return Math.Cos((radians * Math.PI) / 180); } // private double ToDegrees(double radians)
       
        #endregion

    }  // END class CosGen : IDataGenerator<double>

} // END namespace DataGenerator
