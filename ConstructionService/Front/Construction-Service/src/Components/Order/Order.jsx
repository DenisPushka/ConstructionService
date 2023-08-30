import {Component} from "react";
import '../Order/Order.css';
import Header from "../Header/Header";
import {useParams} from "react-router-dom";
import {connect} from "react-redux";

export class Order extends Component {
    state = {
        order: {
            id: 0,
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
    };

    constructor(props) {
        super(props);
        this.TakeOrder = this.TakeOrder.bind(this);
    }

    async componentDidMount() {
        const id = window.location.pathname.split('/')[2];
        await fetch('/api/User/GetOrder/' + id, {
            method: 'GET'
        }).then(r => {
            r.json().then(async data => {
                this.state.order = data;
                this.setState({order: data});
            });
        });
    }

    async TakeOrder(event) {
        event.preventDefault();
        const id = window.location.pathname.split('/')[2];
        let form = new FormData();
        form.append('Login', this.props.user.email);
        form.append('Password', this.props.user.password);
        await fetch('/api/Company/TakeOrder/' + id, {
            method: 'POST',
            body: form
        }).then();
        
        window.location.pathname = '/contactUser/' + id;
    }

    render() {
        const image = new Image();
        image.src = 'data:image/png;base64,' + this.state.order.photo;
        console.log(this.state.order);
        return (
            <>
                <Header/>
                <div className={'card_order_o'}>
                    <div>
                        <h4>{this.state.order.miniDescription}</h4>
                        <h5>{this.state.order.description}</h5>
                        <hr/>
                        <h5><b>Цена: </b>{this.state.order.price} рублей</h5>
                        <img src={image.src} alt={image.title}/>
                        <h5><b>Категория работы:</b> {this.state.order.categoryJob}</h5>
                        <h5><b>Работа:</b> {this.state.order.job}</h5>
                    </div>

                    <div align={'center'}>
                        <a href={"/orders"} className="btn btn-outline-success" onClick={this.TakeOrder}>Принять</a>
                        <a href={"/orders"} className="btn btn-outline-danger button_get">Назад</a>
                    </div>
                </div>
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
(Order);