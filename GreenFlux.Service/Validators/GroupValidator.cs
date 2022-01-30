using GreenFlux.Data.Models;
using GreenFlux.GlobalErrorHandling.Exceptions;

namespace GreenFlux.Service.Validators
{
    public static class GroupValidator
    {
        public static ValidationResult Validate(Group model, Group entity)
        {
            ValidationResult result = new ValidationResult();

            if(!ValidateGroupCapacity(model, entity))
            {
                result.Errors.Add(new ValidationFailure("Capacity", "The capacity in Amps of a Group should always be great than or equal to the sum of the Max current in Amps of the Connector of all Charge Stations in the Group."));
            }

            return result;
        }

        public static bool ValidateGroupCapacity(Group model, Group entity)
        {
            decimal sum = 0;
            foreach (var chargeStation in entity.ChargeStationCollection)
            {
                sum += chargeStation.ConnectorCollection.Sum(x => x.MaxCurrent);
            }
            if (sum > model.Capacity)
            {
                return false;
            }

            return true;
        }
    }
}
