using GreenFlux.Data.Models;
using GreenFlux.GlobalErrorHandling.Exceptions;

namespace GreenFlux.Service.Validators
{
    public static class ConnectorValidator
    {
        public static ValidationResult ValidateForCreate(Connector model, ChargeStation chargeStation, Group group)
        {
            ValidationResult result = new ValidationResult();

            if (!ValidateMaxConnectorNumbers(chargeStation))
            {
                result.Errors.Add(new ValidationFailure("Connectors", "Charge station should have at least one connector and no more than 5 connecotrs."));
            }

            if (!ValidateGroupCapacity(model, group))
            {
                result.Errors.Add(new ValidationFailure("MaxCurrent", "The capacity in Amps of a Group should always be great than or equal to the sum of the Max current in Amps of the Connector of all Charge Stations in the Group."));
            }

            return result;
        }

        public static ValidationResult ValidateForDelete(ChargeStation chargeStation)
        {
            ValidationResult result = new ValidationResult();

            if (!ValidateMinConnectorNumbers(chargeStation))
            {
                result.Errors.Add(new ValidationFailure("Connectors", "Charge station should have at least one connector and no more than 5 connecotrs."));
            }

            return result;
        }

        public static ValidationResult ValidateForUpdate(Connector model, Group group)
        {
            ValidationResult result = new ValidationResult();

            if (!ValidateGroupCapacityAfterUpdate(model, group))
            {
                result.Errors.Add(new ValidationFailure("MaxCurrent", "The capacity in Amps of a Group should always be great than or equal to the sum of the Max current in Amps of the Connector of all Charge Stations in the Group."));
            }

            return result;
        }

        public static bool ValidateMaxConnectorNumbers(ChargeStation chargeStation)
        {
            if (chargeStation.ConnectorCollection.Count() < 5)
            {
                return true;
            }

            return false;
        }

        public static bool ValidateMinConnectorNumbers(ChargeStation chargeStation)
        {
            if (chargeStation.ConnectorCollection.Count() > 1)
            {
                return true;
            }

            return false;
        }

        public static bool ValidateGroupCapacity(Connector model, Group group)
        {
            decimal groupMaxCurrentSum = 0;
            foreach (var chargeStation in group.ChargeStationCollection)
            {
                groupMaxCurrentSum += chargeStation.ConnectorCollection.Sum(x => x.MaxCurrent);
            }

            if (model.MaxCurrent + groupMaxCurrentSum > group.Capacity)
            {
                return false;
            }

            return true;
        }

        public static bool ValidateGroupCapacityAfterUpdate(Connector model, Group group)
        {
            decimal groupMaxCurrentSum = 0;
            foreach (var chargeStation in group.ChargeStationCollection)
            {
                foreach(Connector connector in chargeStation.ConnectorCollection)
                {
                    if(connector.Id != model.Id || connector.ChargeStationId != model.ChargeStationId)
                    {
                        groupMaxCurrentSum += connector.MaxCurrent;
                    }
                }
            }

            if (model.MaxCurrent + groupMaxCurrentSum > group.Capacity)
            {
                return false;
            }

            return true;
        }
    }
}
