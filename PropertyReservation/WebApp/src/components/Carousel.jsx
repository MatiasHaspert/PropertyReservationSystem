import "bootstrap/dist/js/bootstrap.bundle.min.js";


export default function Carousel({ images }) {
  if (!images || images.length === 0) return <p>No hay imágenes.</p>;

  return (
    <div id="propertyCarousel" className="carousel slide mb-4" data-bs-ride="carousel">
      <div className="carousel-inner">
        {images.map((img, idx) => (
        <div key={img.id} className={`carousel-item ${idx === 0 ? "active" : ""}`}>
          <img
            src={img.url || "https://via.placeholder.com/800x400"}
            className="d-block w-100"
            alt={`Imagen ${idx + 1}`}
            style={{ maxHeight: "400px", objectFit: "cover" }}
          />
        </div>
        ))}
      </div>
      {images.length > 1 && (
        <>
        <button className="carousel-control-prev" type="button" data-bs-target="#propertyCarousel" data-bs-slide="prev">
          <span className="carousel-control-prev-icon" aria-hidden="true"></span>
          <span className="visually-hidden">Anterior</span>
        </button>
        <button className="carousel-control-next" type="button" data-bs-target="#propertyCarousel" data-bs-slide="next">
          <span className="carousel-control-next-icon" aria-hidden="true"></span>
          <span className="visually-hidden">Siguiente</span>
        </button>
        </>
      )}
    </div>
  );
}
