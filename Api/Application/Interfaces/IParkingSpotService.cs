using minimal_api.Domain.DTO;
using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IParkingSpotService
    {
        List<ParkingSpot> ListAllParkingSpots();
        ParkingSpot AddParkingSpot(ParkingSpotDTO parkingSpotDTO);
        ParkingSpot? UpdateParkingSpot(UpdateParkingSpotDTO updateParkingSpotDTO, int id);
        ParkingSpot? RemoveParkingSpot(int id);
    }
}