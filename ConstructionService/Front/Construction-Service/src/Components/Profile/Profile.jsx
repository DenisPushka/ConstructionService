import React, {Component} from "react";
import {connect} from "react-redux";
import Header from "../Header/Header";
import './Profile.css';
import {Nav} from "react-bootstrap";

class Profile extends Component {

    state = {
        type: 'user',
        user: null,
        company: null,
        handcraft: null,
        subscriptions: [
            {
                id: 0,
                description: '',
                price: 0
            }
        ],
        choiceSubscription: '',
        orders: []
    };

    constructor(props) {
        super(props);
        
        // Check the field for understand user.
        if (Object.hasOwn(props.user, 'subscription')) {
            this.state.type = 'company';
            this.setState({company: props});
            this.setState({type: 'company'});
            this.state.company = props.user;
        } else {
            this.state.user = props.user;
            this.setState({user: props.user});
        }

        this.changeSubscription = this.changeSubscription.bind(this);
        this.changeSubscriptionToBack = this.changeSubscriptionToBack.bind(this);
        this.getOrders = this.getOrders.bind(this);
        this.removeOrder = this.removeOrder.bind(this);
    }

    async componentDidMount() {
        await fetch('/api/Subscription/getSubscriptions', {
            method: 'GET'
        }).then(r => {
            r.json().then(async data => {
                this.setState({subscriptions: data});
            });
        });
    }

    changeSubscription(event) {
        event.preventDefault();
        this.setState({choiceSubscription: event.target.value});
        console.log(event.target.value);
    }

    async changeSubscriptionToBack(event) {
        event.preventDefault();
        await this.setState({company: {...this.state.company, subscription: this.state.choiceSubscription}});
        let form = new FormData();
        form.append('email', this.state.company.email);
        form.append('password', this.state.company.password);
        form.append('subscription', this.state.company.subscription);
        await fetch('/api/Company/UpdateSubstring', {
            method: 'POST',
            body: form
        }).then(r => {
            r.json().then(async data => {
                console.log(data);
                this.props.onUpdateUser(data);
            });
        });
    }

    async getOrders(event) {
        event.preventDefault();
        let form = new FormData();
        form.append('login', this.state.company.email);
        form.append('password', this.state.company.password);
        await fetch('/api/Company/GetOrders', {
            method: 'POST',
            body: form
        }).then(r => {
            r.json().then(async data => {
                this.setState({orders: data});
            });
        });
    }

    async removeOrder(event, args) {
        await fetch('/api/Company/RemoveOrder/' + args, {
            method: 'DELETE'
        }).then();

        await this.getOrders(event);
    }

