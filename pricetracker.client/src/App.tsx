import { useEffect, useState } from 'react';
import Connector from './signalr-connection'
import Stock from './stock-model';


function App() {
    const { events, connectionStart, connectionClose } = Connector();
    const [stocks, setStocks] = useState<Stock[]>([]);

    const [isConnectionOpen, setIsConnectionOpen] = useState(false);

    useEffect(() => {
        events((stocks) => setStocks(stocks));
    }, []);

    const contents = stocks === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <div style={{ padding: '20px' }} >
            <div>
                <button onClick={() => { connectionStart(); setIsConnectionOpen(true); }} disabled={isConnectionOpen}> Subcribe </button>
                <button onClick={() => { connectionClose(); setIsConnectionOpen(false); }} disabled={!isConnectionOpen}> Unsubcribe </button>
            </div>
            <table className="table table-striped" aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Last Updated</th>
                    </tr>
                </thead>
                <tbody>
                    {stocks.map(stock =>
                        <tr key={stock.id} style={{ backgroundColor: stock.priceChange === 0 ? 'green' : stock.priceChange === 1 ? 'yellow' : 'red' }}>
                            <td>{stock.id}</td>
                            <td>{stock.name}</td>
                            <td>{stock.price}</td>
                            <td>{new Date(stock.lastUpdated).toLocaleString()}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>;

    return (
        <div>
            <h1 id="tabelLabel">Price Tracker</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

}

export default App;