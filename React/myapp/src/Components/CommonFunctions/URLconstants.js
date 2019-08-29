const MainURL = "https://localhost:44312/"

const baseURLuser = MainURL + 'api/User';
const register = '/AddUser';
const login = '/SignIn';
const returnuserbalance = '/ReturnUserBalance/';
const signoutuser = '/SignOut';
const getallusers1 = '/GetAllUsers';
const getfulluserinfo = '/GetUserFullInfo/';
const banuser = '/BanUser';

const baseURLcatalog = MainURL + 'api/Game';
const getallgames1 = '/GetAllGames'
const GetCurrentPlatformGames1 = '/GetCurrentPlatformGames/'
const getchosengame1 = '/GetChosenGame/';
const deletedgame = '/DeleteGame/';
const updategame = '/UpdateGame';
const addgame = '/AddGame';

const baseURLorder = MainURL + 'api/Order';
const addorder = '/AddOrder'
const getunpaidorders = '/GetCurrentOrders/';
const deleteorder = '/DeleteOrders';
const payfororder = '/PayForOrders/';
const getpaidorders = '/GetPaidOrders/';

const baseURLfeedback = MainURL + 'api/Feedback';
const getcurrentgamefeedback = '/GetCurrentGameFeedback/';
const addfeedback = '/AddFeedback';
const getuserfeedback = '/GetUserFeedback/';

const baseURLgamemark = MainURL + 'api/GameMark';
const getavgmark = '/GetAverageMark/';
const getusersmark = '/GetUserMarks/';
const addscore = '/AddScore';

export const REGISTRATION = baseURLuser + register;
export const AUTHORIZATION = baseURLuser + login;
export const USERBALANCE = baseURLuser + returnuserbalance;
export const SIGNOUTUSER = baseURLuser + signoutuser;
export const GETALLUSERS = baseURLuser + getallusers1;
export const GETFULLUSERINFO = baseURLuser + getfulluserinfo;
export const BANUSER = baseURLuser + banuser;

export const GETALLGAMES = baseURLcatalog + getallgames1;
export const GETCURRENTPLATFORMGAMES = baseURLcatalog + GetCurrentPlatformGames1;
export const GETCHOSENGAME = baseURLcatalog + getchosengame1;
export const DELETEGAME = baseURLcatalog + deletedgame;
export const UPDATEGAME = baseURLcatalog + updategame;
export const ADDGAME = baseURLcatalog + addgame;

export const ADDORDER = baseURLorder + addorder;
export const GETUNPAIDORDERS = baseURLorder + getunpaidorders;
export const DELETEORDERS = baseURLorder + deleteorder;
export const PAYFORORDERS = baseURLorder + payfororder;
export const GETPAIDORDERS = baseURLorder + getpaidorders;

export const GETCURRENTGAMEFEEDBACK = baseURLfeedback + getcurrentgamefeedback;
export const ADDFEEDBACK = baseURLfeedback + addfeedback;
export const GETUSERFEEDBACK = baseURLfeedback + getuserfeedback;

export const GETAVERAGEMARK = baseURLgamemark + getavgmark;
export const GETUSERSMARK = baseURLgamemark + getusersmark;
export const ADDSCORE = baseURLgamemark + addscore;