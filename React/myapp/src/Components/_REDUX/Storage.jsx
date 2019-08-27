import { createStore } from 'redux';
import userDataReducer from '../_REDUX/Reducer';

function saveToStorage(state) {
    const serializedState = JSON.stringify(state);
    localStorage.setItem('state', serializedState);
}

function getDataFromStorage() {
    const getStates = localStorage.getItem('state');
    if (getStates === null) return undefined;
    return JSON.parse(getStates);
}

const receivedStore = getDataFromStorage();
const store = createStore(userDataReducer, receivedStore, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__());

store.subscribe(() => saveToStorage(store.getState()));

export default store;