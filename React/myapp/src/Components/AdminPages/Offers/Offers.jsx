import React from 'react';
import '../Offers/Offers.scss';
import { axiosGet, axiosPost, axiosDelete } from '../../CommonFunctions/axioses';
import { GETOFFERGAMES } from '../../CommonFunctions/URLconstants';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';

class Offers extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            offers: []
        }
    }

    componentDidMount() {
        (async () => {
            let res = await axiosGet(GETOFFERGAMES);
            if (res.status === 200) { this.setState({ offers: res.data }); }
        })();
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
                                    <tr>
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
                <div className="">

                </div>
            </div>
        );
    }
}

export default Offers;