import React from 'react';
import '../Users/Users.scss';
import { axiosGet, axiosPost, axiosDelete } from '../../CommonFunctions/axioses';
import { GETALLUSERS, GETFULLUSERINFO, REVOKEBAN } from '../../CommonFunctions/URLconstants';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import { Image, CloudinaryContext } from 'cloudinary-react';
import Moment from 'moment';
import Modal from '../../UserPages/SIgnInAndRegistration/SignAndReg';

class Users extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            users: [],
            selectedUser: {},
            showModal: false,
            showResetPass: false
        }
    }

    componentDidMount() {
        (async () => {
            let res = await axiosGet(GETALLUSERS);
            if (res.status === 200) {
                this.setState({ users: res.data });
            }
        })();
    }

    componentDidUpdate(preProps, preState) {
        if (this.state.selectedUser.banReason !== preState.selectedUser.banReason) {
            (async () => {
                let res = await axiosGet(GETFULLUSERINFO + this.state.selectedUser.username);
                if (res.status === 200) {
                    this.setState({ selectedUser: res.data });
                }
            })()
        }
    }

    getUser = async (e) => {
        let res = await axiosGet(GETFULLUSERINFO + e.currentTarget.dataset.id);
        if (res.status === 200) {
            this.setState({ selectedUser: res.data });
        }
    }

    banUser = async () => {
        this.setState({ showModal: true });
    }

    unbanUser = async () => {
        let res = await axiosDelete(REVOKEBAN + this.state.selectedUser.username);
        if (res.status === 200) {
            this.setState({
                selectedUser: {
                    ...this.state.selectedUser,
                    banReason: 'new'
                }
            });
        }
    }

    resetPassword = async () => {

    }

    render() {
        return (
            <div className="mainUsers">
                <Modal showModal={this.state.showModal} showOrHideModal={(showOrHide) => { this.setState({ showModal: showOrHide }) }} type="ban" />
                <div className="usersList">
                    <Table hover bordered>
                        <thead>
                            <tr>
                                <th>Логин</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.users.map((x) => {
                                return (
                                    <tr data-id={x.username} key={x.username} onClick={this.getUser}>
                                        <td>{x.username}</td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </Table>
                </div>
                <div className="usersInfo">
                    <div className="userDesc">
                        <div>
                            <CloudinaryContext cloudName="djlynoeio">
                                <Image publicId={this.state.selectedUser.userImage} height="220" width="220" />
                            </CloudinaryContext>
                        </div>
                        <div className="userShortInfo">
                            {this.state.selectedUser.banReason === "-" ?
                                <Button className="banButton" variant="outline-danger" onClick={this.banUser}>Забанить</Button>
                                :
                                <Button className="banButton" variant="outline-danger" onClick={this.unbanUser}>Разбанить</Button>
                            }
                            <div className="temps">
                                <h1>{this.state.selectedUser.username}</h1>
                            </div>
                            <div className="temps">
                                <span>Баланс: {this.state.selectedUser.userBalance}</span>
                            </div>
                            <div className="temps" name="passButton">
                                {!this.state.showResetPass ?
                                    <Button variant="outline-info" onClick={() => { this.setState({ showResetPass: true }) }}>Сбросить пароль</Button>
                                    :
                                    <>
                                        <input type="text" />
                                        <Button variant="outline-info" size="sm" className="resetPass" onClick={this.resetPassword}>Сбросить</Button>
                                    </>
                                }
                            </div>
                            <div className="temps">
                                {this.state.selectedUser.banReason === "-" ?
                                    <div className="activeUser">Статус: активен</div>
                                    : <>
                                        <div className="bannedUser">Статус: забанен</div>
                                        <div className="bannedUser">Причина: {this.state.selectedUser.banReason} ({Moment(this.state.selectedUser.banDate).format("DD-MM-YYYY")})</div>
                                    </>}
                            </div>
                        </div>
                    </div>
                    <div className="userTables">
                        <div className="userGameMarks">
                            <h1>Оценки</h1>
                            <Table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Название</th>
                                        <th>Платформа</th>
                                        <th>Оценка</th>
                                    </tr>
                                </thead>
                                {this.state.selectedUser.gameMarks && this.state.selectedUser.gameMarks.length ? this.state.selectedUser.gameMarks.map((x) => {
                                    return (
                                        <tr key={x.scoreID}>
                                            <td>{x.scoreID}</td>
                                            <td>{x.gameName}</td>
                                            <td>{x.gamePlatform}</td>
                                            <td>{x.score}</td>
                                        </tr>
                                    );
                                }) : null}
                            </Table>
                        </div>
                        <div className="userOrders">
                            <h1>Заказы</h1>
                            <Table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Название</th>
                                        <th>Платформа</th>
                                        <th>Дата заказа</th>
                                        <th>Сумма</th>
                                    </tr>
                                </thead>
                                {this.state.selectedUser.orders && this.state.selectedUser.orders.length ? this.state.selectedUser.orders.map((x) => {
                                    return (
                                        <tr key={x.orderID}>
                                            <td>{x.orderID}</td>
                                            <td>{x.gameName}</td>
                                            <td>{x.gamePlatform}</td>
                                            <td>{Moment(x.orderDate).format("DD-MM-YYYY")}</td>
                                            <td>{x.totalSum}</td>
                                        </tr>
                                    );
                                }) : null}
                            </Table>
                        </div>
                        <div className="userFeedbacks">
                            <h1>Комментарии</h1>
                            <Table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Название</th>
                                        <th>Платформа</th>
                                        <th>Дата</th>
                                        <th>Комментарий</th>
                                    </tr>
                                </thead>
                                {this.state.selectedUser.feedbacks && this.state.selectedUser.feedbacks.length ? this.state.selectedUser.feedbacks.map((x) => {
                                    return (
                                        <tr key={x.commentID}>
                                            <td>{x.commentID}</td>
                                            <td>{x.gameName}</td>
                                            <td>{x.gamePlatform}</td>
                                            <td>{Moment(x.orderDate).format("DD-MM-YYYY")}</td>
                                            <td>{x.comment}</td>
                                        </tr>
                                    );
                                }) : null}
                            </Table>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Users;