import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import * as EmployeesListStore from '../store/EmployeesList';

// state
export interface CurrentShiftEmployeesState {
    isLoading: boolean;
    employees: EmployeesListStore.Employee[];
}

// actions
interface LoadCurrentShiftEmployeesAction {
    type: 'LOAD_CURRENT_SUPPORT_EMPLOYEES';
    employees: EmployeesListStore.Employee[];
}

type KnownAction = LoadCurrentShiftEmployeesAction;

// action creators

export const actionCreators = {
    requestEmployees: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        fetch(`api/BAU`)
            .then(response => response.json() as Promise<EmployeesListStore.Employee[]>)
            .then(data => {
                dispatch({ type: 'LOAD_CURRENT_SUPPORT_EMPLOYEES', employees: data });
            });
    },
};

const unloadedState: CurrentShiftEmployeesState = { employees: [], isLoading: false };

export const reducer: Reducer<CurrentShiftEmployeesState> = (state: CurrentShiftEmployeesState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'LOAD_CURRENT_SUPPORT_EMPLOYEES':
            return {
                employees: action.employees,
                isLoading: false
            };
    }

    return state || unloadedState;
};