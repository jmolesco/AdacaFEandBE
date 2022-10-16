import React, { useEffect, useState, useContext } from "react";
import { Form, Container, InputGroup, Button, Col, Row, ListGroup, Alert } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { postApi } from '../redux/services'
import { insertRecord, insertRecordFailed } from '../redux/actions/actions';
import { postInsert } from "../utils/constant";
import Modal from 'react-bootstrap/Modal';

const RegisterLoan = () => {
    const result = useSelector((state) => state);
    const [show, setModalShow] = useState(false);
    const [responseError, setResponseError] = useState(false);
    const [variantColorDecision, setVariantColorDecision] = useState('');
    const [variantColorError, setVariantColoError] = useState('');
    const handleClose = () =>{ setModalShow(false);setResponseError({}); };
    // const handleShow = () => setShow(true);

    const dispatch = useDispatch();

    const [payloadItem, setPayloadItem] = useState({
        firstName: "",
        lastName: "",
        emailAddress: "",
        phoneNumber: "",
        businessNumber: "",
        loanAmount: 0,
        citizenshipStatus: "",
        timeTrading: 0,
        countryCode: "",
        industry: ""
    });
    const handleChange = (event) => {
        const value = event.target.value;
        setPayloadItem(

            {
                ...payloadItem,
                [event.target.name]: value,
            }
        );
    };
    const submitClick = async () => {
        // console.log("pa", payloadItem);
        //var payload = { "firstName": "JOHN", "lastName": "OLESCO", "emailAddress": "jmolesco@yahoo.com", "phoneNumber": "09177634817", "businessNumber": "112345678912", "loanAmount": "1", "citizenshipStatus": "Citizen", "timeTrading": "12", "countryCode": "AU", "industry": "Industry 2" };
        const data = await postApi(postInsert, payloadItem)
            .catch(err => {
                return err;
                // dispatch(insertRecordFailed(err),1000); 
            });
        console.log("data", data);
        if (data && data.status === 400) {

            setResponseError(data.data);
            if (data.data.decision === "Unqualified") {
                setVariantColorDecision("danger");
                setVariantColoError("danger");
            }
            else if (data.data.decision === "Unknown") {
                setVariantColorDecision("warning");
                setVariantColoError("danger");
            }
            setModalShow(true);
        }
        else {
            setVariantColorDecision("success");
            setVariantColoError("success");
            dispatch(insertRecord(data));
            setModalShow(true);
            setResponseError(data);
        }




    };
    const clearClick = () => {
        var items = {
            firstName: "",
            lastName: "",
            emailAddress: "",
            phoneNumber: "",
            businessNumber: "",
            loanAmount: 0,
            citizenshipStatus: "",
            timeTrading: 0,
            countryCode: "",
            industry: ""
        }
        setPayloadItem(items);

    }
    return (
        <Container>
            <br />
            <Modal
                show={show}
                onHide={handleClose}
                backdrop="static"
                keyboard={false}
                size="lg"
            >
                <Modal.Header closeButton>
                    <Modal.Title style={{width:"100%"}}>

                        <Alert key={variantColorDecision} variant={variantColorDecision}>
                            Decision : {responseError && responseError.decision}
                        </Alert>


                    </Modal.Title>

                </Modal.Header>
                <Modal.Body hidden={variantColorDecision === "success"}>
                    <ListGroup>
                        {
                            responseError && responseError.validationResult && responseError.validationResult.map(function (item) {
                                return (<ListGroup.Item>
                                    <Alert key={variantColorError} variant={variantColorError}>
                                        {item.rule} - {item.message}
                                    </Alert>
                                </ListGroup.Item>);
                            })
                        }
                    </ListGroup>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>

            <div style={{ width: "70%", marginLeft: "13%", marginRight: "13%" }}>
                <h3>
                    Register Loan
                </h3>
                <br />
                <Form>
                    <Row className="mb-1">
                        <Form.Group as={Col} controlId="">
                            <Form.Label>First Name</Form.Label>
                            <Form.Control type="text" placeholder="First Name"
                                value={payloadItem.firstName}
                                onChange={handleChange}
                                name="firstName"
                            />
                        </Form.Group>
                        <Form.Group as={Col} controlId="">
                            <Form.Label>Last Name</Form.Label>
                            <Form.Control type="text" placeholder="Last Name"
                                value={payloadItem.lastName}
                                onChange={handleChange}
                                name="lastName"
                            />
                        </Form.Group>
                    </Row>
                    <Row className="mb-1">
                        <Form.Group as={Col} controlId="">
                            <Form.Label>Email Address</Form.Label>
                            <Form.Control type="email" placeholder="Email Address"
                                value={payloadItem.emailAddress}
                                onChange={handleChange}
                                name="emailAddress"
                            />
                        </Form.Group>
                        <Form.Group as={Col} controlId="">
                            <Form.Label>Phone Number</Form.Label>
                            <Form.Control type="text" placeholder="Phone Number"
                                value={payloadItem.phoneNumber}
                                onChange={handleChange}
                                name="phoneNumber"
                            />
                        </Form.Group>
                    </Row>
                    <Row className="mb-1">
                        <Form.Group as={Col} controlId="">
                            <Form.Label>Business Number</Form.Label>
                            <Form.Control type="text" placeholder="Business Number"
                                value={payloadItem.businessNumber}
                                onChange={handleChange}
                                name="businessNumber"

                            />
                        </Form.Group>
                        <Form.Group as={Col} controlId="">
                            <Form.Label>Loan Amount</Form.Label>
                            <Form.Control type="number" placeholder="Loan Amount"
                                value={payloadItem.loanAmount}
                                onChange={handleChange}
                                name="loanAmount"
                            />
                        </Form.Group>
                    </Row>
                    <Row className="mb-1">
                        <Form.Group as={Col} controlId="">
                            <Form.Label>CitizenShip</Form.Label>
                            <Form.Select
                                value={payloadItem.citizenshipStatus}
                                onChange={handleChange}
                                name="citizenshipStatus"
                            >
                                <option value="Select" >--Select--</option>
                                <option value="Citizen" >Citizen</option>
                                <option value="Permanent Resident" >Permanent Resident</option>
                            </Form.Select>
                        </Form.Group>
                        <Form.Group as={Col} controlId="">
                            <Form.Label>Time Trading</Form.Label>
                            <Form.Control type="number" placeholder="Time Trading"
                                value={payloadItem.timeTrading}
                                onChange={handleChange}
                                name="timeTrading"
                            />
                        </Form.Group>
                    </Row>
                    <Row className="mb-1">
                        <Form.Group as={Col} controlId="">
                            <Form.Label>Country Code</Form.Label>
                            <Form.Select

                                value={payloadItem.countryCode}
                                onChange={handleChange}
                                name="countryCode"

                            >
                                <option value="Select" >--Select--</option>
                                <option value="AU" >AU</option>
                            </Form.Select>
                        </Form.Group>
                        <Form.Group as={Col} controlId="">
                            <Form.Label>Industry</Form.Label>
                            <Form.Control type="text" placeholder="Industry"
                                name="industry"
                                value={payloadItem.industry}
                                onChange={handleChange}
                            />
                        </Form.Group>
                    </Row>
                    <Row className="mb-5">
                        <Form.Group as={Col} controlId="">

                        </Form.Group>
                        <Form.Group as={Col} controlId="" style={{ textAlign: "right" }}>
                            <Button onClick={clearClick} variant="secondary" size="sm">&nbsp;&nbsp;&nbsp;&nbsp;Clear Form&nbsp;&nbsp;&nbsp;&nbsp;</Button>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <Button variant="primary" onClick={submitClick} size="sm">
                                &nbsp;&nbsp;&nbsp;&nbsp;Submit Loan&nbsp;&nbsp;&nbsp;&nbsp;
                            </Button>
                        </Form.Group>
                    </Row>
                </Form>
            </div>
        </Container >

    );
}
export default RegisterLoan;