import api from "./api";
import { formatISO } from "date-fns";

const endpoint = "api/v1/tasks";

const getTasks = _ => api.taskService.get(endpoint);
const getTaskByUid = taskUid => api.taskService.get(`${endpoint}/${taskUid}`);
const addTask = task => {
  const data = new FormData();

  data.append("name", task.name);
  data.append("description", task.description);
  data.append("participantsEmails", task.participantsEmails);
  data.append("submissionStart", formatISO(task.submissionStart));
  data.append("submissionEnd", formatISO(task.submissionEnd));
  data.append("reviewStart", formatISO(task.reviewStart));
  data.append("reviewEnd", formatISO(task.reviewEnd));
  data.append("published", formatISO(task.published));
  data.append("criteria", JSON.stringify(task.criteria));

  return api.taskService.post(endpoint, data);
};

const updateTask = taskId => api.taskService.put(`${endpoint}/${taskId}`);
const deleteTask = taskId => api.taskService.delete(`${endpoint}/${taskId}`);

export default {
  getTasks,
  getTaskByUid,
  addTask,
  updateTask,
  deleteTask,
};
