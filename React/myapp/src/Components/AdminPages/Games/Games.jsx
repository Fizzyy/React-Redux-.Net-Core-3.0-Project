import React from 'react';
import '../Games/Games.scss';
import { axiosGet, axiosPost, axiosPut } from '../../CommonFunctions/axioses';
import { GETALLGAMES, ADDGAME, UPDATEGAME, DELETEGAME, DELETEFEEDBACK } from '../../CommonFunctions/URLconstants';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import { Image, CloudinaryContext } from 'cloudinary-react';
import axios from 'axios';

class Games extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            games: [],
            selectComments: [],
            selectedGame: {
                gamePlatform: '',
                gameID: '',
                gameJenre: '',
                gamePrice: 0,
                gameRating: '6+',
                gameName: '',
                gamePlatform: 'PS4',
                gameImage: 'iTechArt/ylslnjnevinqrik0eql0'
            }
        };
        //ImageRef = React.createRef();
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

    clearSelectedItem = () => {
        this.setState({
            selectedGame: {
                gamePlatform: '',
                gameID: '',
                gameJenre: '',
                gamePrice: 0,
                gameRating: '6+',
                gameName: '',
                gamePlatform: 'PS4',
                gameImage: 'iTechArt/ylslnjnevinqrik0eql0'
            }
        });
    }

    changeGameDesc = (e) => {
        this.setState({
            selectedGame: {
                ...this.state.selectedGame,
                [e.target.name]: e.target.value
            }
        });
    }

    selectComments = (e) => {
        let index = this.state.selectComments.indexOf(e.target.value);
        if (index > -1) {
            this.state.selectComments.splice(index, 1)
            this.setState({ selectComments: this.state.selectComments });
        }
        if (index === -1) {
            this.state.selectComments.push(e.target.value)
            this.setState({ selectComments: this.state.selectComments });
        }
    }

    addGame = async () => {
        let res = await axiosPost(ADDGAME, this.state.selectedGame);

        if (res.status === 200) {

        }
    }

    updateGame = async () => {
        let res = await axiosPost(UPDATEGAME, this.state.selectedGame);
        if (res.status === 200) {

        }
    }

    deleteComments = async () => {
        let res = await axiosPost(DELETEFEEDBACK, this.state.selectComments);
        if (res.status === 200) {

        }
    }

    deleteGame = async () => {
        let res = await axiosPost(ADDGAME, this.state.selectedGame);
        if (res.status === 200) {

        }
    }

    uploadGameImage = async () => {
        const formdata = new FormData();
        formdata.append('file', this.state.fileName);
        formdata.append('upload_preset', 'iTechArt');
        let response = await axios.post("https://api.cloudinary.com/v1_1/djlynoeio/image/upload", formdata);
    }

    selectImage = () => {
        //this.ImageRef 
    }

    render() {
        return (
            <div className="gameContainer">
                <div className="gameList">
                    <Table bordered hover>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Название</th>
                            </tr>
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
                            <Button variant="outline-primary" className="addButton" onClick={this.uploadGameImage}>
                                <input type="file" ref={this.ImageRef} hidden onClick={this.selectImage} />
                                Добавить</Button>
                        </div>
                        <div className="gameDesc">
                            <div className="optButtons">
                                <Button variant="outline-secondary" onClick={this.clearSelectedItem}>Очистить</Button>
                                <Button variant="outline-primary" onClick={this.updateGame}>Сохранить</Button>
                                <Button variant="outline-success" onClick={this.addGame}>Добавить</Button>
                                <Button variant="outline-danger" onClick={this.deleteGame}>Удалить</Button>
                            </div>
                            <div>
                                <span>ID: </span>
                                <input type="text" name="gameID" value={this.state.selectedGame.gameID} onChange={this.changeGameDesc} />
                            </div>
                            <div>
                                <span>Название: </span>
                                <input type="text" name="gameName" value={this.state.selectedGame.gameName} onChange={this.changeGameDesc} />
                            </div>
                            <div>
                                <span>Плафторма: </span>
                                <select onChange={this.changeGameDesc} value={this.state.selectedGame.gamePlatform} name="gamePlatform">
                                    <option value="PS4">Playstation 4</option>
                                    <option value="XBOXONE">Xbox One</option>
                                    <option value="PC">PC</option>
                                </select>
                            </div>
                            <div>
                                <span>Жанр: </span>
                                <input type="text" name="gameJenre" value={this.state.selectedGame.gameJenre} onChange={this.changeGameDesc} />
                            </div>
                            <div>
                                <span>Рейтинг: </span>
                                <select onChange={this.changeGameDesc} value={this.state.selectedGame.gameRating} name="gameRating">
                                    <option value="6+">6+</option>
                                    <option value="12+">12+</option>
                                    <option value="18+">18+</option>
                                </select>
                            </div>
                            <div>
                                <span>Цена: </span>
                                <input type="number" name="gamePrice" value={this.state.selectedGame.gamePrice} onChange={this.changeGameDesc} />
                            </div>
                            <div>
                                <span>Общая оценка: {this.state.selectedGame.gameScore}</span>
                            </div>
                        </div>
                    </div>
                    <div className="gamescreens">
                        <div className="rowScreens">
                            <div>

                            </div>
                            <div>

                            </div>
                        </div>
                        <div className="rowScreens">
                            <div>

                            </div>
                            <div>

                            </div>
                        </div>
                    </div>
                    <div className="gametables">
                        <Table bordered>
                            <thead>
                                <tr>
                                    <th>Логин</th>
                                    <th>Комментарий</th>
                                    <th><Button variant="outline-danger" size="sm" onClick={this.deleteComments}>Удалить</Button></th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.selectedGame && this.state.selectedGame.feedbacks ? this.state.selectedGame.feedbacks.map((x) => {
                                    return (
                                        <tr>
                                            <td>{x.username}</td>
                                            <td>{x.comment}</td>
                                            <td><input type="checkbox" value={x.id} onChange={this.selectComments} /></td>
                                        </tr>
                                    );
                                }) : null}
                            </tbody>
                        </Table>
                    </div>
                </div>
            </div>
        );
    }
}

export default Games;