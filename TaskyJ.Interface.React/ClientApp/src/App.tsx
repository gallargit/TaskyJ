import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Confirm from "./components/Confirm";

import './custom.css'

export default () => (
    <Layout>
        A<hr />
        <Route exact path='/' component={Home} />
        B<hr />
        <Route path='/counter' component={Counter} />
        C<hr />
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        D<hr />
        <Confirm title="this is the title" content="this is the content" />
        E<hr />
    </Layout>
);
