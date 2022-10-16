import * as actionType from './actionType'
export const getAllRecord = (param)=>{
    return {
        type:actionType.ACTIONTYPES.FETCH_ALL_DATA,
        payload:param
    }
}
export const getPostedRecord = (param)=>{
    return {
        type:actionType.ACTIONTYPES.FETCH_ALL_POSTED,
        payload:param
    }
}

export const insertRecord = (param)=>{
    return {
        type:actionType.ACTIONTYPES.INSERT_LOAN,
        payload:param
    }
}
export const insertRecordFailed = (error)=>{
    console.log("calle");
    return {
        type:actionType.ACTIONTYPES.INSERT_LOAN_FAILED,
        payload:error
    }
}

