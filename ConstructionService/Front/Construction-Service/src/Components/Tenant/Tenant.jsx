import {Component} from "react";
import {connect} from "react-redux";
import Header from "../Header/Header";
import './Tenant.css';

class Tenant extends Component {
    state = {
        companyArr: [
            {company: {}}
        ]
    };

    async componentDidMount() {
        const id = window.location.pathname.split('/')[2];
        await fetch('/api/Company/CompanyWithEquipment/' + id, {
            method: 'GET'
        }).then(r => r.json().then(data => {
            this.setState({companyArr: data});
        }));
    }

    render() {
        console.log(this.state.companyArr[0]);
        return (
            <>
                <Header/>
                {
                    this.state.companyArr !== 0 && this.state.companyArr.map((company) => {
                        return (
                            <div className={'form_for_tenant_equipment'}>
                                <h2 align={'center'}>{company.name}</h2>
                                <hr/>
                                <h4><b>Контакт</b></h4>
                                <h4><b>Телефон: </b> {company.phone} <b>Почта: </b> {company.email}</h4>
                                <hr/>
                                <h4><b>Адрес</b></h4>
                                <h4>
                                    <b>Город: </b>{company.cityName}; <b>Улица: </b> {company.street}; <b>Дом: </b> {company.home};
                                </h4>
                            </div>
                        );
                    })
                }
                <div className={'for_button_tenant'}>
                    <a href={"/equipments"} className="btn btn-dark">
                        Перейти к оборудованиям</a>
                </div>
            </>
        );
    }
}

export default connect(state => ({
    user: state.user
}))(Tenant);