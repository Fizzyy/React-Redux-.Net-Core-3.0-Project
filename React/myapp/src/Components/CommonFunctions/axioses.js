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
    return await createAxios().post(url, data).then(response => { return response; }).catch(error => { return error; })
}

export async function axiosGet(url) {
    return await createAxios().get(url).then(response => { return response; }).catch(error => { return error; })
}

export async function axiosDelete(url) {
    return await createAxios().delete(url).then(response => { return response; }).catch(error => { return error; })
}