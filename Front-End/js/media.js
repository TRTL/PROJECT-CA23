const urlSearchParams = new URLSearchParams(window.location.search);
const params = Object.fromEntries(urlSearchParams.entries());

const user = JSON.parse(localStorage.getItem('USER'));
window.onload = function () {
    if (!user) {
        alert('Jūs nesate prisijungę! Prisijunkite, jei norite tęsti darbą.');
        window.location.href = "login.html";
    } else {
        getUser();
        getMediaById(params.mediaId);
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


// $('.tab_link').click(function () {
//     var contClass = $(this).data('div');
//     $('.hidable').hide().filter('.' + contClass).show();
// })


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
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}


////////////////////////////////////////// GetMediaById //////////////////////////////////////////


const getMediaById = (id) => {
    media.innerHTML = '';
    fetch('https://localhost:' + user.localhost + '/GetMediaById/' + id,
        {
            method: 'get',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': "Bearer " + user.token
            }
        })
        .then(obj => {
            console.log(obj)
            obj.json()
                .then(media => {
                    console.log(media)
                    if (media.title === null) {
                        message(`Not found`);
                    }
                    else {
                        let genres = '';
                        media.genres.forEach(g => {
                            genres += g.name + ' ';
                        });

                        document.getElementById('media').innerHTML =
                            '<div style="   display: flex; flex-flow: row nowrap; justify-content: start;">' +
                            '<div><div><img src="' + media.poster + '"width=""></div></div>' +
                            '<div><div><b>Title:</b> ' + media.title + '</div>' +
                            '<div><b>Year:</b> ' + media.year + '</div>' +
                            '<div><b>Genres:</b> ' + genres + '</div>' +
                            '<div><b>Director:</b> ' + media.director + '</div>' +
                            '<div><b>Writer:</b>' + media.writer + '</div>' +
                            '<div><b>Actors:</b> ' + media.actors + '</div>' +
                            '<div><b>Plot:</b> ' + media.plot + '</div>' +
                            '</div>';

                        let reviews = '';
                        media.reviews.forEach(r => {
                            document.getElementById('media').innerHTML +=
                                '<div style="   display: flex; flex-flow: row nowrap; justify-content: start;">' +
                                '<div style="margin:5px;"><b>' + r.user.firstName + ' ' + r.user.lastName + ' says:</b></div>' +
                                '<div style="margin:5px;">' + r.reviewText + '</div>' +
                                '</div>'
                        });
                    }
                })

        })
        .catch((err) => message(`Klaida: ${err}`));
}





