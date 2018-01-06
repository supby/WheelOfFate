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

    handleAddShiftClick = () => {
        this.props.addShift(this.props.employees.map(x => x.id));
    }

    public render() {
        const divStyles = {
            marginBottom: '20px'
        };

        return <div>
            <div className="row" style={divStyles}>
                <QueryBar requestEmployees={this.props.requestEmployees}/>
            </div>
            <div className="row">
                <EmployeesList employees={this.props.employees} />
            </div>
            {this.props.employees.length > 0 &&
                <div className="row">
                    <button className="btn btn-primary" onClick={this.handleAddShiftClick}>Add Shift</button>
                </div>
            }            
        </div>
    }
}

// Wire up the React component to the Redux store
export default connect(
    (state: ApplicationState) => state.queryEmployees, // Selects which state properties are merged into the component's props
    QueryEmployeesStore.actionCreators                 // Selects which action creators are merged into the component's props
)(QueryEmployees) as typeof QueryEmployees;

