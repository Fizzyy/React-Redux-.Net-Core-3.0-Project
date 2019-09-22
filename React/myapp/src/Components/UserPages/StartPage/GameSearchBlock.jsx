import React from 'react';
import RatingStars from '../RatingStars/RatingStars';
import '../StartPage/GameSearchBlock.scss';
import { Link } from 'react-router-dom';

class GameSearchBlock extends React.Component {
    constructor(props) {
        super(props);

    }

    render() {
        return (
            <Link className="Linkk">
                <div className="shortGameInfoContainer">
                    <span className="gameShortInfo">{this.props.gameName}  ({this.props.gamePlatform})</span>
                    <div className="gameRating">
                        <RatingStars gameScore={this.props.gameScore} isItEditable={false} size={24} starColor="darkred" />
                    </div>
                </div>
            </Link>
        );
    }
}

export default GameSearchBlock;


{/* {this.props.gameName}
                    {this.props.gamePlatform}
                    {this.props.gameID}
                    {this.props.gameScore} */}