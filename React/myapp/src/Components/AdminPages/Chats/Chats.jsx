import React from 'react';
import '../Chats/Chat.scss';
import { GETROOMS, GETMESSAGESFROMROOM } from '../../CommonFunctions/URLconstants';
import { axiosGet } from '../../CommonFunctions/axioses';
import Button from 'react-bootstrap/Button';
import { HubConnectionBuilder } from '@aspnet/signalr';
import { connect } from 'react-redux';
import { Image, Transformation, CloudinaryContext } from 'cloudinary-react';

class Chats extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            rooms: [],
            selectedRoom: {},
            adminComment: '',
            connection: undefined
        }
    }

    async componentDidMount() {
        let res = await axiosGet(GETROOMS);
        if (res.status === 200) { this.setState({ rooms: res.data }); }

        let connection = new HubConnectionBuilder().withUrl("https://localhost:44312/chat").build();

        this.setState({ connection }, () => {
            this.state.connection.on("Send", (roomID, nick, data) => {
                if (this.state.selectedRoom.roomID === roomID) {
                    this.setState({
                        selectedRoom: {
                            ...this.state.selectedRoom,
                            messages: this.state.selectedRoom.messages.concat({ username: nick, messageText: data })
                        }
                    });
                }
            });
            this.state.connection.start();
        });
    }

    selectRoom = async (e) => {
        let res = await axiosGet(GETMESSAGESFROMROOM + e.currentTarget.dataset.id);
        if (res.status === 200) this.setState({ selectedRoom: res.data });
    }

    sendMessage = () => {
        debugger;
        this.state.connection.invoke("Send", this.state.selectedRoom.roomID, this.props.user.username, this.state.adminComment);
        this.setState({ adminComment: '' });
    }

    render() {
        return (
            <div className="chatsContainer">
                <div className="allChatRooms">
                    {this.state.rooms.map((x, index) => {
                        return (
                            // <div key={x.username} data-id={x.username} onClick={this.selectRoom}>{x.username}</div>
                            <div className="roomsContainer" key={index} data-id={x.username} onClick={this.selectRoom}>
                                <CloudinaryContext cloudName="djlynoeio">
                                    <Image publicId={x.userImage}>
                                        <Transformation width={50} height={50} radius="max" crop="fill" />
                                    </Image>
                                </CloudinaryContext>
                                <span>{x.username}</span>
                            </div>
                        );
                    })}
                </div>
                {this.state.selectedRoom.roomID ?
                    <div className="userChatContainer">
                        <div className="messagesContainer">
                            {this.state.selectedRoom.messages ? this.state.selectedRoom.messages.map(x => {
                                return (
                                    this.props.user.username === x.username ?
                                        <div key={x.id} className="adminMessages">
                                            <span>
                                                {x.messageText}
                                            </span>
                                        </div>
                                        :
                                        <div key={x.id} className="clientMessages">
                                            <span>
                                                {x.messageText}
                                            </span>
                                        </div>
                                )
                            }) : null}
                        </div>
                        <div className="adminCommentContainer">
                            <textarea className="adminCommentArea" placeholder="Введите сообщение" value={this.state.adminComment} onChange={e => this.setState({ adminComment: e.target.value })} />
                            <Button variant="outline-primary" className="sendMessageButton" onClick={this.sendMessage}>Отправить</Button>
                        </div>
                    </div>
                    : <h2 id="chooseChat">Выберите чат</h2>}
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        user: store
    };
}

export default connect(mapStateToProps)(Chats);