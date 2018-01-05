import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesListStore from '../store/EmployeesList';

interface QueryBarProps {
    requestEmployees(bauCapacity:number, minShift:number, workingWindow:number, reqDaysPerWindow:number): void;
}

export default class QueryBar extends React.Component<QueryBarProps, {}> {

    handleOnClick = () => {
        const bauCapacity = this.refs.bauCapacity as HTMLInputElement;
        const minShift = this.refs.minShift as HTMLInputElement;
        const workingWindow = this.refs.workingWindow as HTMLInputElement;
        const reqDaysPerWindow = this.refs.reqDaysPerWindow as HTMLInputElement;

        this.props.requestEmployees(Number(bauCapacity.value), Number(minShift.value), 
                                    Number(workingWindow.value), Number(reqDaysPerWindow.value));
    }

    public render() {
        return <div className="input-group">
                    <div className="col-sm-2">
                        <label>How many employees?</label>
                        <input className="form-control" type="number" ref="bauCapacity"/>
                    </div>
                    <div className="col-sm-2">
                        <label>Minimal days between supports?</label>
                        <input className="form-control" type="number" ref="minShift"/>
                    </div>
                    <div className="col-sm-2">
                        <label>Working cycle (days)</label>
                        <input className="form-control" type="number" ref="workingWindow"/>
                    </div>
                    <div className="col-sm-2">
                    <label>Days per window for employee</label>
                        <input className="form-control" type="number" ref="reqDaysPerWindow"/>
                    </div>
                    <div className="col-sm-2">
                        <button className="btn btn-primary" onClick={this.handleOnClick}>Query</button>
                    </div>
                </div>;
    }
}

