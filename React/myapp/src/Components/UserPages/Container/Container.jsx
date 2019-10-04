import React from 'react';
import MenuBar from '../MenuBar/MenuBar';
import '../Container/Container.css';
import Footer from '../Footer/Footer';
import { Route, Redirect } from 'react-router-dom';
import StartPage from '../StartPage/StartPage';
import Catalog from '../Catalog/Catalog';
import GameDescription2 from '../GameDescription/GameDescription2';
import AccountSettings from '../AccountSettings/AccountSettings';
import MyOrders from '../MyOrders/MyOrders';
import SignInAndRegistration from '../SIgnInAndRegistration/SignAndReg';
import { connect } from 'react-redux';
import 'react-notifications/lib/notifications.css';
import { NotificationContainer } from 'react-notifications';
import AdminStartPage from '../../AdminPages/StartPage/StartPage';
import AdminMenuBar from '../../AdminPages/MenuBar/MenuBar';
import Games from '../../AdminPages/Games/Games';
import Users from '../../AdminPages/Users/Users';
import Offers from '../../AdminPages/Offers/Offers';
import ChatWindow from '../ChatWindow/ChatWindow';
import Chats from '../../AdminPages/Chats/Chats';

class Container extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showModal: false,
            modalType: ''
        }
    }

    componentDidUpdate(preProps, preState) {
        if (this.props.modalData.showModal !== preProps.modalData.showModal) this.setState({ showModal: this.props.modalData.showModal, modalType: this.props.modalData.modalType });
    }

    render() {
        return (
            this.props.userData.userRole != "Admin" ?
                <div id='mainBlockForUser'>
                    <div id='menuBar'>
                        <MenuBar />
                    </div>
                    <div id='bodyy'>
                        <Route exact path="/" render={() => <StartPage />} />
                        <Route path="/Catalog">
                            <Route exact path="/Catalog/:gamePlatform" render={props => <Catalog {...props} />} />
                            <Route exact path="/Catalog/:gamePlatform/:gameID" render={props => <GameDescription2 {...props} />} />
                        </Route>
                        <Route path="/AccountSettings" render={props => <AccountSettings {...props} />} />
                        <Route path="/MyOrders" component={MyOrders} />
                        <Route path="/Offers" render={props => <Catalog {...props} isItOffer={true} />} />
                        {/* <Redirect to="/" /> */}
                        <NotificationContainer />
                        <ChatWindow />
                        {this.state.showModal ?
                            <SignInAndRegistration showModal={this.state.showModal} type={this.state.modalType} /> : null}
                    </div>
                    <div id='footer'>
                        <Footer />
                    </div>
                </div>
                :
                <div id='mainBlockForAdmin'>
                    <Route path="/Admin">
                        <div>
                            <AdminMenuBar />
                        </div>
                        <div className="components">
                            <Route exact path="/Admin" render={props => <AdminStartPage {...props} />} />
                            <Route exact path="/Admin/Games" component={Games} />
                            <Route exact path="/Admin/Users" render={() => <Users />} />
                            <Route exact path="/Admin/Offers" render={() => <Offers />} />
                            <Route exact path="/Admin/Chats" render={() => <Chats />} />
                        </div>
                    </Route>
                </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        userData: store.user,
        modalData: store.modal
    };
}

export default connect(mapStateToProps)(Container);