import * as actionType from '../actions/actionType';
const initialState =[];
const insertLoanReducer = (state = initialState,action)=>{
    switch(action.type){
        case actionType.ACTIONTYPES.INSERT_LOAN:
            return action.payload;
        case actionType.ACTIONTYPES.INSERT_LOAN_FAILED:
            console.log("reducerswitch ", action);
            return action.payload;
            
        default:
            return state;
    }
}
export default insertLoanReducer;