using ReservaPropiedades.Application.Interfaces;
using ReservaPropiedades.Domain.Entities;
using ReservaPropiedades.Domain.Interfaces;
namespace ReservaPropiedades.Application.Services
{
    public class ReseñaService : IReseñaService
    {
        private readonly IReseñaRepository _reseñaRepository;
        public ReseñaService(IReseñaRepository reseñaRepository)
        {
            _reseñaRepository = reseñaRepository;
        }
        public async Task<IEnumerable<Reseña>> GetReseñasForProperty(int propiedadId)
        {
            return await _reseñaRepository.GetReseñasForProperty(propiedadId);
        }

    }
}
