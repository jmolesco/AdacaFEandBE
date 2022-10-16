import axios from "axios";

const localServer = "https://localhost:44316/api/Client";
const devServer = "http://janmarkolesco-001-site1.atempurl.com/api/Client"
const API_URL = devServer;

export function apiGet(uri) {
  console.log("url", uri);
  return api(uri, "get");
}


export function apiPost(uri,data){
  return api(uri, "post", data);
}

async function api(uri,method, data){
  return call(uri, method, data);
}

function call(uri, method, data) 
{
  let request = 
  {
    url: `${API_URL}/${uri}`,
    headers: {
      "Access-Control-Allow-Origin": "*",
      'Access-Control-Allow-Headers': '*',
      "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT",
      "Content-Type": "application/json",
    },
    method
  };

  if (data !== null) {
    request = {
      ...request,
      data,
    };
  }
  //console.log("request",request);
  return axios(request)
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      return error.response;
    });
}

const apiClient = {
  get: apiGet,
  post: apiPost,
};

export default apiClient;
