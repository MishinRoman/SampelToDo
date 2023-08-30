export interface ITodo{
    id:string,
    header:string,
    description:string
}

export interface ITodoCreate extends Omit<ITodo,"id">{};
