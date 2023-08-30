import axios from "axios"
import { ITodo, ITodoCreate } from "../app.interfaces"

class TodoSevice {
    private URL = '/api/NewTodo/'
    async getAll() {
        return axios.get<ITodo[]>(this.URL)
    }
    async getById(id:string){
        return axios.get<ITodo>(this.URL+id)
    }

    async create({header, description}:ITodoCreate): Promise<any>{
        return axios.post<any, any, ITodoCreate>(this.URL, {header, description})
    }
    async delete(id:string):Promise<boolean>{
        return axios.delete<any, any, ITodo>(this.URL+id)
    }
    async update(todo:ITodo):Promise<ITodo>{
        return axios.patch(this.URL, todo)
    }
}
export default new TodoSevice;