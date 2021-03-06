import axios from 'axios';

function getToken(token) {
    let foundtoken = localStorage.getItem(token);
    if (foundtoken === null) return JSON.stringify(foundtoken);
    return foundtoken;
}

function createAxios() {
    return axios.create({
        headers: {
            AccessToken: getToken('Token'),
            RefreshToken: getToken('RefreshToken'),
            Authorization: 'Bearer ' + localStorage.getItem('Token')
        },
        responseType: 'json'
    })
}

export async function axiosPost(url, data) {
    return await createAxios().post(url, data).then(response => {
        if (response.headers.accesstoken) {
            localStorage.setItem('Token', response.headers.accesstoken);
            localStorage.setItem('RefreshToken', response.headers.refreshtoken);
        }
        return response;
    }).catch(error => { return error; })
}

export async function axiosGet(url) {
    return await createAxios().get(url).then(response => {
        if (response.headers.accesstoken) {
            localStorage.setItem('Token', response.headers.accesstoken);
            localStorage.setItem('RefreshToken', response.headers.refreshtoken);
        }
        return response;
    }).catch(error => { return error; })
}

export async function axiosDelete(url, idArray) {
    return await createAxios().delete(url, { data: { idArray } }).then(response => {
        return response;
    }).catch(error => { return error; })
}

export async function axiosPut(url, data) {
    return await createAxios().put(url, data).then(response => {
        if (response.headers.accesstoken) {
            localStorage.setItem('Token', response.headers.accesstoken);
            localStorage.setItem('RefreshToken', response.headers.refreshtoken);
        }
        return response;
    }).catch(error => { return error; })
}