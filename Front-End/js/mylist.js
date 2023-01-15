const user = JSON.parse(localStorage.getItem('USER'));
window.onload = function () {
    if (!user) {
        alert('Jūs nesate prisijungę! Prisijunkite, jei norite tęsti darbą.');
        window.location.href = "login.html";
    } else {
        getUser();
        getAllUserMedias();
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
        .then(res => {
            //console.log(res)
            res.json()
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

////////////////////////////////////////// getAllUserMedias //////////////////////////////////////////


const getAllUserMedias = () => {
    fetch('https://localhost:' + user.localhost + '/GetAllUserMedias?UserId=' + user.userId,
        {
            method: 'get',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': "Bearer " + user.token
            }
        })
        .then(res => {
            console.log(res)
            res.json()
                .then(usermediadata => {
                    console.log(usermediadata)
                    usermediadata.forEach(media => {
                        all_movies_table_body.innerHTML +=
                            '<tr id="all_users_table_user_"' + media.userMediaId + '>' +
                            '<td>' + media.userMediaStatus + '</td>' +
                            '<td>' + media.type + '</td>' +
                            '<td>' + media.title + '</td>' +
                            '<td>' + media.year + '</td>' +
                            '<td>' + media.imdbId + '</td>' +
                            '<td>' + media.imdbRating + '</td>' +
                            '<td>' + media.userRating + '</td>' +
                            '<td></td>' +
                            '</tr>';
                    });
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}









