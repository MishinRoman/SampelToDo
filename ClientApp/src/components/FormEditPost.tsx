import React, { FormEvent, useState } from "react";
import { ITodo, ITodoCreate } from "../app.interfaces";
import todoService from "../services/todo.service";
import {
  QueryObserverResult,
  RefetchOptions,
  RefetchQueryFilters,
  useMutation,
} from "@tanstack/react-query";
import "../custom.css";
import { Toast, ToastBody } from "reactstrap";

type Props = {
  onClickChangeHandler:()=>void;
  refetch():<TPageData>(
    options: RefetchOptions & RefetchQueryFilters<TPageData>
  ) => Promise<QueryObserverResult<ITodo[], unknown>>;
  editData: ITodoCreate;
  id: string; 
};

function FormEditPost(props: Props): React.JSX.Element {
 
  const [saved, setSaved] = useState<boolean>(false);
  const [todo, setTodo] = useState<ITodo>({
    id: props.id??'',
    description: props.editData.description??'',
    header: props.editData.header??''
  });

  const { mutate } = useMutation(
    ["mutate todo"],
    (todo: ITodo) => todoService.update(todo),
    {
      onSuccess() {
        props.onClickChangeHandler();
        props.refetch();
        setSaved(true)
       
      },
      onMutate() {
        
        
      },
    }
  );

  function submitHendler(event: FormEvent<HTMLFormElement>): void {
    event.preventDefault();
    mutate(todo);
    
    
  }
  let timeNow = new Date(Date.now()).toLocaleTimeString("it-IT");
let dateNow = new Date(Date.now()).toLocaleDateString("RU-ru", {
  year: "numeric",
  month: "long",
  day: "numeric",
});

  return (
    <div className="row align-items-center">
      <form onSubmit={submitHendler} className="gy-2 gx-2">
        <label
          className="form-label"
          children="Наименоване задачи"
        
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
        
        />

        <textarea
          className="form-control"
          onChange={(e) => setTodo({ ...todo, description: e.target.value })}
          value={todo.description}
          id="descriptionInput"
        />
        <div className="modal-footer">
          <button type="submit" className="btn btn-primary">
            Сохранить изменения
          </button>
        </div>
      </form>
          <div className="toast-container position-fixed bottom-0 end-0 p-3">
            <Toast
              aria-atomic="true"
              aria-live="assertive"
              role="alert"
              className="alert"
              isOpen={saved}
            >
              <span className="h5 mx-3 my-5">
                Изменена задача: {todo.header}
              </span>
              <button
                type="button"
                className="btn-close float-end"
                onClick={() => setSaved(false)}
                aria-label="Закрыть"
              ></button>

              <ToastBody>
                <div>{"Внесена запись: " + dateNow + " в " + timeNow}</div>
              </ToastBody>
            </Toast>
          </div>
    </div>
  );
}

export default FormEditPost;
