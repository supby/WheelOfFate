import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesListStore from '../store/EmployeesList';

interface EmployeesListProps {
    employees: EmployeesListStore.Employee[];
}

export default class EmployeesList extends React.Component<EmployeesListProps, {}> {
    public render() {
        return <div>
            <ul className="list-group">
                    {(this.props.employees || []).map(employee =>
                        <li className="list-group-item" key={employee.id}>{employee.name}</li>
                    )}
            </ul>
        </div>;
    }
}