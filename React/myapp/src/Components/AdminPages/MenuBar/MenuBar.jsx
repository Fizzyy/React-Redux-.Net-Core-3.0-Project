import React from 'react';
import '../../AdminPages/MenuBar/MenuBar.scss';
import { person } from 'react-icons-kit/ionicons/person';
import { iosGameControllerB } from 'react-icons-kit/ionicons/iosGameControllerB';
import { Icon } from 'react-icons-kit';
import { logOut } from 'react-icons-kit/ionicons/logOut';
import { iosHome } from 'react-icons-kit/ionicons/iosHome';
import { Link } from 'react-router-dom';
import { fire } from 'react-icons-kit/metrize/fire';

class MenuBar extends React.Component {
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
                <Link to="/Admin/LogOut">
                    <div id="logOut"><Icon size={36} icon={logOut} className="menuIcons" /></div>
                </Link>
            </div>
        );
    }
}

export default MenuBar;