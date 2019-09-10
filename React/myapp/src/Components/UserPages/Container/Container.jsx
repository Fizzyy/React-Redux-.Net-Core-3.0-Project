import React from 'react';
import MenuBar from '../MenuBar/MenuBar';
import '../Container/Container.css';
import Footer from '../Footer/Footer';
import { Route } from 'react-router-dom';
import StartPage from '../StartPage/StartPage';
import Catalog from '../Catalog/Catalog';
import GameDescription from '../GameDescription/GameDescription';
import AccountSettings from '../AccountSettings/AccountSettings';
import MyOrders from '../MyOrders/MyOrders';
import SignInAndRegistration from '../SIgnInAndRegistration/SignAndReg';
import jwt_decode from 'jwt-decode';

class Container extends React.Component {
    render() {
        // if (res !== null && res["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == "Admin") {
        //     return (<h1>ANIME!</h1>);
        // }
        // else {
        return (
            <div id='mainBlockForUser'>
                <div id='menuBar'>
                    <MenuBar />
                </div>
                <div id='bodyy'>
                    <Route exact path="/" render={() => <StartPage />} />
                    <Route path="/SignIn" render={props => <SignInAndRegistration {...props} registration={false} />} />
                    <Route path="/SignUp" render={props => <SignInAndRegistration {...props} registration={true} />} />
                    <Route path="/Catalog">
                        <Route exact path="/Catalog/:gamePlatform" render={props => <Catalog {...props} />} />
                        <Route exact path="/Catalog/:gamePlatform/:gameID" render={props => <GameDescription {...props} />} />
                    </Route>
                    <Route path="/AccountSettings" component={AccountSettings} />
                    <Route path="/MyOrders" component={MyOrders} />
                    <Route path="/Offers" render={props => <Catalog {...props} isItOffer={true} />} />
                </div>
                <div id='footer'>
                    <Footer />
                </div>
            </div>
        );
        //}
    }
}

export default Container;