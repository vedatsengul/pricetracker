import * as signalR from "@microsoft/signalr";
import Stock from "./stock-model";
const URL = "https://localhost:7238/stockhub";
class Connector {
    private connection: signalR.HubConnection;
    public events: (onMessageReceived: (stocks: Stock[]) => void) => void;
    static instance: Connector;
    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(URL,
                {
                    skipNegotiation: true,
                    transport: signalR.HttpTransportType.WebSockets
                })
            .withAutomaticReconnect()
            .build();
        this.events = (onMessageReceived) => {
            this.connection.on("ReceivePrice", (stocks) => {
                onMessageReceived(stocks);
            });
        };
    }

    public connectionStart = () => {
        this.connection.start().
            then(() => console.log("connected to signalr hub"))
            .catch(err => console.log('Error connecting to hub:', err));
    }

    public connectionClose = () => {
        this.connection.stop().
            then(() => console.log("connection closed"))
            .catch(err => console.log('Error closing connection to hub:', err));
    }

    public static getInstance(): Connector {
        if (!Connector.instance)
            Connector.instance = new Connector();
        return Connector.instance;
    }
}
export default Connector.getInstance;