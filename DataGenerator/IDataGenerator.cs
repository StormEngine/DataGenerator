using System;

/// <summary>
/// Namespace for data generators implementing the IDataGenerator interface.
/// 
/// Corresponds to PART I. data generation in PROJECT_SPECS.
/// </summary>
namespace DataGenerator
{
    /// <summary>
    /// Interface for data generators based on an interator concept where
    /// the generator is initialized with a starting point/value/seed Seed() 
    /// and progresses to the next data value via Next()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// 
    public interface IDataGenerator<T>
    {
        T Next();  // the next value of T        
        T Seed(T seed); // the new seed value, returns the previous seed        

    }  // END public interface DataGenerator<T>

} // END namespace DataGenerator
