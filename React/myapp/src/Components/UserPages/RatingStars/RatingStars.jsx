import ReactStars from 'react-stars';
import React from 'react';

class RatingStars extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            rating: 0
        }
    }

    ratingChanged = (newRating) => {
        alert(newRating);
    }

    // componentDidMount() {
    //     this.setState({ rating: this.props.gameScore });
    // }

    // componentDidUpdate(preProps, preState) {
    //     if (this.props.gameScore != preProps.gameScore) {
    //         this.setState({ rating: this.props.gameScore });
    //     }
    // }

    render() {
        return (
            <ReactStars
                edit={this.props.isItEditable}
                count={5}
                onChange={this.ratingChanged}
                size={this.props.size}
                value={this.props.gameScore}
                color2={this.props.starColor} />
        );
    }
}

export default RatingStars;