    render() {
        console.log(this.state);
        return (
            <>
                <Header/>
                {
                    this.state.type === 'user' &&
                    <div className={'containerProfile'}>

                        <div className={'Field'}>
                            {this.state.user.lastName} {this.state.user.name} {this.state.user.patronymic}
                        </div>
                        <div className={'Field'}>
                            Ссылка вк: {this.state.user.linkVk} <br/>
                            Ссылка телеграм: {this.state.user.linkTelegram}
                        </div>
                        <div class={'Field'}>
                            Телефон: {this.state.user.phone}
                            <br/>
                            Город: {this.state.user.cityName}
                        </div>

                        <div className={'Field'}>
                            <b>Почта/логин</b>: {this.state.user.email}
                        </div>

                        <div className={'Field'}>
                            Количество заказов: {this.state.user.countMadeOrders}
                        </div>
                        <Nav.Link type="submit" className="btn btn-outline-danger for_button_order"
                                  href="/seeOrdersFromUsers">Просмотреть заказы
                        </Nav.Link>

                    </div>
                }
                {
                    this.state.type === 'company' &&
                    <div className={'containerProfile'}>
                        <div className={'Field name_company'}>
                            {this.state.company.name}
                        </div>
                        <div className={'Field'} align={'center'}>
                            {this.state.company.description}
                        </div>
                        <hr/>
                        <div className={'Field'}>
                            <b>Место в рейтинге:</b> {this.state.company.rating}
                        </div>
                        <div className={'Field'}>
                            <b>Название подписки: </b> {this.state.company.subscription}
                        </div>
                        <div className={'Field'}>
                            <b>Телефон: </b> {this.state.company.phone}
                        </div>
                        <div className={'Field'}>
                            <b>Почта/логин: </b> {this.state.company.email}
                        </div>
                        <div className={'Field'}>
                            <b>Пароль: </b> {this.state.company.password}
                        </div>
                        <div className={'Field'}>
                            <b>Ссылка на сайт компании: </b> {this.state.company.link}
                        </div>
                        <hr/>
                        <div className={'for_subscription'}>
                            Адрес
                        </div>
                        <div className={'Field'}>
                            <b>Город: </b> {this.state.company.cityName}
                        </div>
                        <div className={'Field'}>
                            <b>Улица: </b> {this.state.company.street}
                        </div>
                        <div className={'Field'}>
                            <b>Дом: </b> {this.state.company.home}
                        </div>
                        <hr/>
                        <div className="Field">
                            <div className={'for_subscription'}>Изменение подписки</div>
                            <br/>
                            <h5><b>Варианты:</b></h5>
                            {this.state.subscriptions && this.state.subscriptions.map((subscription) => {
                                return (
                                    <div>
                                        {subscription.description} - {subscription.price}
                                    </div>
                                );
                            })
                            }

                            <br/><h5>Выберите из вышеперечисленных подписок подходящую для вас:</h5>
                            <select className="form-control"
                                    onChange={this.changeSubscription}>
                                {
                                    this.state.subscriptions && this.state.subscriptions.map((subscription) => {
                                        return (
                                            <option>
                                                {subscription.description}
                                            </option>
                                        );
                                    })
                                }
                            </select>

                        </div>
                        <div className={'for_button_subscription'}>
                            <button className="btn btn-dark for_button_subscription" type="button"
                                    onClick={this.changeSubscriptionToBack}>
                                Изменить подписку
                            </button>
                        </div>
                        <hr/>
                        <div className={'for_button_subscription'}>
                            <button className="btn btn-dark for_button_subscription" type="button"
                                    onClick={this.getOrders}>
                                Просмотреть свои заказы
                            </button>
                            <Nav.Link className="btn btn-outline-dark " type="button" href='/readEquipmentsAndJobs'>
                                Просмотреть оборудование на аренду или предлагаемую работу
                            </Nav.Link>
                        </div>
                        {
                            this.state.orders !== null && this.state.orders.map((order) => {
                                const image = new Image();
                                image.src = 'data:image/png;base64,' + order.example;
                                return (
                                    <div className={'for_order'}>
                                        <h4>{order.miniDescription}</h4>
                                        <h5>{order.description}</h5>
                                        <hr/>
                                        <h5><b>Цена: </b>{order.price} рублей</h5>
                                        <img src={image.src} alt={image.title}/>
                                        <h5><b>Категория работы:</b> {order.categoryJob}</h5>
                                        <h5><b>Работа:</b> {order.job}</h5>
                                        <div className={'for_button_subscription'}>
                                            <button className="btn btn-danger for_button_subscription" type="button"
                                                    onClick={(event) => this.removeOrder(event, order.id)}>
                                                Отказаться
                                            </button>
                                        </div>
                                        <hr className={'for_hr'}/>
                                    </div>
                                );
                            })
                        }
                    </div>
                }
                {
                    <div className={'for_button_update_profile'}>
                        <Nav.Link className="btn btn-success button_choice" type="button" href={'/updateProfile'}>
                            Редактировать профиль
                        </Nav.Link>
                    </div>
                }
            </>
        );
    }
}

export default connect(state => (
        {
            user: state.user
        }),
    dispatch => ({
        onUpdateUser: (user) => {
            dispatch({type: 'UPDATE_USER', payload: user});
        }
    })
)
(Profile);