import api from "./api";

const endpoint = "r/api/v1/reviews";

const getReviewDeadlines = workshopUid =>
  api.get(`${endpoint}/deadlines/${workshopUid}`);

const getReviews = workshopUid => api.get(`${endpoint}/${workshopUid}`);

const getReviewsSummary = workshopUid =>
  api.get(`${endpoint}/summary/${workshopUid}`);

const getGrades = reviewId => api.get(`${endpoint}/grades/${reviewId}`);

const updateReview = (reviewId, review) =>
  api.put(`${endpoint}/${reviewId}`, { grades: review });

const reviewOperations = {
  getReviewDeadlines,
  getReviews,
  getReviewsSummary,
  getGrades,
  updateReview,
};

export default reviewOperations;
