import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getProperties, getPropertiesByUser, deleteProperty, updateProperty } from "../services/propertyService";
import PropertyCard from "../components/PropertyCard";

export default function MyPropertiesPage() {
    const navigate = useNavigate();
    const [properties, setProperties] = useState([]);
    const [errorMessage, setErrorMessage] = useState(false);

    // Simulación de usuario logueado (temporal, hasta que tengas auth) TODO: Borrar
    const userId = 1;

    useEffect(() => {
        loadProperties();
    }, [userId]);

    const loadProperties = async () => {
        try {
            // const data = await getPropertiesByUser(userId);
            const res = await getProperties();
            setProperties(res.data);
        } catch (error) {
            console.error("Error cargando propiedades:", error);
            setErrorMessage("Error al cargar tus propiedades. Intenta nuevamente más tarde.");
        }
    };

    const handleEdit = (id) => {
        navigate(`/properties/edit/${id}`);
    };

    const handleDelete = async (id) => {
        if (!window.confirm("¿Seguro que deseas eliminar esta propiedad?")) return;
            try {
                await deleteProperty(id);
                setProperties(properties.filter((p) => p.id !== id));
            } catch (error) {
                console.error("Error al eliminar propiedad:", error);
                setErrorMessage("No se pudo eliminar la propiedad. Intenta nuevamente más tarde.");
            }
    };

    if (errorMessage) {
        return <div className="container-fluid mt-4">{{ errorMessage }}</div>;
    }

    return (
        <div className="container-fluid mt-4">
            <h3 className="mb-4">Mis propiedades</h3>
            <div className="d-flex flex-wrap gap-3 justify-content-center">
                {properties.length === 0 ? (
                    <p>No tienes propiedades publicadas.</p>
                ) : (
                    properties.map((p) => (
                        <PropertyCard
                            key={p.id}
                            property={p}
                            showActions={true}
                            onDelete={handleDelete}
                            onEdit={handleEdit}
                        />
                    ))
                )}
            </div>
        </div>
    );
}
