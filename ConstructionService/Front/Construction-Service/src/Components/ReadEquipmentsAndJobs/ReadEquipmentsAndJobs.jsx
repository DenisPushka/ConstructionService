import React, {Component} from "react";
import {connect} from "react-redux";
import Header from "../Header/Header";
import './ReadEquipmentsAndJobs.css';

class ReadEquipmentsAndJobs extends Component {

    state = {
        equipments: [],
        jobs: [],
        allEquipments: [],
        allJobs: [],
        choiceJob: {},
        choiceEquipment: {}
    };

    constructor(props) {
        super(props);

        this.addEquipment = this.addEquipment.bind(this);
        this.addJobs = this.addJobs.bind(this);
        this.removeEquipment = this.removeEquipment.bind(this);
        this.removeJob = this.removeJob.bind(this);
        this.changeJob = this.changeJob.bind(this);
        this.changeEquipment = this.changeEquipment.bind(this);
    }

    async componentDidMount() {
        let form = new FormData();
        form.append('login', this.props.user.email);
        form.append('password', this.props.user.password);
        await fetch('/api/Company/GetWorks', {
            method: 'POST',
            body: form
        }).then(r => r.json().then((data) => {
            this.setState({jobs: data});
        }));

        await fetch('/api/Company/GetEquipments', {
            method: 'POST',
            body: form
        }).then(r => r.json().then((data) => {
            this.setState({equipments: data});
        }));

        await fetch('/api/Service/getEquipments', {
            method: 'GET'
        }).then(r => r.json().then((data) => {
            console.log(data);
            this.setState({allEquipments: data});
        }));

        await fetch('/api/Service/getWorks', {
            method: 'GET'
        }).then(r => r.json().then((data) => {
            console.log(data);
            this.setState({allJobs: data});
        }));
    }
    
    changeJob(event) {
        this.state.allJobs.map((job) => {
            if (job.name === event.target.value) {
                this.setState({choiceJob: job});
            }
        });
        
    }

    changeEquipment(event) {
        this.state.allEquipments.map((equipment) => {
            if (equipment.name === event.target.value) {
                this.setState({choiceEquipment: equipment});
            }
        });
    }

    async addEquipment(event, args) {
        let form = new FormData();
        form.append('UserAuthentication', JSON.stringify({
            login: this.props.user.email,
            password: this.props.user.password
        }));
        form.append('Equipment', JSON.stringify(this.state.choiceEquipment));
        fetch('/api/Company/TakeEquipment', {
            method: 'POST',
            body: form
        }).then();
        this.componentDidMount();
    }

    async addJobs(event, args) {
        let form = new FormData();
        form.append('UserAuthentication', JSON.stringify({
            login: this.props.user.email,
            password: this.props.user.password
        }));
        form.append('work', JSON.stringify(this.state.choiceJob));
        fetch('/api/Company/TakeWork', {
            method: 'POST',
            body: form
        }).then();
        this.componentDidMount();
    }

    async removeEquipment(event, args) {
        console.log('remove');
    }

    async removeJob(event, args) {

    }

    render() {
        console.log(this.state);
        return (
            <>
                <Header/>

                <div className={'for_form_r_eq_job'}>
                    <h2 align={'center'}>Оборудование</h2>
                    {
                        this.state.equipments.length !== 0 && this.state.equipments.map((equipment) => {
                            return (
                                <div className={'for_input_data_read_equipment_job'}>
                                    <h3>- {equipment.name}</h3>

                                    <button className="btn btn-danger for_button_subscription" type="button"
                                            onClick={(event) => this.removeEquipment(event, equipment.id)}>
                                        Отказаться
                                    </button>
                                </div>
                            );
                        })
                    }

                    <select className="form-control for_add_in_equipment_job" onChange={this.changeEquipment}>{
                        this.state.allEquipments.map((equipment) => {
                            return (
                                <option>{equipment.name}</option>
                            );
                        })
                    }
                    </select>
                    <button className="btn btn-success for_button_add_equipment_job" type="button"
                            onClick={this.addEquipment}>
                        Добавить оборудование
                    </button>
                </div>

                <hr/>

                <div className={'for_form_r_eq_job'}>
                    <h2 align={'center'}>Работа</h2>
                    {
                        this.state.jobs.length !== 0 && this.state.jobs.map((job) => {
                            return (
                                <div className={'for_input_data_read_equipment_job'}>
                                    <h3>- {job.name}</h3>
                                    <button className="btn btn-danger for_button_subscription" type="button"
                                            onClick={(event) => this.removeJob(event, job.id)}>
                                        Отказаться
                                    </button>
                                </div>
                            );
                        })
                    }
                    <select className="form-control for_add_in_equipment_job" onChange={this.changeJob}>{
                        this.state.allJobs.map((Jobs) => {
                            return (
                                <option>{Jobs.name}</option>
                            );
                        })
                    }
                    </select>
                    <button className="btn btn-success for_button_add_equipment_job" type="button"
                            onClick={this.addJobs}>
                        Добавить работу
                    </button>
                </div>
            </>
        );
    }
}

export default connect(state => ({
    user: state.user
}))(ReadEquipmentsAndJobs);