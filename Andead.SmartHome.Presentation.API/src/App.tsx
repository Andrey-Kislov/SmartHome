import React from 'react';
import './App.css';
import FlowChart from './components/flow-chart';
import LogEvents from './components/log-events';

function App() {
    return (
        <div className="App">
            <FlowChart />
            <LogEvents />
        </div >
    );
}

export default App;