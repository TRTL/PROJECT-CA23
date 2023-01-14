const user = JSON.parse(localStorage.getItem('USER'));
window.onload = function () {
    if (!user) {
        alert('Jūs nesate prisijungę! Prisijunkite, jei norite tęsti darbą.');
        window.location.href = "login.html";
    } else {
        getMyInfo();
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


////////////////////////////////////////// getMyInfo //////////////////////////////////////////


const getOptions = {
    method: 'get',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + user.token
    }
}

const getMyInfo = () => {
    fetch('https://localhost:' + user.localhost + '/GetUser/' + user.userId, getOptions)
        .then(obj => {
            console.log(obj)

            obj.json()
                .then(userdata => {
                    console.log(userdata)
                    footer_userid_label.innerHTML = "ID: " + userdata.userId;
                    footer_role_label.innerHTML = "Role: " + userdata.role;
                    footer_username_label.innerHTML = "Username: " + userdata.username;
                    footer_fullname_label.innerHTML = userdata.firstName + ' ' + userdata.lastName;

                    if (userdata.role === 'admin') {
                        a_link_admin.style.display = "inline";
                    }

                    get_user_info_userId.innerHTML = userdata.userId;
                    get_user_info_username.innerHTML = userdata.username;
                    get_user_info_role.innerHTML = userdata.role;
                    get_user_info_firstname.innerHTML = userdata.firstName;
                    get_user_info_lastname.innerHTML = userdata.lastName;
                    get_user_info_created.innerHTML = userdata.created;
                    get_user_info_updated.innerHTML = userdata.updated;
                    get_user_info_lastlogin.innerHTML = userdata.lastLogin;
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}

get_user_info_button.addEventListener('click', getMyInfo);


//////////////////////////////////////////  //////////////////////////////////////////






