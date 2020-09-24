import React, { useState } from 'react';
import Collapse from 'react-bootstrap/Collapse'
import { FcCollapse } from 'react-icons/fc'
import { FcExpand } from 'react-icons/fc'



export function FinesseData(props) {
    const [open, setOpen] = useState(false);
    const noData =
        <p className="indent">No finesse data was found for this part.</p>;
    const subHead =
        <span className="subHead">| {props.finesseStatus}</span>;
    const details =
        <div>
            
            <FinesseDetails {...props} />
            <hr />
            <Defects {...props} />
        </div>;
    return (
        <section id="FinesseData">
            <h1
                onClick={() => setOpen(!open)}
                aria-controls="example-collapse-text"
                aria-expanded={open}

            >{open ? <FcExpand /> : <FcCollapse />} Finesse {props.rejectID ? subHead : <span className="subHead">| no data</span>}
            </h1>
            <Collapse in={!open}>
                <div id="example-collapse-text">
                    {props.rejectID ? details : noData}
                </div>
            </Collapse>
        </section>
    );
}
function FinesseDetails(props) {
    const [open, setOpen] = useState(false);
    return (
        <section id="FinesseHistory">
            <h2
                onClick={() => setOpen(!open)}
                aria-controls="example-collapse-text"
                aria-expanded={open}

            >{open ? <FcExpand /> : <FcCollapse />} Finesse History 
            </h2>
            <Collapse in={!open}>
                <div id="example-collapse-text">
                    <table className="table table-hover table-sm data-table">
                        <tbody>
                            <tr><th>Action</th><th>Date/Time</th><th>Operator</th><th>Work Area</th><th>Station</th></tr>
                            <tr>
                                <td>{props.finesseStatus}</td>
                                <td>{new Date(props.time).toLocaleString()}</td>
                                <td>{props.finneseOperatorName} ({props.finesseOperatorBadge})</td>
                                <td>{props.finesseWorkArea} </td>
                                <td>Printer {props.finessePrinter}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </Collapse>
        </section>
    );
}
function Defects(props) {
    const [open, setOpen] = useState(false);
    const noData =
        <p className="indent">No defects were found for this part.</p>;
    const subHead =
        <span className="subHead">| {props.defects[0].description}</span>;
    const defects = props.defects.map(defect => (
        <tr key={defect.defectID}><td>{defect.count}:</td><td>{defect.category}</td><td>{defect.description}</td><td>{defect.x}</td><td>{defect.y}</td></tr>

    ));
    return (
        <section id="Defects">
            <h2
                onClick={() => setOpen(!open)}
                aria-controls="example-collapse-text"
                aria-expanded={open}
                variant="outline-secondary"
                size="sm"
            > {open ? <FcExpand /> : <FcCollapse />}  Defects {props.defects[0] ? subHead : <span className="subHead">| no data</span>}
            </h2>

            <Collapse in={!open}>
                <div id="example-collapse-text">
                    <table className="table table-hover table-sm data-table">
                        <tbody>

                            <tr><th></th><th>Category</th><th>Description</th><th>X</th><th>Y</th></tr>
                            {defects}

                        </tbody>
                    </table>
                    <DefectCanvas {...props} />
                </div>
            </Collapse>
        </section>
    );
}
class DefectCanvas extends React.Component {
    componentDidMount() {
        const canvas = this.refs.canvas;
        const ctx = canvas.getContext("2d");
        const img = this.refs.image;
        img.onload = () => {
            ctx.drawImage(img, 0, 0, canvas.width - 1, canvas.height - 1)
            this.props.defects.map(defect => (
                ctx.fillStyle = "#800000",
                ctx.beginPath(),
                ctx.arc(defect.x, defect.y, 6, 0, 2 * Math.PI),
                ctx.fill(),
                ctx.fillStyle = "white",
                ctx.font = "9pt sans-serif",
                ctx.fillText("" + defect.count, defect.x - 3, defect.y + 4)
            ));
        };
    }
    render() {
        return (
            <div>
                <canvas ref="canvas" width={800} height={489} />
                <img ref="image" src={'http://decostarimages/parts/' + this.props.styleNumber + '.jpg'} className="hidden" />
            </div>
        )
    }
}