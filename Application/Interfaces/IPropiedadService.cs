using ReservaPropiedades.Domain.Entities;
using ReservaPropiedades.Application.DTOs;
namespace ReservaPropiedades.Application.Interfaces
{
    public interface IPropiedadService
    {
        Task<IEnumerable<Propiedad>> GetAllPropiedadesAsync();
        Task<Propiedad> GetPropiedadByIdAsync(int id);
        Task<bool> PutPropiedadAsync(int id, PropiedadRequestDTO propiedadDTO);
        Task<Propiedad> CreatePropiedadAsync(PropiedadRequestDTO propiedadDTO);
        Task<bool> DeletePropiedadAsync(int id);
        bool PropiedadExists(int id);
    }
}
