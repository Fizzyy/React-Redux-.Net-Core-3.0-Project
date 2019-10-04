import React from 'react';
import '../ChatWindow/ChatWindow.scss';
import { androidSend } from 'react-icons-kit/ionicons/androidSend';
import { Icon } from 'react-icons-kit';
import { HubConnectionBuilder } from '@aspnet/signalr';
import { connect } from 'react-redux';
import { iosClose } from 'react-icons-kit/ionicons/iosClose';
import { axiosGet } from '../../CommonFunctions/axioses';
import { GETMESSAGESFROMROOM } from '../../CommonFunctions/URLconstants';

class ChatWindow extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            userMessage: '',
            connection: undefined,
            nick: '',
            roomID: '',
            messages: [],
            isChatOpened: false
        }
    }

    async componentDidMount() {
        let nick = this.props.user.username;
        let userChat = await axiosGet(GETMESSAGESFROMROOM + nick);
        if (userChat.status === 200) {
            this.setState({ messages: userChat.data.messages, roomID: userChat.data.roomID });
        }

        let connection = new HubConnectionBuilder().withUrl('https://localhost:44312/chat').build();

        this.setState({ connection, nick }, () => {
            this.state.connection.on("Send", (roomID, nick, data) => {
                if (roomID === this.state.roomID) {
                    console.log(roomID + '---' + nick + '---' + data);
                    this.setState({ messages: this.state.messages.concat({ username: nick, messageText: data }) });
                }
            });
            this.state.connection.start();//.then(() => this.state.connection.invoke("Send", "Start", "Hello"));
        })

    }

    // moveChat = (e) => {
    //     if (!this.state.isChatOpened) {
    //         e.currentTarget.style.transform = "translateY(-400px)";
    //         e.currentTarget.style.transitionDuration = ".5s";
    //         this.setState({ isChatOpened: true });
    //     }
    //     else {
    //         e.currentTarget.style.transform = "translateX(0px)";
    //         e.currentTarget.style.transitionDuration = ".5s";
    //         this.setState({ isChatOpened: false });
    //     }
    // }

    changeUserMessage = (e) => {
        this.setState({ userMessage: e.target.value });
    }

    sendMessage = () => {
        this.state.connection.invoke("Send", this.state.roomID, this.props.user.username, this.state.userMessage);
        this.setState({ userMessage: '' });
    }

    render() {
        return (
            <div className="chatContainer" tabIndex="3" data-chat="chat" onClick={this.moveChat}>
                <div className="chatHeader">
                    <span>Оставляйте ваше сообщение</span>
                    <span>С вами скоро свяжутся</span>
                </div>
                <div className="chatBody">
                    {this.state.messages ? this.state.messages.map((x, index) => {
                        return (
                            x.username === this.state.nick ?
                                <div className="MyMessage" key={index}>
                                    <span>{x.messageText}</span>
                                </div>
                                :
                                <div className="PartnerMessage" key={index}>
                                    <span>{x.messageText}</span>
                                </div>
                        );
                    }) : <h4>Напишите сообщение!</h4>}
                </div>
                <div className="chatFooter">
                    <textarea onChange={this.changeUserMessage} className="userMessage" placeholder="Введите сообщение" />
                    <div>
                        <Icon icon={androidSend} size={30} className="sendButton" onClick={this.sendMessage} />
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        user: store.user
    };
}

export default connect(mapStateToProps)(ChatWindow);