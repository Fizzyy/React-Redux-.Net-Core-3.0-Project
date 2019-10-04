const initialState = {
    showModal: false,
    modalType: '',
    banUserInfo: '',
    banUserDate: ''
}

export default function ModalReducer(state = initialState, action) {
    switch (action.type) {
        case 'SHOW_MODAL': return {
            ...state,
            showModal: action.showModal,
            modalType: action.modalType
        };
        case 'CLOSE_MODAL': return {
            ...state,
            showModal: action.showModal
        };
        case 'OPEN_BAN_WINDOW': return {
            ...state,
            showModal: action.showModal,
            modalType: action.modalType,
            banUserDate: action.banUserDate,
            banUserInfo: action.banUserInfo
        }
    }
    return state;
}