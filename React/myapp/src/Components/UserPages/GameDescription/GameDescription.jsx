import React from 'react';
import '../GameDescription/GameDescription.css';
import { axiosGet, axiosPost } from '../../CommonFunctions/axioses';
import { GETCHOSENGAME, ADDORDER } from '../../CommonFunctions/URLconstants';
import { connect } from 'react-redux';
import RatingStars from '../RatingStars/RatingStars';
import Button from 'react-bootstrap/Button';

class GameDescription extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            amount: 1,
            totalprice: 0,
            feedback: [],
            usercomment: '',
            gameFullDescription: {}
        }
    }

    componentDidMount() {
        (async () => {
            let res = await axiosGet(GETCHOSENGAME + this.props.match.params.gameID + '/' + this.props.userData.username);
            if (res.status === 200) {
                this.setState({ gameFullDescription: res.data, totalprice: res.data.gamePrice });
            }
        })();
    }

    setAmount = (e) => {
        this.setState({ amount: e.target.value, totalprice: e.target.value * this.state.gameFullDescription.gamePrice });
    }

    addOrder = async () => {
        let response = await axiosPost(ADDORDER, {
            username: this.props.userData.username,
            gameid: this.state.gameFullDescription.gameID,
            amount: +this.state.amount,
            totalsum: this.state.totalprice
        });
        if (response.status === 200) alert('Successfully ordered!')
        if (response.status == 400) alert('Not ordered!');
        if (response.status == 401) return this.props.history.push('/SignIn');
    }

    render() {
        return (
            // <div id="divMainGameDescription">
            //     <div className="emptyDivs" />
            //     <div className="gameInfoDiv">
            //         <div className="divMainInfo">
            //             <div className="divMainInfo_PosterAndButtons">
            //                 <div id="divGameDescriptionImage">
            //                     <div id="imagee">

            //                     </div>
            //                 </div>
            //                 <div id="divGameDescriptionFuncs">
            //                     <div className="divGameDescriptionFuncs_divs">
            //                         {this.state.isItOffer ?
            //                             <>
            //                                 <label className="span_ShortInfo_OldPrice">Старая цена: {this.state.gameFullDescription.gamePrice}p</label><br />
            //                                 <label className="span_ShortInfo_NewPrice">Новая цена: 2p</label>
            //                             </>
            //                             : <label className="span_ShortInfo" style={{ marginTop: '30px', color: 'whitesmoke' }}>Цена: {this.state.gameFullDescription.gamePrice}p</label>
            //                         }
            //                     </div>
            //                     <div className="divGameDescriptionFuncs_divs">
            //                         <div className="divNumberAndOrder">
            //                             <div>
            //                                 <label style={{ fontSize: '18x', color: 'whitesmoke' }}>Количество:</label>
            //                                 <input type="number" min="1" max="10" defaultValue="1" id="inputNumberField" onChange={this.setAmount} />
            //                             </div>
            //                             <Button variant="success" id="toOrdersButton" onClick={this.addOrder}>В корзину</Button>
            //                         </div>
            //                     </div>
            //                     <div className="divGameDescriptionFuncs_divs">
            //                         <span className="span_ShortInfo">Ваш рейтинг:</span>
            //                         <div style={{ marginLeft: '4em' }}>
            //                             <RatingStars isItEditable={true} gameScore={this.state.gameFullDescription.gameScore} size={32} starColor="gold" />
            //                         </div>
            //                     </div>
            //                 </div>
            //             </div>
            //             <div className="divMainInfo_HeaderAndComments">
            //                 <div className="divMainInfo_HeaderAndComments_Header">
            //                     <span id="span_Header_GameName">{this.state.gameFullDescription.gameName}</span><br />
            //                     <ul className="ul_GameDesc">
            //                         <li className="li_GameDesc">
            //                             {this.state.gameFullDescription.gamePlatform}
            //                         </li>
            //                         <li className="li_GameDesc">
            //                             |
            //                         </li>
            //                         <li className="li_GameDesc">
            //                             {this.state.gameFullDescription.gameJenre}
            //                         </li>
            //                         <li className="li_GameDesc">
            //                             |
            //                         </li>
            //                         <li className="li_GameDesc">
            //                             {this.state.gameFullDescription.gameRating}
            //                         </li>
            //                     </ul>
            //                     <RatingStars isItEditable={false} gameScore={this.state.gameFullDescription.gameScore} size={26} starColor="red" />
            //                 </div>
            //                 <div className="divMainInfo_HeaderAndComments_Comments"></div>
            //             </div>
            //         </div>
            //         <div className="otherSimilarGames">
            //         </div>
            //     </div>
            //     <div className="emptyDivs" />
            // </div>
            <div id="divMainGameDescription">
                <div className="emptyDivs" />
                <div className="gameInfoDiv">
                    <div id="divMainGrid">
                        <div id="gameImage"></div>
                        <div id="header"></div>
                        <div id="comments"></div>
                        <div id="buttons"></div>
                        <div id="others"></div>
                    </div>
                </div>
                <div className="emptyDivs" />
            </div>
        );
    }
}

const mapStateToProps = function (store) {
    return {
        userData: store
    };
}

export default connect(mapStateToProps)(GameDescription);