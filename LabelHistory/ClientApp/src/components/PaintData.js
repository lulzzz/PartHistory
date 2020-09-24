import React, { useState } from 'react';
import Collapse from 'react-bootstrap/Collapse'
import { FcCollapse } from 'react-icons/fc'
import { FcExpand } from 'react-icons/fc'

export function PaintData(props) {
    const [open, setOpen] = useState(false);
    const noData =
        <p className="indent">No paint data was found for this part.</p>;
    const subHead =
        <span className="subHead">| {props.colorDescription}</span>;
    const paintDataTable =
        <>        

            <PaintDetails {...props} />
            <hr />
            <PaintHistoryData {...props} />
        </>
        ;

    return (
        <section id="PaintData">
            <h1 
                    
                    onClick={() => setOpen(!open)}
                    aria-controls="example-collapse-text"
                    aria-expanded={open}
            >{open ? <FcExpand /> : <FcCollapse />} Paint {props.colorDescription ? subHead : <span className="subHead">| no data</span>} 
            </h1>
            <Collapse in={!open}>
                <div id="example-collapse-text">
                    {props.colorDescription ? paintDataTable : noData} 
                </div>
            </Collapse>
            
        </section>
    );
}
function PaintDetails(props) {
    const [open, setOpen] = useState(false);

    return (
        <section id="PaintDetails" className="indent">
            <h2
                onClick={() => setOpen(!open)}
                aria-controls="example-collapse-text"
                aria-expanded={open}
            > {open ? <FcExpand /> : <FcCollapse />} Paint Details
            </h2>
            <Collapse in={!open}>
                <div id="example-collapse-text">
                    <table className="table table-hover table-sm data-table">

                        <tbody>
                            <tr><td>Part Program:</td><th>{props.program}</th></tr>
                            <tr><td>Part Set:</td><th>{props.set}</th></tr>
                            <tr><td>Style:</td><th>{props.styleNumber}</th></tr>
                            <tr><td>Color:</td><th>{props.colorDescription}</th></tr>
                            <tr><td>Color Number:</td><th>{props.colorNumber}</th></tr>
                            <tr><td>Parts per Carrier:</td><th>{props.partsPerCarrier}</th></tr>
                            <tr><td>Paint Barcode:</td><th>{props.paintBarcode} </th></tr>
                            <tr><td>Decostar/Raw Part Number:</td><th>{props.rawPartNumber}</th></tr>
                        </tbody>
                    </table>
                </div>
            </Collapse>
        </section>
    );
} 
function PaintHistoryData(props) {
    const [open, setOpen] = useState(false);

    return (
        <section id="PaintHistory" className="indent">
            <h2
                    onClick={() => setOpen(!open)}
                    aria-controls="example-collapse-text"
                    aria-expanded={open}
            > {open ? <FcExpand /> : <FcCollapse />} Paint History
            </h2>
            <Collapse in={!open}>
                <div id="example-collapse-text">
                    <table className="table table-hover table-sm data-table">

                        <tbody>
                            <tr><td>Paint Exit Date/Time:</td><th>{props.paintLabelCreatedAt && new Date(props.paintLabelCreatedAt).toLocaleString()} </th></tr>
                            <tr><td>Paint Round:</td><th>{props.paintRound}</th></tr>
                            <tr><td>Paint Carrier:</td><th>{props.paintCarrier}</th></tr>
                            <tr><td>Paint Carrier Position:</td><th>{props.paintCarrierPosition}</th></tr>
                            <tr><td>Paint Scan Status:</td><th>{props.paintExitScanStatus}</th></tr>
                            <tr><td>Last Status Change:</td><th>{props.paintExitStatusChangedAt && new Date(props.paintExitStatusChangedAt).toLocaleString()} </th></tr>
                        </tbody>
                    </table>
                </div>
            </Collapse>
        </section>
    );
} 