import React from 'react';
import '../Catalog/Catalog.scss';
import RatingStars from '../RatingStars/RatingStars';
import { fire } from 'react-icons-kit/metrize/fire';
import { Icon } from 'react-icons-kit';
import { Link } from 'react-router-dom';
import { Image } from 'cloudinary-react';

class GameBlockInfo extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            picture: undefined
        }
    }

    render() {
        return (
            <Link to={`/Catalog/${this.props.gamePlatform}/${this.props.gameID}`}>
                <div id="divMainGameBlock">
                    <Image cloudName="djlynoeio" publicId={this.props.gameImage} id="divMainGameBlock_Image" />
                    {this.props.gameOfferAmount !== 0 ?
                        <div className="divMainGameBlock_OfferDiv">
                            <Icon size={24} icon={fire} />
                            <label style={{ marginLeft: '5px', marginTop: '2px' }}>Скидка {this.props.gameOfferAmount}%</label>
                        </div>
                        : null
                    }
                    <div id="divMainGameBlock_Info">
                        <label style={{ fontSize: 20, marginLeft: '5px', color: 'black' }}>{this.props.gameName}</label>
                        <div className="divMainGameBlock_Info_Ratings">
                            <RatingStars gameScore={this.props.gameScore} isItEditable={false} size={26} starColor="darkred" />
                        </div>
                    </div>
                    <div id="divMainGameBlock_Price">
                        {this.props.gameOfferAmount !== 0 ?
                            <>
                                <label className="OfferPrice">{this.props.newGamePrice}p</label>
                                <label className="NormalPriceCrossed">{this.props.oldGamePrice}p</label>
                            </>
                            :
                            <label className="NormalPrice">{this.props.oldGamePrice}p</label>
                        }
                    </div>
                </div>
            </Link>
        );
    }
}

export default GameBlockInfo;