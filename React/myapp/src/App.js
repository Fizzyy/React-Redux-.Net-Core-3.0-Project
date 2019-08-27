import React from 'react';
import './App.css';
import Container from './Components/UserPages/Container/Container';
import { Provider } from 'react-redux';
import store from '../src/Components/_REDUX/Storage';

function App() {
  return (
    <div className="App">
      <Provider store={store}>
        <Container />
      </Provider>
    </div>
  );
}

export default App;
