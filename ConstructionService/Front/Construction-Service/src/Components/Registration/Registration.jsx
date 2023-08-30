import {Component} from "react";
import Header from "../Header/Header";
import './Registration.css';

export class Registration extends Component {
    state = {
        user: {
            id: 0,
            name: '',
            lastname: '',
            patronymic: '',
            countMadeOrders: 0,
            dateOfBrith: '',
            phone: '',
            cityName: '',
            photo: '',
            email: '',
            password: '',
            linkTelegram: '',
            linkVk: ''
        },
        company: {
            id: 0,
            name: '',
            description: '',
            rating: 0,
            subscription: '',
            phone: '',
            email: '',
            password: '',
            link: '',
            cityName: '',
            street: '',
            home: ''
        },
        type: 'Пользователь',
        Cities: [
            {
                id: 0,
                nameCity: []
            }
        ],
        subscriptions: [
            {
                id: 0,
                description: '',
                price: 0
            }
        ]
    };
    
    constructor(props) {
        super(props);
        
        // User
        this.changeName = this.changeName.bind(this);
        this.changeLastname = this.changeLastname.bind(this);
        this.changePatronymic = this.changePatronymic.bind(this);
        this.changeDateOfBrith = this.changeDateOfBrith.bind(this);
        this.changePhoneUser = this.changePhoneUser.bind(this);
        this.changeCityName = this.changeCityName.bind(this);
        this.changePhoto = this.changePhoto.bind(this);
        this.changeEmail = this.changeEmail.bind(this);
        this.changePassword = this.changePassword.bind(this);
        this.changeLinkTelegram = this.changeLinkTelegram.bind(this);
        this.changeLinkVk = this.changeLinkVk.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        //------------------------------------------

        this.handleChangeType = this.handleChangeType.bind(this);

        // Company
        this.changeNameCompany = this.changeNameCompany.bind(this);
        this.changeDescription = this.changeDescription.bind(this);
        this.changePhoneCompany = this.changePhoneCompany.bind(this);
        this.changeEmailCompany = this.changeEmailCompany.bind(this);
        this.changeCompanyPassword = this.changeCompanyPassword.bind(this);
        this.changeLink = this.changeLink.bind(this);
        this.changeNameCompany = this.changeNameCompany.bind(this);
        this.changeSubscriptionId = this.changeSubscriptionId.bind(this);
        this.changeStreet = this.changeStreet.bind(this);
        this.changeHome = this.changeHome.bind(this);
        this.onSubmitCompany = this.onSubmitCompany.bind(this);
    }

    async componentDidMount() {
        let response = await fetch('/api/Cities/GetAll', {
            method: 'GET'
        });
        let result = await response.json();
        await this.setState({Cities: {...this.state.Cities, result}});

        response = await fetch('/api/Subscription/getSubscriptions', {
            method: 'GET'
        });
        result = await response.json();
        await this.setState({subscriptions: {...this.state.subscriptions, result}});
    }

