import React, { useRef, useEffect, useState, Component } from 'react';
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AccountPage from './Pages/AccountPage.js';
import ManageCarPage from './Pages/ManageCarPage.js';
import UserRouteOverviewPage from './Pages/UserRouteOverviewPage.js';
import UserRoutePage from './Pages/UserRoutePage.js';
import Navbar from './Components/Nav/Navbar.js';

function App() {

    return (
        <BrowserRouter>
            <Navbar />
            <Routes>
                <Route path="Account" element={<AccountPage />} />
                <Route path="Cars" element={<ManageCarPage />} />
                <Route path="RouteOverview" element={<UserRouteOverviewPage />} />
                <Route path="Route" element={<UserRoutePage />} />
                <Route
                    path="*"
                    element={
                        <main style={{ padding: "1rem" }}>
                            <p>There's nothing here!</p>
                        </main>
                    }
                />
            </Routes>
        </BrowserRouter>
    )
}

export default App;