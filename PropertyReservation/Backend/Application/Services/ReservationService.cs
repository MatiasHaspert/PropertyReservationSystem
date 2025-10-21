using AutoMapper;
using Backend.Application.DTOs.Reservation;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ReservationService(
            IReservationRepository reservationRepository,
            IMapper mapper,
            IPropertyRepository propertyRepository,
            IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
        }

        public Task<bool> CancelReservationAsync(int reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<ReservationResponseDTO> CreateReservationAsync(ReservationRequestDTO reservationRequest)
        {
            Reservation reservation = _mapper.Map<Reservation>(reservationRequest);
            
            var property = await _propertyRepository.GetByIdAsync(reservationRequest.PropertyId);
            if (property is null)
            {
                throw new ArgumentException($"La propiedad con ID {reservationRequest.PropertyId} no existe.");
            }

            var guest = await _userRepository.GetByIdAsync(reservationRequest.GuestId);
            if (guest is null)
            {
                throw new ArgumentException($"El usuario con ID {reservationRequest.GuestId} no existe.");
            }

            // Validar solapamientos con otras reservas activas y pendientes
            bool overlaps = await _reservationRepository.HasOverlappingReservationAsync(
                reservationRequest.PropertyId,
                reservationRequest.StartDate,
                reservationRequest.EndDate
            );

            if (overlaps)
                throw new InvalidOperationException("El rango de fechas solicitado se superpone con una reserva existente.");

            // Validar rango dentro de disponibilidad de la propiedad
            bool isWithinAvailability = await _propertyRepository.IsWithinAvailabilityRangeAsync(
                reservationRequest.PropertyId,
                reservationRequest.StartDate,
                reservationRequest.EndDate
            );

            if (!isWithinAvailability)
                throw new InvalidOperationException("Las fechas seleccionadas no están disponibles para esta propiedad.");

            // Calcular precio total
            var nights = (reservationRequest.EndDate - reservationRequest.StartDate).Days;
            if (nights <= 0)
                throw new InvalidOperationException("El rango de fechas es inválido.");

            reservation.TotalPrice = nights * property.NightlyPrice;
            reservation.Status = ReservationStatus.Pending;
            reservation.CreatedAt = DateTime.UtcNow;

            // Guardar reserva
            var createdReservation = await _reservationRepository.AddReservationAsync(reservation);

            // Mapear a DTO de respuesta
            var response = _mapper.Map<ReservationResponseDTO>(createdReservation);

            return response;
        }

        public Task<IEnumerable<ReservationResponseDTO>> GetAllReservationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReservationResponseDTO> GetReservationByIdAsync(int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
