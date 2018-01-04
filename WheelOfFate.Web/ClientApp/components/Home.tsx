import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import EmployeesManager from './EmployeesManager';
import QueryEmployees from './QueryEmployees'

export default class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <div className="col-sm-4">
            <EmployeesManager {...this.props as any} />
            </div>
            <div className="col-sm-5">
            <QueryEmployees {...this.props as any} />
            </div>
        </div>;
    }
}
