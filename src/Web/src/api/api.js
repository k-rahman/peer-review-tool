import { create } from "apisauce";

const taskServiceUrl = process.env.REACT_APP_TASK_SERVICE_URL;
const workServiceUrl = process.env.REACT_APP_WORK_SERVICE_URL;

const taskService = create({
  baseURL: taskServiceUrl,
});

const workService = create({
  baseURL: workServiceUrl,
});

// // add token to every task api request
// taskService.addAsyncRequestTransform(async request => {
//   const token = await getAccessTokenSilently();
//   if (!token) return;
//   request.headers["Authorization"] = "Bearer " + token;
// });

export default { taskService, workService };
