import React, { Component, useState } from 'react';
import Collapse from 'react-bootstrap/Collapse'
import { FcCollapse } from 'react-icons/fc'
import { FcExpand } from 'react-icons/fc'

export function MoldData(props) {
    const [open, setOpen] = useState(false);
    const noData =
        <p className="indent">No mold data was found for this part.</p>;
    const subHead =
        <span className="subHead">| {props.press} - {new Date(props.createdAt).toLocaleDateString()}</span>;
    const details =
        <div>
            <table className="table table-hover table-sm data-table">
                <tbody>
                    <tr><td>Mold Label/Serial Number:</td><th>{props.moldBarcode}</th></tr>                   
                    <tr><td>Created Date/Time:</td><th>{new Date(props.createdAt).toLocaleString()}</th></tr>
                    <tr><td>Press:</td><th>{props.press}</th></tr>                  
                    <tr><td>MachineId ID:</td><th>{props.machineId}</th></tr>
                    <tr><td>PartNumber:</td><th>{props.partNumber}</th></tr>
                    <tr><td>Status Code:</td><th>{props.statusCode}</th></tr>
                    <tr><td>UniqueID Row Guid:</td><th>{props.rowGuid}</th></tr>
                    <tr><td>UniqueID RefenceID:</td><th>{props.uniqueRefenceID}</th></tr>
                    <tr><td>UniqueID msrepl_tran_version:</td><th>{props.tranVersion}</th></tr>
                    
                </tbody>
            </table>
        </div>;
    return (
        <section id="MoldData">
            <h1
                onClick={() => setOpen(!open)}
                aria-controls="example-collapse-text"
                aria-expanded={open}

            >{open ? <FcExpand /> : <FcCollapse />} Mold {props.uniqueRefenceID ? subHead : <span className="subHead">| no data</span>}
            </h1>
            <Collapse in={!open}>
                <div id="example-collapse-text">
                    {props.uniqueRefenceID ? details : noData}
                </div>
            </Collapse>
        </section>
    );
}