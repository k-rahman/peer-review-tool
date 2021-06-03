import api from "./api";

const endpoint = "api/v1/works";

const getAuthorWorkByTask = taskUid =>
  api.workService.get(`${endpoint}/${taskUid}`);
const updateStudentWork = (workId, studentId) =>
  api.workService.put(`${endpoint}/${workId}/student/${studentId}`);

export default { getAuthorWorkByTask, updateStudentWork };
