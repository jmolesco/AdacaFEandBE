import React, { useEffect, useState, useContext } from "react";
import { Form, Container, InputGroup, Button, Col, Row } from "react-bootstrap";
import {  useDispatch,useSelector } from "react-redux";
import { getApi } from '../redux/services'
import { getPostedRecord,  } from '../redux/actions/actions';
import { postInsert } from "../utils/constant";
import CanvasJSReact from '../canvasjs-3.6.7/canvasjs.react';
var CanvasJS = CanvasJSReact.CanvasJS;
var CanvasJSChart = CanvasJSReact.CanvasJSChart;

const GraphRecord = () => {
    const result = useSelector((state) => state);
    const [chartValues,setChartPayload] = useState();
    const dispatch = useDispatch();

    const fetchPostedRecord = async () => {
        const data = await getApi(postInsert).catch((err) => {
            console.log("Err", err);
        });
        dispatch(getPostedRecord(data));
    }

    const chartBasicColumnCreator=()=>{
       
        var records = result.postedRecord;
        var dataPoints = records.map(x=>{
            return{
                label: x.posted_Speed,
                y:x.count
            }
        }); 
       
        return {
            title: {
              text: "Group By Posted Speed - Basic Column Chart"
            },
            animationEnabled: true,
            theme: "light2",
            data: [{				
                      type: "column",
                      dataPoints: dataPoints
             }]
         }
    }

    const chartReactBarCreator=()=>{
       
        var records = result.postedRecord;
        var dataPoints = records.map(x=>{
            return{
                label: x.posted_Speed,
                y:x.count
            }
        }); 
       
        return {
			animationEnabled: true,
			theme: "light2",
			title:{
				text: "Group By Posted Speed - React Bar Charts & Graphs"
			},
			axisX: {
				title: "Posted Speed",
				reversed: true,
			},
			axisY: {
				title: "Total Count Per Posted Speed",
				includeZero: true,
			},
			data: [{
				type: "bar",
				dataPoints: dataPoints
			}]
		}
    }

    useEffect(() => {
        fetchPostedRecord();
        
    }, []);
     
    return (
        <Container>
            <br></br>
            <h3>
                Posted Speed Charts
            </h3>
            <br ></br>
            <CanvasJSChart options = {chartBasicColumnCreator()} containerProps={{ width: '100%', height: '300px' }} ></CanvasJSChart>
            <br></br>
            <CanvasJSChart options = {chartReactBarCreator()} containerProps={{ width: '100%', height: '300px' }} ></CanvasJSChart>
        </Container>

    );
}
export default GraphRecord;