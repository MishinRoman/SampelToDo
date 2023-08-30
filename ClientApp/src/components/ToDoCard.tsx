import React, { MouseEventHandler, useState } from "react";
import { ITodo, ITodoCreate } from "../app.interfaces";
import { BsPencilSquare, BsTrash } from "react-icons/bs";
import todoService from "../services/todo.service";
import {
  RefetchOptions,
  RefetchQueryFilters,
  useMutation,
} from "@tanstack/react-query";
import FormEditPost from "./FormEditPost";
import "./FormEditPost.css";

import { Modal, ModalBody, ModalHeader, Toast, ToastBody } from "reactstrap";

type Props = {
  task: ITodo;
  refetch: <TPageData>(
    options?: RefetchOptions & RefetchQueryFilters<TPageData>
  ) => any;
};
let timeNow = new Date(Date.now()).toLocaleTimeString("it-IT");
let dateNow = new Date(Date.now()).toLocaleDateString("RU-ru", {
  year: "numeric",
  month: "long",
  day: "numeric",
});

function ToDoCard(props: Props) {
  const [edit, setEdit] = useState<boolean>(false);
  const [saved, setSaved] = useState<boolean>(edit);

  const { mutate } = useMutation(
    ["delete todo"],
    (todo: ITodo) => todoService.delete(todo.id),
    {
      onSuccess() {
        setSaved(true);
				props.refetch();
      },
      onMutate() {
				
      },
    }
  );

  function onClickChangeHangler(): void {
    setEdit(!edit);
  }

  function onClickDeleteHandler(): void {
    mutate(props.task);
  }
  return (
    <>
      <div className={"card mb-4 border-success bg-primary-subtle "}>
        <div className="card-body">
          <h3 className="card-title">{props.task.header}</h3>
          <p className="card-text">{props.task.description} </p>

          <button
            type="button"
            className="btn m-2 btn-danger"
            onClick={onClickDeleteHandler}
            children={[<BsTrash />, <span> Удалить </span>]}
          />

          <button
            type="button"
            className="btn m-2 btn-success"
            onClick={onClickChangeHangler}
            children={[<BsPencilSquare />, <span> Изменить </span>]}
          />
        </div>
      </div>

      <Modal isOpen={edit}>
        <ModalHeader>
          <div className="row">
            <div className="py-2 start-0">
              <h4>Изменения {props.task.header}</h4>
            </div>
            <div className="position-absolute start-100">
              <button
                type="button"
                className="btn-close "
                onClick={onClickChangeHangler}
                aria-label="Закрыть"
              />
            </div>
          </div>
          
        </ModalHeader>
        <ModalBody>
          <FormEditPost
            onClickChangeHandler={onClickChangeHangler}
            id={props.task.id}
            refetch={props.refetch}
            editData={props.task}
          />
        </ModalBody>
      </Modal>
			<div className="toast-container position-fixed bottom-0 end-0 p-3">
            <Toast
              aria-atomic="true"
              aria-live="assertive"
              role="alert"
              className="alert"
              isOpen={saved}
            >
              <span className="h5 mx-3 my-5">
                Удалена задача: {props.task.header}
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
    </>
  );
}

export default ToDoCard;
