import * as actionType from '../actions/actionType';
const initialState =[];
const getPostedRecord = (state = initialState,action)=>{
    switch(action.type){
        case actionType.ACTIONTYPES.FETCH_ALL_POSTED:
            return action.payload;
        default:
            return state;
    }
}
export default getPostedRecord;