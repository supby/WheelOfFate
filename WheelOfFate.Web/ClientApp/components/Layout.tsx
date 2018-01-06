import * as React from 'react';
import { NavMenu } from './NavMenu';

export class Layout extends React.Component<{}, {}> {

    

    public render() {
        const divStyle = {
            paddingTop: '50px'
        };

        return <div className='container'>
                <div className='row'>
                    <div className='col-sm-3'>
                        <NavMenu />
                    </div>
                    <div className='col-sm-9' style={divStyle}>
                        { this.props.children }
                    </div>
                </div>
        </div>;
    }
}
