using Backend.Application.DTOs.Reservation;

namespace Backend.Application.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResponseDTO> CreateReservationAsync(ReservationRequestDTO reservationRequest);
        Task<ReservationResponseDTO> GetReservationByIdAsync(int reservationId);
        Task<IEnumerable<ReservationResponseDTO>> GetAllReservationsAsync();
        Task<bool> CancelReservationAsync(int reservationId);
    }
}
