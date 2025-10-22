import { BrowserRouter, Routes, Route } from "react-router-dom";
import HomePage from "../pages/HomePage";
import Layout from "../components/Layout";


export default function AppRouter() {
    return (
        <BrowserRouter>
            <Layout>
                <Routes>
                    <Route path="/" element={<HomePage />} />
                </Routes>
            </Layout>
        </BrowserRouter>
    );
}
