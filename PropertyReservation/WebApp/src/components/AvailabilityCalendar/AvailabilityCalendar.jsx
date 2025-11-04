import { useEffect, useState } from "react";
import { DayPicker } from "react-day-picker";
import "react-day-picker/dist/style.css";
import { eachDayOfInterval, parseISO, addYears, startOfToday, startOfMonth, addMonths } from "date-fns";
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

    // Días reservados (no se pueden seleccionar)
    const reservedDays = availability.reservedRanges.flatMap(range =>
        eachDayOfInterval({
            start: parseISO(range.startDate),
            end: parseISO(range.endDate),
        })
    );

    // Días disponibles de la propiedad
    const availableDays = availability.availableRanges.flatMap(range =>
        eachDayOfInterval({
            start: parseISO(range.startDate),
            end: parseISO(range.endDate),
        })
    );

    // Calcular días fuera de los rangos disponibles
    const allPossibleDays = eachDayOfInterval({
        start: startOfToday(),
        end: addYears(startOfToday(), 1), // rango de 1 año
    });

    const unavailableDays = allPossibleDays.filter(
        day => !availableDays.some(av => av.toDateString() === day.toDateString())
    );

    // Juntar reservados + fuera de disponibilidad
    const disabledDays = [...reservedDays, ...unavailableDays];

    const handleSelectRange = (range) => {
        if (!range?.from) {
            setSelectedRange(range);
            onRangeSelected?.(range);
            return;
        }

        if (!range?.to) {
            setSelectedRange(range);
            onRangeSelected?.(range);
            return;
        }

        const allDays = eachDayOfInterval({ start: range.from, end: range.to });

        // Si el rango incluye días deshabilitados, se corta
        const firstDisabled = allDays.find(day =>
            disabledDays.some(d => d.toDateString() === day.toDateString())
        );

        if (firstDisabled) {
            const validEnd = new Date(firstDisabled);
            validEnd.setDate(validEnd.getDate() - 1);

            if (validEnd >= range.from) {
                setSelectedRange({ from: range.from, to: validEnd });
                onRangeSelected?.({ from: range.from, to: validEnd });
            } else {
                setSelectedRange(undefined);
                onRangeSelected?.(undefined);
            }
            return;
        }

        setSelectedRange(range);
        onRangeSelected?.(range);
    };

    // Límite de meses visibles (desde hoy hasta 11 meses)
    const today = startOfToday();

    return (
        <div className="border rounded p-3 bg-light">
            <h5 className="mb-3">Disponibilidad</h5>

            <DayPicker
                mode="range"
                selected={selectedRange}
                onSelect={handleSelectRange}
                disabled={disabledDays}
                fromMonth={startOfMonth(today)} // primer mes visible
                toMonth={addMonths(today, 11)}  // último mes visible (11 meses más)
                numberOfMonths={2}              // 2 meses se muestran a la vez
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
