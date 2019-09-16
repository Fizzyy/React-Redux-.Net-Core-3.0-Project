import React from 'react';
import '../../AdminPages/MenuBar/MenuBar.scss';
import { person } from 'react-icons-kit/ionicons/person';
import { iosGameControllerB } from 'react-icons-kit/ionicons/iosGameControllerB';
import { Icon } from 'react-icons-kit';
import { logOut } from 'react-icons-kit/ionicons/logOut';
import { iosHome } from 'react-icons-kit/ionicons/iosHome';
import { Link } from 'react-router-dom';
import { fire } from 'react-icons-kit/metrize/fire';
import store from '../../_REDUX/Storage';
import { connect } from 'react-redux';
import { axiosDelete } from '../../CommonFunctions/axioses';
import { SIGNOUTUSER } from '../../CommonFunctions/URLconstants';

class MenuBar extends React.Component {

    logOut = async () => {
        let res = await axiosDelete(SIGNOUTUSER + this.props.user.username);
        if (res.status === 200) {
            store.dispatch({ type: 'LOGGED_USER', username: 'Войти', userRole: 'User', isLogged: false });
            localStorage.clear();
        }
    }

    render() {
        return (
            <div className="menuSpace">
                <Link to="/Admin">
                    <div><Icon size={36} icon={iosHome} className="menuIcons" /></div>
                </Link>
                <Link to="/Admin/Games">
                    <div><Icon size={36} icon={iosGameControllerB} className="menuIcons" /></div>
                </Link>
                <Link to="/Admin/Users">
                    <div><Icon size={36} icon={person} className="menuIcons" /></div>
                </Link>
                <Link to="/Admin/Offers">
                    <div><Icon size={36} icon={fire} className="menuIcons" /></div>
                </Link>
                <div id="logOut"><Icon size={36} icon={logOut} className="menuIcons" onClick={this.logOut} /></div>
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        user: store
    };
}

export default connect(mapStateToProps)(MenuBar);