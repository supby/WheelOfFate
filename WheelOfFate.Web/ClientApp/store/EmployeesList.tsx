import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

// state
export interface EmployeesListState {
    isLoading: boolean;
    employees: Employee[];
}

export interface Employee {
    id: number;
    name: string;
}

// actions
interface LoadEmployeesListAction {
    type: 'LOAD_EMPLOYEES_LIST';
    employees: Employee[];
}
interface AddEmployeeAction {
    type: 'ADD_EMPLOYEE';
    employees: Employee[];
}

type KnownAction = LoadEmployeesListAction | AddEmployeeAction;

// action creators

export const actionCreators = {
    requestEmployees: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        fetch(`api/employee`)
            .then(response => response.json() as Promise<Employee[]>)
            .then(data => {
                dispatch({ type: 'LOAD_EMPLOYEES_LIST', employees: data });
            });
    },
    addEmployee: (employee: Employee): AppThunkAction<KnownAction> => (dispatch, getState) => {
        fetch('api/employee', {
            method: 'POST',
            headers: new Headers({
              'Accept': 'application/json',
              'Content-Type': 'application/json',
            }),
            body: JSON.stringify([employee])
        })
        .then(response => response.json() as Promise<Employee[]>)
        .then(data => {
            dispatch({ type: 'ADD_EMPLOYEE', employees: data });
        });
    }
};

// reducer

const unloadedState: EmployeesListState = { employees: [], isLoading: false };

export const reducer: Reducer<EmployeesListState> = (state: EmployeesListState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'LOAD_EMPLOYEES_LIST':
            return {
                employees: action.employees,
                isLoading: false
            };
        case 'ADD_EMPLOYEE':
            return { isLoading: false, employees: state.employees.concat(action.employees) };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};