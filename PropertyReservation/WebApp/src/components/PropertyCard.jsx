import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faBed, faBath, faStar } from "@fortawesome/free-solid-svg-icons";

export function PropertyCard({ property }) {
    const {
        title,
        nightlyPrice,
        maxGuests,
        bedrooms,
        bathrooms,
        address,
        averageRating,
        mainImage,
    } = property;

    const fullAddress = `${address.streetAddress}, ${address.city}, ${address.state}, ${address.country}`;

    return (
        <div className="card mb-3" style={{ width: "22rem" }}>
            {mainImage ? (
                <img
                    src={mainImage || "https://via.placeholder.com/350x200?text=Propiedad"}
                    className="card-img-top"
                    alt={title}
                    style={{ height: "200px", objectFit: "cover" }}
                />)
            : (
                <div style={{ height: "200px" }}></div>
            )
}
            <div className="card-body">
                <h5 className="card-title">{title}</h5>
                <p className="card-text fw-bold">${nightlyPrice.toLocaleString()} / noche</p>

                <div className="d-flex justify-content-between mb-2">
                    <span>
                        <FontAwesomeIcon icon={faUser} className="me-1" />
                        {maxGuests} Huéspedes
                    </span>
                    <span>
                        <FontAwesomeIcon icon={faBed} className="me-1" />
                        {bedrooms} Dormitorios
                    </span>
                    <span>
                        <FontAwesomeIcon icon={faBath} className="me-1" />
                        {bathrooms} Baños
                    </span>
                </div>

                <p className="mb-2" style={{ fontSize: "0.9rem", color: "#555" }}>
                    {fullAddress}
                </p>

                <div>
                    <FontAwesomeIcon icon={faStar} className="text-warning me-1" />
                    {averageRating.toFixed(1)}
                </div>
            </div>
        </div>
    );
}
