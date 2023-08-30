import {Component} from "react";
import './CreateOrder.css';
import Header from "../Header/Header";
import {connect} from "react-redux";

class CreateOrder extends Component {

    state = {
        newOrder: {
            miniDescription: '',
            description: '',
            getOrder: false,
            completedOrder: false,
            price: 0,
            nameCity: '',
            dateStart: '',
            dateEnd: '',
            categoryWork: 1,
            work: 1,
            photo: undefined,
            login: [],
            password: []
        },
        Cities: [
            {nameCity: []}
        ],
        CategoryWork: [
            {
                id: 0,
                name: '',
                serviceId: 0
            }
        ],
        Works: [{
            id: 0,
            WorkName: ''
        }]
    };

    constructor(props) {
        super(props);
        if (this.props.user === null){
            alert('Авторизуйтесь!')
            window.location = '/'
        }
        else if (Object.hasOwn(this.props.user, 'subscription')){
            alert('Заказ может создать только заказчик!')
            window.location = '/'
        }
        
        this.handleChangeImage = this.handleChangeImage.bind(this);
        this.handleChangeMiniDescription = this.handleChangeMiniDescription.bind(this);
        this.handleChangeDescription = this.handleChangeDescription.bind(this);
        this.handleChangePrice = this.handleChangePrice.bind(this);
        this.handleChangeCity = this.handleChangeCity.bind(this);
        this.handleChangeDateStart = this.handleChangeDateStart.bind(this);
        this.handleChangeDateEnd = this.handleChangeDateEnd.bind(this);
        this.handleChangeCategoryWork = this.handleChangeCategoryWork.bind(this);
        this.handleChangeUserId = this.handleChangeUserId.bind(this);
        this.handleChangeWork = this.handleChangeWork.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    async componentDidMount() {
        // Get Cities
        let response = await fetch('/api/Cities/GetAll', {
            method: 'GET'
        });
        let result = await response.json();
        await this.setState({Cities: {...this.state.Cities, result}});

        // GetCategoryWorks
        response = await fetch('/api/Service/getCategoryWorks', {
            method: 'GET'
        });
        result = await response.json();
        await this.setState({CategoryWork: {...this.state.CategoryWork, result}});

        // GetWorks
        response = await fetch('/api/Service/getWorks', {
            method: 'GET'
        });
        result = await response.json();
        await this.setState({Works: {...this.state.Works, result}});
    }

    async onSubmit(e) {
        await e.preventDefault();
        let form = new FormData();
        if (this.props.user === null){
            alert('Вы не авторизованы!')
            return;
        }
        this.state.newOrder.login = this.props.user.email;
        this.state.newOrder.password = this.props.user.password;

        // this.setState({newOrder: {...this.state.newOrder, login: this.props.user.email}});
        // this.setState({newOrder: {...this.state.newOrder, password: this.props.user.password}});
        
        // form.append('orderFromVie', this.state.newOrder);
        form.append('MiniDescription', this.state.newOrder.miniDescription)
        form.append('Description', this.state.newOrder.description)
        form.append('GetOrder', this.state.newOrder.getOrder)
        form.append('CompletedOrder', this.state.newOrder.completedOrder)
        form.append('Price', this.state.newOrder.price)
        form.append('NameCity', this.state.newOrder.nameCity)
        form.append('DateStart', this.state.newOrder.dateStart)
        form.append('DateEnd', this.state.newOrder.dateEnd)
        form.append('CategoryWork', this.state.newOrder.categoryWork)
        form.append('Work', this.state.newOrder.work)
        form.append('Photo', this.state.newOrder.photo)
        form.append('Login', this.state.newOrder.login)
        form.append('Password', this.state.newOrder.password)
        let response = await fetch('/api/User/AddOrder', {
            method: 'POST',
            body: form
        });

        let result = await response.json();
        console.log(result); 
    }

    async handleChangeMiniDescription(event) {
        await event.preventDefault();
        await this.setState({newOrder: {...this.state.newOrder, miniDescription: event.target.value}});
    }

    async handleChangeDescription(event) {
        await event.preventDefault();
        await this.setState({newOrder: {...this.state.newOrder, description: event.target.value}});
    }

    async handleChangePrice(event) {
        await event.preventDefault();
        await this.setState({newOrder: {...this.state.newOrder, price: event.target.value}});
    }

    async handleChangeCity(event) {
        await event.preventDefault();
        await this.setState({newOrder: {...this.state.newOrder, nameCity: event.target.value}});
    }

    async handleChangeDateStart(event) {
        await event.preventDefault();
        // await this.setState({newOrder: {...this.state.newOrder, time: {...this.state.newOrder.time, dateStart: event.target.value}}});
        this.setState({newOrder: {...this.state.newOrder, dateStart: event.target.value}})
    }

    async handleChangeDateEnd(event) {
        await event.preventDefault();
        this.setState({newOrder: {...this.state.newOrder, dateEnd: event.target.value}})
    }

    async handleChangeCategoryWork(event) {
        await event.preventDefault();
        let number = 0;
        this.state.CategoryWork.result.map((CW) => {
            if (CW.name === event.target.value) {
                number = CW.id;
            }
        });
        await this.setState({newOrder: {...this.state.newOrder, categoryWork: number}});
    }

    async handleChangeWork(event) {
        await event.preventDefault();
        let number = 0;
        this.state.Works.result.map((work) => {
            if (work.name === event.target.value) {
                number = work.id;
            }
        });
        await this.setState({newOrder: {...this.state.newOrder, work: number}});
    }

    async handleChangeImage(e) {
        await e.preventDefault();
        await this.setState({newOrder: {...this.state.newOrder, photo: e.target.files[0]}});
    }

    async handleChangeUserId(event) {
        await event.preventDefault();
        await this.setState({newOrder: {...this.state.newOrder, userId: event.target.value}});
    }

    render() {
        return (
            <div>
                <Header/>
                <form className={'for_form_create_order'} onSubmit={this.onSubmit}>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlInput1">Краткое описание</label>
                        <input className="form-control" id="exampleFormControlInput1"
                               onChange={this.handleChangeMiniDescription}/>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlInput1">Описание</label>
                        <input className="form-control" id="exampleFormControlInput1"
                               onChange={this.handleChangeDescription}/>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlInput1">Цена</label>
                        <input className="form-control" id="exampleFormControlInput1"
                               onChange={this.handleChangePrice}/>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlSelect1">Выберите город</label>
                        <select className="form-control" id="exampleFormControlSelect1"
                                onChange={this.handleChangeCity}>
                            {
                                this.state.Cities.result && this.state.Cities.result.map((city) => {
                                    return (
                                        <option>{city.nameCity}</option>
                                    );
                                })
                            }
                        </select>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlSelect1">Категория работы</label>
                        <select className="form-control" id="exampleFormControlSelect1"
                                onChange={this.handleChangeCategoryWork}>
                            {
                                this.state.CategoryWork.result && this.state.CategoryWork.result.map((categoryWork) => {
                                    return (
                                        <option>{categoryWork.name}</option>
                                    );
                                })
                            }
                        </select>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlSelect2">Работа</label>
                        <select className="form-control" id="exampleFormControlSelect1"
                                onChange={this.handleChangeWork}>
                            {
                                this.state.Works.result && this.state.Works.result.map((works) => {
                                    return (
                                        <option>{works.name}</option>
                                    );
                                })
                            }
                        </select>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlInput1">Введите дату начала работы в формате (гггг-мм-дд)</label>
                        <input className="form-control" id="exampleFormControlInput1"
                               onChange={this.handleChangeDateStart}/>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlInput1">Введите дату окончания работы в формате (гггг-мм-дд)</label>
                        <input className="form-control" id="exampleFormControlInput1"
                               onChange={this.handleChangeDateEnd}/>
                    </div>
                    <form className="form-group" method="post" encType="multipart/form-data">
                        <label className="input-file">
                            <p className={'span_file'} align={'center'}>Загрузите фото с примером: <br/>
                                <input type="file" name="picture" accept="image/*" onChange={this.handleChangeImage}/>
                                <span>Выберите файл</span>
                            </p>
                        </label>
                    </form>
                    <input className={'btn btn-outline-dark button_enter_cr_or'} type="submit"/>
                </form>
            </div>
        );
    }
}

export default connect(state => (
        {user: state.user}),
    dispatch => {
    }
)
(CreateOrder);
