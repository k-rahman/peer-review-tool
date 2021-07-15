import { create } from "apisauce";

export default create({
  baseURL: process.env.REACT_APP_BACKEND_URL,
});
