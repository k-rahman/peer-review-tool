import api from "./api";
import { formatISO } from "date-fns";

// const endpoint = "api/v1/workshops";
const endpoint = "w/api/v1/workshops";

const getWorkshops = _ => api.get(endpoint);
// const getWorkshops = _ => api.workshopService.get(endpoint);
const getWorkshopByUid = workshopUid => api.get(`${endpoint}/${workshopUid}`);
const addWorkshop = newWorkshop => {
  const data = new FormData();

  data.append("name", newWorkshop.name);
  data.append("description", newWorkshop.description);
  data.append("published", formatISO(newWorkshop.published));
  data.append("participantsEmails", newWorkshop.participants);
  data.append("criteria", JSON.stringify(newWorkshop.criteria));
  data.append("submissionStart", formatISO(newWorkshop.submissionStart));
  data.append("submissionEnd", formatISO(newWorkshop.submissionEnd));
  data.append("numberOfReviews", newWorkshop.numberOfReviews);
  data.append("reviewStart", formatISO(newWorkshop.reviewStart));
  data.append("reviewEnd", formatISO(newWorkshop.reviewEnd));
  data.append("instructor", newWorkshop.instructor);

  return api.post(endpoint, data);
};

const updateWorkshop = (workshopId, workshop) =>
  api.put(`${endpoint}/${workshopId}`);

const deleteWorkshop = workshopId => api.delete(`${endpoint}/${workshopId}`);

export default {
  getWorkshops,
  getWorkshopByUid,
  addWorkshop,
  updateWorkshop,
  deleteWorkshop,
};
