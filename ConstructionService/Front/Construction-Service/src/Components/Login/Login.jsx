import React, {Component} from "react";
import {connect} from "react-redux";
import Header from "../Header/Header";
import '../Login/Login.css';
import {Nav} from "react-bootstrap";

class Login extends Component {
    state = {
        user: {
            email: '',
            password: ''
        }
    };

    constructor(props) {
        super(props);
        this.updateEmail = this.updateEmail.bind(this);
        this.updatePassword = this.updatePassword.bind(this);
        this.updateAuthentication = this.updateAuthentication.bind(this);
        this.authorization = this.authorization.bind(this);
    }

    async updateEmail(event) {
        await event.preventDefault();
        this.setState({user: {...this.state.user, email: event.target.value}});
    }

    async updatePassword(event) {
        await event.preventDefault();
        this.setState({user: {...this.state.user, password: event.target.value}});
    }

    updateAuthentication(data) {
        console.log(data);
        this.props.onAddUser(data);
    }

    async authorization(event) {
        await event.preventDefault();
        let form = new FormData();
        form.append('login', this.state.user.email);
        form.append('password', this.state.user.password);
        fetch("/api/Authorization/authentication", {
            method: 'POST',
            body: form
        })
            .then(res => {
                res.json().then(async (data) => {
                    if (data.company === null && data.user === null && data.handcraft === null) {
                        alert('Неправильно введен логин или пароль!')
                        return
                    }
                    if (data.user === null && data.handcraft === null) data = data.company;
                    else if (data.company === null && data.handcraft === null) data = data.user;
                    else if (data.company === null && data.user === null) {
                        if (data.handcraft.email === '')
                            return;
                        data = data.handcraft;
                    }
                    if (data.email !== '') {
                        await this.updateAuthentication(data);
                        window.location = '/';
                    }
                });
            });
    }

    render() {
        return (
            <div className={"body_authorization"}>
                <Header/>
                <div className={'down_all'}>
                    <div className="containerAuthorization down_all">
                        <div className="form-container sign-in-container">
                            <div className="social-container">
                                <form>
                                    <h1 className={"h1_authorization"}>Вход</h1>

                                    <div className="form-group move_right">
                                        <label htmlFor="exampleInputEmail1">Email</label>
                                        <input type="email" className="form-control" id="exampleInputEmail1"
                                               aria-describedby="emailHelp" placeholder="Enter email"
                                               onChange={this.updateEmail}/>

                                    </div>
                                    <div className="form-group move_right">
                                        <label htmlFor="exampleInputPassword1">Password</label>
                                        <input type="password" className="form-control" id="exampleInputPassword1"
                                               placeholder={"Password"} onChange={this.updatePassword}/>
                                    </div>

                                    <button type="submit" className="btn btn-outline-danger button_enter"
                                            onClick={this.authorization}>Войти
                                    </button>

                                    <Nav.Link type="submit" className="btn btn-outline-success button_authorization"
                                              href="/registration">Зарегестрироватсья</Nav.Link>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
)(Login);