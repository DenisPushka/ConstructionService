import React, {Component} from "react";
import {connect} from "react-redux";
import Header from "../Header/Header";
import './UpdateProfile.css';

class UpdateProfile extends Component {

    state = {
        type: 'user',
        user: null,
        company: null,
        handcraft: null,
        updateUser: null,
        updateCompany: null,
        updateHandcraft: null
    };

    constructor(props) {
        super(props);

        if (Object.hasOwn(props.user, 'subscription')) {
            this.state.type = 'company';
            this.setState({company: props});
            this.setState({updateCompany: props});
            this.setState({type: 'company'});
            this.state.company = props.user;
            this.state.updateCompany = props.user;
        } else {
            this.state.user = props.user;
            this.state.updateUser = props.user;
            this.setState({user: props.user});
            this.setState({updateUser: props.user});
        }

        this.updateNameCompany = this.updateNameCompany.bind(this);
        this.updateDescriptionCompany = this.updateDescriptionCompany.bind(this);
        this.updatePhoneCompany = this.updatePhoneCompany.bind(this);
        this.updateLinkCompany = this.updateLinkCompany.bind(this);
        this.updateCityCompany = this.updateCityCompany.bind(this);
        this.updateHomeCompany = this.updateHomeCompany.bind(this);
        this.updateStreetCompany = this.updateStreetCompany.bind(this);
        this.updateProfileCompany = this.updateProfileCompany.bind(this);

        this.updateFirstName = this.updateFirstName.bind(this);
        this.updateLastName = this.updateLastName.bind(this);
        this.updatePatronymic = this.updatePatronymic.bind(this);
        this.updateLinkVk = this.updateLinkVk.bind(this);
        this.updateLinkTelegram = this.updateLinkTelegram.bind(this);
        this.updatePhoneUser = this.updatePhoneUser.bind(this);
        this.updateCityNameUser = this.updateCityNameUser.bind(this);
        this.updateUser = this.updateUser.bind(this);
    }

    updateNameCompany(event) {
        event.preventDefault();
        this.setState({updateCompany: {...this.state.updateCompany, name: event.target.value}});
    }

    updateDescriptionCompany(event) {
        event.preventDefault();
        this.setState({updateCompany: {...this.state.updateCompany, description: event.target.value}});
    }

    updatePhoneCompany(event) {
        event.preventDefault();
        this.setState({updateCompany: {...this.state.updateCompany, phone: event.target.value}});
    }

    updateLinkCompany(event) {
        event.preventDefault();
        this.setState({updateCompany: {...this.state.updateCompany, link: event.target.value}});
    }

    updateCityCompany(event) {
        event.preventDefault();
        this.setState({updateCompany: {...this.state.updateCompany, cityName: event.target.value}});
    }

    updateHomeCompany(event) {
        event.preventDefault();
        this.setState({updateCompany: {...this.state.updateCompany, home: event.target.value}});
    }

    updateStreetCompany(event) {
        event.preventDefault();
        this.setState({updateCompany: {...this.state.updateCompany, street: event.target.value}});
    }

    async updateProfileCompany(event) {
        let form = new FormData();
        form.append('name', this.state.updateCompany.name);
        form.append('description', this.state.updateCompany.description);
        form.append('email', this.state.updateCompany.email);
        form.append('password', this.state.updateCompany.password);
        form.append('link', this.state.updateCompany.link);
        form.append('home', this.state.updateCompany.home);
        form.append('street', this.state.updateCompany.street);
        form.append('phone', this.state.updateCompany.phone);
        form.append('cityName', this.state.updateCompany.cityName);
        form.append('subscription', this.state.updateCompany.subscription);

        await fetch('/api/Company/UpdateCompany', {
            method: 'POST',
            body: form
        }).then();
    }

    updateFirstName(event) {
        event.preventDefault();
        this.setState({updateUser: {...this.state.updateUser, name: event.target.value}});
    }

    updateLastName(event) {
        event.preventDefault();
        this.setState({updateUser: {...this.state.updateUser, lastName: event.target.value}});
    }

    updatePatronymic(event) {
        event.preventDefault();
        this.setState({updateUser: {...this.state.updateUser, patronymic: event.target.value}});
    }

    updateLinkVk(event) {
        event.preventDefault();
        this.setState({updateUser: {...this.state.updateUser, linkVk: event.target.value}});
    }

    updateLinkTelegram(event) {
        event.preventDefault();
        this.setState({updateUser: {...this.state.user, linkTelegram: event.target.value}});
    }

    updateCityNameUser(event) {
        event.preventDefault();
        this.setState({updateUser: {...this.state.updateUser, cityName: event.target.value}});
    }

    updatePhoneUser(event) {
        event.preventDefault();
        this.setState({updateUser: {...this.state.updateUser, phone: event.target.value}});
    }

