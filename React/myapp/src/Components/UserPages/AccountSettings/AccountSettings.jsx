import React from 'react';
import '../AccountSettings/AccountSettings.css';
import { axiosGet, axiosPost, axiosDelete, axiosPut } from '../../CommonFunctions/axioses';
import { Image, Transformation } from 'cloudinary-react';
import { GETFULLUSERINFO, UPDATEFEEDBACK, DELETEGAMEMARK, DELETEFEEDBACK } from '../../CommonFunctions/URLconstants';
import { connect } from 'react-redux';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import Table from 'react-bootstrap/Table';
import Moment from 'moment';
import LoadingSpinner from '../../CommonFunctions/LoadingSpinner';
import UserComments from '../AccountSettings/UserComments';
import Button from 'react-bootstrap/Button';
import RatingStars from '../RatingStars/RatingStars';

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
        let res = await axiosPut(UPDATEFEEDBACK, {
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

    deleteMark = async (e) => {
        let res = await axiosDelete(DELETEGAMEMARK + `Username=${this.props.user.username}&scoreID=${e.target.value}`);
        if (res.status === 200) {
            this.setState({
                userInfo: {
                    ...this.state.userInfo,
                    gameMarks: res.data
                }
            });
        }
    }

    deleteFeedback = async () => {
        let res = await axiosDelete(DELETEFEEDBACK + `Username=${this.props.user.username}&FeedBackID=${this.state.selectedComment.commentID}`);
        if (res.status === 200) {
            this.setState({
                userInfo: {
                    ...this.state.userInfo,
                    feedbacks: res.data
                },
                comment: ''
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
                            <Image publicId={this.state.userInfo.userImage} cloudName="djlynoeio" width="210" height="210">
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
                                        }) : <div className="spinnerAlign">
                                                <LoadingSpinner color="black" width={60} height={60} visible={true} />
                                            </div>}
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
                                    }) : <div className="spinnerAlign">
                                            <LoadingSpinner color="black" width={60} height={60} visible={true} />
                                        </div>}
                                </div>
                                <div className="commentsOptions">
                                    <div className="fieldCommentArea">
                                        <textarea onChange={this.setNewComment} value={this.state.selectedComment.comment} className="changedCommentsArea" maxLength={500} />
                                    </div>
                                    <div className="fieldCommentOptions">
                                        <label>{this.state.selectedComment.comment.length}/500</label>
                                        <div className="buttonsContainer">
                                            <Button variant="outline-primary" style={{ marginBottom: '10px' }} onClick={this.UpdateComment}>Сохранить</Button>
                                            <Button variant="outline-danger" onClick={this.deleteFeedback}>Удалить</Button>
                                        </div>
                                    </div>
                                </div>
                            </Tab>
                            <Tab eventKey="marks" title="Оценки">
                                <Table>
                                    <thead>
                                        <tr>
                                            <th>Название</th>
                                            <th>Платформа</th>
                                            <th>Оценка</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.userInfo.gameMarks && this.state.userInfo.gameMarks.length ? this.state.userInfo.gameMarks.map((x) => {
                                            return (
                                                <tr key={x.scoreID}>
                                                    <td>{x.gameName}</td>
                                                    <td>{x.gamePlatform}</td>
                                                    <td>
                                                        <div className="starContainer">
                                                            <RatingStars isItEditable={true} gameScore={x.score} size={26} starColor="orange" />
                                                        </div>
                                                    </td>
                                                    <td><Button variant="outline-danger" size="sm" onClick={this.deleteMark} value={x.scoreID}>Удалить</Button></td>
                                                </tr>
                                            );
                                        }) : <div className="spinnerAlign">
                                                <LoadingSpinner color="black" width={60} height={60} visible={true} />
                                            </div>}
                                    </tbody>
                                </Table>
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