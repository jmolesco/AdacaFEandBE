import { combineReducers} from "redux";
import getAllRecord from "./getAllRecord";
import getPostedRecord from "./getPostedRecord";
import insertLoanReducer from './insertLoanReducer'

const allReducers = combineReducers({
    allRecords:getAllRecord,
    postedRecord:getPostedRecord,
    insertLoanRecord:insertLoanReducer
});
export default allReducers;