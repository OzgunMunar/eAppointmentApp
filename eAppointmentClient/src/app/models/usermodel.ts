export interface UserModel {

    id: string,
    firstName: string,
    lastName: string,
    fullName: string,
    userName: string,
    email: string,
    roleId: string

}

export const initialUser: UserModel = {

    id: "",
    firstName: "",
    lastName: "",
    fullName: "",
    userName: "",
    email: "",
    roleId: ""
    
}