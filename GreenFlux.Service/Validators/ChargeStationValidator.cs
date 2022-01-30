using GreenFlux.Data.Models;
using GreenFlux.GlobalErrorHandling.Exceptions;

namespace GreenFlux.Service.Validators
{
    public static class ChargeStationValidator
    {
        public static ValidationResult Validate(ChargeStation model, Group group)
        {
            ValidationResult result = new ValidationResult();

            if (!ValidateConnectors(model))
            {
                result.Errors.Add(new ValidationFailure("Connectors", "Charge station should have at least one connector and no more than 5 connecotrs."));
            }

            if (!ValidateGroupCapacity(model, group))
            {
                result.Errors.Add(new ValidationFailure("MaxCurrent", "The capacity in Amps of a Group should always be great than or equal to the sum of the Max current in Amps of the Connector of all Charge Stations in the Group."));
            }

            return result;
        }

        public static bool ValidateConnectors(ChargeStation model)
        {
            if (model.ConnectorCollection.Count() > 0 && model.ConnectorCollection.Count() <= 5)
            {
                return true;
            }

            return false;
        }

        public static bool ValidateGroupCapacity(ChargeStation model, Group group)
        {
            decimal groupMaxCurrentSum = 0;
            foreach (var chargeStation in group.ChargeStationCollection)
            {
                groupMaxCurrentSum += chargeStation.ConnectorCollection.Sum(x => x.MaxCurrent);
            }

            decimal modelMaxCurrentSum = model.ConnectorCollection.Sum(x => x.MaxCurrent);

            if (groupMaxCurrentSum + modelMaxCurrentSum > group.Capacity)
            {
                return false;
            }

            return true;
        }
    }
}
