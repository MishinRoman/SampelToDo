import React, { useEffect, useState } from "react";
import { isError, useQuery } from "@tanstack/react-query";
import todoServices from "../services/todo.service";
import FormNewPost from "./FormNewPost";
import ToDoCard from "./ToDoCard";
import * as signalR from "@microsoft/signalr";
import { Toast, ToastBody } from "reactstrap";

const ToDoList = () => {
  //const connection = new signalR.HubConnectionBuilder().withUrl("/").build();
  const { data, refetch, status, isFetching, error } = useQuery(
    ["todos"],
    () => todoServices.getAll(),
    {
      select: (data) => data.data,
      onError: (error: string[]) => error,
    }
  );
  
  
  
  return status === "loading" ?( 
    <div className="d-flex justify-content-center">
      <div className="spinner-border" role="status">
        <span className="visually-hidden">Загрузка...</span>
      </div>
    </div>
  ) : status === "error" ? (
    <div className="alert alert-danger" role="alert">
      <span>{error["name"]}: </span>
      <span>
        {" "}
        {error["code"]} <span> {error["response"]["statusText"]}</span>
      </span>
      <span> {error["message"]}</span>
    </div>
  ) : (
    <div className="row">
      <div className="col-8 position-relative">
        <div className="overflow-auto scroll-container">
        
          {isFetching ? (
            <div className="d-flex justify-content-center">
              <div className="spinner-border" role="status">
                <span className="visually-hidden">Загрузка...</span>
              </div>
            </div>
          ) : (
          data?.map((todo) => (
              <div key={todo.id} className="col mb-sm-0">
                <ToDoCard refetch={refetch} task={todo} />
              </div>
            ))
          
          
          )}
        </div>
      </div>
          <div className="col-3 position-fixed top-5 end-0">
            <FormNewPost refetch={refetch} />
          </div>
          
    </div>
  );
  
};

export default ToDoList;
