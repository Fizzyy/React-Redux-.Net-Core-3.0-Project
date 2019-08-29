import React from 'react';
import '../Catalog/Catalog.css';
import { axiosGet } from '../../CommonFunctions/axioses';
import { GETCURRENTPLATFORMGAMES } from '../../CommonFunctions/URLconstants';
import GameBlockInfo from './GameBlockInfo';

class Catalog extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            games: []
        }
    }

    componentDidMount() {
        (async () => {
            let res = await axiosGet(GETCURRENTPLATFORMGAMES + this.props.match.params.gamePlatform);
            if (res.status === 200) {
                this.setState({ games: res.data });
            }
        })()
    }

    componentDidUpdate(preProps, preState) {
        if (this.props.match.params.gamePlatform !== preProps.match.params.gamePlatform) {
            (async () => {
                let res = await axiosGet(GETCURRENTPLATFORMGAMES + this.props.match.params.gamePlatform);
                if (res.status === 200) {
                    this.setState({ games: res.data });
                }
            })()
        }
    }

    getGamesByRating = (e) => {
        let foundGamesByRating = this.state.games.filter(game => {
            if (e.target.value == "All") return true;
            else {
                if (game.gameRating == e.target.value) return true;
                else return false;
            }
        });
        this.setState({ games: foundGamesByRating });
    }

    getGamesByGenres = (e) => {
        let foundGamesByGenres = this.state.games.filter(game => {
            if (e.target.value == "All") return true;
            else {
                if (game.gameJenre == e.target.value) return true;
                else return false;
            }
        });
        this.setState({ games: foundGamesByGenres });
    }

    render() {
        return (
            <div id="divMainCatalog">
                <div id="divMainCatalog_Options">
                    <h2 style={{ marginTop: '15px' }}>{this.props.match.params.gamePlatform}</h2>
                    <div id="divMainCatalog_Options_Selects">
                        <h4 style={{ marginLeft: '40px', marginTop: '10px' }}>Sorting:</h4>
                        <div className="divContainer_Sort">
                            <label>Критерий:</label>
                            <select className="Selects" style={{ width: '100px' }}>
                                <option>Имя</option>
                                <option>Цена</option>
                                <option>Рейтинг</option>
                            </select>
                        </div>
                        <div className="divContainer_Sort">
                            <label>Тип:</label>
                            <select className="Selects">
                                <option>По возрастанию</option>
                                <option>По убыванию</option>
                            </select>
                        </div>
                    </div>
                    <div id="divMainCatalog_Options_Radios">
                        <h4 style={{ marginLeft: '40px' }}>Жанры:</h4>
                        <ul>
                            <li className="liStyles" >
                                <input type="radio" name="jenres" value="All" defaultChecked onChange={this.getGamesByGenres} />Все
                            </li>
                            <li className="liStyles" >
                                <input type="radio" name="jenres" value="Shooter" onChange={this.getGamesByGenres} />Shooter
                            </li>
                            <li className="liStyles" >
                                <input type="radio" name="jenres" value="Strategy" onChange={this.getGamesByGenres} />Strategy
                            </li>
                            <li className="liStyles" >
                                <input type="radio" name="jenres" value="Racing" onChange={this.getGamesByGenres} />Racing
                            </li>
                            <li className="liStyles" >
                                <input type="radio" name="jenres" value="Fighting" onChange={this.getGamesByGenres} />Fighting
                            </li>
                        </ul>
                    </div>
                    <div id="divMainCatalog_Options_Radios">
                        <h4 style={{ marginLeft: '40px' }}>Возраст:</h4>
                        <ul>
                            <li className="liStyles" >
                                <input type="radio" name="rating" value="All" defaultChecked onChange={this.getGamesByRating} />Все
                            </li>
                            <li className="liStyles" >
                                <input type="radio" name="rating" value="6+" onChange={this.getGamesByRating} />6+
                            </li>
                            <li className="liStyles" >
                                <input type="radio" name="rating" value="12+" onChange={this.getGamesByRating} />12+
                            </li>
                            <li className="liStyles" >
                                <input type="radio" name="rating" value="18+" onChange={this.getGamesByRating} />18+
                            </li>
                        </ul>
                    </div>
                </div>
                <div id="divMainCatalog_Games">
                    <div id="divMainCatalog_Games_GameBlockInfo">
                        {this.state.games.map((x) => {
                            return (
                                <GameBlockInfo
                                    gameID={x.gameID}
                                    gameName={x.gameName}
                                    gameJenre={x.gameJenre}
                                    gamePrice={x.gamePrice}
                                    gameRating={x.gameRating}
                                    gameScore={x.gameScore}
                                    gamePlatform={this.props.match.params.gamePlatform}
                                />
                            );
                        })}
                    </div>
                </div>
            </div>
        );
    }
}

export default Catalog;