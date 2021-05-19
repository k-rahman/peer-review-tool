import api from "./api";

const endpoint = "/api/v1/tasks";

const getTasks = _ => api.get(endpoint);
const getTaskById = taskId => api.get(`${endpoint}/${taskId}`);
const addTask = task => api.post(endpoint, task);
const deleteTask = taskId => api.delete(`${endpoint}/${taskId}`);

export default {
  getTasks,
  getTaskById,
  addTask,
  deleteTask,
};
