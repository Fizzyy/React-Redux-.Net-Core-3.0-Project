const MainURL = "https://localhost:44312/"

const baseURLuser = MainURL + 'api/User';
const register = '/AddUser';
const login = '/SignIn';
const returnuserbalance = '/ReturnUserBalance/';
const signoutuser = '/SignOut/';
const getallusers1 = '/GetAllUsers';
const getfulluserinfo = '/GetUserFullInfo/';
const resetpassword = '/ResetPassword?';
const updateavatar = '/UpdateAvatar?';

const baseURLcatalog = MainURL + 'api/Game';
const getallgames1 = '/GetAllGames'
const GetCurrentPlatformGames1 = '/GetCurrentPlatformGames/'
const getchosengame1 = '/GetChosenGame/';
const deletedgame = '/DeleteGame/';
const updategame = '/UpdateGame';
const addgame = '/AddGame';
const ordergames = '/OrderGames?';
const getgamesbyregex = '/GetGamesByRegex?';
const getsamejenregames = '/GetSameGenreGames?';
const startpagegames = '/GetGameForStartPage';

const baseURLorder = MainURL + 'api/Order';
const addorder = '/AddOrder'
const getunpaidorders = '/GetCurrentOrders/';
const deleteorder = '/DeleteOrders';
const payfororder = '/PayForOrders';
const getpaidorders = '/GetPaidOrders/';

const baseURLfeedback = MainURL + 'api/Feedback';
const getcurrentgamefeedback = '/GetCurrentGameFeedback/';
const addfeedback = '/AddFeedback';
const getuserfeedback = '/GetUserFeedback/';
const updatefeedback = '/UpdateFeedback';
const deletefeedback = '/DeleteFeedback?';

const baseURLgamemark = MainURL + 'api/GameMark';
const getavgmark = '/GetAverageMark/';
const getusersmark = '/GetUserMarks/';
const addscore = '/AddScore';
const deletescore = '/DeleteScore?';

const baseURLoffers = MainURL + 'api/Offers';
const getalloffers = '/GetOfferGames';
const getoffersbyregex = '/GetOffersByRegex?';
const getoffersfromplatform = '/GetOffersFromPlatform/';
const addoffer = '/AddOffer';
const updateoffer = '/UpdateOffer';
const deleteoffer = '/DeleteOffer/';

const baseURLbannedusers = MainURL + 'api/BannedUsers';
const banuser = '/GrantBan';
const revokeban = '/RevokeBan/';

const baseURLbalance = MainURL + 'api/MoneyKeys';
const activatekey = '/ActivateKey?';
const addkey = '/AddKey';

const baseURLmessages = MainURL + 'api/Messages';
const getmessagesfromroom = '/GetMessagesFromRoom/';
const getallrooms = '/GetRooms';

export const REGISTRATION = baseURLuser + register;
export const AUTHORIZATION = baseURLuser + login;
export const USERBALANCE = baseURLuser + returnuserbalance;
export const SIGNOUTUSER = baseURLuser + signoutuser;
export const GETALLUSERS = baseURLuser + getallusers1;
export const GETFULLUSERINFO = baseURLuser + getfulluserinfo;
export const RESETPASSWORD = baseURLuser + resetpassword;
export const UPDATEAVATAR = baseURLuser + updateavatar;

export const GETALLGAMES = baseURLcatalog + getallgames1;
export const GETCURRENTPLATFORMGAMES = baseURLcatalog + GetCurrentPlatformGames1;
export const GETCHOSENGAME = baseURLcatalog + getchosengame1;
export const DELETEGAME = baseURLcatalog + deletedgame;
export const UPDATEGAME = baseURLcatalog + updategame;
export const ADDGAME = baseURLcatalog + addgame;
export const ORDERGAMES = baseURLcatalog + ordergames;
export const GETGAMESBYREGEX = baseURLcatalog + getgamesbyregex;
export const GETSAMEGENREGAMES = baseURLcatalog + getsamejenregames;
export const GETSTARTPAGEGAMES = baseURLcatalog + startpagegames;

export const ADDORDER = baseURLorder + addorder;
export const GETUNPAIDORDERS = baseURLorder + getunpaidorders;
export const DELETEORDERS = baseURLorder + deleteorder;
export const PAYFORORDERS = baseURLorder + payfororder;
export const GETPAIDORDERS = baseURLorder + getpaidorders;

export const GETCURRENTGAMEFEEDBACK = baseURLfeedback + getcurrentgamefeedback;
export const ADDFEEDBACK = baseURLfeedback + addfeedback;
export const GETUSERFEEDBACK = baseURLfeedback + getuserfeedback;
export const UPDATEFEEDBACK = baseURLfeedback + updatefeedback;
export const DELETEFEEDBACK = baseURLfeedback + deletefeedback;

export const GETAVERAGEMARK = baseURLgamemark + getavgmark;
export const GETUSERSMARK = baseURLgamemark + getusersmark;
export const ADDSCORE = baseURLgamemark + addscore;
export const DELETEGAMEMARK = baseURLgamemark + deletescore;

export const GETOFFERGAMES = baseURLoffers + getalloffers;
export const GETOFFERSBYREGEX = baseURLoffers + getoffersbyregex;
export const GETOFFERSFROMPLATFORM = baseURLoffers + getoffersfromplatform;
export const ADDOFFER = baseURLoffers + addoffer;
export const UPDATEOFFER = baseURLoffers + updateoffer;
export const DELETEOFFER = baseURLoffers + deleteoffer;

export const BANUSER = baseURLbannedusers + banuser;
export const REVOKEBAN = baseURLbannedusers + revokeban;

export const ACTIVATEKEY = baseURLbalance + activatekey;
export const ADDKEY = baseURLbalance + addkey;

export const GETMESSAGESFROMROOM = baseURLmessages + getmessagesfromroom;
export const GETROOMS = baseURLmessages + getallrooms;