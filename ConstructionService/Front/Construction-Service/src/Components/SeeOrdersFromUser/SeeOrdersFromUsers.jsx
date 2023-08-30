import {connect} from "react-redux";
import './SeeOrders.css';
import {Component} from "react";
import Header from "../Header/Header";

class SeeOrdersFromUsers extends Component {
    state = {
        orders: [],
        user: {}
    };

    constructor(props) {
        super(props);

        let form = new FormData();
        form.append('login', props.user.email);
        form.append('password', props.user.password);
        fetch("/api/User/ReceivingOrders", {
            method: 'POST',
            body: form
        })
            .then(res => {
                res.json().then(async (data) => {
                    if (data.length !== 0) {
                        // this.state.orders = data
                        this.setState({orders: data});
                    }
                });
            });
    }

    render() {
        console.log(this.state);
        return (
            <>
                <Header/>
                <div className={'form_orders_user'}>
                    {
                        this.state.orders !== 0 &&
                        this.state.orders.map((order) => {
                            const image = new Image();
                            image.src = 'data:image/png;base64,' + order.example;
                            return (
                                <div className={'container_order'}>
                                    <div className={'for_mini_description'}>{order.miniDescription}</div>
                                    <div className={'for_description'}>{order.description}</div>
                                    <div className={'for_field'}> Город: {order.nameCity} </div>
                                    <div className={'for_field'}>Категория работы: {order.categoryJob}</div>
                                    <div className={'for_field'}>Работа: {order.job}</div>
                                    <div className={'for_field'}>Цена: {order.price}</div>
                                    <div className={'for_field'}>Заказ взят: {order.getOrder.toString()}</div>
                                    <div className={'for_field'}>Заказ выполнен: {order.completedOrder.toString()}</div>
                                    <img className='img_order' src={image.src} alt={image.title} />
                                </div>
                            );
                        })
                    }
                </div>
            </>
        );
    }
}

export default connect(state => (
        {
            orders: state.orders,
            user: state.user
        }),
    dispatch => {
    }
)
(SeeOrdersFromUsers);