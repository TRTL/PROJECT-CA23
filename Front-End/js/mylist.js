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
                            '<tr id="all_users_table_user_media_id_' + media.userMediaId + '">' +
                            '<td>' + media.type + '</td>' +
                            '<td>' + media.title + '</td>' +
                            '<td>' + media.year + '</td>' +
                            '<td><a href="https://www.imdb.com/title/' + media.imdbId + '/" target="_blank">IMDB</a></td>' +
                            '<td>⭐' + media.imdbRating + '</td>' +
                            '<td><span id="u_m_status_span_' + media.userMediaId + '">' + media.userMediaStatus + '</span>' +
                            '<select id="u_m_status_select_' + media.userMediaId + '" style="display:none;">' +
                            '<option value="1"' + (media.userMediaStatus === 0 ? ' selected' : '') + '>Want to watch</option>' +
                            '<option value="2"' + (media.userMediaStatus === 1 ? ' selected' : '') + '>Watching</option>' +
                            '<option value="5"' + (media.userMediaStatus === 2 ? ' selected' : '') + '>Finished</option></select></td>' +
                            '<td>' + (media.note ?? '') + '</td>' +
                            '<td>' + (media.userRating ?? '') + '</td>' +
                            '<td>' + (media.reviewText ?? '') + '</td>' +
                            '<td class="table-actions">' +
                            '<div id="u_m_edit_' + media.userMediaId + '" onclick="editUserMediaReview(' + media.userMediaId + ')" title="Edit">✏️</div>' +
                            '<div id="u_m_delete_' + media.userMediaId + '" onclick="deleteUserMediaReview(' + media.userMediaId + ')" title="Delete">️🗑️</div>' +
                            '<div id="u_m_confirm_' + media.userMediaId + '" onclick="updateUserMediaReview(' + media.userMediaId + ')" title="Confirm" style="display:none;">✔️</div>' +
                            '<div id="u_m_cancel_' + media.userMediaId + '" onclick="cancelUserMediaReview(' + media.userMediaId + ')" title="Cancel" style="display:none;">❌</div>' +

                            '</td>' +
                            '</tr>';
                    });
                })
        })
        .catch((err) => message(`Klaida: ${err}`));
}


////////////////////////////////////////// Edit Delete Confirm Cancel //////////////////////////////////////////

const editUserMediaReview = (id) => {
    document.getElementById('u_m_edit_' + id).style.display = 'none';
    document.getElementById('u_m_delete_' + id).style.display = 'none';
    document.getElementById('u_m_confirm_' + id).style.display = 'inline-block';
    document.getElementById('u_m_cancel_' + id).style.display = 'inline-block';
    document.getElementById('u_m_status_span_' + id).style.display = 'none';
    document.getElementById('u_m_status_select_' + id).style.display = 'block';
}

const cancelUserMediaReview = (id) => {
    document.getElementById('u_m_edit_' + id).style.display = 'inline-block';
    document.getElementById('u_m_delete_' + id).style.display = 'inline-block';
    document.getElementById('u_m_confirm_' + id).style.display = 'none';
    document.getElementById('u_m_cancel_' + id).style.display = 'none';
    document.getElementById('u_m_status_span_' + id).style.display = 'block';
    document.getElementById('u_m_status_select_' + id).style.display = 'none';
}

const deleteUserMediaReview = (id) => {
    if (confirm("Do you really want to delete this record?") == true) {
        confirmDeleteUserMediaReview(id);
    }
};

const confirmDeleteUserMediaReview = (id) => {
    fetch('https://localhost:7012/UserMedia/' + id + '/Delete',
        {
            method: 'delete',
            headers: {
                'Authorization': "Bearer " + user.token
            }
        })
        .then(deleteResponse => {
            console.log(deleteResponse)
            if (deleteResponse.ok) {
                document.getElementById('all_users_table_user_media_id_' + id).remove();
                message(`UserMedia (ID:${id}) Deleted successfully`);
            }
            else {
                deleteResponse.text()
                    .then(text => {
                        message(text);
                    })
            }
        })
        .catch((error) => message(`Klaida: ${error}`))
}






