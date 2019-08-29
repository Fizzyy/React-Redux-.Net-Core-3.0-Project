import React from 'react';
import '../MyOrders/MyOrders.css';
import { axiosGet } from '../../CommonFunctions/axioses';
import { GETUNPAIDORDERS } from '../../CommonFunctions/URLconstants';
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
            totalSumToPay: 0
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
            this.setState({ selectedOrders: this.state.selectedOrders });
        }
        if (index === -1) {
            this.state.selectedOrders.push(e.target.value)
            this.setState({ selectedOrders: this.state.selectedOrders });
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
                                            <input type="checkbox" value={x.orderID} onChange={this.selectOrder} />
                                        </td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </Table>
                    <div>

                    </div>
                </div>
                <div className="emptyDivsOrders">
                    <div className="divWithOrdersOptions">
                        <p className="pMoneyInfo">Ваш баланс: {this.state.userBalance}p</p>
                        <p className="pMoneyInfo">Всего к оплате: {this.state.totalSumToPay}p</p>
                        <div style={{ marginTop: '10px' }}>
                            <Button variant="outline-danger">Удалить</Button>
                            <Button variant="outline-success" style={{ marginLeft: '10px' }}>Оплатить</Button>
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

export default connect(mapStateToProps)(MyOrders);