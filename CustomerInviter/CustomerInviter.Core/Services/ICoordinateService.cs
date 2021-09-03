using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Services
{
    public interface ICoordinateService
    {
        /// <summary>
        /// Returns the distance between two coordinates in rounded to the nearest whole meter
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        double GetDistance(Coordinates source, Coordinates destination);

        /// <summary>
        /// Returns the distance between two coordinates in kilometers rounded to two decimal places
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        double GetDistanceInKm(Coordinates source, Coordinates destination);
    }
}