import React, {Component} from "react";
import {Nav} from "react-bootstrap";
import Header from "../Header/Header";
import '../Home/Home.css';
import {connect} from "react-redux";

class HomeComponent extends Component {
    state = {linkStore: undefined};

    constructor(props) {
        super(props);
    }

    componentDidMount() {
    }

    render() {
        const user = this.props.user;
        return (
            <>
                <Header/>
                <div className={"down_form"}>

                    <div className="align-items-center row">
                        <div className="col">
                            <form className={"for_form"}>

                                <Nav.Link className="btn btn-success button_choice" type="button" href="/equipments">Выбрать оборудование</Nav.Link>
                                <br/>
                                {
                                    ((user !== null && Object.hasOwn(user, 'subscription')) || (user === null))
                                    && <Nav.Link className="btn btn-success button_choice" type="button" href="/orders">Заказы</Nav.Link>
                                }
                            </form>
                        </div>


                        <div className="col">
                            <figure className="figure">
                                <img src={"ConstructinHome.jpeg"} className="figure-img img-fluid rounded"/>
                            </figure>
                        </div>
                    </div>


                    <div className={'button_create_order'}>
                        <Nav.Link className="btn btn-dark"
                                  type="button" href={"/createOrder"}>
                            Сделать заказ</Nav.Link>
                    </div>
                </div>
            </>
        );
    }
}
export default connect(state => (
        {user: state.user}),
    dispatch => {
    }
)(HomeComponent);