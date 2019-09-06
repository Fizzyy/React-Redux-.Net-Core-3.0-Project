import React from 'react';
import '../AccountSettings/AccountSettings.css';
import { axiosGet } from '../../CommonFunctions/axioses';
import { Image, Transformation } from 'cloudinary-react';
import { GETFULLUSERINFO } from '../../CommonFunctions/URLconstants';
import { Row, Col, Nav, TabContainer } from 'react-bootstrap';
import { connect } from 'react-redux';

class AccountSettings extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            userInfo: {}
        }
    }

    componentDidMount() {
        (async () => {
            let res = await axiosGet(GETFULLUSERINFO + this.props.user.username);
            if (res.status === 200) {
                this.setState({ userInfo: res.data });
            }
        })();
    }

    render() {
        return (
            <div className="divMainAccountSettings">
                <div id="emptyDiv" />
                <div id="UserInfoDiv">
                    <div className="div_UserHeaderInfo">
                        <div className="div_UserAvatar">
                            <Image publicId="iTechArt/bof86omvdaggy8cxvjvm" cloudName="djlynoeio" width="210" height="210">
                                <Transformation radius="max" />
                            </Image>
                        </div>
                        <div className="div_UserInfo">
                            <h1>{this.props.user.username}</h1>
                            
                        </div>
                    </div>
                    <div className="div_UserAction">

                    </div>
                </div>
                <div id="emptyDiv" />
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        user: store
    };
}

export default connect(mapStateToProps)(AccountSettings);