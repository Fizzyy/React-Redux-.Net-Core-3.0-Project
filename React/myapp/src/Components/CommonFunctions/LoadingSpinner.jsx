import React from 'react';
import Loader from 'react-loader-spinner';

class LoaderSpinner extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Loader
                type="Oval"
                color={this.props.color}
                height={this.props.height}
                width={this.props.width}
                visible={this.props.visible}
            />
        );
    }
}

export default LoaderSpinner;