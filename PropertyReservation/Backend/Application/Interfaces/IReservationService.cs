using Backend.Application.DTOs.Reservation;
using Backend.Domain.Enums;
using System.Security.Claims;

namespace Backend.Application.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResponseDTO> CreateReservationAsync(ReservationRequestDTO reservationRequest);
        Task<ReservationResponseDTO> GetReservationByIdAsync(int reservationId);
        Task<IEnumerable<ReservationResponseDTO>> GetAllReservationsAsync();
        Task<bool> CancelReservationAsync(int reservationId);
        Task UpdateReservationAsync(int id, ReservationRequestDTO reservationRequestDTO);
    }
}
