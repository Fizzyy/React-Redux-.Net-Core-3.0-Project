import React from 'react';
import '../StartPage/StartPage.scss';
import { Icon } from 'react-icons-kit';
import { speech_bubbles } from 'react-icons-kit/ikons/speech_bubbles';
import { Link } from 'react-router-dom';
import { fire } from 'react-icons-kit/metrize/fire';
import { iosGameControllerB } from 'react-icons-kit/ionicons/iosGameControllerB';
import { person } from 'react-icons-kit/ionicons/person';

class StartPage extends React.Component {
    constructor(props) {
        super(props);
    }

    goToPage = (e) => {
        switch (e.currentTarget.dataset.id) {
            case "Games": return (this.props.history.push("/Admin/Games"));
            case "Offers": return (this.props.history.push("/Admin/Offers"));
            case "Users": return (this.props.history.push("/Admin/Users"));
            case "Chats": return (this.props.history.push("/Admin/Chats"));
        }
    }

    render() {
        return (
            <div className="startPageContainer">
                <div className="startPageOptionsContainer">
                    <h1>С возвращением!</h1>
                    <div className="OptionsContainer">
                        <div className="rowDirection">
                            <div className="optContainer" data-id="Games" onClick={this.goToPage}>
                                <Icon icon={iosGameControllerB} size={90} />
                                <span>Товары</span>
                            </div>
                            <div className="optContainer" data-id="Offers" onClick={this.goToPage}>
                                <Icon icon={fire} size={90} />
                                <span>Предложения</span>
                            </div>
                        </div>
                        <div className="rowDirection">
                            <div className="optContainer" data-id="Users" onClick={this.goToPage}>
                                <Icon icon={person} size={90} />
                                <span>Пользователи</span>
                            </div>
                            <div className="optContainer" data-id="Chats" onClick={this.goToPage}>
                                <Icon icon={speech_bubbles} size={90} />
                                <span>Чаты</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default StartPage;