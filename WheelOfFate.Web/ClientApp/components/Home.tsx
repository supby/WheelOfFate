import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import EmployeesList from './EmployeesList';

export default class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <div className="col-sm-4">
            <EmployeesList {...this.props as any} />
            </div>
            <div className="col-sm-4">
            
            </div>
        </div>;
    }
}
