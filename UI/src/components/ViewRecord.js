import React, { useEffect, useState, useContext } from "react";
import { Form, Container, InputGroup, Button, Col, Row } from "react-bootstrap";
import { useSelector, useDispatch } from "react-redux";
import axios from "axios";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
import { getApi, postApi } from '../redux/services'
import { getPostedRecord, getAllRecord } from '../redux/actions/actions';
import { search } from "../utils/constant";
const ViewRecord = () => {
    const result = useSelector((state) => state);
    const dispatch = useDispatch();
    const [payloadReq, setPayload] = useState({
        orderClientField: {
            fieldName: 0,
            orderBy: 0
        },
        filterClientField: {
            fieldName: 0,
            filterKeyword: ""
        }
    });
    const [payloadItem, setPayloadItem] = useState({
        fieldName: "",
        orderBy: "",
        filterBy: "",
        filterKeyword: ""
    });
    const [fieldName, setFieldName] = useState([
        {
            key: '-- Select --',
            val: 0
        },
        {
            key: 'CitizenShip',
            val: 1
        },
        {
            key: 'Loan Amount',
            val: 2
        },
        {
            key: 'Time Trading',
            val: 3
        },
        {
            key: 'Email Address',
            val: 4
        },
        
    ]);
    const [orderType, setOrderType] = useState([
        {
            key: '-- Select --',
            val: 0
        },
        {
            key: 'Ascending',
            val: 1
        },
        {
            key: 'Descending',
            val: 2
        }
    ]);
    const [isFormShow, setFormShow] = useState(true);
    const [isLinkShow, setLinkShow] = useState(false);

    const showFormClick = (isShow) => {
        setFormShow(isShow);
        setLinkShow(!isShow);
    }

    const handleChange = (event) => {
        const value = event.target.value;
        setPayloadItem(

            {
                ...payloadItem,
                [event.target.name]: value,
            }
        );
    };

    const submitClick = () => {
        var items = {
            orderClientField: {
                fieldName: parseInt(payloadItem.fieldName || 0),
                orderBy: parseInt(payloadItem.orderBy || 0)
            },
            filterClientField: {
                fieldName: parseInt(payloadItem.filterBy || 0),
                filterKeyword: payloadItem.filterKeyword,
            }
        };
        setPayload(items);
        console.log("pa", items);
        fetchAllRecord(items);

    };
    const clearClick = () => {
        var items = {
            fieldName: "",
            orderBy: "",
            filterBy: "",
            filterKeyword: ""
        }
        setPayloadItem(items);
        var payload = {
            orderClientField: {
                fieldName: 0,
                orderBy: 0
            },
            filterClientField: {
                fieldName: 0,
                filterKeyword: '',
            }
        };
        setPayload(payload);
        fetchAllRecord(payload);
    }
    const fetchAllRecord = async (request) => {
        console.log("payloadReq", request);
        const data = await postApi(search, request).catch((err) => {
            console.log("Err", err);
        });
        dispatch(getAllRecord(data));
    }


    useEffect(() => {
        fetchAllRecord(payloadReq);
    }, []);

    //console.log("result", result);
    return (
        <div>
            <Container>
               
                <br></br>
                <br></br>
                <Form hidden={isFormShow}>
                    <Row>
                        <Col>
                            <InputGroup className="mb-3">
                                <InputGroup.Text id="basic-addon1">Order By Field</InputGroup.Text>
                                <Form.Select
                                    value={payloadItem.fieldName}
                                    onChange={handleChange}
                                    name="fieldName" >
                                    {fieldName.map(({ key, val }, index) => <option value={val} >{key}</option>)}
                                </Form.Select>
                            </InputGroup>
                        </Col>
                        <Col>
                            <InputGroup className="mb-3">
                                <InputGroup.Text id="basic-addon1">Order By Type</InputGroup.Text>
                                <Form.Select
                                    value={payloadItem.orderBy}
                                    name="orderBy"
                                    onChange={handleChange}
                                >
                                    {orderType.map(({ key, val }, index) => <option value={val} >{key}</option>)}
                                </Form.Select>
                            </InputGroup>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <InputGroup className="mb-3">
                                <InputGroup.Text id="basic-addon1">Filter By Field</InputGroup.Text>
                                <Form.Select
                                    value={payloadItem.filterBy}
                                    name="filterBy"
                                    onChange={handleChange}
                                >
                                    {fieldName.map(({ key, val }, index) => <option value={val} >{key}</option>)}
                                </Form.Select>
                            </InputGroup>
                        </Col>
                        <Col>
                            <InputGroup className="mb-3">
                                <InputGroup.Text id="basic-addon1">Filter Keyword</InputGroup.Text>
                                <Form.Control placeholder="Input Keyword" name="filterKeyword" value={payloadItem.filterKeyword} onChange={handleChange} />
                            </InputGroup>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <Button onClick={submitClick}>Submit Query</Button> &nbsp;&nbsp;&nbsp;
                            <Button onClick={clearClick}> &nbsp; Clear Query &nbsp;</Button>

                        </Col>
                        <Col style={{ textAlign: "right" }}>
                            <Button variant="link" onClick={() => showFormClick(true)}>Hide Form</Button>
                        </Col>
                    </Row>
                </Form>

                <Button variant="link" onClick={() => showFormClick(false)} hidden={isLinkShow}>&raquo;Show Order by and Filter By Criteria</Button>

                <br ></br>
                <BootstrapTable
                    data={result && result.allRecords}
                    striped
                    hover
                    pagination={true}
                >
                    <TableHeaderColumn isKey dataField='name'       >Name</TableHeaderColumn>
                    <TableHeaderColumn dataField='emailAddress'     >Email Address</TableHeaderColumn>
                    <TableHeaderColumn dataField='phoneNumber'      width="110px">Phone #</TableHeaderColumn>
                    <TableHeaderColumn dataField='businessNumber'   width="115px">Business #</TableHeaderColumn>
                    <TableHeaderColumn dataField='loanAmount'       width="90px">Loan Amt</TableHeaderColumn>
                    <TableHeaderColumn dataField='citizenshipStatus' width="165px">CitizenShip</TableHeaderColumn>
                    <TableHeaderColumn dataField='timeTrading'      width="120px">Time Trading</TableHeaderColumn>
                    <TableHeaderColumn dataField='countryCode'      width="120px">CountryCode</TableHeaderColumn>
                    <TableHeaderColumn dataField='industry'         >Industry</TableHeaderColumn>
                </BootstrapTable>
               
            </Container>

        </div>

    );
}
export default ViewRecord;