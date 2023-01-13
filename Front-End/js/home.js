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
logout_label.addEventListener('click', logout);

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

const getOptions = {
    method: 'get',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + user.token
    }
}

const getMyInfo = () => {
    fetch('https://localhost:' + user.localhost + '/GetUser/' + user.userId + '/Info', getOptions)
        .then(obj => {
            console.log(obj)

            obj.json()
                .then(userdata => {
                    console.log(userdata)
                    userid_label.innerHTML = "ID: " + userdata.userId;
                    role_label.innerHTML = "Role: " + userdata.role;
                    username_label.innerHTML = "Username: " + userdata.username;
                    fullname_label.innerHTML = userdata.firstName + ' ' + userdata.lastName;

                    // for (const todo of userTodoData.data) {
                    //     if (todo.UserID === user.ID) {
                    //         userTodosArr.push({
                    //             ID: todo.id,
                    //             Completed: todo.Completed,
                    //             Type: todo.Type,
                    //             Content: todo.Content,
                    //             EndDate: todo.EndDate,
                    //             Created: todo.createdAt.slice(0, 10) + ' ' + todo.createdAt.slice(11, 19),
                    //             Updated: todo.updatedAt.slice(0, 10) + ' ' + todo.updatedAt.slice(11, 19)
                    //         });
                    //     }
                    // }
                    //saveToLocalStorage(userTodosArr)
                    //searchLocalToDos();
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}