    async updateUser(event) {
        let form = new FormData();
        form.append('name', this.state.updateUser.name);
        form.append('lastName', this.state.updateUser.lastName);
        form.append('patronymic', this.state.updateUser.patronymic);
        form.append('email', this.state.updateUser.email);
        form.append('password', this.state.updateUser.password);
        form.append('countMadeOrders', this.state.updateUser.countMadeOrders);
        form.append('dateOfBrith', this.state.updateUser.dateOfBrith);
        form.append('image', this.state.updateUser.image);
        form.append('linkVk', this.state.updateUser.linkVk);
        form.append('linkTelegram', this.state.updateUser.linkTelegram);
        form.append('phone', this.state.updateUser.phone);
        form.append('cityName', this.state.updateUser.cityName);

        await fetch('/api/User/UpdateUser', {
            method: 'POST',
            body: form
        }).then();
    }

    render() {
        return (
            <>
                <Header/>
                {
                    this.state.type === 'user' &&
                    <div className={'containerProfile'}>
                        <div className={'Field'}>
                            {this.state.user.name}
                            <input className='text-field_input_profile' type={"text"} placeholder="Имя" 
                                   onChange={this.updateFirstName}/>
                        <br/>
                        {this.state.user.lastName}
                        <input className='text-field_input_profile' placeholder="Фамилия"
                               onChange={this.updateLastName}/>
                        <br/>
                        {this.state.user.patronymic}
                        <input className='text-field_input_profile' placeholder="Отчество"
                               onChange={this.updatePatronymic}/>
                        </div>
                        
                        <div className={'Field'}>
                            Ссылка вк: {this.state.user.linkVk}
                            <input className='text-field_input_profile' placeholder="Ссылка на вк"
                                   onChange={this.updateLinkVk}/>
                            <br/>
                            Ссылка телеграм: {this.state.user.linkTelegram}
                            <input className='text-field_input_profile' placeholder="Ссылка на телеграм"
                                   onChange={this.updateLinkTelegram}/>
                        </div>
                        <div class={'Field'}>
                            Телефон: {this.state.user.phone}
                            <input className='text-field_input_profile' placeholder="Телефон"
                                   onChange={this.updatePhoneUser}/>
                            <br/>
                            Город: {this.state.user.cityName}
                            <input className='text-field_input_profile' placeholder="Город"
                                   onChange={this.updateCityNameUser}/>
                        </div>

                        <div className={'Field'}>
                            <b>Почта/логин</b>: {this.state.user.email}
                        </div>

                        <div className={'Field'}>
                            Количество заказов: {this.state.user.countMadeOrders}
                        </div>
                        <div className={'for_button_subscription'}>
                            <button className="btn btn-dark for_button_subscription" type="button"
                                    onClick={this.updateUser}>
                                Изменить профиль
                            </button>
                        </div>

                    </div>
                }
                {
                    this.state.type === 'company' &&
                    <div className={'containerProfile'}>
                        <div className={'Field name_company'}>
                            {this.state.company.name}
                            <input className='text-field_input_profile' placeholder="Название компании"
                                   onChange={this.updateNameCompany}/>
                        </div>
                        <div className={'Field'} align={'center'}>
                            {this.state.company.description}
                            <input className='text-field_input_profile' placeholder="Описание"
                                   onChange={this.updateDescriptionCompany}/>
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
                            <input className='text-field_input_profile' placeholder="Телефон"
                                   onChange={this.updatePhoneCompany}/>
                        </div>
                        <div className={'Field'}>
                            <b>Почта/логин: </b> {this.state.company.email}
                        </div>
                        <div className={'Field'}>
                            <b>Пароль: </b> {this.state.company.password}
                        </div>
                        <div className={'Field'}>
                            <b>Ссылка на сайт компании: </b> {this.state.company.link}
                            <input className='text-field_input_profile' placeholder="Ссылка на сайт"
                                   onChange={this.updateLinkCompany}/>
                        </div>
                        <hr/>
                        <div className={'for_subscription'}>
                            Адрес
                        </div>
                        <div className={'Field'}>
                            <b>Город: </b> {this.state.company.cityName}
                            <input className='text-field_input_profile' placeholder="Город"
                                   onChange={this.updateCityCompany}/>
                        </div>
                        <div className={'Field'}>
                            <b>Улица: </b> {this.state.company.street}
                            <input className='text-field_input_profile' placeholder="Улица"
                                   onChange={this.updateStreetCompany}/>
                        </div>
                        <div className={'Field'}>
                            <b>Дом: </b> {this.state.company.home}
                            <input className='text-field_input_profile' placeholder="Дом"
                                   onChange={this.updateHomeCompany}/>
                        </div>

                        <div className={'for_button_subscription'}>
                            <button className="btn btn-dark for_button_subscription" type="button"
                                    onClick={this.updateProfileCompany}>
                                Изменить профиль
                            </button>
                        </div>
                    </div>
                }
            </>
        );
    }
}

export default connect(
    state => ({
        user: state.user
    }),
    dispatch => ({
        onAddUser: (user) => {
            dispatch({type: 'ADD_USER', payload: user});
        }
    })
)(UpdateProfile);