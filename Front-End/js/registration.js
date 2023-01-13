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
    alert(text);
    //messenger.innerHTML += `<div id="message_${messageID}">${text}</div>`;
    //clearMessage(messageID);
    //messageID++;
}

///////////////////////////////////////////////////////////////////////////////////////////

const validateForm = () => {
    if (!login_firstname.value) return false;
    if (!login_lastname.value) return false;
    if (!login_role.value) return false;
    if (!login_username.value) return false;
    if (!login_password.value) return false;
    return true;
};

const clearForm = () => {
    login_firstname.value = '';
    login_lastname.value = '';
    login_role.value = '';
    login_username.value = '';
    login_password.value = '';
};

const goLandingPage = () => window.location.href = "landing-page.html";

const saveToLocalStorage = (obj) => localStorage.setItem('USER', JSON.stringify(obj));

///////////////////////////////////////////////////////////////////////////////////////////

const login = () => {
    let form = new FormData(registration_form);
    let newObject = {};

    form.forEach((value, key) => { newObject[key] = value });

    fetch('https://localhost:' + LocalHost.value + '/User/Register', {
        method: 'post',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newObject)
    })
        .then(res => {
            //console.log(res);
            //console.log(res.json());
            if (res.ok) {
                clearForm();
                return res.json();
            }
            else message('Klaida 1: ' + res.status);

        })
        .then(u => {
            const userObj = {
                ID: u.id,
                FirstName: u.FirstName,
                LastName: u.LastName
            }
            saveToLocalStorage(userObj);
            goLandingPage();
        })
        .catch((err) => message('Klaida 2: ' + err));
}

const register_button = document.querySelector('#register_button');
register_button.addEventListener('click', (e) => {
    e.preventDefault(); // Breaks manual refresh after submit
    if (validateForm())
        login();
    else {
        message('Visi laukai turi būti užpildyti!');
    }
})