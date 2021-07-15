import { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";

import api from "../api/api";

const useApi = apiFunc => {
  const { getAccessTokenSilently } = useAuth0();

  api.addAsyncRequestTransform(async request => {
    const token = await getAccessTokenSilently();
    if (!token) return;
    request.headers["Authorization"] = "Bearer " + token;
  });

  const [response, setResponse] = useState([]);
  const [data, setData] = useState([]);
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);

  const request = async (...args) => {
    setLoading(true);
    const response = await apiFunc(...args);
    setLoading(false);

    setResponse(response);

    if (!response.ok) return setError(true);

    setData(response.data);
    return response;
  };

  return { response, data, setData, error, loading, request };
};

export default useApi;
