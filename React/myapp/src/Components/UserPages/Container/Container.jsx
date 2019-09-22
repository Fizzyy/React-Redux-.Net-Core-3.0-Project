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
import Chat from '../Chat/Chat';

class Container extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showModal: true
        }
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
                        <Route path="/SignIn" render={props => <SignInAndRegistration {...props} showModal={this.state.showModal} showOrHideModal={(showOrHide) => { this.setState({ showModal: showOrHide }) }} type="login" />} />
                        <Route path="/SignUp" render={props => <SignInAndRegistration {...props} showModal={this.state.showModal} showOrHideModal={(showOrHide) => { this.setState({ showModal: showOrHide }) }} type="registration" />} />
                        <Route path="/Catalog">
                            <Route exact path="/Catalog/:gamePlatform" render={props => <Catalog {...props} />} />
                            <Route exact path="/Catalog/:gamePlatform/:gameID" render={props => <GameDescription2 {...props} />} />
                        </Route>
                        <Route path="/AccountSettings" render={props => <AccountSettings {...props} />} />
                        <Route path="/MyOrders" component={MyOrders} />
                        <Route path="/Offers" render={props => <Catalog {...props} isItOffer={true} />} />
                        <Route path="/Chat" component={Chat} />
                        {/* <Redirect to="/" /> */}
                        <NotificationContainer />
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
                            <Route exact path="/Admin" component={AdminStartPage} />
                            <Route exact path="/Admin/Games" component={Games} />
                            <Route exact path="/Admin/Users" render={() => <Users />} />
                            <Route exact path="/Admin/Offers" render={() => <Offers />} />
                        </div>
                    </Route>
                </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        userData: store
    };
}

export default connect(mapStateToProps)(Container);