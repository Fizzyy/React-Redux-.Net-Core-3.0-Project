import React from 'react';
import '../AccountSettings/AccountSettings.css';
import { axiosGet, axiosPost } from '../../CommonFunctions/axioses';
import { Image, Transformation } from 'cloudinary-react';
import { GETFULLUSERINFO, UPDATEFEEDBACK } from '../../CommonFunctions/URLconstants';
import { connect } from 'react-redux';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import Table from 'react-bootstrap/Table';
import Moment from 'moment';
import LoadingSpinner from '../../CommonFunctions/LoadingSpinner';
import UserComments from '../AccountSettings/UserComments';
import Button from 'react-bootstrap/Button';

class AccountSettings extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            userInfo: {},
            selectedComment: {
                comment: ''
            }
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

    setNewComment = (e) => {
        this.setState({
            selectedComment: {
                ...this.state.selectedComment,
                comment: e.target.value
            }
        });
    }

    getID = (selectedID) => {
        this.state.selectedComment = this.state.userInfo.feedbacks.find((x) => {
            if (x.commentID == selectedID) return x;
        });
        this.setState({ selectedComment: this.state.selectedComment });
    }

    UpdateComment = async () => {
        let res = await axiosPost(UPDATEFEEDBACK, {
            Id: this.state.selectedComment.commentID,
            Username: this.props.user.username,
            GameID: this.state.selectedComment.gameID,
            Comment: this.state.selectedComment.comment
        });
        if (res.status === 200) {
            this.setState({
                userInfo: {
                    ...this.state.userInfo,
                    feedbacks: res.data
                }
            });
        }
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
                        <Tabs defaultActiveKey="orders" id="uncontrolled-tab-example">
                            <Tab eventKey="orders" title="Заказы">
                                <Table>
                                    <thead>
                                        <tr>
                                            <th>Название</th>
                                            <th>Платформа</th>
                                            <th>Дата заказа</th>
                                            <th>Количество</th>
                                            <th>Цена</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.userInfo.orders && this.state.userInfo.orders.length ? this.state.userInfo.orders.map((x) => {
                                            return (
                                                <tr key={x.orderID}>
                                                    <td>{x.gameName}</td>
                                                    <td>{x.gamePlatform}</td>
                                                    <td>{Moment(x.orderDate).format("DD-MM-YYYY")}</td>
                                                    <td>{x.amount}</td>
                                                    <td>{x.totalSum}</td>
                                                </tr>
                                            );
                                        }) : <LoadingSpinner color="black" width={60} height={60} visible={true} />}
                                    </tbody>
                                </Table>
                            </Tab>
                            <Tab eventKey="feedback" title="Отзывы">
                                <div className="comments">
                                    {this.state.userInfo.feedbacks && this.state.userInfo.feedbacks.length ? this.state.userInfo.feedbacks.map((x) => {
                                        return (
                                            <UserComments
                                                key={x.commentID}
                                                getID={this.getID}
                                                commentID={x.commentID}
                                                comment={x.comment}
                                                gameName={x.gameName}
                                                gamePlatform={x.gamePlatform}
                                                commentDate={Moment(x.commentDate).format("DD-MM-YYYY")}
                                            />
                                        );
                                    }) : <LoadingSpinner color="black" width={60} height={60} visible={true} />}
                                </div>
                                <div className="commentsOptions">
                                    <div className="fieldCommentArea">
                                        <textarea onChange={this.setNewComment} value={this.state.selectedComment.comment} className="changedCommentsArea" maxLength={500} />
                                    </div>
                                    <div className="fieldCommentOptions">
                                        <label>{this.state.selectedComment.comment.length}/500</label>
                                        <div className="buttonsContainer">
                                            <Button variant="outline-primary" style={{ marginBottom: '10px' }} onClick={this.UpdateComment}>Сохранить</Button>
                                            <Button variant="outline-danger">Удалить</Button>
                                        </div>
                                    </div>
                                </div>
                            </Tab>
                            <Tab eventKey="marks" title="Оценки">
                                <div style={{ backgroundColor: 'green', width: '100px', height: '100px' }} />
                            </Tab>
                        </Tabs>
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