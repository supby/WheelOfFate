import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesListStore from '../store/EmployeesList';

interface AddEmployeeProps {
    addEmployee(employee: EmployeesListStore.Employee): void;
}

export default class AddEmployee extends React.Component<AddEmployeeProps, {}> {

    handleOnClick = () => {
        const input = this.refs.userNameInput as HTMLInputElement;
        this.props.addEmployee({ id: 0, name: input.value });
        input.value = '';
    }

    public render() {
        return <div className="input-group">
                    <input className="form-control" type="text" name="newEmployee" placeholder="New Employee Name" ref="userNameInput" />
                    <span className="input-group-btn">
                        <button className="btn btn-primary" onClick={this.handleOnClick}>Add</button>
                    </span>
                </div>;
    }
}

