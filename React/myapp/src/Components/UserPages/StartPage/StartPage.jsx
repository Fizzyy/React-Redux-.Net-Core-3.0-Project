import React from 'react';
import '../StartPage/StartPage.css';
import Carousel from 'react-bootstrap/Carousel';
import PC1 from '../../../pc1.jpg';
import PC2 from '../../../pc2.jpg';
import { Link } from 'react-router-dom';

class StartPage extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div id="divMainStartPage">
                <div id="divMainStartPage_header">
                    <h2>Добро пожаловать!</h2>
                </div>
                <div id="divForCarousel">
                    <Carousel>
                        <Carousel.Item>
                            <img className="d-block w-100" src={PC1} />
                            <Carousel.Caption>
                                <h3>First slide label</h3>
                                <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img className="d-block w-100" src={PC2} />
                            <Carousel.Caption>
                                <h3>Second slide label</h3>
                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    </Carousel>
                </div>
                <div>
                    <Link to="/Catalog/PS4">ps4</Link>
                </div>
            </div>
        );
    }
}

export default StartPage;