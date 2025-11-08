import { useState, useRef, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { DayPicker } from "react-day-picker";
import "react-day-picker/dist/style.css";
import { faSearch, faMapMarkerAlt, faUsers, faCalendar } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import "./SearchBar.css";

export default function SearchBar() {
    const navigate = useNavigate();
    const [city, setCity] = useState("");
    const [guests, setGuests] = useState(1);
    const [checkIn, setCheckIn] = useState("");
    const [checkOut, setCheckOut] = useState("");
    const [calendarOpen, setCalendarOpen] = useState(false);
    const [range, setRange] = useState(); // { from: Date, to: Date }

    const popupRef = useRef(null);
    const toggleButtonRef = useRef(null);

    // Cierra el popup al hacer click afuera
    useEffect(() => {
        function handleClickOutside(e) {
            if (
                popupRef.current &&
                !popupRef.current.contains(e.target) &&
                toggleButtonRef.current &&
                !toggleButtonRef.current.contains(e.target)
            ) {
                setCalendarOpen(false);
            }
        }
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        // Navegamos al searchResultsPage y le pasamos los parametros
        const params = new URLSearchParams({
            city,
            guests: String(guests || 1),
            checkIn: checkIn || "",
            checkOut: checkOut || "",
        });
        navigate(`/search?${params.toString()}`);
    };

    const handleRangeSelect = (r) => {
        setRange(r);
        if (r?.from) setCheckIn(r.from.toISOString().slice(0, 10));
        else setCheckIn("");
        if (r?.to) setCheckOut(r.to.toISOString().slice(0, 10));
        else setCheckOut("");
    };

    return (
        <form className="search-bar-container" onSubmit={handleSubmit}>
            <div className="search-field">
                <FontAwesomeIcon icon={faMapMarkerAlt} className="icon" />
                <input
                    type="text"
                    placeholder="A donde viajas?"
                    value={city}
                    onChange={(e) => setCity(e.target.value)}
                />
            </div>

            <div className="search-field date-field">
                <div className="date-trigger" ref={toggleButtonRef}>
                    <FontAwesomeIcon icon={faCalendar} className="icon calendar-icon" onClick={() => setCalendarOpen((s) => !s)} />
                    <input
                        type="text"
                        placeholder="Check-in"
                        value={checkIn}
                        readOnly
                        onClick={() => setCalendarOpen((s) => !s)}
                    />
                    <input
                        type="text"
                        placeholder="Check-out"
                        value={checkOut}
                        readOnly
                        onClick={() => setCalendarOpen((s) => !s)}
                    />
                </div>

                {/* Popup del DayPicker */}
                {calendarOpen && (
                    <div className="calendar-popup" ref={popupRef}>
                        <DayPicker
                            mode="range"
                            selected={range}
                            onSelect={handleRangeSelect}
                            numberOfMonths={1}
                        />
                        <div className="calendar-actions">
                            <button
                                type="button"
                                className="btn btn-sm btn-outline-secondary me-2"
                                onClick={() => {
                                    setRange(undefined);
                                    setCheckIn("");
                                    setCheckOut("");
                                }}
                            >
                                Limpiar
                            </button>
                            <button
                                type="button"
                                className="btn btn-sm btn-primary"
                                onClick={() => setCalendarOpen(false)}
                            >
                                Cerrar
                            </button>
                        </div>
                    </div>
                )}
            </div>


            <div className="search-field">
                <FontAwesomeIcon icon={faUsers} className="icon" />
                <input
                    type="number"
                    min="1"
                    value={guests}
                    onChange={(e) => setGuests(e.target.value ? Number(e.target.value) : "")}
                />
            </div>

            <button type="submit" className="search-button">
                <FontAwesomeIcon icon={faSearch} className="me-2" />
                Buscar
            </button>
        </form>
    );
}
