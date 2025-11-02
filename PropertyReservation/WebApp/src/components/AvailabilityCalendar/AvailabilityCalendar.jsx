import { useEffect, useState } from "react";
import { DayPicker } from "react-day-picker";
import "react-day-picker/dist/style.css";
import { eachDayOfInterval, parseISO } from "date-fns";
import { getPropertyCalendar } from "../../services/propertyService";
import "./AvailabilityCalendar.css";

export default function AvailabilityCalendar({ propertyId, onRangeSelected}) {
    const [availability, setAvailability] = useState(null);
    const [selectedRange, setSelectedRange] = useState();

    useEffect(() => {
        loadAvailability();
    }, [propertyId]);

    const loadAvailability = async () => {
        try {
            const data = await getPropertyCalendar(propertyId);
            setAvailability(data);
        } catch (err) {
            console.error("Error al cargar disponibilidad:", err);
        }
    };

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

    const handleSelectRange = (range) => {
        if (!range?.from) {
            setSelectedRange(range);
            onRangeSelected?.(range);
            return;
        }

        // Si el usuario solo seleccionó el primer día
        if (!range?.to) {
            setSelectedRange(range);
            onRangeSelected?.(range);
            return;
        }

        const allDays = eachDayOfInterval({ start: range.from, end: range.to });

        // Encontrar el primer día deshabilitado dentro del rango
        const firstDisabled = allDays.find(day =>
            disabledDays.some(disabled =>
                disabled.toDateString() === day.toDateString()
            )
        );

        if (firstDisabled) {
            // "Cortamos" el rango justo antes del día deshabilitado
            const validEnd = new Date(firstDisabled);
            validEnd.setDate(validEnd.getDate() - 1);

            // Solo actualizar si el rango válido es mayor o igual a la fecha de inicio
            if (validEnd >= range.from) {
                setSelectedRange({ from: range.from, to: validEnd });
                onRangeSelected?.(range);
            } else {
                // Si el primer día ya está deshabilitado, no marcamos nada
                setSelectedRange(undefined);
                onRangeSelected?.(undefined);
            }
            return;
        }

        setSelectedRange(range);
        onRangeSelected?.(range);
    };



    return (
        <div className="border rounded p-3 bg-light">
            <h5 className="mb-3">Disponibilidad</h5>

            <DayPicker
                mode="range"
                selected={selectedRange}
                onSelect={handleSelectRange}
                disabled={disabledDays}
                modifiers={{
                    checkIn: selectedRange?.from,
                    checkOut: selectedRange?.to,
                }}
                modifiersAttributes={{
                    checkIn: { title: "Día de check-in" },
                    checkOut: { title: "Día de check-out" },
                }}
                modifiersClassNames={{
                    selected: "selected-day",
                    disabled: "disabled-day",
                    range_start: "rdp-day_range_start",
                    range_end: "rdp-day_range_end",
                    range_middle: "rdp-day_range_middle",
                }}
                styles={{
                    caption: { color: "#333", fontWeight: "bold" },
                    head_cell: { color: "#555", fontWeight: 500 },
                    day: { borderRadius: 0 },
                }}
            />

            <div className="mt-3">
                <span className="badge bg-info me-2">Seleccionado</span>
                <span className="badge bg-secondary me-2">Reservado</span>
            </div>
        </div>
    );
}
