import React, { Component } from "react";
import {
  useQuery,
  useMutation,
  useQueryClient,
  QueryClient,
  QueryClientProvider,
} from "@tanstack/react-query";
import { Route, Routes } from "react-router-dom";
import AppRoutes from "./AppRoutes";
import  Layout  from "./components/Layout";
import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    const client = new QueryClient();
    return (
      <QueryClientProvider client={client}>
        <Layout>
          <Routes>
            {AppRoutes.map((route, index) => {
              const { element, ...rest } = route;
              return <Route key={index} {...rest} element={element} />;
            })}
          </Routes>
        </Layout>
      </QueryClientProvider>
    );
  }
}
