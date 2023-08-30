import React from 'react';
import ReactDOM from 'react-dom/client';
import {BrowserRouter, Routes, Route} from "react-router-dom";
import {createStore, applyMiddleware } from "redux";
import {Provider} from "react-redux";
import { composeWithDevTools } from 'redux-devtools-extension';
import thunk from 'redux-thunk'
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import HomeComponent from "./Components/Home/Home";
import Equipments from "./Components/Equipments/Equipments";
import CreateOrder from "./Components/CreateOrder/CreateOrder";
import {Registration} from './Components/Registration/Registration';
import Orders from "./Components/Orders/Orders";
import Order from "./Components/Order/Order";
import reducers from "./Reducers";
import Login from "./Components/Login/Login";
import Profile from "./Components/Profile/Profile";
import SeeOrdersFromUsers from "./Components/SeeOrdersFromUser/SeeOrdersFromUsers";
import Tenant from "./Components/Tenant/Tenant";
import ContactUser from "./Components/ContactUser/ContactUser";
import ReadEquipmentsAndJobs from "./Components/ReadEquipmentsAndJobs/ReadEquipmentsAndJobs";
import UpdateProfile from "./Components/UpdateProfile/UpdateProfile";

const store = createStore(reducers, composeWithDevTools(applyMiddleware(thunk)));

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    // <React.StrictMode>
        <Provider store={store}>
            <BrowserRouter>
                <Routes>
                    <Route path="*" element={<HomeComponent/>}/>
                    <Route path="/equipments" element={<Equipments/>}/>
                    <Route path="/createOrder" element={<CreateOrder/>}/>
                    <Route path="/orders" element={<Orders/>}/>
                    <Route path="/order/:id" element={<Order/>}/>
                    <Route path="/login" element={<Login/>}/>
                    <Route path="/registration" element={<Registration/>}/>
                    <Route path="/profile" element={<Profile/>}/>
                    <Route path="/seeOrdersFromUsers" element={<SeeOrdersFromUsers/>}/>
                    <Route path="/tenant/:id" element={<Tenant/>}/>
                    <Route path="/contactUser/:id" element={<ContactUser/>}/>
                    <Route path="/readEquipmentsAndJobs" element={<ReadEquipmentsAndJobs/>}/>
                    <Route path="/updateProfile" element={<UpdateProfile/>}/>
                </Routes>
            </BrowserRouter>
        </Provider>
    /*</React.StrictMode>*/
);