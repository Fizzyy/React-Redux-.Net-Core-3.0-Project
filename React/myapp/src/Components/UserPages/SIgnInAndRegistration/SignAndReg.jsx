import React from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import '../SIgnInAndRegistration/SignAndReg.scss';
import { Icon } from 'react-icons-kit';
import { user_circle } from 'react-icons-kit/ikons/user_circle';
import { key } from 'react-icons-kit/ionicons/key';
import axios from 'axios';
import { AUTHORIZATION, REGISTRATION, ACTIVATEKEY, RESETPASSWORD } from '../../CommonFunctions/URLconstants';
import store from '../../_REDUX/Storage';
import jwt_decode from 'jwt-decode';
import { axiosPost } from '../../CommonFunctions/axioses';
import { NotificationManager } from 'react-notifications';
import { connect } from 'react-redux';
import banImage from '../../../ban.jpg';
import Moment from 'moment';

class SignInAndRegistration extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: false,
            username: '',
            oldPassword: '',
            password1: '',
            password2: '',
            arePasswordsSame: true,
            isOldPassCorrect: true
        }
    }

    componentDidMount() {
        this.setState({ show: this.props.showModal });
    }

    componentDidUpdate(preProps, preState) {
        if (this.props.showModal !== preProps.showModal) {
            this.setState({ show: this.props.user.showModal });
        }

        if (this.state.password2 !== preState.password2 || this.state.password1 !== preState.password1) {
            if (this.state.password1 === this.state.password2) this.setState({ arePasswordsSame: true });
            else this.setState({ arePasswordsSame: false });
        }
    }

    handleClose = () => {
        store.dispatch({ type: 'CLOSE_MODAL', showModal: false });
    }

    setData = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    sendDataToServer = async () => {
        let response = undefined;
        this.props.type === "registration" ?
            await axios.post(REGISTRATION, { username: this.state.username, password: this.state.password1 }).then(responsee => { response = responsee })
            :
            await axios.post(AUTHORIZATION, { username: this.state.username, password: this.state.password1 }).then(responsee => { response = responsee });
        if (response.status === 200) {
            if (this.props.type !== "registration") {
                debugger;
                if (!response.data.banReason) {
                    localStorage.setItem('Token', response.data.token.access_token);
                    localStorage.setItem('RefreshToken', response.data.refreshToken);

                    let tokendata = jwt_decode(response.data.token.access_token);
                    //if (tokendata["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == "Admin") this.props.history.push('/Admin');

                    store.dispatch({
                        type: 'LOGGED_USER',
                        username: tokendata["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
                        userRole: tokendata["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
                        userBalance: +tokendata.UserBalance,
                        isUserLogged: true
                    });
                    this.handleClose();
                }
                else {
                    this.handleClose();
                    store.dispatch({ type: 'OPEN_BAN_WINDOW', showModal: true, modalType: 'ban', banUserDate: response.data.banDate, banUserInfo: response.data.banReason });
                }
            }
            else {
                this.handleClose();
                NotificationManager.success('Успешно!', 'Ваша учетная запись создана', 5000);
            }
        };
    }

    updateUserBalance = async () => {
        let res = await axiosPost(ACTIVATEKEY + `username=${this.props.user.username}&keycode=${this.state.password1}`);
        if (res.status === 200) {
            if (Number(res.data) === 0) this.setState({ isOldPassCorrect: false });
            else {
                store.dispatch({ type: 'UPDATE_BALANCE', userBalance: Number(res.data) });
                this.setState({ show: false });
            }
        }
    }

    resetPassword = async () => {
        let res = await axiosPost(RESETPASSWORD + `username=${this.props.user.username}&oldpassword=${this.state.oldPassword}&newpassword=${this.state.password1}`);
        if (res.status === 200) this.setState({ show: false });
        if (res.status === 204) this.setState({ isOldPassCorrect: false });
    }

    propsToButton = () => {
        switch (this.props.type) {
            case 'login': return <Button variant="outline-primary" onClick={this.sendDataToServer} id="SignInButton">Вход</Button>;
            case 'registration': return <Button variant="outline-success" onClick={this.sendDataToServer} id="SignInButton">Регистрация</Button>;
            case 'balance': return <Button variant="outline-info" id="SignInButton" onClick={this.updateUserBalance}>Пополнить</Button>;
            case 'password': return <Button variant="outline-primary" id="SignInButton" onClick={this.resetPassword}>Обновить</Button>;
            case 'ban': return <Button variant="outline-info" id="SignInButton" onClick={this.handleClose}>Ок</Button>;
        }
    }

    loginAndRegistration = () => {
        return (
            <div className="loginContainer">
                <div className="loginSpans">
                    <div>
                        <Icon icon={user_circle} size={26} />
                        <label className="modal-body-labels">Логин:</label>
                    </div>
                    <div>
                        <Icon icon={key} size={26} />
                        <label className="modal-body-labels">Пароль:</label>
                    </div>
                    {this.props.type === "registration" ?
                        <div>
                            <Icon icon={key} size={26} />
                            <label className="modal-body-labels">Пароль:</label>
                        </div>
                        : null}
                </div>
                <div className="loginInputs">
                    <div>
                        <input type="text" name="username" onChange={this.setData} />
                    </div>
                    <div>
                        <input type="password" name="password1" onChange={this.setData} />
                        {!this.state.arePasswordsSame && this.props.type === "registration" ?
                            <label>Пароли не совпадают</label>
                            : null}
                    </div>
                    {this.props.type === "registration" ?
                        <div>
                            <input type="password" name="password2" onChange={this.setData} />
                            {!this.state.arePasswordsSame && this.props.type === "registration" ?
                                <label>Пароли не совпадают</label>
                                : null}
                        </div>
                        : null}
                </div>
            </div>
        );
    }

    updateBalance = () => {
        return (
            <div className="updateBalanceBody">
                <div>
                    <span>Введите код: </span>
                    <input type="text" name="password1" onChange={this.checkNewPasswords} />
                </div>
                {!this.state.isOldPassCorrect ?
                    <span className="statusMessage" style={{ color: 'red' }}>Данный код недействителен</span>
                    : null}
            </div>
        );
    }

    checkNewPasswords = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    updatePassword = () => {
        return (
            <div className="updatePasswordBody">
                <div className="passwordSpans">
                    <div>
                        <span>Старый пароль:</span>
                    </div>
                    <div>
                        <span>Новый пароль:</span>
                    </div>
                    <div>
                        <span>Подтвердите пароль:</span>
                    </div>
                </div>
                <div className="passwordInputs">
                    <div>
                        <input type="password" onChange={this.checkNewPasswords} name="oldPassword" />
                        {!this.state.isOldPassCorrect ?
                            <label>Неверный пароль</label>
                            : null}
                    </div>
                    <div>
                        <input type="password" onChange={this.checkNewPasswords} name="password1" />
                        {!this.state.arePasswordsSame ?
                            <label>пароли не совпадают</label>
                            : null}
                    </div>
                    <div>
                        <input type="password" onChange={this.checkNewPasswords} name="password2" />
                        {!this.state.arePasswordsSame ?
                            <label>пароли не совпадают</label>
                            : null}
                    </div>
                </div>
            </div>
        );
    }

    banUser = () => {
        return (
            <div className="banInfo-Container">
                <img src={banImage} />
                <label><span>Причина:</span> {this.props.modal.banUserInfo} ({Moment(this.props.modal.banUserDate).format("DD-MM-YYYY")})</label>
                <label>Свяжитесь с администратором для снятия бана</label>
            </div>);
    }

    render() {
        return (
            <Modal show={this.state.show} onHide={this.handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        {(() => {
                            switch (this.props.type) {
                                case 'login': return ("Вход");
                                case 'registration': return ("Регистрация");
                                case 'balance': return ("Пополнение баланса");
                                case 'password': return ("Смена пароля");
                                case 'ban': return "Бан";
                            }
                        })()}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {(() => {
                        switch (this.props.type) {
                            case 'login': return (this.loginAndRegistration());
                            case 'registration': return (this.loginAndRegistration());
                            case 'balance': return (this.updateBalance());
                            case 'password': return (this.updatePassword());
                            case 'ban': return this.banUser();
                        }
                    })()}
                </Modal.Body>
                <Modal.Footer>
                    {this.propsToButton()}
                </Modal.Footer>
            </Modal>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        user: store.user,
        modal: store.modal
    }
}

export default connect(mapStateToProps)(SignInAndRegistration);