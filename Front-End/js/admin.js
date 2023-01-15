const user = JSON.parse(localStorage.getItem('USER'));
window.onload = function () {
    if (!user) {
        alert('Jūs nesate prisijungę! Prisijunkite, jei norite tęsti darbą.');
        window.location.href = "login.html";
    } else {
        getUser();
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

                    if (userdata.role != 'admin') {
                        message('You are not admin! Go away!')
                        setTimeout(function () { message('3') }, 1000);
                        setTimeout(function () { message('2') }, 2000);
                        setTimeout(function () { message('1') }, 3000);
                        setTimeout(function () { window.location.href = "mylist.html" }, 4000);

                    }
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}


////////////////////////////////////////// getAllUsers //////////////////////////////////////////


const getAllUsers = () => {
    fetch('https://localhost:' + user.localhost + '/GetAllUsers',
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
                .then(usersdata => {
                    //console.log(usersdata)
                    usersdata.forEach(u => {
                        all_users_table_body.innerHTML +=
                            '<tr id="all_users_table_user_"' + u.userId + '>' +
                            '<td>' + u.userId + '</td>' +
                            '<td>' + u.firstName + '</td>' +
                            '<td>' + u.lastName + '</td>' +
                            '<td>' + u.username + '</td>' +
                            '<td>' + u.role + '</td>' +
                            '<td>' + u.created + '</td>' +
                            '<td>' + u.updated + '</td>' +
                            '<td>' + u.lastLogin + '</td>' +
                            '<td>' + u.isDeleted + '</td>' +
                            '<td></td>' +
                            '</tr>';
                    });
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}

get_all_users_button.addEventListener('click', getAllUsers);


////////////////////////////////////////// getAllAddresses //////////////////////////////////////////


const getAllAddresses = () => {
    fetch('https://localhost:' + user.localhost + '/GetAllAddresses',
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
                .then(addressesdata => {
                    //console.log(addressesdata)
                    addressesdata.forEach(a => {
                        all_addresses_table_body.innerHTML +=
                            '<tr id="all_addresses_table_user_"' + a.userId + '>' +
                            '<td>' + a.userId + '</td>' +
                            '<td>' + a.firstName + ' ' + a.lastName + '</td>' +
                            '<td>' + a.country + '</td>' +
                            '<td>' + a.city + '</td>' +
                            '<td>' + a.addressText + '</td>' +
                            '<td>' + a.postCode + '</td>' +
                            '<td></td>' +
                            '</tr>';
                    });
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}

get_all_addresses_button.addEventListener('click', getAllAddresses);


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
                    mediadata.forEach(m => {
                        let allGenres = '';
                        m.genres.forEach(g => {
                            allGenres += g.name + ' ';
                        });
                        all_media_table_body.innerHTML +=
                            '<tr id="all_addresses_table_user_"' + m.mediaId + '>' +
                            '<td>' + m.mediaId + '</td>' +
                            '<td>' + m.type + '</td>' +
                            '<td>' + m.title + '</td>' +
                            '<td>' + m.year + '</td>' +
                            '<td>' + allGenres + '</td>' +
                            '<td>' + m.imdbId + '</td>' +
                            '<td>⭐' + m.imdbRating + '</td>' +
                            '<td></td>' +
                            '</tr>';
                    });
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}

get_all_media_button.addEventListener('click', getAllMedias);


////////////////////////////////////////// SearchForMediaAtOmdb //////////////////////////////////////////


const searchForMediaAtOmdb = () => {
    localStorage.removeItem('OmdbApiMedia');
    omdb_result_media_container.innerHTML = '';
    fetch('https://localhost:' + user.localhost + '/SearchForMediaAtOmdb?mediaTitle=' + search_for_media_at_omdb_input.value,
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
                        omdb_result_media_container.innerHTML +=
                            '<div>' +
                            '<div><img src="' + media.poster + '"width="120"></div>' +
                            '<div><b>Title:</b> ' + media.title + '</div>' +
                            '<div><b>Year:</b> ' + media.year + '</div>' +
                            '<div><b>Genre:</b> ' + media.genre + '</div>' +
                            '<div><b>Director:</b> ' + media.director + '</div>' +
                            '<div><b>Writer:</b>' + media.writer + '</div>' +
                            '<div><b>Actors:</b> ' + media.actors + '</div>' +
                            '<div><b>Plot:</b> ' + media.plot + '</div>' +
                            '</div>';
                        localStorage.setItem('OmdbApiMedia', JSON.stringify(media));
                    }
                })

        })
        .catch((err) => message(`Klaida: ${err}`));
}

search_for_media_at_omdb_button.addEventListener('click', searchForMediaAtOmdb);


////////////////////////////////////////// AddMediaFromOmdb //////////////////////////////////////////


const addMediaFromOmdb = () => {
    let movieInStorage = localStorage.getItem('OmdbApiMedia');
    if (movieInStorage === null) {
        message(`No media found. Search for media, before adding.`)
    }
    else {
        fetch('https://localhost:' + user.localhost + '/AddMediaFromOmdb',
            {
                method: 'post',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': "Bearer " + user.token
                },
                body: localStorage.getItem('OmdbApiMedia')
            })
            .then(res => {
                //console.log(res)
                if (res.ok) {
                    search_for_media_at_omdb_input.value = '';
                    omdb_result_media_container.innerHTML = '';
                    localStorage.removeItem('OmdbApiMedia');
                    message(`Media added to the library successfully`);
                }
                else message('Klaida: ' + res.status + ' ' + res.statusText);
            })
            .catch((err) => message(`Klaida: ${err}`));
    }
}


add_media_from_omdb_button.addEventListener('click', addMediaFromOmdb);













