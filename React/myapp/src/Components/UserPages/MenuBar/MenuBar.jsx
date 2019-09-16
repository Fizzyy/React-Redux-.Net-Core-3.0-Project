import React from 'react';
import '../MenuBar/MenuBar.css';
import DropdownButton from 'react-bootstrap/DropdownButton';
import { BrowserRouter, Link, Route } from 'react-router-dom';
import Dropdown from 'react-bootstrap/Dropdown';
import { Icon } from 'react-icons-kit';
import { windows } from 'react-icons-kit/ikons/windows';
import { playstation } from 'react-icons-kit/ionicons/playstation';
import { xbox } from 'react-icons-kit/ionicons/xbox';
import { user_circle } from 'react-icons-kit/ikons/user_circle';
import { ic_account_balance_wallet } from 'react-icons-kit/md/ic_account_balance_wallet';
import { connect } from 'react-redux';
import SignInAndRegistration from '../SIgnInAndRegistration/SignAndReg';

class MenuBar extends React.Component {
    constructor(props) {
        super(props);
    }

    existingItems = () => {
        return (
            <>
                <div className="divMainBar_divsInsideBar">
                    <Link to='/' className='Links'>Главная</Link>
                </div>
                <div className="divMainBar_divsInsideBar">
                    <DropdownButton title="Категория" id="dropDownButton">
                        <Dropdown.Item><Icon size={16} icon={windows} /><Link to="/Catalog/PC" className="dropDownItemsSpans">PC Windows</Link></Dropdown.Item>
                        <Dropdown.Item><Icon size={16} icon={playstation} /><Link to="/Catalog/PS4" className="dropDownItemsSpans">Playstation 4</Link></Dropdown.Item>
                        <Dropdown.Item><Icon size={16} icon={xbox} /><Link to="/Catalog/XBOXONE" className="dropDownItemsSpans">Xbox One</Link></Dropdown.Item>
                    </DropdownButton>
                </div>
                <div className="divMainBar_divsInsideBar">
                    <Link to="/Offers" className="Links">Акции</Link>
                </div>
            </>
        );
    }

    render() {
        return (
            this.props.userData.isUserLogged ?
                <div id="divMainBar">
                    {this.existingItems()}
                    <div className="divMainBar_divsInsideBar">
                        <Link to="/MyOrders" className="Links">Моя корзина</Link>
                    </div>
                    <div className="divMainBar_divsInsideBar_UserData">
                        <div id="divUserInfo">
                            <div className="divUserInfo_divs">
                                <Icon size={26} icon={user_circle} /><Link to="/AccountSettings" className="dropDownItemsSpans" style={{ color: 'whitesmoke' }}>{this.props.userData.username}</Link>
                            </div>
                            <div className="divUserInfo_divs">
                                <Icon size={26} icon={ic_account_balance_wallet} /><Link className="dropDownItemsSpans" style={{ color: 'whitesmoke' }}>{this.props.userData.userBalance}p</Link>
                            </div>
                        </div>
                    </div>
                </div>
                :
                <div id="divMainBar">
                    {this.existingItems()}
                    <div className="divMainBar_divsInsideBar_UserData">
                        <div className="divForUnloggedUser">
                            <Link to="/SignIn" className="linksUnloggedUser" style={{ marginBottom: '10px' }}>Войти</Link>
                            <Link to="/SignUp" className="linksUnloggedUser">Регистрация</Link>
                        </div>
                    </div>
                </div>

        );
    }
}

const mapStateToProps = function (store) {
    return {
        userData: store
    };
}

export default connect(mapStateToProps)(MenuBar);