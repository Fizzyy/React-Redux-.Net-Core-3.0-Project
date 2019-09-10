import React from 'react';
import '../MyOrders/MyOrders.css';
import { axiosGet, axiosPost, axiosDelete } from '../../CommonFunctions/axioses';
import { GETUNPAIDORDERS, DELETEORDERS, PAYFORORDERS } from '../../CommonFunctions/URLconstants';
import { connect } from 'react-redux';
import Moment from 'moment';
import jwt_decode from 'jwt-decode';
import Table from 'react-bootstrap/Table'
import Button from 'react-bootstrap/Button';

class MyOrders extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            orders: [],
            selectedOrders: [],
            userBalance: 0,
            totalSumToPay: 0,
            tempSum: 0
        }
    }

    componentDidMount() {
        (async () => {
            const token = localStorage.getItem('Token');
            const decoding = jwt_decode(token);
            let res = await axiosGet(GETUNPAIDORDERS + this.props.userData.username);
            if (res.status === 200) {
                for (let i = 0; i < res.data.length; i++) {
                    this.state.totalSumToPay += res.data[i].totalSum;
                }
                this.setState({
                    orders: res.data,
                    userBalance: decoding.UserBalance,
                    totalSumToPay: this.state.totalSumToPay
                });
            }
        })();
    }

    selectOrder = (e) => {
        let index = this.state.selectedOrders.indexOf(e.target.value);
        if (index > -1) {
            this.state.selectedOrders.splice(index, 1)
            this.setState({ selectedOrders: this.state.selectedOrders, tempSum: this.state.tempSum - Number(e.target.name) });
        }
        if (index === -1) {
            this.state.selectedOrders.push(e.target.value)
            this.setState({ selectedOrders: this.state.selectedOrders, tempSum: this.state.tempSum + Number(e.target.name) });
        }
    }

    deleteOrders = async () => {
        let response = await axiosPost(DELETEORDERS, { Username: this.props.userData.username, orders: this.state.selectedOrders });
        if (response.status === 200) {
            for (let i = 0; i < response.data.length; i++) {
                this.state.totalSumToPay += response.data[i].totalSum;
            }
            this.setState({
                orders: response.data,
                userBalance: this.state.tempSum,
                totalSumToPay: this.state.totalSumToPay
            });
        }
    }

    payForOrders = async () => {
        if (this.state.userBalance < this.state.totalSumToPay) alert('Not enough money');
        else {
            let res = await axiosPost(PAYFORORDERS, { Username: this.props.userData.username, orders: this.state.selectedOrders });
            if (res.status === 200) {
                for (let i = 0; i < res.data.length; i++) {
                    this.state.totalSumToPay += res.data[i].totalSum;
                }
                this.setState({
                    orders: res.data,
                    totalSumToPay: this.state.totalSumToPay
                });
            }
        }
    }

    render() {
        return (
            <div id="divMainMyOrders">
                <div className="emptyDivsOrders"></div>
                <div className="divOrders">
                    <h4 style={{ margin: '30px 0 30px 0' }}>Таблица заказов</h4>
                    <Table responsive>
                        <thead>
                            <tr>
                                <th>Название</th>
                                <th>Платформа</th>
                                <th>Дата заказа</th>
                                <th>Количество</th>
                                <th>Цена</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.orders.map((x) => {
                                return (
                                    <tr key={x.orderID}>
                                        <td>{x.gameName}</td>
                                        <td>{x.gamePlatform}</td>
                                        <td>{Moment(x.orderDate).format("DD-MM-YYYY")}</td>
                                        <td>{x.amount}</td>
                                        <td>{x.totalSum}</td>
                                        <td>
                                            <input type="checkbox" value={x.orderID} name={x.totalSum} onChange={this.selectOrder} />
                                        </td>
                                    </tr>
                                );
                            })}
                            <tr>
                                <td colSpan="6">
                                    <div className="tr_MenuOrders">
                                        <div>
                                            <p className="pMoneyInfo">Ваш баланс: {this.state.userBalance}p</p>
                                            <p className="pMoneyInfo">Всего к оплате: {this.state.totalSumToPay}p</p>
                                        </div>
                                        <div className="div_OptionButtons">
                                            <Button variant="outline-danger" onClick={this.deleteOrders}>Удалить</Button>
                                            <Button variant="outline-success" style={{ marginTop: '10px' }} onClick={this.payForOrders}>Оплатить</Button>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </Table>
                    <div>

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

export default connect(mapStateToProps)(MyOrders);