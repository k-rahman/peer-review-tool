import { create } from "apisauce";

export default create({
  baseURL: process.env.REACT_APP_BACKEND_URL,
});

// const workshopServiceUrl = process.env.REACT_APP_WORKSHOP_SERVICE_URL;
// const submissionServiceUrl = process.env.REACT_APP_SUBMISSION_SERVICE_URL;
// const reviewServiceUrl = process.env.REACT_APP_REVIEW_SERVICE_URL;

// const workshopService = create({
//   baseURL: workshopServiceUrl,
// });

// const submissionService = create({
//   baseURL: submissionServiceUrl,
// });

// const reviewService = create({
//   baseURL: reviewServiceUrl,
// });

// // add token to every workshop api request
// workshopService.addAsyncRequestTransform(async request => {
//   const token = await getAccessTokenSilently();
//   if (!token) return;
//   request.headers["Authorization"] = "Bearer " + token;
// });

// export default { api, workshopService, submissionService, reviewService };
