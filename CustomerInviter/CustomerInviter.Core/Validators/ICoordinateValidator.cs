using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Validators
{
    public interface ICoordinateValidator
    {
        public void Validate(Coordinates coordinates);
    }
}