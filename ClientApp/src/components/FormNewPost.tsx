import React, { Children, FormEvent, useState } from "react";
import { ITodo, ITodoCreate } from "../app.interfaces";
import todoService from "../services/todo.service";
import {
  RefetchOptions,
  RefetchQueryFilters,
  useMutation,
} from "@tanstack/react-query";
import "../custom.css";
import { Toast, ToastBody, ToastHeader } from "reactstrap";

type Props = {
  refetch?: <TPageData>(
    options?: RefetchOptions & RefetchQueryFilters<TPageData>
  ) => any;
  editData?: ITodo;
};

type ToastMessageShow ={
  show:boolean,
  data?:ITodoCreate,
};

function FormNewPost(props: Props): React.JSX.Element {
  const [todo, setTodo] = useState<ITodoCreate>(
    { ...props.editData } ?? { description: "", header: "" }
  );
  const [message, setMessage] = useState<ToastMessageShow> ({show:false, data:todo});

  const { mutate } = useMutation(
    ["create todo"],
    (todo: ITodoCreate) => todoService.create(todo),
    {
      onSuccess() {
        setTodo({ description: "", header: "" });
        props.refetch();
      },
      onMutate() {
        setMessage({show:true, data:todo});
        {setTimeout(() => {
          setMessage({show:false, data:todo});
        }, 40000)}
        
      },
    }
  );

  function submitHendler(event: FormEvent<HTMLFormElement>): void {
    event.preventDefault();
    mutate(todo);
  }
 
  let timeNow = new Date(Date.now()).toLocaleTimeString("it-IT");
  let dateNow = new Date(Date.now()).toLocaleDateString('RU-ru',{ year: 'numeric', month: 'long', day: 'numeric' })
  return (
    <form
      onSubmit={submitHendler}
      className="col gy-2 gx-2 align-items-center mr-5"
    >
      <label
        className="form-label"
        children="Наименоване задачи"
        htmlFor="headerInput"
      />

      <input
        type="text"
        className="form-control primary"
        onChange={(e) => setTodo({ ...todo, header: e.target.value })}
        value={todo.header}
        id="headerInput"
      />

      <label
        className="form-label"
        children="Описание задачи"
        htmlFor="descriptionInput"
      />

      <textarea
        className="form-control"
        onChange={(e) => setTodo({ ...todo, description: e.target.value })}
        value={todo.description}
        id="descriptionInput"
      />

      <div className="col-auto">
        <button className="btn btn-primary my-3">Сохранить</button>
      </div>
      <div className="toast-container position-fixed bottom-0 end-0 p-3">

      <Toast aria-atomic="true" aria-live="assertive" role="alert" className="alert" isOpen={message.show}>
          <button type="button" className="btn-close float-end" onClick={()=>setMessage({show:false, data:{header:'', description:''}})} aria-label="Закрыть"></button>
          <span className="h5 mx-3 my-5">Добалвена задача: {message.data.header}</span>
      
          <ToastBody>
            <div >{"Внесена запись: "+ dateNow +" в "+ timeNow}</div>

          </ToastBody>
      </Toast>
      </div>
      
    </form>

  );
}

export default FormNewPost;
