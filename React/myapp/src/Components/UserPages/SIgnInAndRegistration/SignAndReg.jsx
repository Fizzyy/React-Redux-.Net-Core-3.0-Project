import React from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import '../SIgnInAndRegistration/SignAndReg.css';
import { Icon } from 'react-icons-kit';
import { user_circle } from 'react-icons-kit/ikons/user_circle';
import { key } from 'react-icons-kit/ionicons/key';
import { axiosPost } from '../../CommonFunctions/axioses';
import { AUTHORIZATION, REGISTRATION } from '../../CommonFunctions/URLconstants';
import store from '../../_REDUX/Storage';
import jwt_decode from 'jwt-decode';

class SignInAndRegistration extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: true,
            username: '',
            password1: '',
            password2: '',
            arePasswordsSame: false
        }
    }

    handleClose = () => {
        this.setState({ show: false });
        this.props.history.push("/");
    }

    setData = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    sendDataToServer = async () => {
        let response = undefined;
        this.props.registration ?
            response = await axiosPost(REGISTRATION, { username: this.state.username, password: this.state.password1 })
            :
            response = await axiosPost(AUTHORIZATION, { username: this.state.username, password: this.state.password1 });

        if (response.status === 200) {
            localStorage.setItem('Token', response.data.token.access_token);
            localStorage.setItem('RefreshToken', response.data.refreshToken);

            let tokendata = jwt_decode(response.data.token.access_token);
            if (tokendata["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == "Admin") this.props.history.push('/Admin');
            debugger;

            store.dispatch({
                type: 'LOGGED_USER',
                username: tokendata["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
                userRole: tokendata["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
                userBalance: Number(tokendata.UserBalance),
                isUserLogged: true
            });

            this.handleClose();
        };
    }

    render() {
        return (
            <Modal show={this.state.show} onHide={this.handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>{this.props.registration ? "Регистрация" : "Вход"}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
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
                    {this.props.registration ?
                        <div className="modal-body-divs">
                            <Icon icon={key} size={26} />
                            <label className="modal-body-labels">Повторите пароль:</label>
                            <input type="password" name="password2" onChange={this.setData} />
                        </div>
                        : null}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-primary" onClick={this.sendDataToServer} id="SignInButton">{this.props.registration ? "Регистрация" : "Вход"}</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}

export default SignInAndRegistration;