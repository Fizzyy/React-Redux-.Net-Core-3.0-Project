import React from 'react';
import '../GameDescription/UsersComments.css';
import { Image, Transformation, CloudinaryContext } from 'cloudinary-react';
import Moment from 'moment';

class UsersComments extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div id="divMainUsersComments">
                <div className="div_UserData">
                    <div style={{ marginLeft: '5px' }}>
                        <CloudinaryContext cloudName="djlynoeio">
                            <Image publicId={this.props.userImage}>
                                <Transformation height="45" width="45" crop="fill" radius="max" />
                            </Image>
                        </CloudinaryContext>
                    </div>
                    <span className="span_Username">{this.props.username}</span>
                    <span className="span_CommentDate">{Moment(this.props.commentDate).format('DD-MM-YYYY')}</span>
                </div>
                <div className="div_UserComment">
                    <span className="span_UserComment">{this.props.comment}</span>
                </div>
            </div>
        );
    }
}

export default UsersComments;