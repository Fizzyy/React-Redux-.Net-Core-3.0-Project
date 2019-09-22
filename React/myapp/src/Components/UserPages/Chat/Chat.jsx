import React from 'react';
import WebSockets from '../../CommonFunctions/WebSockets';
import axios from 'axios';
import { HubConnection } from 'signalr-client-react';

class Chat extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            message: '',
            connection: undefined
        }
    }

    componentDidMount() {
        let connection = new HubConnection('https://localhost:44312/chat');

        this.setState({ connection }, () => {
            this.state.connection.start().then(() => console.info('START')).catch(error => console.error(error));

            this.state.connection.on('Send', (nick) => console.warn(nick));
        });

        // connection.on('newMessage', data => {
        //     console.log(data);
        // });

        // connection.start();
    }

    sendMessage = () => {
        this.state.connection.invoke('Send', 'HELLO!').catch(error => { debugger; console.error(error) });
    }

    render() {
        return (
            <div>
                <input type="text" onChange={e => this.setState({ message: e.target.value })} />
                <button onClick={this.sendMessage}>send</button>
                {/* <WebSockets /> */}
            </div>
        );
    }
}

export default Chat;