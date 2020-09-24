import React, { Component, useState } from 'react';
import Collapse from 'react-bootstrap/Collapse'
import { FcCollapse } from 'react-icons/fc'
import { FcExpand } from 'react-icons/fc'

export class RackData extends Component {
    constructor(props) {
        super(props);
        this.state = {
            rackID: "",
            rack: []
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);

    }
    handleChange(event) {
        this.setState({ rackID: event.target.value });
    }
    handleSubmit(event) {
        event.preventDefault();
        this.loadDataFromServer();
        this.setState({ barcodeEntry: "" });
    }
    componentDidMount() {
        this.props.ID && this.setState({ rack: this.props });
    }

    loadDataFromServer() {
        fetch('api/Rack?rackID=' + this.state.rackID)
            .then((response) => response.json())
            .then(rackDetails => {
                this.setState({ rack: rackDetails });
            });
    }
    render() {
        const { rackID, rack } = this.state; 
        const rackInput =
                <form onSubmit={this.handleSubmit}>
                    <div className="col-sm input-group">
                        <input type="text" id="rackInput" autoFocus value={rackID} className="form-control" onChange={this.handleChange} placeholder="Enter Rack ID" />
                    </div>
                </form>;
        return (
            <section id="RackData">
                               
                 <RackDetails {... this.props}/>
            </section>
        );       
    }
}

function RackDetails(props) {
    const [open, setOpen] = useState(false);
    const noData =
        <p className="indent">This part has not been added to a rack.</p>;
    const subHead =
        <span className="subHead">| #{props.id}</span>;
    const details =
        <ul>
            <li className="row"><dt className="col-sm-3">Rack ID:</dt><dd className="col-sm-9">{props.id} </dd></li>
            <li className="row"><dt className="col-sm-3">Rack Created At:</dt><dd className="col-sm-9">{new Date(props.createdAt).toLocaleString()}</dd></li>
            <li className="row"><dt className="col-sm-3">Part Number:</dt><dd className="col-sm-9">{props.partNumber}</dd></li>
            <li className="row"><dt className="col-sm-3">Last Location:</dt><dd className="col-sm-9">{props.lastLocation}</dd></li>
            <li className="row"><dt className="col-sm-3">App Used:</dt><dd className="col-sm-9">{props.appUsed}</dd></li>
            <li className="row"><dt className="col-sm-3">Quantity in Rack:</dt><dd className="col-sm-9">{props.quantity}  </dd></li>
            <li className="row"><dt className="col-sm-3">Rack Completed:</dt><dd className="col-sm-9"> {props.complete ? 'Complete' : 'Incomplete'}  </dd></li>
            <li className="row"><dt className="col-sm-3">Ship Location:</dt><dd className="col-sm-9">{props.shipLocation}  </dd></li>
            <hr />
            <RackHistory {...props} />
            <hr />
            <RackContents {...props} />
        </ul>;
    return (
        <section id="RackDetails">
                <h1
                        onClick={() => setOpen(!open)}
                        aria-controls="example-collapse-text"
                        aria-expanded={open}                        
                > {open ? <FcExpand /> : <FcCollapse />} Rack {props.id ? subHead :  <span className="subHead">| no data</span>} 
                </h1>

                <Collapse in={!open}>
                    <div id="example-collapse-text">
                    {props.id ? details : noData } 
                    </div>
                </Collapse>          
        </section>
    );
}

function RackContents(props) {
    const [open, setOpen] = useState(false);
    const rackItems = props.rackContents.map(rackItem => (
        <tr key={rackItem.rackItemID}><td>{rackItem.rackItemID}</td><td>{rackItem.paintScan}</td><td>{rackItem.moldScan}</td><td>{new Date(rackItem.dateTimeAdded).toLocaleString()}</td></tr>
    ));
    return (
        <section id="RackContents">
            <h2
                    onClick={() => setOpen(!open)}
                    aria-controls="example-collapse-text"
                    aria-expanded={open}
                    variant="outline-secondary"
                    size="sm"
            > {open ? <FcExpand /> : <FcCollapse />}  Rack Contents                   
            </h2>

            <Collapse in={!open}>
                <div id="example-collapse-text">
                    <table className="table table-hover table-sm data-table">
                        <tbody>                           
                            <tr><th>Rack Item ID</th><th>Paint Label</th><th>Mold Label</th><th>DateTime Added</th></tr>
                            {rackItems}
                        </tbody>
                    </table>

                </div>
            </Collapse>
        </section>
    );
}

function RackHistory(props) {
    const [open, setOpen] = useState(false);
    const logItems = props.rackActivityLog.map(logItem => (
        <tr key={logItem.id}><td>{logItem.action}</td><td>{logItem.username}</td><td>{new Date(logItem.actionDateTime).toLocaleString()}</td><td>{logItem.message}</td></tr>
    ));
    return (
        <section id="RackHistory">
            <h2
                    onClick={() => setOpen(!open)}
                    aria-controls="example-collapse-text"
                    aria-expanded={open}
                    variant="outline-secondary"
                    size="sm"
            > {open ? <FcExpand /> : <FcCollapse />} Rack History
            </h2>

            <Collapse in={!open}>
                <div id="example-collapse-text">
                    <table className="table table-hover table-sm data-table">
                        <tbody>
                            <tr><th>Rack Action</th><th>Operator</th><th>DateTime</th><th>Message</th></tr>
                            {logItems}
                        </tbody>
                    </table>
                </div>
            </Collapse>
        </section>
    );
}