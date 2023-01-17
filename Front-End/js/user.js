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


const getMyInfo = () => {
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


////////////////////////////////////////// getUserInfo //////////////////////////////////////////


const getUserInfo = () => {
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
                    get_user_info_userId.innerHTML = userdata.userId;
                    get_user_info_username.innerHTML = userdata.username;
                    get_user_info_role.innerHTML = userdata.role;
                    get_user_info_firstname.innerHTML = userdata.firstName;
                    get_user_info_lastname.innerHTML = userdata.lastName;
                    get_user_info_created.innerHTML = userdata.created;
                    get_user_info_updated.innerHTML = userdata.updated;
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}

get_user_info_button.addEventListener('click', getUserInfo);


////////////////////////////////////////// getAddress //////////////////////////////////////////


const getAddress = () => {
    fetch('https://localhost:' + user.localhost + '/GetAddress/' + user.userId,
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
            if (res.ok) {
                res.json()
                    .then(addressdata => {
                        get_address_address_id.innerHTML = addressdata.addressId;
                        get_address_country.innerHTML = addressdata.country;
                        get_address_city.innerHTML = addressdata.city;
                        get_address_address.innerHTML = addressdata.addressText;
                        get_address_postcode.innerHTML = addressdata.postCode;
                    })
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

get_address_button.addEventListener('click', getAddress);


////////////////////////////////////////// updateAddress //////////////////////////////////////////


const updateAddress = () => {
    let form_UpdateAddress = new FormData(update_address_form);
    let newObject_UpdateAddress = {};
    newObject_UpdateAddress['addressId'] = get_address_address_id.innerHTML;
    form_UpdateAddress.forEach((value, key) => { newObject_UpdateAddress[key] = value });

    fetch('https://localhost:' + user.localhost + '/UpdateAddress',
        {
            method: 'put',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': "Bearer " + user.token
            },
            body: JSON.stringify(newObject_UpdateAddress)
        })
        .then(res => {
            //console.log(res)
            if (res.ok) {
                message('Address updated successfully')
            }
            else message('Klaida: ' + res.status + ' ' + res.statusText);
        })
        .catch((err) => message(`Klaida: ${err}`));
}

update_address_button.addEventListener('click', updateAddress);




