import {combineReducers} from 'redux';
import {routerReducer} from 'react-router-redux'

import user from './users';
import orders from './orders';

export default combineReducers({
    routing: routerReducer,
    user,
    orders
});