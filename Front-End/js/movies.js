const user = JSON.parse(localStorage.getItem('USER'));
window.onload = function () {
    if (!user) {
        alert('Jūs nesate prisijungę! Prisijunkite, jei norite tęsti darbą.');
        window.location.href = "login.html";
    } else {
        getUser();
        getAllMedias();
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


////////////////////////////////////////// Tab links //////////////////////////////////////////


$('.tab_link').click(function () {
    var contClass = $(this).data('div');
    $('.hidable').hide().filter('.' + contClass).show();
})


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


////////////////////////////////////////// getAllMedias //////////////////////////////////////////


const getAllMedias = () => {
    fetch('https://localhost:' + user.localhost + '/GetAllMedias',
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
                        console.log(media)
                        all_movies.innerHTML +=
                            '<div class="media_container">' +
                            '<div class="m_c_pic"><img src="' + media.poster + '"width="120"></div>' +
                            '<div class="m_c_title" title="' + media.title + '">' + media.title + '</div>' +
                            '</div>';

                    });
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}






