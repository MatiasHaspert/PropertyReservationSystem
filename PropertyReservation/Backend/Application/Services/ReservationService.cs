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

            if (reservation.TotalGuests > property.MaxGuests)
            {
                throw new InvalidOperationException("El número de huéspedes excede la capacidad máxima de la propiedad.");
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
                reservationRequest.EndDate,
                null
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

        public async Task UpdateReservationAsync(int id, ReservationRequestDTO reservationRequestDTO)
        {
            // Buscar la reserva existente
            var existingReservation = await _reservationRepository.GetByIdAsync(id);
            if (existingReservation is null)
                throw new KeyNotFoundException($"La reserva con ID {id} no existe.");

            // No permitir actualizar reservas canceladas o finalizadas
            if (existingReservation.Status == ReservationStatus.Cancelled ||
                existingReservation.Status == ReservationStatus.Completed)
            {
                throw new InvalidOperationException("No se puede modificar una reserva cancelada o finalizada.");
            }

            // Validar propiedad
            var property = await _propertyRepository.GetByIdAsync(reservationRequestDTO.PropertyId);
            if (property is null)
                throw new ArgumentException($"La propiedad con ID {reservationRequestDTO.PropertyId} no existe.");

            // Validar usuario
            var guest = await _userRepository.GetByIdAsync(reservationRequestDTO.GuestId);
            if (guest is null)
                throw new ArgumentException($"El usuario con ID {reservationRequestDTO.GuestId} no existe.");

            // Validar capacidad máxima
            if (reservationRequestDTO.TotalGuests > property.MaxGuests)
                throw new InvalidOperationException("El número de huéspedes excede la capacidad máxima de la propiedad.");

            // Validar superposición de reservas (excluyendo la reserva actual)
            bool overlaps = await _reservationRepository.HasOverlappingReservationAsync(
                reservationRequestDTO.PropertyId,
                reservationRequestDTO.StartDate,
                reservationRequestDTO.EndDate,
                excludeReservationId: id
            );

            if (overlaps)
                throw new InvalidOperationException("El rango de fechas solicitado se superpone con otra reserva existente.");

            // Validar rango dentro de disponibilidades
            bool isWithinAvailability = await _propertyRepository.IsWithinAvailabilityRangeAsync(
                reservationRequestDTO.PropertyId,
                reservationRequestDTO.StartDate,
                reservationRequestDTO.EndDate
            );

            if (!isWithinAvailability)
                throw new InvalidOperationException("Las fechas seleccionadas no están disponibles para esta propiedad.");

            // Calcular precio total
            var nights = (reservationRequestDTO.EndDate - reservationRequestDTO.StartDate).Days;
            if (nights <= 0)
                throw new InvalidOperationException("El rango de fechas es inválido.");

            decimal totalPrice = nights * property.NightlyPrice;

            // Actualizar campos de la reserva
            existingReservation.StartDate = reservationRequestDTO.StartDate;
            existingReservation.EndDate = reservationRequestDTO.EndDate;
            existingReservation.TotalGuests = reservationRequestDTO.TotalGuests;
            existingReservation.TotalPrice = totalPrice;
            existingReservation.PropertyId = reservationRequestDTO.PropertyId;
            existingReservation.GuestId = reservationRequestDTO.GuestId;

            // Guardar cambios
            await _reservationRepository.UpdateAsync(existingReservation);
        }

        public Task<IEnumerable<ReservationResponseDTO>> GetAllReservationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ReservationResponseDTO> GetReservationByIdAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation is null)
            {
                throw new ArgumentException($"La reserva con ID {reservationId} no existe.");
            }

            return _mapper.Map<ReservationResponseDTO>(reservation);
        }
    }
}
