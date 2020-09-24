import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Label } from './components/Label';
import { RackData } from './components/RackData';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Label}/>
            <Route path='/labelHistory' component={Label} />
            <Route path='/rackData' component={RackData} />
      </Layout>
    );
  }
}
