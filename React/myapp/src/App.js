import React from 'react';
import './App.css';
import Container from './Components/UserPages/Container/Container';
import { Provider } from 'react-redux';
import store from '../src/Components/_REDUX/Storage';
import { BrowserRouter } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      <Provider store={store}>
        <BrowserRouter>
          <Container />
        </BrowserRouter>
      </Provider>
    </div>
  );
}

export default App;
