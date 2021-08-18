import api from "./api";

const endpoint = "s/api/v1/submissions";

const getSubmissionDeadlines = workshopUid =>
  api.get(`${endpoint}/deadlines/${workshopUid}`);

const getSubmissions = workshopUid => api.get(`${endpoint}/${workshopUid}`);

const getSubmission = workshopUid =>
  api.get(`${endpoint}/${workshopUid}/author`);

const createSubmission = (workshopUid, submission) =>
  api.post(`${endpoint}/${workshopUid}`, submission);

const updateSubmission = (submissionId, submission) =>
  api.put(`${endpoint}/${submissionId}`, submission);

const submissionOperations = {
  getSubmissionDeadlines,
  getSubmissions,
  getSubmission,
  createSubmission,
  updateSubmission,
};

export default submissionOperations;
