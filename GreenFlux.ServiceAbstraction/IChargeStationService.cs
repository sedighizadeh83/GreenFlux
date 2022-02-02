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
        Task<IEnumerable<ChargeStationReadWithDetailDto>> GetAllChargeStations();

        Task<ChargeStationReadWithDetailDto> GetChargeStationById(int chargeStationId);

        Task CreateChargeStation(ChargeStationCreateDto chargeStation);

        Task UpdateChargeStation(ChargeStationUpdateDto chargeStation);

        Task DeleteChargeStation(int chargeStationId);
    }
}
