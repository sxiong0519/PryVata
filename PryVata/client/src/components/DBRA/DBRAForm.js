import React, { useEffect, useState } from "react";
import { useHistory, useParams, render } from "react-router-dom";
import { getDBRAById, addDBRA, updateDBRA } from "../../modules/dbraManager";
import { getAllCircumstances } from "../../modules/circumstanceManager";
import { getAllControls } from "../../modules/controlsManager";
import { getAllExceptions } from "../../modules/exceptionManager";
import { getAllInformation } from "../../modules/informationManager";
import { getAllMethods } from "../../modules/methodManager";
import { getAllRecipients } from "../../modules/recipientManager";


const DBRAForm = () => {
    const [ dbra, setDBRA ] = useState({});
    const [ circumstances, setCircumstances ] = useState([]);
    const [ controls, setControls ] = useState([]);
    const [ exceptions, setExceptions ] = useState([]);
    const [ information, setInformation ] = useState([]);
    const [ methods, setMethods ] = useState([]);
    const [ recipients, setRecipients ] = useState([]);
    
    const history = useHistory();
    
    useEffect(() => {
        getAllCircumstances().then(c => {setCircumstances(c)})
        getAllControls.then(co => {setControls(co)})
        getAllExceptions.then(e => {set})
    })
}