import { useEffect, useState } from "react";
import { DayPicker } from "react-day-picker";
import "react-day-picker/dist/style.css";
import { addDays, eachDayOfInterval, parseISO } from "date-fns";
import { getPropertyCalendar } from "../services/propertyService";
export default function AvailabilityCalendar({ propertyId }) {
    const [availability, setAvailability] = useState(null);

    useEffect(() => {
        const loadAvailability = async () => {
            try {
                const data = await getPropertyCalendar(propertyId);
                setAvailability(data);
            } catch (err) {
                console.error("Error al cargar disponibilidad:", err);
            }
        };
        loadAvailability();
    }, [propertyId]);

    if (!availability) return <p>Cargando calendario...</p>;

    // Convertir reservedRanges a fechas deshabilitadas
    const disabledDays = availability.reservedRanges.flatMap(range =>
        eachDayOfInterval({
            start: parseISO(range.startDate),
            end: parseISO(range.endDate),
        })
    );

    // Convertir availableRanges a intervalos seleccionables
    const availableDays = availability.availableRanges.flatMap(range =>
        eachDayOfInterval({
            start: parseISO(range.startDate),
            end: parseISO(range.endDate),
        })
    );

    return (
        <div className="border rounded p-3 bg-light">
            <h5 className="mb-3">Disponibilidad</h5>
            <DayPicker
                mode="range"
                disabled={disabledDays}
                selected={availableDays}
                modifiersClassNames={{
                    selected: "bg-success text-white",
                    disabled: "bg-danger text-white",
                }}
            />
            <div className="mt-3">
                <span className="badge bg-success me-2">Disponible</span>
                <span className="badge bg-danger">Reservado</span>
            </div>
        </div>
    );
}
