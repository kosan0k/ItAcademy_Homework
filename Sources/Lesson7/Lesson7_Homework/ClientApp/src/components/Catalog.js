import React, { Component } from 'react';
import { ItemsSet } from './ItemsSet';

export class Catalog extends Component {

    constructor(props) {
        super(props);
        this.state = { catalogs: [], catalogsLoading: true, catalogItems: []};
    }

    componentDidMount() {
        this.GetCatalogs();
    }

    render() {
        let contents = this.state.catalogsLoading
        ? <p><em>Loading...</em></p>
        : this.renderContent();

        return (
            <div>
              <h1 id="shopLabel" >Shop</h1>
              {contents}
            </div>
          );
    }

    renderContent() {

        return <div class="row">
                    <div class="column">
                        {this.state.catalogs.map((catalog, j) => {  
                            return <div class="row">
                                        <button onClick={() => this.GetCatalogItems(catalog)}>{catalog.name}</button>
                                    </div>
                        })}
                    </div>
                    <div class="column">
                        <ItemsSet items={this.state.catalogItems}/>
                    </div>
                </div>
    }

    async GetCatalogs() {
        const response = await fetch('catalog');
        const data = await response.json();
        this.setState({ catalogs: data, catalogsLoading: false });
    }

    async GetCatalogItems(catalog) {
        const response = await fetch('catalogitem', {
            method : "post",
            headers: {
                'Content-Type': 'application/json'
              },
            body : JSON.stringify(catalog)
        });
        const data = await response.json();
        this.setState({catalogItems: data, itemsLoading: false});
    }
}