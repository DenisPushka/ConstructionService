import React from "react";
import {signal, computed} from "@preact/signals";
import {Component} from "preact";

// let _store = { // должен быть глобально доступен через импорт в тех модулях где нежуна информация
//     user: {
//         name: '',
//         lastname: '',
//         countMadeOrders: 0,
//         dateOfBrith: '',
//         phone: '',
//         cityId: '',
//         photo: '',
//         email: '',
//         password: '',
//         linkTelegram: '',
//         linkVk: ''
//     },
//     company: {
//         id: 0,
//         name: '',
//         description: '',
//         rating: 0,
//         phone: '',
//         email: '',
//         password: '',
//         link: '',
//         cityName: '',
//         street: '',
//         home: ''
//     }
// };

export class StoreComponent extends Component {

    user = signal("");
    
     createAppState() {
        const user = signal("");

        return {user};
    }
}
export default StoreComponent
