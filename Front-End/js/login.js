const form_login = document.querySelector('#login_form');
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
    if (!login_username.value) return false;
    if (!login_password.value) return false;
    return true;
};

const clearForm = () => {
    login_username.value = '';
    login_password.value = '';
};

const goLandingPage = () => window.location.href = "landing-page.html";

const saveToLocalStorage = (obj) => localStorage.setItem('USER', JSON.stringify(obj));

///////////////////////////////////////////////////////////////////////////////////////////

const postURL = 'https://localhost:44307/User/Login';
const login = () => {
    let form = new FormData(login_form);
    let newObject = {};

    form.forEach((value, key) => { newObject[key] = value });

    fetch(postURL, {
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
            else message('Klaida: ' + res.status);

        })
        .then(u => {
            console.log(u);
            const userObj = {
                username: u.username,
                token: u.token
            }
            saveToLocalStorage(userObj);
            goLandingPage();
        })
        .catch((err) => message('Klaida - ' + err));
}


const login_button = document.querySelector('#login_button');
login_button.addEventListener('click', (e) => {
    e.preventDefault(); // Breaks manual refresh after submit
    if (validateForm())
        login();
    else {
        message('Visi laukai turi būti užpildyti!');
    }
})





