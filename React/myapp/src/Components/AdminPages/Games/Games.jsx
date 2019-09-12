import React from 'react';
import '../Games/Games.scss';
import { axiosGet } from '../../CommonFunctions/axioses';
import { GETALLGAMES } from '../../CommonFunctions/URLconstants';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import { Image, Transformation, CloudinaryContext } from 'cloudinary-react';

class Games extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            games: [],
            selectedGame: {
                gameImage: 'iTechArt/ylslnjnevinqrik0eql0'
            }
        }
    }

    componentDidMount() {
        (async () => {
            let res = await axiosGet(GETALLGAMES);
            if (res.status === 200) {
                this.setState({ games: res.data });
            }
        })();
    }

    getGame = (e) => {
        this.state.selectedGame = this.state.games.find(game => {
            if (game.gameName === e.currentTarget.dataset.id) return true;
        });
        this.setState({ selectedGame: this.state.selectedGame });
    }

    render() {
        return (
            <div className="gameContainer">
                <div className="gameList">
                    <Table bordered hover>
                        <thead>
                            <th>ID</th>
                            <th>Название</th>
                        </thead>
                        <tbody>
                            {this.state.games.map((x) => {
                                return (
                                    <tr key={x.gameID} data-id={x.gameName} onClick={this.getGame}>
                                        <td>{x.gameID}</td>
                                        <td>{x.gameName}</td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </Table>
                </div>
                <div className="gameFullInfo">
                    <div className="gameInfo">
                        <div>
                            <CloudinaryContext cloudName="djlynoeio">
                                <Image publicId={this.state.selectedGame.gameImage} height="350" width="260" />
                            </CloudinaryContext>
                        </div>
                        <div className="gameDesc">
                            <div className="optButtons">
                                <Button variant="outline-secondary">Очистить</Button>
                                <Button variant="outline-primary">Сохранить</Button>
                                <Button variant="outline-success">Добавить</Button>
                                <Button variant="outline-danger">Удалить</Button>
                            </div>
                            <div>
                                <h1>{this.state.selectedGame.gameName} ({this.state.selectedGame.gameID})</h1>
                            </div>
                            <div>
                                <span>Плафторма: </span>
                                <input type="text" value={this.state.selectedGame.gamePlatform} />
                            </div>
                            <div>
                                <span>Жанр: </span>
                                <input type="text" value={this.state.selectedGame.gameJenre} />
                            </div>
                            <div>
                                <span>Рейтинг: </span>
                                <input type="text" value={this.state.selectedGame.gameRating} />
                            </div>
                            <div>
                                <span>Цена: </span>
                                <input type="text" value={this.state.selectedGame.gamePrice} />
                            </div>
                            <div>
                                <span>Общая оценка: {this.state.selectedGame.gamePrice}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Games;