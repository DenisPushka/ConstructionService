const initialState = [
    {nameOrder: 'название заказа1', price: 1},
    {nameOrder: 'название заказа2', price: 2}
];

export default function createUsers(state = initialState, action) {
    if (action.type === 'ADD_ORDER') {
        return [
            ...state, action.payload
        ];
    }

    return state;
}