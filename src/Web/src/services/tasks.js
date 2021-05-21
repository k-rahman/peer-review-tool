import api from "./api";

const endpoint = "/api/v1/tasks";

const getTasks = _ => api.get(endpoint);
const getTaskById = taskId => api.get(`${endpoint}/${taskId}`);
const getTaskByLink = taskLink => api.get(`${endpoint}/link/${taskLink}`);
const addTask = task => api.post(endpoint, task);
const deleteTask = taskId => api.delete(`${endpoint}/${taskId}`);

export default {
  getTasks,
  getTaskById,
  getTaskByLink,
  addTask,
  deleteTask,
};
