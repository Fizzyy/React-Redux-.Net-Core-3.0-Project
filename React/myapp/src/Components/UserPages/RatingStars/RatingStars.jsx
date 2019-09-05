import ReactStars from 'react-stars';
import React from 'react';
class RatingStars extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            rating: 0
        }
    }

    /*ratingChanged = async (newRating) => {
        let res = await axiosPost(ADDSCORE, { Username: this.props.username, GameID: this.props.gameID, Score: newRating });
        if (res.status === 200) {

        }
    }*/

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
                onChange={this.props.ratingChanged}
                size={this.props.size}
                value={this.props.gameScore}
                color2={this.props.starColor} />
        );
    }
}

export default RatingStars;