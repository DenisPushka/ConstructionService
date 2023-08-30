import {Component} from "react";
import './Orders.css';
import Header from "../Header/Header";
import {connect} from "react-redux";

class Orders extends Component {
    state = {
        Orders: [
            {
                Order: {
                    miniDescription: [],
                    description: [],
                    getOrder: 0,
                    completedOrder: 0,
                    price: 0,
                    nameCity: [],
                    date: [],
                    categoryJob: [],
                    job: [],
                    example: undefined,
                    userId: 0,
                    companyId: 0,
                    handcraftId: 0
                }
            }
        ],
        getTakenOrders: 0
    };

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
        let response = await fetch('api/User/getOrders', {
            method: 'GET'
        });

        let result = await response.json();
        this.setState({...this.state, ...{Orders: result}});
        console.log(this.state.Orders);
        
        if (this.props.user === null) return;
        
        let form = new FormData();
        form.append('Login', this.props.user.email)
        form.append('Password', this.props.user.password)
        await fetch('/api/Company/GetOrdersTaken', {
            method: 'POST',
            body: form
        }).then(r => {
            r.json().then(async data => {
                console.log(data);
                this.setState({getTakenOrders: data})
            });
        });
    }

    render() {
        return (
            <>
                <Header/>
                <h1 className={'head'} align={'center'}>Заказы:</h1>
                {
                    this.state.Orders.length && this.state.Orders.map((order) => {
                        const image = new Image();
                        image.src = 'data:image/png;base64,' + order.example;
                        return (
                            <div className="card_order">

                                <h2>{order.miniDescription}</h2>
                                <p className={'for_info_order'}><b>Работа: </b>{order.job}</p>
                                <p className={'for_info_order'}><b>Цена:</b> {order.price}</p>
                                <img className="img_order" src={image.src} alt={image.title}/>
                                {this.props.user !== null && this.state.getTakenOrders > 0 &&
                                    <a href={"/order/" + order.id} className="btn btn-primary button_order">
                                        Перейти к заказу</a>
                                }
                            </div>
                        );
                    })
                }
            </>
        );
    }
}

export default connect(state => (
        {
            user: state.user
        }),
    dispatch => {
    }
)
(Orders);