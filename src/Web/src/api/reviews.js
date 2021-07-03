import api from "./api";

const endpoint = "r/api/v1/reviews";

const getReviews = workshopUid => api.get(`${endpoint}/${workshopUid}`);

const getGrades = reviewId => api.get(`${endpoint}/grades/${reviewId}`);

const updateReview = (reviewId, review) =>
  api.put(`${endpoint}/${reviewId}`, review);

export default {
  getReviews,
  getGrades,
  updateReview,
};
