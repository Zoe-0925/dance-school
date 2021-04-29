import { combineReducers } from 'redux';
import UserReducer from "./user.reducer"
import InstructorReducer from "./instructor.reducer"

const RootReducer = combineReducers({
    UserReducer, InstructorReducer
});

export default RootReducer;