import { createStore, combineReducers } from 'redux';
import userDataReducer from '../_REDUX/Reducer';
import ModalReducer from '../_REDUX/ModalReducer';

function saveToStorage(state) {
    const serializedUserState = JSON.stringify(state.user);
    localStorage.setItem('state', serializedUserState);
}

function getDataFromStorage() {
    const getStates = localStorage.getItem('state');
    if (getStates === null) return undefined;
    return JSON.parse(getStates);
}

const combiner = combineReducers({
    user: userDataReducer,
    modal: ModalReducer
});

const receivedStore = getDataFromStorage();

const preCombiner = {
    user: receivedStore,
    modal: ModalReducer
};

const store = createStore(combiner, preCombiner, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__());

store.subscribe(() => saveToStorage(store.getState()));

export default store;