import { create } from "apisauce";
import authStorage from "../auth/storage";

const taskService = create({
  baseURL: "http://192.168.1.7:5000",
});

const workService = create({
  baseURL: "http://192.168.1.7:5001",
});

// add token to every task api request
taskService.addAsyncRequestTransform(async request => {
  const token = await authStorage.getToken();
  if (!token) return;
  request.headers["Authorization"] = "Bearer " + token;
});

export default { taskService, workService };
