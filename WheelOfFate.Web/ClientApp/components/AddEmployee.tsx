import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesListStore from '../store/EmployeesList';

interface AddEmployeeProps {
    addEmployee(employee: EmployeesListStore.Employee): void;
}

interface AddEmployeeState {
    employeeName: string;
}

export default class AddEmployee extends React.Component<AddEmployeeProps, AddEmployeeState> {
    constructor()
    {
        super();
        this.state = {employeeName: ''};
    }

    handleOnClick = () => {
        this.props.addEmployee({ id: 0, name: this.state.employeeName });
        this.setState({employeeName: ''});
    }

    onChange = (event:any) => {
        this.setState({employeeName: event.target.value});
    }

    public render() {
        return <div className="input-group">
                    <input value={this.state.employeeName} onChange={this.onChange} className="form-control" type="text" placeholder="New Employee Name" />
                    <span className="input-group-btn">
                        <button className="btn btn-primary" onClick={this.handleOnClick}>Add</button>
                    </span>
                </div>;
    }
}

