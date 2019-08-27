const userData = {
    username: 'Войти',
    userRole: 'User',
    userBalance: 0,
    isUserLogged: false
}

export default function userDataReducer(state = userData, action) {
    switch (action.type) {
        case 'LOGGED_USER': return {
            username: action.username,
            userRole: action.userRole,
            userBalance: action.userBalance,
            isUserLogged: action.isUserLogged
        };
    }
    return state;
}