import React from 'react';
import '../AccountSettings/UserComments.scss';

class UserComments extends React.Component {
    constructor(props) {
        super(props);
    }

    getCommentID = () => {
        this.props.getID(this.props.commentID);
    }

    render() {
        return (
            <div className="mainDivUserComments" onClick={this.getCommentID}>
                <div className="comment-info">
                    <ul className="ul_UserComments">
                        <li className="li_UserComments">
                            {this.props.gameName}
                        </li>
                        <li className="li_UserComments">
                            |
                        </li>
                        <li className="li_UserComments">
                            {this.props.gamePlatform}
                        </li>
                        <li className="li_UserComments">
                            |
                        </li>
                        <li className="li_UserComments">
                            {this.props.commentDate}
                        </li>
                    </ul>
                </div>
                <div className="div_Comment">
                    <span className="span_Comment">{this.props.comment}</span>
                </div>
            </div>
        );
    }
}

export default UserComments;