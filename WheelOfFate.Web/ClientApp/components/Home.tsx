import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import QueryEmployees from './QueryEmployees'
import CurrentShiftEmployees from './CurrentShiftEmployees'

export default class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <div className="row">
                <QueryEmployees {...this.props as any} />
            </div>
            <hr />
            <div className="row">
                <CurrentShiftEmployees {...this.props as any} />
            </div>
        </div>;
    }
}
