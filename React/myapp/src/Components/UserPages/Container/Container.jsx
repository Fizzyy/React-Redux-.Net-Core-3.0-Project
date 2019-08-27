import React from 'react';
import MenuBar from '../MenuBar/MenuBar';
import '../Container/Container.css';
import Footer from '../Footer/Footer';

class Container extends React.Component {
    render() {
        return (
            <div id='mainBlockForUser'>
                <div id='menuBar'>
                    <MenuBar />
                </div>
                <div id='bodyy'>
                    <h1>qwe</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                    <h1>asfasfasf</h1>
                </div>
                <div id='footer'>
                    <Footer />
                </div>
            </div>
        );
    }
}

export default Container;