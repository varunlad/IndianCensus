using Indian_Census_058;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{

    [TestClass]
    public class UnitTest1
    {
        //Initializing the path 
        //string stateCodePath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\IndiaStateCode.csv";
        string stateCensusPath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\IndiaStateCensusData.csv";
        string wrongCensusPath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\IndiaStateData.csv";
        string wrongStateCodePath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\IndiaCode.csv";
        string wrongTypeStateCodePath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\IndiaStateCode.txt";
        string wrongHeaderStateCodePath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\WrongIndiaStateCode.csv";
        string wrongHeaderStateCensusPath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\WrongIndiaStateCensusData.csv";
        string delimiterStateCodePath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\DelimiterIndiaStateCode.csv";
        string delimiterStateCensusPath = @"H:\visualstudio\IndianCensusAnalyserProblem\IndianCensusAnalyserProblem\CSVFiles\DelimiterIndiaStateCensusData.csv";
        IndiaCensusDataDemo.CSVAdapterFactory csv = null;
        Dictionary<string, CensusDataDAO> stateRecords;

        [TestInitialize]
        public void SetUp()
        {
            csv = new IndiaCensusDataDemo.CSVAdapterFactory();
            stateRecords = new Dictionary<string, CensusDataDAO>();
        }
        /// TC 1.1
        /// Giving the correct path it should return the total count from the census
        [TestMethod]
        public void GivenStateCensusCSVShouldReturnRecords()
        {
            stateRecords = csv.LoadCsvData(IndiaCensusDataDemo.CensusAnalyser.Country.INDIA, stateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm");
            Assert.AreEqual(29, stateRecords.Count);
        }

        /// TC 1.2
        /// Giving incorrect path should return file not found custom exception
        [TestMethod]
        public void GivenIncorrectFileShouldThrowCustomException()
        {
            try
            {
                var customException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
                //total no of rows excluding header are 29 in indian state census data.
                //Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, customException.exception);
            }
            catch (CensusAnalyserException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert.AreEqual(customException.exception, CensusAnalyserException.ExceptionType.FILE_NOT_FOUND);
        }

        /// TC 1.3
        /// Giving wrong type of path should return invalid file type custom exception
        [TestMethod]
        public void GivenWrongTypeReturnsCustomException()
        {
            try
            {
                var customException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongTypeStateCodePath, "SrNo,State Name,TIN,StateCode"));
                Assert.AreEqual(customException.exception, CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE);
            }
            catch (CensusAnalyserException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// TC 1.4
        /// Giving wrong delimiter should return incorrect delimiter custom exception
        [TestMethod]
        public void GivenWrongDelimeterReturnsCustomException()
        {
            try
            {
                var censusException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, delimiterStateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
                Assert.AreEqual(censusException.exception, CensusAnalyserException.ExceptionType.INCOREECT_DELIMITER);
            }
            catch (CensusAnalyserException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// TC 1.5
        /// Giving wrong header type should return incorrect header type custom exception
        [TestMethod]
        public void GivenWrongHeaderReturnsCustomException()
        {
            try
            {
                var censusException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongHeaderStateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
                Assert.AreEqual(censusException.exception, CensusAnalyserException.ExceptionType.INCORRECT_HEADER);
            }
            catch (CensusAnalyserException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
