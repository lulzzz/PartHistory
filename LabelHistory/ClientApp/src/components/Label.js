import React, { Component, useState } from 'react';
import { RackData } from './RackData';
import { PaintData } from './PaintData';
import { MoldData } from './MoldData';
import { FinesseData } from './FinesseData';
import Collapse from 'react-bootstrap/Collapse'
import Navbar from 'react-bootstrap/Navbar'
import Button from 'react-bootstrap/Button'
import Alert from 'react-bootstrap/Alert'
import Col from 'react-bootstrap/Col'
import Row from 'react-bootstrap/Row'
import Image from 'react-bootstrap/Image'
import { FcCollapse } from 'react-icons/fc'
import { FcExpand } from 'react-icons/fc'
import { BiBarcodeReader } from 'react-icons/bi'
import { Container } from 'reactstrap';
import Scrollspy from 'react-scrollspy'
import { IconContext } from "react-icons";



export class Label extends Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            barcodeEntry: "",
            part: [],
            labelScanned: ""
        };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleSubmit(event) {
        event.preventDefault();
           
        this.loadDataFromServer();
        this.setState({ labelScanned: this.state.barcodeEntry, barcodeEntry: "" });    

    }
    handleChange(event) {
        this.setState({ barcodeEntry: event.target.value });
    }
    loadDataFromServer() {
        fetch('api/LabelHistory?barcode=' + this.state.barcodeEntry)
            .then((response) => response.json())
            .then(partData => {
                this.setState({ part: partData, isLoaded: true });
            });       
        
    }

    render() {
        const { error} = this.state;          
        const instructions =
            <div id="instructions">
                <br />
                <br />
                <BiBarcodeReader size="400px" className="rotate30" />
                <h1>Scan a paint or mold label to see part details and history.</h1>                       
            </div>;
        if (error) {
            return <div>Error: {error.message}</div>;
        }
        else {
            return (
                <div>
                    <header>
                        <Navbar fixed="top" bg="dark" variant="dark">
                            <Container className="justify-content-start">
                                <Navbar.Brand href="/labelHistory">                                                                    
                                    <IconContext.Provider value={{ color: "white", className: "global-class-name" }}>
                                        <div>
                                            <BiBarcodeReader size="36px" />
                                              Decostar Label History
                                        </div>
                                    </IconContext.Provider> 
                                </Navbar.Brand>    
                                
                                <form inline="true" onSubmit={this.handleSubmit}>
                                    <div className="col-sm input-group">                                       
                                        <input type="text" id="barcodeInput" autoFocus value={this.state.barcodeEntry} className="form-control" onChange={this.handleChange} size="75" placeholder="Scan a label.." />                                       
                                    </div>
                                </form>
                            </Container>
                        </Navbar>
                    </header>
                    <Container fluid id="dataContainer">
                        <Row>
                            < NavColumn />
                            <Col fluid="true" id="scrollableContent">
                                {this.state.isLoaded ? <LabelData {...this.state} /> : instructions }
                            </Col>
                        </Row>
                    </Container>
                    
                </div>
            );
        }
    }
}


export class LabelData extends Component {
    render() {       
        return (
            <div id="rootEl">              
                
                <Alert variant="primary"> A {this.props.scannedLabelType} Label was scanned: {this.props.labelScanned} </Alert>   
                
                <PaintData {...this.props.part.paintData} />
                <hr />
                <MoldData {...this.props.part.moldData} />
                <hr />
                <RackData {...this.props.part.rackData} />
                <hr />
                <FinesseData {...this.props.part.finesseData} />
                <hr />
                <PartInfo id="PartInfo"{...this.props} />
                <hr />
            </div>
        );
    }
} 


