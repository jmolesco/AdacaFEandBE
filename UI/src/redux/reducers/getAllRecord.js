import * as actionType from '../actions/actionType';
const initialState =[];
const getAllRecordReducer = (state = initialState,action)=>{
    switch(action.type){
        case actionType.ACTIONTYPES.FETCH_ALL_DATA:
            return action.payload;
        default:
            return state;
    }
}
export default getAllRecordReducer;