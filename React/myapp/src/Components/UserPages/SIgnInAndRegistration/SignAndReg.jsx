import React from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import '../SIgnInAndRegistration/SignAndReg.css';
import { Icon } from 'react-icons-kit';
import { user_circle } from 'react-icons-kit/ikons/user_circle';
import { key } from 'react-icons-kit/ionicons/key';
import axios from 'axios';
import { AUTHORIZATION, REGISTRATION } from '../../CommonFunctions/URLconstants';
import store from '../../_REDUX/Storage';
import jwt_decode from 'jwt-decode';

class SignInAndRegistration extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: false,
            username: '',
            password1: '',
            password2: '',
            arePasswordsSame: false
        }
    }

    componentDidMount() {
        this.setState({ show: this.props.showModal });
    }

    componentDidUpdate(preProps) {
        if (this.props.showModal !== preProps.showModal) {
            this.setState({ show: this.props.showModal });
        }
    }

    handleClose = () => {
        this.setState({ show: false });
        this.props.showOrHideModal(false);
    }

    setData = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    sendDataToServer = async () => {
        let response = undefined;
        this.props.registration ?
            await axios.post(REGISTRATION, { username: this.state.username, password: this.state.password1 }).then(responsee => { response = responsee })
            :
            await axios.post(AUTHORIZATION, { username: this.state.username, password: this.state.password1 }).then(responsee => { response = responsee });
        if (response.status === 200) {
            localStorage.setItem('Token', response.data.token.access_token);
            localStorage.setItem('RefreshToken', response.data.refreshToken);

            let tokendata = jwt_decode(response.data.token.access_token);
            if (tokendata["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == "Admin") this.props.history.push('/Admin');

            store.dispatch({
                type: 'LOGGED_USER',
                username: tokendata["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
                userRole: tokendata["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
                userBalance: +tokendata.UserBalance,
                isUserLogged: true
            });
        };
        this.handleClose();
    }

    login = () => {
        return (
            <>
                <div className="modal-body-divs">
                    <Icon icon={user_circle} size={26} />
                    <label className="modal-body-labels">Логин:</label>
                    <input type="text" className="modal-body-inputs" name="username" onChange={this.setData} />
                </div>
                <div className="modal-body-divs">
                    <Icon icon={key} size={26} />
                    <label className="modal-body-labels">Пароль:</label>
                    <input type="password" name="password1" onChange={this.setData} />
                </div>
            </>
        );
    }

    registration = () => {
        return (
            <>
                {this.login()}
                <div className="modal-body-divs">
                    <Icon icon={key} size={26} />
                    <label className="modal-body-labels">Повторите пароль:</label>
                    <input type="password" name="password2" onChange={this.setData} />
                </div>
            </>
        );
    }

    updateBalance = () => {
        return (
            <h1>balance</h1>
        );
    }

    updatePassword = () => {
        return (
            <h1>password</h1>
        );
    }

    banUser = () => {
        return (<h1>ban</h1>);
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
                                case 'ban': return "Бан пользователя";
                            }
                        })()}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {(() => {
                        switch (this.props.type) {
                            case 'login': return (this.login());
                            case 'registration': return (this.registration());
                            case 'balance': return (this.updateBalance());
                            case 'password': return (this.updatePassword());
                            case 'ban': return this.banUser();
                        }
                    })()}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-primary" onClick={this.sendDataToServer} id="SignInButton">
                        {(() => {
                            switch (this.props.type) {
                                case 'login': return "Вход";
                                case 'registration': return "Регистрация";
                                case 'balance': return "Пополнить";
                                case 'password': return "Обновить";
                                case 'ban': return "Забанить";
                            }
                        })()}
                    </Button>
                </Modal.Footer>
            </Modal>
        );
    }
}

export default SignInAndRegistration;