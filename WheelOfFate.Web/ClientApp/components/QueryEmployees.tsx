import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as QueryEmployeesStore from '../store/QueryEmployees';
import EmployeesList from './EmployeesList';
import QueryBar from './QueryBar';

type QueryEmployeesProps = QueryEmployeesStore.QueryEmployeesState
                            & typeof QueryEmployeesStore.actionCreators;

class QueryEmployees extends  React.Component<QueryEmployeesProps, {}> {

    handleQueryClick = () => {

    }

    public render() {
        return <div>
            <div className="row">
                <QueryBar requestEmployees={this.props.requestEmployees}/>
            </div>
            <div className="row">
                <EmployeesList employees={this.props.employees} />
            </div>
        </div>
    }
}

// Wire up the React component to the Redux store
export default connect(
    (state: ApplicationState) => state.queryEmployees, // Selects which state properties are merged into the component's props
    QueryEmployeesStore.actionCreators                 // Selects which action creators are merged into the component's props
)(QueryEmployees) as typeof QueryEmployees;

