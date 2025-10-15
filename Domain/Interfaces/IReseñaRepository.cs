using ReservaPropiedades.Domain.Entities;
namespace ReservaPropiedades.Domain.Interfaces
{
    public interface IReseñaRepository
    {
        Task<IEnumerable<Reseña>> GetReseñasForProperty(int propiedadId);
        //Task<Reseña?> GetByIdAsync(int id);
        //Task<Reseña> AddAsync(Reseña reseña);
        //Task UpdateAsync(Reseña reseña);
        //Task DeleteAsync(int id);
        //bool Exists(int id);


    }
}
