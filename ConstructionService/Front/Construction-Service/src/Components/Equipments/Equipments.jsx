import {Component} from "react";
import {connect} from "react-redux";
import Header from "../Header/Header";
import './Equipments.css';

class Equipments extends Component {

    state = {
        equipments: [],
        typeEquipments: []
    };

    async componentDidMount() {
        await fetch('api/Service/getEquipments', {
            method: 'GET'
        }).then(res => {
            res.json().then(async data => {
                this.setState({equipments: data});
            });
        });
        await fetch('api/Service/getTypeEquipment', {
            method: 'GET'
        }).then(res => {
            res.json().then(async data => {
                this.setState({typeEquipments: data});
            });
        });
    }

    render() {
        return (
            <>
                <Header/>
                {
                    this.state.typeEquipments.length !== 0 && this.state.typeEquipments.map((typeEquipment) => {
                        return (
                            <div className={'for_form_typeEquipments'}>
                                <h2><b>{typeEquipment.name}</b></h2>
                                <br/>
                                {this.state.equipments.map((equipment) => {
                                    if (equipment.typeEquipmentId === typeEquipment.id) {
                                        return (
                                            <div className={'for_equipment'}>
                                                <div className={'for_name_equipment'}>{equipment.name}</div>
                                                {equipment.description}
                                                <br/>
                                                <a href={"/tenant/" + equipment.id} className="btn btn-outline-dark">
                                                    Перейти к поставщикам оборудования</a>
                                                <hr/>
                                            </div>
                                        );
                                    }
                                })}
                            </div>
                        );
                    })
                }
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
(Equipments);