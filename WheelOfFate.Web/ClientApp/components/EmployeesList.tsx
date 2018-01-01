import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesListStore from '../store/EmployeesList';

type EmployeesListProps =
    EmployeesListStore.EmployeesListState
    & typeof EmployeesListStore.actionCreators
    & RouteComponentProps<{}>;

class EmployeesList extends React.Component<EmployeesListProps, {}> {
    componentWillMount() {
        this.props.requestEmployees();
    }

    handleOnClick = () => {
        var input = this.refs.userNameInput as HTMLInputElement;
        this.props.addEmployee({ id: 0, name: input.value });
        input.value = '';
    }

    public render() {
        return <div>
            <div className="row">
                <div className="input-group">
                    <input className="form-control" type="text" name="newEmployee" ref="userNameInput" />
                    <span className="input-group-btn">
                        <button className="btn btn-primary" onClick={this.handleOnClick}>Add</button>
                    </span>
                </div>                
            </div>
            <div className="row">
                <ul className="list-group">
                    {(this.props.employees || []).map(employee =>
                        <li className="list-group-item" key={employee.id}>{employee.name}</li>
                    )}
                </ul>
            </div>
        </div>;
    }
}

// Wire up the React component to the Redux store
export default connect(
    (state: ApplicationState) => state.employees, // Selects which state properties are merged into the component's props
    EmployeesListStore.actionCreators                 // Selects which action creators are merged into the component's props
)(EmployeesList) as typeof EmployeesList;