import {Component} from "react";
import {connect} from "react-redux";
import Header from "../Header/Header";
import './ContactUser.css';

class ContactUser extends Component {

    state = {
        user: {}
    };

    async componentDidMount() {
        console.log(1);
        const id = window.location.pathname.split('/')[2];
        await fetch('/api/User/GetUserWithOrder/' + id, {
            method: 'GET'
        }).then(r => {
            r.json().then(async data => {
                this.setState({user: data});
            });
        });
    }

    render() {
        console.log(this.state.user);
        const image = new Image();
        image.src = 'data:image/png;base64,' + this.state.image;
        return (
            <>
                <Header/>
                <div className={'user_for_company'}>
                    <h2 align={'center'}>{this.state.user.lastName} {this.state.user.name} {this.state.user.patronymic}</h2>
                    <div className={'contact_info_user'}>
                        <hr/>
                        <h4><b>Конакты: </b></h4>
                        <h4><b>Почта: </b>{this.state.user.email} <b>Телефон: </b>{this.state.user.phone}</h4>
                        <hr/>
                        <h5><b>Ссылки: </b></h5>
                        <h5><b>Вк:</b> {this.state.user.linkVk} <b>Телеграм: </b> {this.state.user.linkTelegram}</h5>
                        <img className="img_order" src={image.src} alt={image.title}/>
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
(ContactUser);