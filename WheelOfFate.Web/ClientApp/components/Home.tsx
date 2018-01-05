import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import EmployeesManager from './EmployeesManager';
import QueryEmployees from './QueryEmployees'
import CurrentShiftEmployees from './CurrentShiftEmployees'

export default class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <div className="row">
                <div className="col-sm-4">
                    <EmployeesManager {...this.props as any} />
                </div>
                <div className="col-sm-7">
                    <QueryEmployees {...this.props as any} />
                </div>
            </div>
            <div className="row">
                <CurrentShiftEmployees {...this.props as any} />
            </div>
        </div>;
    }
}
