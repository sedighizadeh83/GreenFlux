using GreenFlux.DTO.ChargeStation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.ServiceAbstraction
{
    public interface IChargeStationService
    {
        IEnumerable<ChargeStationReadWithDetailDto> GetAllChargeStations();

        ChargeStationReadWithDetailDto GetChargeStationById(int chargeStationId);

        void CreateChargeStation(ChargeStationCreateDto chargeStation);

        void UpdateChargeStation(ChargeStationUpdateDto chargeStation);

        void DeleteChargeStation(int chargeStationId);
    }
}
