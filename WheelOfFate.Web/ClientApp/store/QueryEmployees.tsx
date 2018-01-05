import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import * as EmployeesListStore from '../store/EmployeesList';

// state
export interface QueryEmployeesState {
    isLoading: boolean;
    employees: EmployeesListStore.Employee[];
}

// actions
interface QueryEmployeesAction {
    type: 'LOAD_QUERIED_EMPLOYEES';
    employees: EmployeesListStore.Employee[];
}

type KnownAction = QueryEmployeesAction;

// action creators

export const actionCreators = {
    requestEmployees: (bauCapacity:number, minShift:number, workingWindow:number, reqDaysPerWindow:number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        fetch(`api/BAU/${bauCapacity}/${minShift}/${workingWindow}/${reqDaysPerWindow}`)
            .then(response => response.json() as Promise<EmployeesListStore.Employee[]>)
            .then(data => {
                dispatch({ type: 'LOAD_QUERIED_EMPLOYEES', employees: data });
            });
    },
};

// reducer

const unloadedState: QueryEmployeesState = { employees: [], isLoading: false };

export const reducer: Reducer<QueryEmployeesState> = (state: QueryEmployeesState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'LOAD_QUERIED_EMPLOYEES':
            return {
                employees: action.employees,
                isLoading: false
            };
    }

    return state || unloadedState;
};