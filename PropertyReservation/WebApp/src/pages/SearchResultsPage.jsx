import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { searchProperties } from "../services/propertyService";
import PropertyCard from "../components/PropertyCard";

export default function SearchResultsPage() {
    const { search } = useLocation();
    const [results, setResults] = useState([]);

    useEffect(() => {
        loadResults();
    }, [search]);

    const loadResults = async () => {
        const data = await searchProperties(search);
        setResults(data);
    };

    return (
        <div className="container mt-4">
            <h5>Resultados</h5>
            <hr />
            <div className="d-flex flex-wrap gap-3">
                {results.length === 0 ? (<p>No existen propiedades para mostrar en este momento</p>)
                :
                 (results.map((p) => <PropertyCard key={p.id} property={p} />))}
            </div>
        </div>
    );
}
