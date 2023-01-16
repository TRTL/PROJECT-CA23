const user = JSON.parse(localStorage.getItem('USER'));
window.onload = function () {
    if (!user) {
        alert('Jūs nesate prisijungę! Prisijunkite, jei norite tęsti darbą.');
        window.location.href = "login.html";
    } else {
        getUser();
        getAllMediasTypeSeries();
    };
};

const logout = () => {
    localStorage.clear();
    window.location.href = "login.html";
}
footer_logout_label.addEventListener('click', logout);


////////////////////////////////////////// Messages //////////////////////////////////////////


let messageID = 0;
const clearMessage = (id) => {
    setTimeout(() => {
        document.getElementById('message_' + id).remove();
    }, 5000);
}

const message = (text) => {
    messenger.innerHTML += `<div id="message_${messageID}">${text}</div>`;
    clearMessage(messageID);
    messageID++;
}


////////////////////////////////////////// getUser //////////////////////////////////////////


const getUser = () => {
    fetch('https://localhost:' + user.localhost + '/GetUser/' + user.userId,
        {
            method: 'get',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': "Bearer " + user.token
            }
        })
        .then(obj => {
            //console.log(obj)

            obj.json()
                .then(userdata => {
                    //console.log(userdata)
                    footer_userid_label.innerHTML = "ID: " + userdata.userId;
                    footer_role_label.innerHTML = "Role: " + userdata.role;
                    footer_username_label.innerHTML = "Username: " + userdata.username;
                    footer_fullname_label.innerHTML = userdata.firstName + ' ' + userdata.lastName;

                    if (userdata.role === 'admin') {
                        a_link_admin.style.display = "inline";
                    }
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}


////////////////////////////////////////// GetAllMediasTypeSeries //////////////////////////////////////////


const getAllMediasTypeSeries = () => {
    fetch('https://localhost:' + user.localhost + '/GetAllMedias?type=series',
        {
            method: 'get',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': "Bearer " + user.token
            }
        })
        .then(obj => {
            //console.log(obj)
            obj.json()
                .then(mediadata => {
                    //console.log(mediadata)
                    mediadata.forEach(media => {
                        //console.log(media)
                        all_movies.innerHTML +=
                            '<div class="media_container">' +
                            '<div class="m_c_mask">' +
                            '<div class="m_c_star" onclick="addUserMedia(' + media.mediaId + ')" title="Add ' + media.title + ' to your list">⭐</div></div>' +
                            '<div class="m_c_pic"><img src="' + media.poster + '"></div>' +
                            '<div class="m_c_title" title="' + media.title + '">' + media.title + '</div>' +
                            '</div>';
                    });
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}


////////////////////////////////////////// addUserMedia //////////////////////////////////////////


const addUserMedia = (mediaId) => {
    let newObject_AddUserMedia = {};
    newObject_AddUserMedia['userId'] = user.userId;
    newObject_AddUserMedia['mediaId'] = mediaId;

    fetch('https://localhost:' + user.localhost + '/AddUserMedia',
        {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': "Bearer " + user.token
            },
            body: JSON.stringify(newObject_AddUserMedia)
        })
        .then(res => {
            //console.log(res)
            if (res.ok) {
                message('Media added to my list successfully')
            }
            else {
                res.text()
                    .then(text => {
                        message(text);
                    })
            }
        })
        .catch((err) => message(`Klaida: ${err}`));
}









