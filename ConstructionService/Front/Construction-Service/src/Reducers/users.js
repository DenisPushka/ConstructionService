const initialState = [
    {email: 'email1', login: 'login1', type: 'user'}
];

export default function createUsers(state = initialState, action) {
    if (action.type === 'ADD_USER') {
        if (sessionStorage.getItem('state') === 'undefined' || sessionStorage.getItem('state') === null) {
            sessionStorage.setItem('state', JSON.stringify(action.payload));
        } else {
            const userAuthentication = JSON.parse(sessionStorage.getItem('state'));
            if (action.payload.email !== userAuthentication.email) {
                sessionStorage.setItem('state', JSON.stringify(action.payload));
            }
        }
        return JSON.parse(sessionStorage.getItem('state'));
    }

    if (action.type === 'UPDATE_USER') {
        sessionStorage.setItem('state', JSON.stringify(action.payload));
    }

    const def = sessionStorage.getItem('state');
    return def === 'undefined' ? {email: '', login: ''} : JSON.parse(def);
}