import { useEffect, useState } from "react";
import { getProperties } from "../services/propertyService";
import PropertyCard from "../components/PropertyCard"

export default function HomePage() {
    const [properties, setProperties] = useState([]);

    useEffect(() => {
        loadProperties();
    }, []);

    const loadProperties = async () => {
        try {
            const res = await getProperties();
            setProperties(res.data);
        } catch (error) {
            console.error("Error cargando propiedades:", error);
        }
    };

    return (
        <div className="container-fluid mt-4">
            <div className="d-flex flex-wrap gap-3 justify-content-center">
                {properties.length === 0 ? (
                    <p>No hay propiedades para mostrar.</p>
                ) : (
                    properties.map((p) => <PropertyCard key={p.id} property={p} />)
                )}
            </div>
        </div>
    );
}