    // Event for user
    async changeName(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, name: event.target.value}});
    }

    async changeLastname(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, lastname: event.target.value}});
    }

    async changePatronymic(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, patronymic: event.target.value}});
    }

    async changeDateOfBrith(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, dateOfBrith: event.target.value}});
    }

    async changePhoneUser(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, phone: event.target.value}});
    }

    async changeCityName(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, cityName: event.target.value}});
    }

    async changePhoto(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, photo: event.target.value}});
    }

    async changeEmail(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, email: event.target.value}});
    }

    async changePassword(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, password: event.target.value}});
    }

    async changeLinkTelegram(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, linkTelegram: event.target.value}});
    }

    async changeLinkVk(event) {
        await event.preventDefault();
        await this.setState({user: {...this.state.user, linkVk: event.target.value}});
    }

    async onSubmit(event) {
        await event.preventDefault();
        let form = new FormData();
        console.log(this.state.user);
        form.append('User', this.state.user);
        let response = await fetch('/api/User/Add', {
            method: 'POST',
            body: form
        });

        let result = await response.json();
        console.log(result);
    }

    // ------------------------------------------


    // Event for company
    async changeNameCompany(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, name: event.target.value}});
    }

    async changeDescription(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, description: event.target.value}});
    }

    async changePhoneCompany(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, phone: event.target.value}});
    }

    async changeEmailCompany(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, email: event.target.value}});
    }

    async changeCompanyPassword(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, password: event.target.value}});
    }

    async changeSubscriptionId(event) {
        await event.preventDefault();
        let number = 0;
        this.state.subscriptions.result.map((subscr) => {
            if (subscr.description === event.target.value) {
                number = subscr.id - 1;
            }
        });
        await this.setState({user: {...this.state.user, subscription: number.toString()}});
    }

    async changeLink(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, link: event.target.value}});
    }

    async changeCityNameCompany(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, cityName: event.target.value}});
    }

    async changeStreet(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, street: event.target.value}});
    }

    async changeHome(event) {
        await event.preventDefault();
        await this.setState({company: {...this.state.company, home: event.target.value}});
    }

    async onSubmitCompany(event) {
        await event.preventDefault();
        let form = new FormData();
        console.log(this.state);
        form.append('company', this.state.company);

        let response = await fetch('/api/Company/AddCompany', {
            method: 'POST',
            body: form
        });

        let result = await response.json();
        console.log(result);
    }

    async handleChangeType(event) {
        await event.preventDefault();
        this.setState({
            type: event.target.name
        });
    };


    render() {
        let check = [];
        if (this.state.type === 'Пользователь') {
            if (this.state.user.name !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.user.lastname !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.user.phone !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.user.cityName !== 0) {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.user.email !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.user.password !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.user.linkVk !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.user.linkTelegram !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
        } else if (this.state.type === 'Компания') {
            if (this.state.company.name !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.company.description !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.company.phone !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.company.email !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.company.password !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.company.link !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.company.cityName !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.company.street !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }
            if (this.state.company.home !== '') {
                check.push(<div className="valid-feedback">Выглядит неплохо!</div>);
                check.push("form-control is-valid");
            } else {
                check.push(<div className="invalid-feedback">Заполните данные!</div>);
                check.push("form-control is-invalid");
            }

        }

        return (
            <div>
                <Header/>
                <h1 align={'center'} className={'header_to_registry'}>Регистрационная форма</h1>

                <form className={'for_input_type'}>
                    <button className={'btn btn-outline-secondary type_user'}
                            onClick={this.handleChangeType} name={'Пользователь'}>Пользователь
                    </button>

                    <button className={'btn btn-outline-dark type_company'}
                            onClick={this.handleChangeType} name={'Компания'}>Компания
                    </button>
                </form>
                {
                    this.state.type === 'Пользователь' &&
                    <form className={"form_render"}>
                        <div className="form-row">
                            <div className="col-md-10 mb-3">
                                <label form="validationServer01">Имя</label>
                                <input type="text" className={check[1]} id="validationServer01"
                                       placeholder="Имя" onChange={this.changeName} required/>
                                {check[0]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label form="validationServer02">Фамилия</label>
                                <input type="text" className={check[3]} id="validationServer02"
                                       placeholder="Фамилия" onChange={this.changeLastname} required/>
                                {check[2]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label form="validationServer02">Отчество</label><br/>
                                <input type="text" className={'for_patronymic'} id="validationServer02"
                                       placeholder="Отчество" onChange={this.changePatronymic} required/>

                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer03"><h3>Дата рождения:</h3></label>
                                <input type="text" className={'date_of_brith'} id="validationServer03"
                                       placeholder="дд.мм.гггг" onChange={this.changeDateOfBrith} required/>
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer03">Телефон</label>
                                <input type="text" className={check[5]} id="validationServer03"
                                       placeholder="Телефон" onChange={this.changePhoneUser} required/>
                                {check[4]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="exampleFormControlSelect1">Выберите город</label>
                                <select className="form-control" id="exampleFormControlSelect1"
                                        onChange={this.changeCityName}>
                                    {
                                        this.state.Cities.result && this.state.Cities.result.map((city) => {
                                            return (
                                                <option>{city.nameCity}</option>
                                            );
                                        })
                                    }
                                </select>
                            </div>
                            <form method="post" encType="multipart/form-data">
                                <label className="input-file">
                                    <input type="file" name="file"/>
                                    <h3 align={'center'}>Фото:</h3>
                                    <span>Выберите файл</span>
                                </label>
                            </form>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Почта</label>
                                <input type="text" className={check[9]} id="validationServer04"
                                       placeholder="почта" onChange={this.changeEmail} required/>
                                {check[8]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Пароль</label>
                                <input type="text" className={check[11]} id="validationServer04"
                                       placeholder="пароль" onChange={this.changePassword} required/>
                                {check[10]}
                            </div>

                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Ссылка на вк</label>
                                <input type="text" className={check[13]} id="validationServer04"
                                       onChange={this.changeLinkVk} required/>
                                {check[12]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Ссылка на телеграм</label>
                                <input type="text" className={check[15]} id="validationServer04"
                                       onChange={this.changeLinkTelegram} required/>
                                {check[14]}
                            </div>
                        </div>

                        <button className="btn btn-outline-dark col-md-10 mb-3" onClick={this.onSubmit}
                                type="submit">Зарегистрироваться
                        </button>
                    </form>
                }
                {
                    this.state.type === 'Компания' &&
                    <form className={"form_render"}>
                        <div className="form-row">
                            <div className="col-md-10 mb-3">
                                <label form="validationServer01">Название компании</label>
                                <input type="text" className={check[1]} id="validationServer01"
                                       placeholder="Название компании" onChange={this.changeNameCompany} required/>
                                {check[0]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label form="validationServer02">Описание</label>
                                <input type="text" className={check[3]} id="validationServer02"
                                       placeholder="Описание" onChange={this.changeDescription} required/>
                                {check[2]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer03">Телефон</label>
                                <input type="text" className={check[5]} id="validationServer03"
                                       placeholder="Телефон" onChange={this.changePhoneCompany} required/>
                                {check[4]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Почта (логин)</label>
                                <input type="text" className={check[7]} id="validationServer04"
                                       placeholder="Почта" onChange={this.changeEmailCompany} required/>
                                {check[6]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Пароль</label>
                                <input type="text" className={check[9]} id="validationServer04"
                                       placeholder="Пароль" onChange={this.changeCompanyPassword} required/>
                                {check[8]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Ссылка компании</label>
                                <input type="text" className={check[11]} id="validationServer04"
                                       placeholder="ссылка" onChange={this.changeLink} required/>
                                {check[10]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="exampleFormControlSelect1">Выберите подписку</label>
                                <select className="form-control" id="exampleFormControlSelect1"
                                        onChange={this.changeSubscriptionId}>
                                    {
                                        this.state.subscriptions.result && this.state.subscriptions.result.map((subscr) => {
                                            return (
                                                <option>{subscr.description} - {subscr.price}</option>
                                            );
                                        })
                                    }
                                </select>
                            </div>

                            <div className="col-md-10 mb-3">
                                <label htmlFor="exampleFormControlSelect1">Выберите город</label>
                                <select className="form-control" id="exampleFormControlSelect1"
                                        onChange={this.changeCityNameCompany}>
                                    {
                                        this.state.Cities.result && this.state.Cities.result.map((city) => {
                                            return (
                                                <option>{city.nameCity}</option>
                                            );
                                        })
                                    }
                                </select>
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Улица</label>
                                <input type="text" className={check[15]} id="validationServer04"
                                       onChange={this.changeStreet} required/>
                                {check[14]}
                            </div>
                            <div className="col-md-10 mb-3">
                                <label htmlFor="validationServer04">Дом</label>
                                <input type="text" className={check[17]} id="validationServer04"
                                       onChange={this.changeHome} required/>
                                {check[16]}
                            </div>
                        </div>

                        <button className="btn btn-outline-dark col-md-10 mb-3" onClick={this.onSubmitCompany}
                                type="submit">Зарегистрироваться
                        </button>
                    </form>
                }
            </div>
        );
    }
}