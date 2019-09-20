import React from 'react';
import '../GameDescription/OtherGames.scss';
import RatingStars from '../RatingStars/RatingStars';
import { Image, CloudinaryContext } from 'cloudinary-react';
import { Link } from 'react-router-dom';

class OtherGames extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Link to={`/Catalog/${this.props.gamePlatform}/${this.props.gameID}`}>
                <div className="gameBlock">
                    <CloudinaryContext cloudName="djlynoeio">
                        <Image publicId publicId={this.props.gameImage} width="170" height="220" />
                    </CloudinaryContext>
                    <div className="othersRating">
                        <RatingStars isItEditable={false} gameScore={this.props.gameScore} size={18} starColor="darkorange" />
                    </div>
                    <div className="gameShortDescription">
                        <span>{this.props.gameName}</span>
                    </div>
                </div>
            </Link>
        );
    }
}

export default OtherGames;