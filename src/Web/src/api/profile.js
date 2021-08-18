import api from "./api";

const endpoint = "w/api/v1/participant";

const updateUserMetadata = data => api.post(endpoint, { user_metadata: data });

const profileOperations = {
  updateUserMetadata,
};

export default profileOperations;
