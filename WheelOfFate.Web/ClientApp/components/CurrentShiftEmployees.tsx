import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CurrentShiftEmployeesStore from '../store/CurrentShiftEmployees';
import EmployeesList from './EmployeesList';
import QueryBar from './QueryBar';

type CurrentShiftEmployeesProps = CurrentShiftEmployeesStore.CurrentShiftEmployeesState
                            & typeof CurrentShiftEmployeesStore.actionCreators;

class CurrentShiftEmployees extends  React.Component<CurrentShiftEmployeesProps, {}> {

    timer: any;

    componentDidMount()
    {
        this.timer = setInterval(this.tick, 1000);
    }

    componentWillUnmount()
    {
        clearInterval(this.timer);
    }

    tick = () => {
        this.props.requestEmployees();
    }

    public render() {
        return <div>
            <label>Current shift employees list</label>
            <div className="row">
                <EmployeesList employees={this.props.employees} />
            </div>
        </div>
    }
}

// Wire up the React component to the Redux store
export default connect(
    (state: ApplicationState) => state.currentSupportEmplyees, // Selects which state properties are merged into the component's props
    CurrentShiftEmployeesStore.actionCreators                 // Selects which action creators are merged into the component's props
)(CurrentShiftEmployees) as typeof CurrentShiftEmployees;

