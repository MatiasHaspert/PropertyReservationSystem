using ReservaPropiedades.Application.Interfaces;
using ReservaPropiedades.Domain.Interfaces;
using ReservaPropiedades.Domain.Entities;
using ReservaPropiedades.Domain.ValueObjects;
using ReservaPropiedades.Application.DTOs.Propiedad;

namespace ReservaPropiedades.Application.Services
{
    public class PropiedadService : IPropiedadService
    {
        private readonly IPropiedadRepository _propiedadRepository;

        public PropiedadService(IPropiedadRepository propiedadRepository)
        {
            _propiedadRepository = propiedadRepository;
        }

        public async Task<IEnumerable<Propiedad>> GetAllPropiedadesAsync()
        {
            return await _propiedadRepository.GetAllAsync();
        }

        public async Task<Propiedad> GetPropiedadByIdAsync(int id)
        {
            return await _propiedadRepository.GetByIdAsync(id);
        }

        public async Task<bool> PutPropiedadAsync(int id, PropiedadRequestDTO propiedadDTO)
        {
            if (!PropiedadExists(id))
            {
                return false;
            }
            Propiedad propiedad = mapearDtoPropiedad(propiedadDTO);
            await _propiedadRepository.UpdateAsync(propiedad);
            return true;
        }

        public async Task<Propiedad> CreatePropiedadAsync(PropiedadRequestDTO propiedadDTO)
        {
            Propiedad propiedad = mapearDtoPropiedad(propiedadDTO);
            return await _propiedadRepository.AddAsync(propiedad);
        }

        public async Task<bool> DeletePropiedadAsync(int id)
        {
            if (!PropiedadExists(id))
            {
                return false;
            }
            await _propiedadRepository.DeleteAsync(id);
            return true;
        }

        public bool PropiedadExists(int id)
        {
            return _propiedadRepository.Exists(id);
        }

        // Posibilidad de luego usar AutoMapper
        public Propiedad mapearDtoPropiedad(PropiedadRequestDTO dto)
        {
            return new Propiedad
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                PrecioPorNoche = dto.PrecioPorNoche,
                CapacidadHuespedes = dto.CapacidadHuespedes,
                Ubicacion = new Ubicacion
                {
                    Ciudad = dto.Ubicacion.Ciudad,
                    Pais = dto.Ubicacion.Pais,
                    Direccion = dto.Ubicacion.Direccion,
                    CodigoPostal = dto.Ubicacion.CodigoPostal
                }
            };
        }
    }
}
