import { useEffect, useState } from "react";
import { getProperties } from "../services/propertyService";
import PropertyCard from "../components/PropertyCard"
import SearchBar from "../components/SearchBar/SearchBar";

export default function HomePage() {
  const [properties, setProperties] = useState([]);

   // Filtros
   const [location, setLocation] = useState("");
   const [checkIn, setCheckIn] = useState("");
   const [checkOut, setCheckOut] = useState("");
   const [guests, setGuests] = useState(1);

  useEffect(() => {
    loadProperties();
  }, []);

  const loadProperties = async () => {
    try {
      const data = await getProperties();
      const popularProperties = data.filter(
            (p) => p.averageRating >= 4.5
        )   
      setProperties(data);
    } catch (error) {
      console.error("Error cargando propiedades:", error);
    }
  };

   return (
      <div className="container-fluid mt-4">
          <SearchBar></SearchBar>
        <div className="d-flex justify-content-between align-items-center">
            <h5 className="m-0">Alojamientos Populares</h5>
        </div>
        <hr />
        <div className="d-flex flex-nowrap gap-3 overflow-x-auto">
            {properties.length === 0 ? (
                <p>No hay propiedades populares para mostrar en este momento.</p>
            ) : (
                properties.map((p) => <PropertyCard key={p.id} property={p} />)
            )}
        </div>
      </div>
    );
}
