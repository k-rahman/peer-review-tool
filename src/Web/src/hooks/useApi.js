import { useState } from "react";

const useApi = apiFunc => {
  const [response, setResponse] = useState([]);
  const [data, setData] = useState([]);
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);

  const request = async (...args) => {
    setLoading(true);
    const response = await apiFunc(...args);
    setLoading(false);

    setError(!response.ok);
    setResponse(response);
    setData(response.data);

    return response;
  };

  return { response, data, setData, error, loading, request };
};

export default useApi;
