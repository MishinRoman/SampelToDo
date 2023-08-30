import React, { ButtonHTMLAttributes, useState } from "react";


type Props = {
  messageText: string;
  header: string;
  //setMessage:(boolean)=>any,
  show: boolean;
};

export default function ToastMessage({ ...props }: Props) {
  const [show, setShow] = useState(props.show);
  return show ? (
    <div className="toast-container position-fixed bottom-0 end-0 p-3">
      <div
        className={"toast"}
        role="alert"
        aria-live="assertive"
        aria-atomic="true"
      >
        <div className="toast-header">
          <strong className="me-auto">{props.header}</strong>
          <small>{Date.now().toLocaleString("it-IT")}</small>
          <button
            type="button"
            className="btn-close"
            //data-bs-dismiss="toast"
            aria-label="Закрыть"
            onClick={() => setShow(false)}
          ></button>
        </div>
        <div className="toast-body">{props.messageText}</div>
      </div>
    </div>
  ) : null;
}