function NavColumn(props) {
        return (
            <Col  id="NavColumn" >              
                <Scrollspy
                    className="scrollspy"
                    currentClassName="is-current"
                    items={['PaintData', 'PaintDetails', 'PaintHistory', 'MoldData', 'RackData', 'RackHistory', 'RackContents', 'FinesseData', 'FinesseHistory', 'Defects', 'PartInfo','PartImage']}
                    offset={-200}
                    
                    onUpdate={
                        (el) => {
                            console.log(el)
                        }
                    }
                >
                    
                    <h3><a href="#PaintData">Paint Data</a></h3>
                    <h4 className="subNav"><a href="#PaintDetails">Paint Details</a></h4>
                    <h4 className="subNav"><a href="#PaintHistory">Paint History</a></h4>
                    <h3><a href="#MoldData">Mold Data</a></h3>
                    <h3><a href="#RackData">Rack Data</a></h3>
                    <h4 className="subNav" ><a href="#RackHistory">Rack History</a></h4>
                    <h4 className="subNav" ><a href="#RackContents">Rack Contents</a></h4>
                    <h3><a href="#FinesseData">Finesse Data</a></h3>
                    <h4 className="subNav" ><a href="#FinesseHistory">Finesse History</a></h4>
                    <h4 className="subNav" ><a href="#Defects">Defects</a></h4>
                    <h3><a href="#PartInfo">Part Description</a></h3>
                    <h4 className="subNav"><a href="#PartImage">Part Image</a></h4>
                </Scrollspy>
            </Col>
        );
}



function PartInfo(props) {
    const [open, setOpen] = useState(false);
    const imgSource = "http://decostarimages/parts/" + props.part.styleNumber + ".jpg"; 
    const noData =
        <p className="indent">No data was found for this part.</p>;
    const subHead =
        <span className="subHead">| {props.part.partMasterData.fullDescription }</span>;
    return (
        <section id="PartInfo">
            <h1
                    onClick={() => setOpen(!open)}
                    aria-controls="example-collapse-text"
                    aria-expanded={open}
            >{open ? <FcExpand /> : <FcCollapse />} Part Description {props.part.partMasterData.fullDescription ? subHead : <span className="subHead">| no data</span>}       
            </h1>
            <Collapse in={!open}>
                
                <div id="example-collapse-text">
                    <table className="table table-hover table-sm data-table">
                        <tbody>
                            <tr><td>Paint Label:</td><th>{props.part.paintBarcode}</th></tr>
                            <tr><td>Mold Label:</td><th>{props.part.moldBarcode}</th></tr>
                            <tr><td>Customer Part Number:</td><th>{props.part.partMasterData.customerPartNumber}</th></tr>
                            <tr><td>Master Part Description 1:</td><th>{props.part.partMasterData.partDescription1}</th></tr>
                            <tr><td>Master Part Description 2:</td><th>{props.part.partMasterData.partDescription2}</th></tr>
                            <tr><td>Master Part Group:</td><th>{props.part.partMasterData.partGroup}</th></tr>
                            <tr><td>Master Part Status:</td><th>{props.part.partMasterData.partStatus}</th></tr>
                            <tr><td>Master Part Type:</td><th>{props.part.partMasterData.partType}</th></tr>
                            <tr><td>Master Part ProdLine:</td><th>{props.part.partMasterData.prodLine}</th></tr>
                            <tr><td>Part Full Description:</td><th>{props.part.partMasterData.fullDescription}</th></tr>
                            <tr><td>Option Description:</td><th>{props.part.partMasterData.optionDescription}</th></tr>
                            <tr><td>Part Type Description:</td><th>{props.part.partMasterData.partTypeDescription}</th></tr>
                            <tr><td>Part Position:</td><th>{props.part.partMasterData.position}</th></tr>                         
                        </tbody>
                    </table>
                    <PartImage {...props} className="indent" />
                </div>
            </Collapse>
        </section>
    );
}


function PartImage(props) {
    const [open, setOpen] = useState(false);
    const imgSource = "http://decostarimages/parts/" + props.part.paintData.styleNumber + ".jpg";
    return (
        <section id="PartImage" className="indent">
            <h1
                onClick={() => setOpen(!open)}
                aria-controls="example-collapse-text"
                aria-expanded={open}
            >{open ? <FcExpand /> : <FcCollapse />} Part Image 
            </h1>
            <Collapse in={!open}>
                <div id="example-collapse-text">                   
                    <Image src={imgSource} width="400" height="245" />
                </div>
            </Collapse>
        </section>
    );
}
