import apiClient from "./apiClient";

export const getApi = (uri) => {
  return apiClient.get(uri);
};

export const postApi = (uri, data) => {
  return apiClient.post(uri, data);
};

