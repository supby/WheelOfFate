import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesListStore from '../store/EmployeesList';

interface QueryBarProps {
    requestEmployees(bauCapacity:number, minShift:number, workingWindow:number, reqDaysPerWindow:number): void;
}

interface QueryBarState {
    bauCapacity: number;
    minShift: number;
    workingWindow: number;
    reqDaysPerWindow: number;
}

export default class QueryBar extends React.Component<QueryBarProps, QueryBarState> {

    constructor()
    {
        super();
        this.state = {
            bauCapacity: 0,
            minShift: 0,
            workingWindow: 0,
            reqDaysPerWindow: 0
        };
    }

    handleOnClick = () => {
        this.props.requestEmployees(this.state.bauCapacity, this.state.minShift, 
                                    this.state.workingWindow, this.state.reqDaysPerWindow);
    }

    onCapacityChange = (event: any) => {
        this.setState({bauCapacity: event.target.value});
    }

    onMinShiftChange = (event: any) => {
        this.setState({minShift: event.target.value});
    }

    onWindowChange = (event: any) => {
        this.setState({workingWindow: event.target.value});
    }

    onDaysPerWindowChange = (event: any) => {
        this.setState({reqDaysPerWindow: event.target.value});
    }

    public render() {
        return <div>
                    <div className="row">
                        <div className="form-group col-md-4">
                            <label className="control-label">How many employees?</label>
                            <input className="form-control" type="number" onChange={this.onCapacityChange} value={this.state.bauCapacity}/>
                        </div>
                        <div className="form-group col-md-4">
                            <label>Minimal days between shifts?</label>
                            <input className="form-control" type="number" onChange={this.onMinShiftChange} value={this.state.minShift}/>
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-md-4">
                            <label>Working cycle (days)</label>
                            <input className="form-control" type="number" onChange={this.onWindowChange} value={this.state.workingWindow}/>
                        </div>
                        <div className="form-group col-md-4">
                            <label>Days per window for employee</label>
                            <input className="form-control" type="number" onChange={this.onDaysPerWindowChange} value={this.state.reqDaysPerWindow}/>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-md-8">
                            <button className="btn btn-primary pull-right" onClick={this.handleOnClick}>Query</button>
                        </div>
                    </div>
                </div>;
    }
}

