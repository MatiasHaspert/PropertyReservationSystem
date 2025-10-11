using ReservaPropiedades.Domain.Entities;

namespace ReservaPropiedades.Domain.Interfaces
{
    public interface IPropiedadRepository
    {
        Task<IEnumerable<Propiedad>> GetAllAsync();
        Task<Propiedad> GetByIdAsync(int id);
        Task<Propiedad> AddAsync(Propiedad propiedad);

        Task UpdateAsync(Propiedad propiedad);
        Task DeleteAsync(int id);
        bool Exists(int id);

    }
}
