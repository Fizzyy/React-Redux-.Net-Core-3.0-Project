import React from 'react';
import '../StartPage/StartPage.scss';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { search } from 'react-icons-kit/fa/search';
import { windows } from 'react-icons-kit/ikons/windows';
import { playstation } from 'react-icons-kit/ionicons/playstation';
import { xbox } from 'react-icons-kit/ionicons/xbox';
import { Icon } from 'react-icons-kit';
import GameBlockInfo from '../Catalog/GameBlockInfo';
import { axiosGet } from '../../CommonFunctions/axioses';
import { GETGAMESBYREGEX, GETSTARTPAGEGAMES } from '../../CommonFunctions/URLconstants';
import GameSearchBlock from '../StartPage/GameSearchBlock';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

class StartPage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            regexGames: [],
            topGames: []
        }
    }

    async componentDidMount() {
        let res = await axiosGet(GETSTARTPAGEGAMES);
        if (res.status === 200) this.setState({ topGames: res.data });
    }

    getGamesByRegex = async (e) => {
        if (e.target.value === null || e.target.value === "") this.setState({ regexGames: [] });
        else {
            let res = await axiosGet(GETGAMESBYREGEX + `GamePlatform=All&GameName=${e.target.value}`);
            if (res.status === 200) this.setState({ regexGames: res.data });
        }
    }

    render() {
        return (
            <div id="mainStartPage">
                {this.props.user.username !== "Войти" ?
                    <div className="userGreeting">
                        <h2>С возвращением, {this.props.user.username}!</h2>
                    </div>
                    : null}
                <div className="mainContainer">
                    <div className="topBody">
                        <div className="mainLogo"></div>
                        <div className="mainSearch">
                            <div className="mainSearchFieldGroup">
                                <div>
                                    <span>Поиск</span>
                                </div>
                                <input type="text" placeholder="Введите название игры" onChange={this.getGamesByRegex} />
                                <div>
                                    <Icon icon={search} size={28} />
                                </div>
                            </div>
                            <div className="foundGames">
                                {this.state.regexGames ? this.state.regexGames.map((x, index) => {
                                    return (
                                        <GameSearchBlock key={index}
                                            gameName={x.gameName}
                                            gameID={x.gameID}
                                            gameScore={x.gameScore}
                                            gamePlatform={x.gamePlatform}
                                        />
                                    );
                                }) : null}
                            </div>
                        </div>
                        <div className="platformContainer">
                            <Link to="/Catalog/PS4" className="catalogLinks">
                                <div>
                                    <Icon icon={playstation} size={60} className="iconsStyle" />
                                    <span>Playstation</span>
                                </div>
                            </Link>
                            <Link to="/Catalog/XBOXONE" className="catalogLinks">
                                <div>
                                    <Icon icon={xbox} size={60} className="iconsStyle" />
                                    <span>Xbox</span>
                                </div>
                            </Link>
                            <Link to="/Catalog/PC" className="catalogLinks">
                                <div>
                                    <Icon icon={windows} size={60} className="iconsStyle" />
                                    <span>PC</span>
                                </div>
                            </Link>
                        </div>
                    </div>
                    <div className="mostPopularContainer">
                        <h3>Самые популярные игры</h3>
                        <div className="gamesRatingContainer">
                            {this.state.topGames.offers ? this.state.topGames.offers.map((x, index) => {
                                return (
                                    <div>
                                        <GameBlockInfo key={index}
                                            gameID={x.gameID}
                                            gameName={x.gameName}
                                            gameJenre={x.gameJenre}
                                            gameRating={x.gameRating}
                                            gameScore={x.gameScore}
                                            gamePlatform={x.gamePlatform}
                                            gameOfferAmount={x.gameOfferAmount}
                                            gameImage={x.gameImage}
                                            oldGamePrice={x.oldGamePrice}
                                            newGamePrice={x.gamePrice}
                                        />
                                    </div>
                                );
                            }) : null}
                        </div>
                    </div>
                    <div className="mostPopularContainer">
                        <h3>Игры на акциях</h3>
                        <div className="gamesRatingContainer">
                            {this.state.topGames.rated ? this.state.topGames.rated.map((x, index) => {
                                return (
                                    <div>
                                        <GameBlockInfo key={index}
                                            gameID={x.gameID}
                                            gameName={x.gameName}
                                            gameJenre={x.gameJenre}
                                            gameRating={x.gameRating}
                                            gameScore={x.gameScore}
                                            gamePlatform={x.gamePlatform}
                                            gameOfferAmount={x.gameOfferAmount}
                                            gameImage={x.gameImage}
                                            oldGamePrice={x.oldGamePrice}
                                            newGamePrice={x.gamePrice}
                                        />
                                    </div>
                                );
                            }) : null}
                        </div>
                    </div>
                    <div className="siteFeatures">

                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        user: store
    };
}

export default connect(mapStateToProps)(StartPage);