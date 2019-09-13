import axios from 'axios';

function createAxios() {
    return axios.create({
        headers: {
            'AccessToken': localStorage.getItem('Token'),
            'RefreshToken': localStorage.getItem('RefreshToken')
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