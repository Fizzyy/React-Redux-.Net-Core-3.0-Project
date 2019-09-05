import React from 'react';
import '../Catalog/Catalog.css';
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
                    {this.props.isItOffer ?
                        <div className="divMainGameBlock_OfferDiv">
                            <Icon size={24} icon={fire} />
                            <label style={{ marginLeft: '5px', marginTop: '2px' }}>Скидка 20%</label>
                        </div>
                        : null}
                    <div id="divMainGameBlock_Info">
                        <label style={{ fontSize: 20, marginLeft: '5px', color: 'black' }}>{this.props.gameName}</label>
                        <div className="divMainGameBlock_Info_Ratings">
                            <RatingStars gameScore={this.props.gameScore} isItEditable={false} size={26} starColor="darkred" />
                        </div>
                    </div>
                    <div id="divMainGameBlock_Price">
                        {this.props.isItOffer ?
                            <>
                                <label className="OfferPrice">2p</label>
                                <label className="NormalPriceCrossed">3p</label>
                            </>
                            :
                            <label className="NormalPrice">{this.props.gamePrice}p</label>
                        }
                    </div>
                </div>
            </Link>
        );
    }
}

export default GameBlockInfo;