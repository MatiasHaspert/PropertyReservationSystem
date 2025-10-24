import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "../components/Layout";
import HomePage from "../pages/HomePage";
import MyPropertiesPage from "../pages/MyPropertiesPage";
import PropertyDetailPage from "../pages/PropertyDetailPage";

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/my-properties" element={<MyPropertiesPage />} />
          <Route path="/property/:id" element={<PropertyDetailPage />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}
