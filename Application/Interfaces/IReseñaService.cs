using ReservaPropiedades.Domain.Entities;
namespace ReservaPropiedades.Application.Interfaces
{
    public interface IReseñaService
    {
        Task<IEnumerable<Reseña>> GetReseñasForProperty(int propiedadId);

    }
}
