import React from 'react';
import '../GameDescription/GameDescription.css';
import { axiosGet, axiosPost } from '../../CommonFunctions/axioses';
import { GETCHOSENGAME, ADDORDER, GETSAMEGENREGAMES } from '../../CommonFunctions/URLconstants';
import { connect } from 'react-redux';
import RatingStars from '../RatingStars/RatingStars';
import Button from 'react-bootstrap/Button';
import { Image } from 'cloudinary-react';
import Carousel from 'react-bootstrap/Carousel';
import PC1 from '../../../pc1.jpg';
import PC2 from '../../../pc2.jpg';

class GameDescription extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            amount: 1,
            totalprice: 0,
            sameGenreGames: [],
            usercomment: '',
            gameFullDescription: {}
        }
    }

    componentDidMount() {
        (async () => {
            let gameGenre = '';
            let res = await axiosGet(GETCHOSENGAME + this.props.match.params.gameID + '/' + this.props.userData.username);
            if (res.status === 200) {
                gameGenre = res.data.gameJenre;
                this.setState({ gameFullDescription: res.data, totalprice: res.data.gamePrice });
            }
            let sameGenreGames = await axiosGet(GETSAMEGENREGAMES + `GameGenre=${gameGenre}&GameID=${this.props.match.params.gameID}`);
            if (sameGenreGames.status === 200) {
                this.setState({ sameGenreGames: sameGenreGames });
            }
        })();
    }

    setAmount = (e) => {
        this.setState({ amount: e.target.value, totalprice: e.target.value * this.state.gameFullDescription.gamePrice });
    }

    addOrder = async () => {
        let response = await axiosPost(ADDORDER, {
            username: this.props.userData.username,
            gameid: this.state.gameFullDescription.gameID,
            amount: +this.state.amount,
            totalsum: this.state.totalprice
        });
        if (response.status === 200) alert('Successfully ordered!')
        if (response.status == 400) alert('Not ordered!');
        if (response.status == 401) return this.props.history.push('/SignIn');
    }

    render() {
        return (
            <div id="divMainGameDescription">
                <div className="emptyDivs" />
                <div className="gameInfoDiv">
                    <div id="divMainGrid">
                        <div id="gameImage">
                            <Image cloudName="djlynoeio" publicId={this.state.gameFullDescription.gameImage} id="gamePoster" />
                            {this.state.isItOffer ?
                                <>
                                    <h4 className="span_ShortInfo_OldPrice">RUB {this.state.gameFullDescription.gamePrice}</h4>
                                    <h4 className="span_ShortInfo_NewPrice">RUB 10</h4>
                                </>
                                : <h4 className="label_PriceInfo">RUB {this.state.gameFullDescription.gamePrice}</h4>
                            }
                        </div>
                        <div id="header">
                            <div className="div_HeaderAndInfo">
                                <span id="span_Header_GameName">{this.state.gameFullDescription.gameName}</span><br />
                                <ul className="ul_GameDesc">
                                    <li className="li_GameDesc">
                                        {this.state.gameFullDescription.gamePlatform}
                                    </li>
                                    <li className="li_GameDesc">
                                        |
                                   </li>
                                    <li className="li_GameDesc">
                                        {this.state.gameFullDescription.gameJenre}
                                    </li>
                                    <li className="li_GameDesc">
                                        |
                                     </li>
                                    <li className="li_GameDesc">
                                        {this.state.gameFullDescription.gameRating}
                                    </li>
                                </ul>
                                <div className="userStars">
                                    <RatingStars isItEditable={false} gameScore={this.state.gameFullDescription.gameScore} size={28} starColor="red" />
                                </div>
                            </div>
                        </div>
                        <div id="comments">
                            <div className="div_CommentsHeader">
                                <h2>Скриншоты:</h2>
                            </div>
                            <div className="div_CommentsCarousel">
                                <Carousel>
                                    <Carousel.Item>
                                        <img className="d-block w-100" src={PC1} />
                                    </Carousel.Item>
                                    <Carousel.Item>
                                        <img className="d-block w-100" src={PC2} />
                                    </Carousel.Item>
                                </Carousel>
                            </div>
                        </div>
                        <div id="buttons">
                            <div className="div_InputAndButton">
                                <input type="number" className="input_AmountOfGames" min="1" max="10" />
                                <Button variant="outline-success" className="buyButton">В корзину</Button>
                            </div>
                            <div>
                                <label className="label_CurrentUserRating">Ваш рейтинг:</label>
                                <RatingStars isItEditable={true} gameScore={this.state.gameFullDescription.gameScore} size={30} starColor="gold" />
                            </div>
                        </div>
                        <div id="others"></div>
                    </div>
                </div>
                <div className="emptyDivs" />
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        userData: store
    };
}

export default connect(mapStateToProps)(GameDescription);