import React from 'react';
import '../GameDescription/GameDescription2.scss';
import { axiosGet, axiosPost } from '../../CommonFunctions/axioses';
import { GETCHOSENGAME, ADDORDER, GETSAMEGENREGAMES, ADDSCORE, ADDFEEDBACK } from '../../CommonFunctions/URLconstants';
import { connect } from 'react-redux';
import RatingStars from '../RatingStars/RatingStars';
import Button from 'react-bootstrap/Button';
import { Image } from 'cloudinary-react';
import Carousel from 'react-bootstrap/Carousel';
import PC1 from '../../../pc1.jpg';
import LoadingSpinner from '../../CommonFunctions/LoadingSpinner';
import UsersComments from '../GameDescription/UsersComments';
import { NotificationManager } from 'react-notifications';
import { fire } from 'react-icons-kit/metrize/fire';
import { Icon } from 'react-icons-kit';
import { androidCart } from 'react-icons-kit/ionicons/androidCart';
import OtherGames from '../GameDescription/OtherGames';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

class GameDesc2 extends React.Component {
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

    async componentDidMount() {
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
    }

    componentDidUpdate(preProps) {
        if (this.props.match.params.gameID !== preProps.match.params.gameID) {
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
            this.setState({
                spinnerVisibility: false,
                gameFullDescription: {
                    ...this.state.gameFullDescription,
                    userScore: newRating,
                    gameScore: res.data.gameScore,
                    amountOfVotes: res.data.amountOfVotes
                }
            });
        }
    }

    addFeedback = async () => {
        let res = await axiosPost(ADDFEEDBACK, {
            username: this.props.userData.username,
            gameID: this.props.match.params.gameID,
            comment: this.state.userComment
        });
        if (res.status === 200) {
            console.log(res.data);
            debugger;
            NotificationManager.success('Успешно!', 'Комментарий добавлен', 3000);
            this.setState({
                gameFullDescription: {
                    ...this.state.gameFullDescription,
                    feedbacks: res.data
                }
            });
        }
    }

    setUserComment = (e) => {
        this.setState({ userComment: e.target.value });
    }

    render() {
        return (
            <div className="mainBlockGameDesc" style={{ backgroundImage: `url('${this.state.gameFullDescription.gameBackgroundImage}')` }}>
                <div className="mainGrid">
                    <div id="gameImage">
                        <Image cloudName="djlynoeio" publicId={this.state.gameFullDescription.gameImage} width="240" height="320" />
                        {this.state.gameFullDescription.gameOfferAmount !== 0 ?
                            <div className="offerStripe">
                                <Icon size={28} icon={fire} />
                                <label>Скидка {this.state.gameFullDescription.gameOfferAmount}%</label>
                            </div>
                            : null
                        }
                        <div className="priceinfo">
                            {this.state.gameFullDescription.gameOfferAmount !== 0 ?
                                <>
                                    <h4 className="oldPrice">RUB {this.state.gameFullDescription.oldGamePrice}</h4>
                                    <h4 className="newPrice">RUB {this.state.gameFullDescription.gamePrice}</h4>
                                </>
                                : <h4 className="normalPrice">RUB {this.state.gameFullDescription.oldGamePrice}</h4>
                            }
                        </div>
                        <div className="buttons-options">
                            <label className="userRating">Ваш рейтинг</label>
                            <div className="starContainer">
                                <RatingStars
                                    gameScore={this.state.gameFullDescription.userScore}
                                    ratingChanged={this.ratingChanged}
                                    isItEditable={true}
                                    size={30}
                                    starColor="gold"
                                />
                            </div>
                            <div className="orderButton">
                                <select onChange={this.setAmount}>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                    <option value="9">9</option>
                                    <option value="10">10</option>
                                </select>
                                <Button variant="outline-success" onClick={this.addOrder} className="buyButton"><Icon icon={androidCart} size={22} />В корзину</Button>
                            </div>
                        </div>
                    </div>
                    <div id="header">
                        <div>
                            <span className="gameName">{this.state.gameFullDescription.gameName}</span>
                            <ul>
                                <li>
                                    {this.state.gameFullDescription.gamePlatform}
                                </li>
                                <li>
                                    |
                                   </li>
                                <li>
                                    {this.state.gameFullDescription.gameJenre}
                                </li>
                                <li>
                                    |
                                     </li>
                                <li>
                                    {this.state.gameFullDescription.gameRating}
                                </li>
                            </ul>
                            <div className="userStars">
                                <RatingStars isItEditable={false} gameScore={this.state.gameFullDescription.gameScore} size={30} starColor="red" />
                                <span className="slash">|</span>
                                <span className="votedPeople">{this.state.gameFullDescription.amountOfVotes === 0 ? "Еще никто не поставил оценку"
                                    : `Количество оценок: ${this.state.gameFullDescription.amountOfVotes}`
                                }</span>
                            </div>
                        </div>
                    </div>
                    <div id="carousel">
                        <h2>Скриншоты: </h2>
                        <Carousel>
                            {this.state.gameFullDescription.gameScreenshots && this.state.gameFullDescription.gameScreenshots.length !== 0 ? this.state.gameFullDescription.gameScreenshots.map((x, index) => {
                                return (
                                    <Carousel.Item key={index}>
                                        <img className="d-block w-100" src={x} />
                                    </Carousel.Item>
                                );
                            }) : <Carousel.Item>
                                    <img className="d-block w-100" src={PC1} />
                                </Carousel.Item>}
                        </Carousel>
                    </div>
                    <div id="others">
                        <h4>Похожие игры</h4>
                        <div className="otherContainer">
                            {this.state.sameGenreGames.map(x => {
                                return (
                                    <OtherGames
                                        key={x.gameID}
                                        gameImage={x.gameImage}
                                        gameName={x.gameName}
                                        gameID={x.gameID}
                                        gameScore={x.gameScore}
                                        gamePlatform={x.gamePlatform}
                                    />
                                );
                            })}
                        </div>
                    </div>
                    <div id="comments">
                        <h4>Комментарии</h4>
                        <div>
                            {this.state.gameFullDescription && this.state.gameFullDescription.feedbacks ? this.state.gameFullDescription.feedbacks.map((x, index) => {
                                return (
                                    <UsersComments
                                        key={index}
                                        username={x.username}
                                        userImage={x.userAvatar}
                                        commentDate={x.commentDate}
                                        comment={x.comment}
                                    />
                                );
                            }) : null}
                        </div>
                    </div>
                    <div id="userReview">
                        <textarea onChange={this.setUserComment} className="userCommentField" maxLength={500} />
                        <div>
                            <label>{this.state.userComment.length}/500</label>
                            <Button variant="outline-info" onClick={this.addFeedback}>Добавить</Button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        userData: store
    };
}

export default connect(mapStateToProps)(GameDesc2);