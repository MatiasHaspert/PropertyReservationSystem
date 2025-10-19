using AutoMapper;
using Backend.Application.DTOs.PropertyImage;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IWebHostEnvironment _environment; // Informacion del entorno actual donde corre mi app 
        private readonly IMapper _mapper;

        public PropertyImageService(
            IPropertyImageRepository propertyImageRepository,
            IPropertyRepository propertyRepository,
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper
        )
        {
            _propertyImageRepository = propertyImageRepository;
            _propertyRepository = propertyRepository;
            _environment = webHostEnvironment;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyImageResponseDTO>> GetImagesByPropertyAsync(int propertyId)
        {
            var propertyExists = await _propertyRepository.PropertyExistsAsync(propertyId);
            if (!propertyExists)
            {
                throw new ArgumentException("La propiedad no existe.");
            }
            var images = await _propertyImageRepository.GetPropertyImagesByPropertyIdAsync(propertyId);
            return images.Select(img => _mapper.Map<PropertyImageResponseDTO>(img)).ToList();
        }

        public async Task<List<PropertyImageResponseDTO>> UploadPropertyImagesAsync(int propertyId, List<IFormFile> files, HttpRequest request)
        {
            var property = await _propertyRepository.GetByIdAsync(propertyId);
            if (property == null)
            {
                throw new ArgumentException("No se pueden crear las imagenes: la propiedad no existe.");
            }

            if (files == null || files.Count() == 0)
            {
                throw new InvalidOperationException("No se pueden crear las imagenes: no se han proporcionado archivos.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var propertyFolder = Path.Combine(_environment.WebRootPath, "uploads", "properties", propertyId.ToString());

            if (!Directory.Exists(propertyFolder))
            {
                Directory.CreateDirectory(propertyFolder);
            }

            var uploadedImages = new List<PropertyImage>();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    throw new InvalidOperationException($"El tipo de archivo {extension} no esta permitido.");

                var fileName = $"{Guid.NewGuid()}{extension}"; // Generar un nombre unico
                var physicalPath = Path.Combine(propertyFolder, fileName);

                // Guardar el archivo en el sistema de archivos
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Construir la URL accesible publicamente
                var url = $"{request.Scheme}://{request.Host}/uploads/properties/{propertyId}/{fileName}";

                uploadedImages.Add(new PropertyImage
                {
                    PropertyId = propertyId,
                    Url = url,
                    FileName = fileName,
                    IsMainImage = false,
                    CreationDate = DateTime.UtcNow
                });
            }

            await _propertyImageRepository.AddRangePropertyImageAsync(uploadedImages);
            var propertyImagesDTOs =  uploadedImages.Select(pi => _mapper.Map<PropertyImageResponseDTO>(pi)).ToList();
            return propertyImagesDTOs;
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _propertyImageRepository.GetPropertyImageByIdAsync(imageId);

            if (image == null)
            {
                throw new ArgumentException("La imagen no existe.");
            }
            // Eliminar el archivo fisico si existe
            var physicalPath = Path.Combine(_environment.WebRootPath, "uploads", "properties", image.PropertyId.ToString(), image.FileName);
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
            await _propertyImageRepository.DeletePropertyImageAsync(image);
        }

        public async Task<PropertyImageResponseDTO> SetMainImageAsync(int propertyId, int imageId)
        {
            if (!await _propertyRepository.PropertyExistsAsync(propertyId))
                throw new KeyNotFoundException("La propiedad no existe.");

            var images = await _propertyImageRepository.GetPropertyImagesByPropertyIdAsync(propertyId);
            if (images == null || !images.Any())
                throw new InvalidOperationException("No hay imágenes asociadas a esta propiedad.");

            var newMain = images.FirstOrDefault(i => i.Id == imageId);
            if (newMain == null)
                throw new KeyNotFoundException("La imagen especificada no pertenece a esta propiedad.");

            // Desactivar la anterior principal (si existía)
            foreach (var img in images)
            {
                // Solo una imagen será principal (la que coincida con imageId)
                img.IsMainImage = img.Id == imageId;
            }

            await _propertyImageRepository.UpdateRangeAsync(images);

            return _mapper.Map<PropertyImageResponseDTO>(newMain);
        }

    }
}
