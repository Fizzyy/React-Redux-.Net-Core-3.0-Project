import React from 'react';
import '../GameDescription/GameDescription.css';
import { axiosGet, axiosPost } from '../../CommonFunctions/axioses';
import { GETCHOSENGAME, ADDORDER, GETSAMEGENREGAMES, ADDSCORE, ADDFEEDBACK } from '../../CommonFunctions/URLconstants';
import { connect } from 'react-redux';
import RatingStars from '../RatingStars/RatingStars';
import Button from 'react-bootstrap/Button';
import { Image } from 'cloudinary-react';
import Carousel from 'react-bootstrap/Carousel';
import PC1 from '../../../pc1.jpg';
import PC2 from '../../../pc2.jpg';
import LoadingSpinner from '../../CommonFunctions/LoadingSpinner';
import UsersComments from '../GameDescription/UsersComments';
import { NotificationManager } from 'react-notifications';

class GameDescription extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            amount: 1,
            totalprice: 0,
            sameGenreGames: [],
            userComment: '',
            gameFullDescription: {},
            spinnerVisibility: false
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
                this.setState({ sameGenreGames: sameGenreGames.data });
            }
        })();
    }

    setAmount = (e) => {
        this.setState({ amount: e.target.value, totalprice: e.target.value * this.state.gameFullDescription.gamePrice });
    }

    setUserComment = (e) => {
        this.setState({ userComment: e.target.value });
    }

    addOrder = async () => {
        let response = await axiosPost(ADDORDER, {
            username: this.props.userData.username,
            gameid: this.state.gameFullDescription.gameID,
            amount: +this.state.amount,
            totalsum: this.state.totalprice
        });
        if (response.status === 200) NotificationManager.success('Заказ добавлен!', 'Успешно', 5000);
        if (response.status === 400) NotificationManager.error('Войдите в аккаунт чтобы продолжить!', 'Ошибка', 5000);
        if (response.status === 401) return this.props.history.push('/SignIn');
    }

    ratingChanged = async (newRating) => {
        this.setState({ spinnerVisibility: true });
        let res = await axiosPost(ADDSCORE, { Username: this.props.userData.username, GameID: this.state.gameFullDescription.gameID, Score: +newRating });
        if (res.status === 200) {
            this.setState({ spinnerVisibility: false, gameFullDescription: { ...this.state.gameFullDescription, userScore: newRating } });
        }
    }

    addFeedback = async () => {
        let res = await axiosPost(ADDFEEDBACK, {
            username: this.props.userData.username,
            gameID: this.props.match.params.gameID,
            comment: this.state.userComment
        });
        if (res.status === 200) {
            alert('added');
            this.setState({
                gameFullDescription: {
                    ...this.state.gameFullDescription,
                    feedbacks: res.data
                }
            });
        }
    }

    render() {
        return (
            <div id="divMainGameDescription">
                <div className="emptyDivs" />
                <div className="gameInfoDiv">
                    <div id="divMainGrid">
                        <div id="gameImage">
                            <Image cloudName="djlynoeio" publicId={this.state.gameFullDescription.gameImage} id="gamePoster" />
                            {this.state.gameFullDescription.gameOfferAmount !== 0 ?
                                <>
                                    <h4 className="span_ShortInfo_OldPrice">RUB {this.state.gameFullDescription.oldGamePrice}</h4>
                                    <h4 className="span_ShortInfo_NewPrice">RUB {this.state.gameFullDescription.gamePrice}</h4>
                                </>
                                : <h4 className="label_PriceInfo">RUB {this.state.gameFullDescription.oldGamePrice}</h4>
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
                                <h3>Скриншоты:</h3>
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
                                <p>Количество:</p>
                                <input type="number" className="input_AmountOfGames" min="1" max="10" defaultValue="1" onChange={this.setAmount} />
                                <Button variant="outline-success" className="buyButton" onClick={this.addOrder}>В корзину</Button>
                            </div>
                            <div>
                                <label className="label_CurrentUserRating">Ваш рейтинг:</label>
                                <RatingStars
                                    gameScore={this.state.gameFullDescription.userScore}
                                    ratingChanged={this.ratingChanged}
                                    isItEditable={true}
                                    size={30}
                                    starColor="gold"
                                />
                                <LoadingSpinner color="orange" width={20} height={20} visible={this.state.spinnerVisibility} />
                            </div>
                        </div>
                        <div id="others"></div>
                        <div id="userReviews">
                            {this.state.gameFullDescription && this.state.gameFullDescription.feedbacks ? this.state.gameFullDescription.feedbacks.map((x) => {
                                return (
                                    <UsersComments
                                        username={x.username}
                                        userImage={x.userImage}
                                        commentDate={x.commentDate}
                                        comment={x.comment}
                                    />
                                );
                            }) : null}
                        </div>
                        <div id="currentUserReviews">
                            <textarea onChange={this.setUserComment} className="textarea_UserComment" maxLength={500} />
                        </div>
                        <div id="commentOptions">
                            <label className="label_CharCounter">{this.state.userComment.length}/500</label>
                            <Button variant="outline-info" className="Button_AddComment" onClick={this.addFeedback}>Добавить</Button>
                        </div>
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