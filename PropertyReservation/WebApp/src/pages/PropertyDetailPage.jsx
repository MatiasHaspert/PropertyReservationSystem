import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getPropertyDetails } from "../services/propertyService";
import Carousel from "../components/Carousel";
import Reviews from "../components/Reviews";
import "bootstrap/dist/css/bootstrap.min.css";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faBed, faBath, faStar } from '@fortawesome/free-solid-svg-icons';

export default function PropertyDetailPage() {
  const { id } = useParams();
  const [property, setProperty] = useState(null);

  useEffect(() => {
    loadPropertyDetails();
  }, [id]);

  const loadPropertyDetails = async () => {
    try {
      const res = await getPropertyDetails(id);
      setProperty(res.data);
    } catch (error) {
      console.error("Error al cargar el detalle de la propiedad:", error);
    }
  };

  if (!property) {
    return <div className="container mt-4">La propiedad no existe o se ha eliminado...</div>;
  }

  return (
    <div className="container mt-4">

      {/* Fila principal: información y carrusel */}
      <div className="row mb-4">
        {/* Columna izquierda: datos principales */}
        <div className="col-lg-6 mb-3">
          <h4 className="mb-3">{property.title}</h4>
          <p className="fw-bold mb-2">${property.nightlyPrice.toLocaleString()} / noche</p>
          <div className="d-flex justify-content-center gap-3 mb-2">
              <span>
                <FontAwesomeIcon icon={faUser} className="me-1" />
                {property.maxGuests} Huéspedes
              </span>
              <span>
                <FontAwesomeIcon icon={faBed} className="me-1" />
                {property.bedrooms} Dormitorios
              </span>
              <span>
                <FontAwesomeIcon icon={faBath} className="me-1" />
                {property.bathrooms} Baños
              </span>
          </div>

          <p className="mb-2 text-muted" style={{ fontSize: "0.9rem" }}>
              {property.address.streetAddress}, {property.address.city}, {property.address.state}, {property.address.country}, {property.address.postalCode}
          </p>

          <p>{property.description}</p>

          <div className="mb-2">
              <FontAwesomeIcon icon={faStar} className="text-warning me-1" />
              {property.averageRating.toFixed(1)}
          </div>

          {/* Amenities */}
          {property.amenities.length > 0 && (
            <div className="mt-3">
              <div className="d-flex flex-wrap gap-2">
              {property.amenities.map(a => (
                <span
                  key={a.id}
                  className="badge rounded-pill border py-2 px-2"
                  style={{ backgroundColor: "transparent", borderColor: "#000000ff", color: "#494848ff", fontSize: "0.85rem" }}
                >
                  {a.name}
                </span>
              ))}
              </div>
            </div>
          )}
        </div>

        {/* Columna derecha: carrusel */}
        <div className="col-lg-6 mb-3">
          <Carousel images={property.images} />
        </div>
      </div>

      <hr className="my-4" bg-light-subtle />
      <Reviews reviews={property.reviews} />
    </div>
  );
}
