import React, { Component } from 'react';

function GoodItem(props) {
    return <ul>
                <li><h1>{props.name}</h1></li>
                <li><h5>{props.price}</h5></li>
            </ul>;
}

export class ItemsSet extends Component {
    render() { 
        let result = this.props.items === undefined 
        ? <div/>
        : <div class="column">
            {this.props.items.map((item, i) => {   
                return <GoodItem name={item.name} price={item.price}/>
            })}
        </div>
        return result;
    }
}

