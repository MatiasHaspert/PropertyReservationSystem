import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getPropertyDetails } from "../services/propertyService";
import Carousel from "../components/Carousel";
import Reviews from "../components/Reviews";
import "bootstrap/dist/css/bootstrap.min.css";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faBed, faBath, faStar } from '@fortawesome/free-solid-svg-icons';
import AvailabilityCalendar from "../components/AvailabilityCalendar/AvailabilityCalendar";
import ReservationPanel from "../components/ReservationPanel";


export default function PropertyDetailPage() {
    const { id } = useParams();
    const [property, setProperty] = useState(null);
    const [selectedRange, setSelectedRange] = useState(null);

    useEffect(() => {
        loadPropertyDetails();
    }, [id]);

    const loadPropertyDetails = async () => {
        try {
            const data = await getPropertyDetails(id);
            setProperty(data);
        } catch (error) {
            console.error("Error al cargar el detalle de la propiedad:", error);
        }
    };

    if (!property) {
        return <div className="container mt-4">La propiedad no existe o se ha eliminado...</div>;
    }

    return (
        <div className="container mt-4">
            <div className="row">
                {/* Columna izquierda: info + calendario */}
                <div className="col-lg-8">
                    <h4 className="mb-3">{property.title}</h4>
                    <p className="fw-bold mb-2">${property.nightlyPrice.toLocaleString()} / noche</p>

                    <div className="d-flex justify-content-start gap-3 mb-2">
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

                    <div className="mb-3">
                        <FontAwesomeIcon icon={faStar} className="text-warning me-1" />
                        {property.averageRating.toFixed(1)}
                    </div>

                    {property.amenities.length > 0 && (
                        <div className="mt-3">
                            <div className="d-flex flex-wrap gap-2">
                                {property.amenities.map(a => (
                                    <span
                                        key={a.id}
                                        className="badge rounded-pill border py-2 px-2"
                                        style0={{
                                            backgroundColor: "transparent",
                                            borderColor: "#000000ff",
                                            color: "#494848ff",
                                            fontSize: "0.85rem"
                                        }}
                                    >
                                        {a.name}
                                    </span>
                                ))}
                            </div>
                        </div>
                    )}

                    <div className="mt-4">
                        <Carousel images={property.images} />
                    </div>

                    <div className="mt-4">
                        <AvailabilityCalendar propertyId={property.id} onRangeSelected={setSelectedRange} />
                    </div>

                    <hr className="my-4 bg-light-subtle" />
                    <Reviews reviews={property.reviews} />
                </div>

                {/* Columna derecha: panel de reserva */}
                <div className="col-lg-4 position-relative">
                    <div className="reservation-sticky"> 
                        <ReservationPanel
                            selectedRange={selectedRange}
                            nightlyPrice={property.nightlyPrice}
                            maxGuests={property.maxGuests}
                            propertyId={property.id}
                            />
                    </div>
                </div>


            </div>
        </div>
    );
}

/*
Luego cuando agreguemos autenticacion podes ocultar el panel de reservas a los usuario no registrados
<div className="col-lg-4 position-relative">
    {isAuthenticated ? (
        <div className="reservation-sticky">
            <ReservationPanel
                selectedRange={selectedRange}
                nightlyPrice={property.nightlyPrice}
                maxGuests={property.maxGuests}
                propertyId={property.id}
            />
        </div>
    ) : (
        <div className="alert alert-info p-3">
            <p className="mb-2">Para reservar esta propiedad necesitas una cuenta.</p>
            <a href="/login" className="btn btn-primary w-100">Iniciar sesión</a>
        </div>
    )}
</div>

*/
