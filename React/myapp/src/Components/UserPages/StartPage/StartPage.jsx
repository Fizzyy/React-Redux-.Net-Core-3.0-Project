import React from 'react';
import '../StartPage/StartPage.scss';
import Carousel from 'react-bootstrap/Carousel';
import PC1 from '../../../pc1.jpg';
import PC2 from '../../../pc2.jpg';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

class StartPage extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div id="mainStartPage">
                {this.props.user.username !== "Войти" ?
                    <div className="userGreeting">
                        <h2>С возвращением, {this.props.user.username}!</h2>
                    </div>
                    : null}
                <div className="mainContainer">
                    <div className="platformContainer">
                        <div></div>
                        <div></div>
                        <div></div>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        user: store
    };
}

export default connect(mapStateToProps)(StartPage);