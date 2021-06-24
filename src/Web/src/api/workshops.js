import api from "./api";
import { formatISO } from "date-fns";

// const endpoint = "api/v1/workshops";
const endpoint = "w/api/v1/workshops";

const getWorkshops = _ => api.get(endpoint);
// const getWorkshops = _ => api.workshopService.get(endpoint);
const getWorkshopByUid = workshopUid => api.get(`${endpoint}/${workshopUid}`);
const addWorkshop = workshop => {
  const data = new FormData();

  data.append("name", workshop.name);
  data.append("description", workshop.description);
  data.append("numberOfReviews", workshop.numberOfReviews);
  data.append("participantsEmails", workshop.participantsEmails);
  data.append("submissionStart", formatISO(workshop.submissionStart));
  data.append("submissionEnd", formatISO(workshop.submissionEnd));
  data.append("reviewStart", formatISO(workshop.reviewStart));
  data.append("reviewEnd", formatISO(workshop.reviewEnd));
  data.append("published", formatISO(workshop.published));
  data.append("criteria", JSON.stringify(workshop.criteria));

  // return api.workshopService.post(endpoint, data);
  return api.api.post(endpoint, data);
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
