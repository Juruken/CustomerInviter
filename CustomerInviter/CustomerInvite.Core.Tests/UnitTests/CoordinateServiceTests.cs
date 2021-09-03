using System.Collections;
using CustomerInviter.Core.Models;
using CustomerInviter.Core.Services;
using CustomerInviter.Core.Validators;
using NUnit.Framework;


namespace CustomerInvite.Tests.UnitTests
{
    [TestFixture]
    public class CoordinateServiceTests
    {
        private ICoordinateService _service;

        [SetUp]
        public void Setup()
        {
            _service = new CoordinateService(new CoordinateValidator());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="expectedDistance">Expected distance is based in meters to the nearest meter</param>
        [Test, TestCaseSource(typeof(DataProvider), "DistanceByMeter")]
        public void CalculateDistance(Coordinates source, Coordinates destination, double expectedDistance)
        {
            var distance = _service.GetDistance(source, destination);
            Assert.AreEqual(expectedDistance, distance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="expectedDistance">Expected distance is based in Kilometers rounded to the nearest KM</param>
        [Test, TestCaseSource(typeof(DataProvider), "DistanceByKM")]
        public void CalculateDistanceInKm(Coordinates source, Coordinates destination, double expectedDistance)
        {
            var distance = _service.GetDistanceInKm(source, destination);
            Assert.AreEqual(expectedDistance, distance);
        }
    }
}

public class DataProvider
{
    public static Coordinates Destination = new Coordinates { Latitude = 53.339428, Longitude = -6.257664 };

    public static IEnumerable DistanceByMeter
    {
        get
        {
            yield return new TestCaseData(new Coordinates { Latitude = 52.986375, Longitude = -6.043701 }, Destination, 41769);
            yield return new TestCaseData(new Coordinates { Latitude = 51.92893, Longitude = -10.27699 }, Destination, 313256);
            yield return new TestCaseData(new Coordinates { Latitude = 51.8856167, Longitude = -10.4240951 }, Destination, 324375);
            yield return new TestCaseData(new Coordinates { Latitude = 52.3191841, Longitude = -8.5072391 }, Destination, 188960);
            yield return new TestCaseData(new Coordinates { Latitude = 53.807778, Longitude = -7.714444 }, Destination, 109377);
            yield return new TestCaseData(new Coordinates { Latitude = 53.4692815, Longitude = -9.436036 }, Destination, 211172);
            yield return new TestCaseData(new Coordinates { Latitude = 54.0894797, Longitude = -6.18671 }, Destination, 83533);
            yield return new TestCaseData(new Coordinates { Latitude = 53.038056, Longitude = -7.653889 }, Destination, 98875);
            yield return new TestCaseData(new Coordinates { Latitude = 54.1225, Longitude = -8.143333 }, Destination, 151543);
            yield return new TestCaseData(new Coordinates { Latitude = 53.1229599, Longitude = -6.2705202 }, Destination, 24085);
            yield return new TestCaseData(new Coordinates { Latitude = 52.2559432, Longitude = -7.1048927 }, Destination, 133263);
            yield return new TestCaseData(new Coordinates { Latitude = 52.240382, Longitude = -6.972413 }, Destination, 131318);
            yield return new TestCaseData(new Coordinates { Latitude = 53.2451022, Longitude = -6.238335 }, Destination, 10567);
            yield return new TestCaseData(new Coordinates { Latitude = 53.1302756, Longitude = -6.2397222 }, Destination, 23287);
            yield return new TestCaseData(new Coordinates { Latitude = 53.008769, Longitude = -6.1056711 }, Destination, 38138);
            yield return new TestCaseData(new Coordinates { Latitude = 53.1489345, Longitude = -6.8422408 }, Destination, 44291);
            yield return new TestCaseData(new Coordinates { Latitude = 53, Longitude = -7 }, Destination, 62232);
            yield return new TestCaseData(new Coordinates { Latitude = 51.999447, Longitude = -9.742744 }, Destination, 278207);
            yield return new TestCaseData(new Coordinates { Latitude = 52.966, Longitude = -6.463 }, Destination, 43723);
            yield return new TestCaseData(new Coordinates { Latitude = 52.366037, Longitude = -8.179118 }, Destination, 168397);
            yield return new TestCaseData(new Coordinates { Latitude = 54.180238, Longitude = -5.920898 }, Destination, 96079);
            yield return new TestCaseData(new Coordinates { Latitude = 53.0033946, Longitude = -6.3877505 }, Destination, 38358);
            yield return new TestCaseData(new Coordinates { Latitude = 52.228056, Longitude = -7.915833 }, Destination, 166448);
            yield return new TestCaseData(new Coordinates { Latitude = 54.133333, Longitude = -6.433333 }, Destination, 89031);
            yield return new TestCaseData(new Coordinates { Latitude = 55.033, Longitude = -8.112 }, Destination, 223635);
            yield return new TestCaseData(new Coordinates { Latitude = 53.521111, Longitude = -9.831111 }, Destination, 237576);
            yield return new TestCaseData(new Coordinates { Latitude = 51.802, Longitude = -9.442 }, Destination, 274798);
            yield return new TestCaseData(new Coordinates { Latitude = 54.374208, Longitude = -8.371639 }, Destination, 180156);
            yield return new TestCaseData(new Coordinates { Latitude = 53.74452, Longitude = -7.11167 }, Destination, 72202);
            yield return new TestCaseData(new Coordinates { Latitude = 53.761389, Longitude = -7.2875 }, Destination, 82643);
            yield return new TestCaseData(new Coordinates { Latitude = 54.080556, Longitude = -6.361944 }, Destination, 82695);
            yield return new TestCaseData(new Coordinates { Latitude = 52.833502, Longitude = -8.522366 }, Destination, 161362);
        }
    }

    public static IEnumerable DistanceByKM
    {
        get
        {
            yield return new TestCaseData(new Coordinates { Latitude = 52.986375, Longitude = -6.043701 }, Destination, 41.77);
            yield return new TestCaseData(new Coordinates { Latitude = 51.92893, Longitude = -10.27699 }, Destination, 313.26);
            yield return new TestCaseData(new Coordinates { Latitude = 51.8856167, Longitude = -10.4240951 }, Destination, 324.38);
            yield return new TestCaseData(new Coordinates { Latitude = 52.3191841, Longitude = -8.5072391 }, Destination, 188.96);
            yield return new TestCaseData(new Coordinates { Latitude = 53.807778, Longitude = -7.714444 }, Destination, 109.38);
            yield return new TestCaseData(new Coordinates { Latitude = 53.4692815, Longitude = -9.436036 }, Destination, 211.17);
            yield return new TestCaseData(new Coordinates { Latitude = 54.0894797, Longitude = -6.18671 }, Destination, 83.53);
            yield return new TestCaseData(new Coordinates { Latitude = 53.038056, Longitude = -7.653889 }, Destination, 98.87);
            yield return new TestCaseData(new Coordinates { Latitude = 54.1225, Longitude = -8.143333 }, Destination, 151.54);
            yield return new TestCaseData(new Coordinates { Latitude = 53.1229599, Longitude = -6.2705202 }, Destination, 24.09);
            yield return new TestCaseData(new Coordinates { Latitude = 52.2559432, Longitude = -7.1048927 }, Destination, 133.26);
            yield return new TestCaseData(new Coordinates { Latitude = 52.240382, Longitude = -6.972413 }, Destination, 131.32);
            yield return new TestCaseData(new Coordinates { Latitude = 53.2451022, Longitude = -6.238335 }, Destination, 10.57);
            yield return new TestCaseData(new Coordinates { Latitude = 53.1302756, Longitude = -6.2397222 }, Destination, 23.29);
            yield return new TestCaseData(new Coordinates { Latitude = 53.008769, Longitude = -6.1056711 }, Destination, 38.14);
            yield return new TestCaseData(new Coordinates { Latitude = 53.1489345, Longitude = -6.8422408 }, Destination, 44.29);
            yield return new TestCaseData(new Coordinates { Latitude = 53, Longitude = -7 }, Destination, 62.23);
            yield return new TestCaseData(new Coordinates { Latitude = 51.999447, Longitude = -9.742744 }, Destination, 278.21);
            yield return new TestCaseData(new Coordinates { Latitude = 52.966, Longitude = -6.463 }, Destination, 43.72);
            yield return new TestCaseData(new Coordinates { Latitude = 52.366037, Longitude = -8.179118 }, Destination, 168.4);
            yield return new TestCaseData(new Coordinates { Latitude = 54.180238, Longitude = -5.920898 }, Destination, 96.08);
            yield return new TestCaseData(new Coordinates { Latitude = 53.0033946, Longitude = -6.3877505 }, Destination, 38.36);
            yield return new TestCaseData(new Coordinates { Latitude = 52.228056, Longitude = -7.915833 }, Destination, 166.45);
            yield return new TestCaseData(new Coordinates { Latitude = 54.133333, Longitude = -6.433333 }, Destination, 89.03);
            yield return new TestCaseData(new Coordinates { Latitude = 55.033, Longitude = -8.112 }, Destination, 223.64);
            yield return new TestCaseData(new Coordinates { Latitude = 53.521111, Longitude = -9.831111 }, Destination, 237.58);
            yield return new TestCaseData(new Coordinates { Latitude = 51.802, Longitude = -9.442 }, Destination, 274.8);
            yield return new TestCaseData(new Coordinates { Latitude = 54.374208, Longitude = -8.371639 }, Destination, 180.16);
            yield return new TestCaseData(new Coordinates { Latitude = 53.74452, Longitude = -7.11167 }, Destination, 72.2);
            yield return new TestCaseData(new Coordinates { Latitude = 53.761389, Longitude = -7.2875 }, Destination, 82.64);
            yield return new TestCaseData(new Coordinates { Latitude = 54.080556, Longitude = -6.361944 }, Destination, 82.7);
            yield return new TestCaseData(new Coordinates { Latitude = 52.833502, Longitude = -8.522366 }, Destination, 161.36);
        }
    }
}