using System;
using CustomerInviter.Core.Models;
using CustomerInviter.Core.Validators;

namespace CustomerInviter.Core.Services
{
    /// <summary>
    /// Calculates the distance between two points using radius of 6371.009
    /// </summary>
    public class CoordinateService : ICoordinateService
    {
        private const double EARTH_RADIUS = 6371009.0;

        private readonly ICoordinateValidator _validator;

        public CoordinateService(ICoordinateValidator validator)
        {
            _validator = validator;
        }

        public double GetDistance(Coordinates source, Coordinates destination)
        {
            return Math.Round(CalculateDistance(source, destination), 0);
        }

        public double GetDistanceInKm(Coordinates source, Coordinates destination)
        {
            return Math.Round(CalculateDistance(source, destination) / 1000, 2);
        }
        
        private double CalculateDistance(Coordinates source, Coordinates destination)
        {
            _validator.Validate(source);
            _validator.Validate(destination);

            var d1 = source.Latitude * (Math.PI / 180.0);
            var num1 = source.Longitude * (Math.PI / 180.0);
            var d2 = destination.Latitude * (Math.PI / 180.0);
            var num2 = destination.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return EARTH_RADIUS * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}