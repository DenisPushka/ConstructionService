import React, {Component} from "react";
import {connect} from "react-redux";
import {Container, Navbar, Nav} from "react-bootstrap";
import logo from '../../public/1-97.ico';
import '../Header/Header.css';


class Header extends Component {
    
    constructor(props) {
        super(props);
    }

    render() {
        const login = this.props.user === null ? '' : this.props.user.email;
        const user = this.props.user;
        return (
            <Navbar fixed={"top"} collapseOnSelect expand={'md'} bg={'dark'} variant={'dark'}>
                <Container>
                    <Navbar.Brand href={"/"}>
                        <img src={logo} height={30} width={30} className="d-inline-block align-top" alt="Logo"/>
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls={'responsive-navbar-nav'}/>
                    <Navbar.Collapse id={'responsive-navbar-nav'}>
                        <Nav className={'ms-auto start-0'}>
                            <Nav.Link href="/">Главная</Nav.Link>
                            <Nav.Link href="/equipments">Оборудование</Nav.Link>
                            {
                                ((user !== null && Object.hasOwn(user, 'subscription')) || (user === null)) 
                                && <Nav.Link href="/orders">Заказы</Nav.Link>
                            }
                            {
                                login === '' && <Nav.Link href="/login">Войти</Nav.Link>
                            }
                            {
                                login !== '' && <Nav.Link href="/profile">{login}</Nav.Link>
                            }
                           
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        );
    }
}

export default connect(state => (
        {user: state.user}),
    dispatch => {
    }
)(Header);