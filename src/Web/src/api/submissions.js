import api from "./api";

const endpoint = "s/api/v1/submissions";

const getSubmissionDeadlines = workshopUid =>
  api.get(`${endpoint}/deadlines/${workshopUid}`);

const getSubmission = workshopUid => api.get(`${endpoint}/${workshopUid}`);

const createSubmission = (workshopUid, submission) =>
  api.post(`${endpoint}/${workshopUid}`, submission);

const updateSubmission = (submissionId, submission) =>
  api.put(`${endpoint}/${submissionId}`, submission);

export default {
  getSubmission,
  getSubmissionDeadlines,
  createSubmission,
  updateSubmission,
};
