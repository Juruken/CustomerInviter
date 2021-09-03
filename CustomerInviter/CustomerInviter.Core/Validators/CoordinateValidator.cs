using System;
using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Validators
{
    public class CoordinateValidator : ICoordinateValidator
    {
        public void Validate(Coordinates coordinates)
        {
            ValidateLatitude(coordinates.Latitude);
            ValidateLongitude(coordinates.Longitude);
        }

        private void ValidateLatitude(double value)
        {
            if (value > 90.0 || value < -90.0)
            {
                throw new ArgumentOutOfRangeException("Latitude", "Argument must be in range of -90 to 90");
            }
        }

        private void ValidateLongitude(double value)
        {
            if (value > 180.0 || value < -180.0)
            {
                throw new ArgumentOutOfRangeException("Longitude", "Argument must be in range of -180 to 180");
            }
        }
    }
}