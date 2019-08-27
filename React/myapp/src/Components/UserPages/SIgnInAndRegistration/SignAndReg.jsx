import React from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import '../SIgnInAndRegistration/SignAndReg.css';
import { Icon } from 'react-icons-kit';
import { user_circle } from 'react-icons-kit/ikons/user_circle';
import { key } from 'react-icons-kit/ionicons/key';

class SignInAndRegistration extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: true,
            username: '',
            password: ''
        }
    }

    handleClose = () => {
        this.setState({ show: false });
        this.props.history.push("/");
    }

    render() {
        return (
            <Modal show={this.state.show} onHide={this.handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Вход</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="modal-body-divs">
                        <Icon icon={user_circle} size={26} />
                        <label className="modal-body-labels">Логин:</label>
                        <input type="text" />
                    </div>
                    <div className="modal-body-divs">
                        <Icon icon={key} size={26} />
                        <label className="modal-body-labels">Пароль:</label>
                        <input type="text" />
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-primary" onClick={this.handleClose} id="SignInButton">Вход</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}

export default SignInAndRegistration;