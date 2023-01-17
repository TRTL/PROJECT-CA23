const login_firstname = document.querySelector('#login_firstname');
const login_lastname = document.querySelector('#login_lastname');
const login_role = document.querySelector('#login_role');
const login_username = document.querySelector('#login_username');
const login_password = document.querySelector('#login_password');

document.addEventListener("keypress", (event) => {
    if (event.key === "Enter") {
        event.preventDefault();
        document.getElementById("login-button").click();
    }
});

///////////////////////////////////////////////////////////////////////////////////////////

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

///////////////////////////////////////////////////////////////////////////////////////////

const validateForm = () => {
    if (!registration_firstname.value) return false;
    if (!registration_lastname.value) return false;
    if (!registration_role.value) return false;
    if (!registration_username.value) return false;
    if (!registration_password.value) return false;
    return true;
};

const clearForm = () => {
    registration_firstname.value = '';
    registration_lastname.value = '';
    registration_role.value = '';
    registration_username.value = '';
    registration_password.value = '';
};

//const goToHomePage = () => window.location.href = "home.html";

const saveToLocalStorage = (obj) => localStorage.setItem('USER', JSON.stringify(obj));

///////////////////////////////////////////////////////////////////////////////////////////

const register = () => {
    let form = new FormData(registration_form);
    let newObject = {};

    form.forEach((value, key) => { newObject[key] = value });

    fetch('https://localhost:' + registration_localhost.value + '/Register', {
        method: 'post',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newObject)
    })
        .then(res => {
            if (res.ok) {
                clearForm();
                message("Registration successful. You may now Login.");
            }
            else {
                res.text()
                    .then(text => {
                        message(text);
                    })
            }
        })
        .catch((err) => message(err));
}

const register_button = document.querySelector('#register_button');
register_button.addEventListener('click', (e) => {
    e.preventDefault(); // Breaks manual refresh after submit
    if (validateForm())
        register();
    else {
        message('Visi laukai turi būti užpildyti!');
    }
})