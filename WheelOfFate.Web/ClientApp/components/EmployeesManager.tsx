import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesListStore from '../store/EmployeesList';
import AddEmployee from './AddEmployee'
import EmployeesList from './EmployeesList';

type EmployeesManagerProps =
    EmployeesListStore.EmployeesListState
    & typeof EmployeesListStore.actionCreators
    & RouteComponentProps<{}>;

class EmployeesManager extends React.Component<EmployeesManagerProps, {}> {
    componentWillMount() {
        this.props.requestEmployees();
    }

    public render() {
        return <div>
            <div className="row">
                <AddEmployee addEmployee={this.props.addEmployee} />
            </div>
            <div className="row">
                <EmployeesList employees={this.props.employees} />
            </div>
        </div>;
    }
}

// Wire up the React component to the Redux store
export default connect(
    (state: ApplicationState) => state.employees, // Selects which state properties are merged into the component's props
    EmployeesListStore.actionCreators                 // Selects which action creators are merged into the component's props
)(EmployeesManager) as typeof EmployeesManager;