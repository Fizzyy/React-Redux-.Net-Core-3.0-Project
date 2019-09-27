import React from 'react';
import '../Offers/Offers.scss';
import { axiosGet, axiosPost, axiosDelete, axiosPut } from '../../CommonFunctions/axioses';
import { GETOFFERGAMES, GETALLGAMES, ADDOFFER, UPDATEOFFER, DELETEOFFER } from '../../CommonFunctions/URLconstants';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';

class Offers extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            offers: [],
            iDs: [],
            selectedOffer: {}
        }
    }

    async componentDidMount() {
        let res = await axiosGet(GETOFFERGAMES);
        if (res.status === 200) { this.setState({ offers: res.data }); }
        let gamesIDs = await axiosGet(GETALLGAMES);
        if (gamesIDs.status === 200) { this.setState({ iDs: gamesIDs.data }); }
    }

    componentDidUpdate(preProps, preState) {
        if (preState.selectedOffer !== this.state.selectedOffer) console.info(this.state.selectedOffer);
    }

    selectOffer = (e) => {
        this.state.selectedOffer = this.state.offers.find(offer => {
            if (offer.gameID === e.currentTarget.dataset.id) return true;
        });
        this.setState({ selectedOffer: this.state.selectedOffer });
    }

    setOfferAmount = (e) => {
        //debugger;
        let offer = Number(e.target.value);
        let newPrice = this.state.selectedOffer.oldGamePrice - (this.state.selectedOffer.oldGamePrice * (offer / 100));
        this.setState({
            selectedOffer: {
                ...this.state.selectedOffer,
                gamePrice: newPrice,
                gameOfferAmount: offer
            }
        });
    }

    selectGameID = (e) => {
        //debugger;
        let gamee = this.state.iDs.find((game) => {
            if (game.gameID === e.target.value) return true;
        });
        this.setState({
            selectedOffer: {
                ...this.state.selectedOffer,
                gameID: gamee.gameID,
                gameName: gamee.gameName,
                gamePlatform: gamee.gamePlatform,
                gameOfferAmount: 0,
                oldGamePrice: gamee.oldGamePrice
            }
        });
    }

    addOffer = async () => {
        let res = await axiosPost(ADDOFFER, {
            GameID: this.state.selectedOffer.gameID,
            GameOfferAmount: this.state.selectedOffer.gameOfferAmount,
            GameNewPrice: this.state.selectedOffer.gamePrice
        });
    }

    updateOffer = async () => {
        let res = await axiosPut(UPDATEOFFER, {
            GameID: this.state.selectedOffer.gameID,
            GameOfferAmount: this.state.selectedOffer.gameOfferAmount,
            GameNewPrice: this.state.selectedOffer.gamePrice
        })
    }

    deleteOffer = async () => {
        let res = await axiosDelete(DELETEOFFER + this.state.selectedOffer.gameID);
    }

    render() {
        return (
            <div className="mainBlockOffer">
                <div className="offerTable">
                    <Table hover bordered>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Название</th>
                                <th>Платформа</th>
                                <th>Начальная цена(p)</th>
                                <th>Скидка(%)</th>
                                <th>Новая цена(p)</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.offers.map((x) => {
                                return (
                                    <tr key={x.gameID} data-id={x.gameID} onClick={this.selectOffer}>
                                        <td>{x.gameID}</td>
                                        <td>{x.gameName}</td>
                                        <td>{x.gamePlatform}</td>
                                        <td>{x.oldGamePrice}</td>
                                        <td>{x.gameOfferAmount}</td>
                                        <td>{x.gamePrice}</td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </Table>
                </div>
                <div className="offerDescription">
                    <div>
                        <span>ID: <select value={this.state.selectedOffer.gameID} onChange={this.selectGameID}>{
                            this.state.iDs.map(x => {
                                return (
                                    <option key={x.gameID} value={x.gameID}>{x.gameID}</option>
                                );
                            })
                        }
                        </select>
                        </span>
                        <span>Название: <span className="selectInfo">
                            {this.state.selectedOffer.gameName}
                        </span></span>
                        <span>Платформа: <span className="selectInfo">{this.state.selectedOffer.gamePlatform}</span></span>
                    </div>
                    <div>
                        <span>Старая цена: <span className="selectInfo">{this.state.selectedOffer.oldGamePrice}p</span></span>
                        <div className="offerAmount">
                            <span>Скидка (%):</span>
                            <input type="text" placeholder={this.state.selectedOffer.gameOfferAmount} onChange={this.setOfferAmount} />
                        </div>
                        <span>Цена со скидкой: <span className="selectInfo">{this.state.selectedOffer.gamePrice}p</span></span>
                    </div>
                    <div className="optionButtons">
                        <Button variant="outline-success" onClick={this.addOffer}>Добавить</Button>
                        <Button variant="outline-info" onClick={this.updateOffer}>Обновить</Button>
                        <Button variant="outline-danger" onClick={this.deleteOffer}>Удалить</Button>
                    </div>
                </div>
            </div>
        );
    }
}

export default Offers;