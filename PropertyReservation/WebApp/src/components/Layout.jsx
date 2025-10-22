import React from "react";
import { Link } from "react-router-dom";

export default function Layout ({ children }) {
  return (
    <div className="d-flex flex-column min-vh-100">
      {/* Navbar */}
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
        <div className="container-fluid">
          <Link className="navbar-brand" to="/">Seminario I</Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav ms-auto">
              <li className="nav-item">
                <Link className="nav-link" to="/">Home</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/properties/new">Crear propiedad</Link>
              </li>
            </ul>
          </div>
        </div>
      </nav>

      {/* Contenido principal */}
      <main className="flex-grow-1 container-fluid px-0" style={{ paddingTop: '30px', paddingBottom: '30px' }}>
        {children}
      </main>

      {/* Footer */}
      <footer className="bg-dark text-white fixed-bottom">
        <div className="container-fluid text-center">
          &copy; {new Date().getFullYear()} Seminario I - Todos los derechos reservados
        </div>
      </footer>
    </div>
  );
}
