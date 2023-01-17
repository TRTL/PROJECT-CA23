const user = JSON.parse(localStorage.getItem('USER'));
window.onload = function () {
    if (!user) {
        alert('Jūs nesate prisijungę! Prisijunkite, jei norite tęsti darbą.');
        window.location.href = "login.html";
    } else {
        getUser();
        all_movies_table_body.innerHTML = '';
        getAllUserMedias("Wishlist");
        getAllUserMedias("Watching");
        getAllUserMedias("Finished");
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


const getAllUserMedias = (filter) => {
    fetch('https://localhost:' + user.localhost + '/GetAllUserMedias?UserId=' + user.userId + '&Filter=' + filter,
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
                    .then(usermediadata => {
                        console.log(usermediadata)
                        usermediadata.forEach(media => {
                            all_movies_table_body.innerHTML +=
                                '<tr id="all_users_table_user_media_id_' + media.userMediaId + '">' +
                                '<td><select id="u_m_status_select_' + media.userMediaId + '" disabled="" data-oldvalue="' + media.userMediaStatus + '">' +
                                '<option value="Wishlist" ' + (media.userMediaStatus === 0 ? ' selected' : '') + '>Watchlist</option>' +
                                '<option value="Watching" ' + (media.userMediaStatus === 1 ? ' selected' : '') + '>Watching</option>' +
                                '<option value="Finished" ' + (media.userMediaStatus === 2 ? ' selected' : '') + '>Finished</option></select></td>' +
                                '<td>' + media.type + '</td>' +
                                '<td><a class="table-media-link" href="./media.html?mediaId=' + media.mediaId + '" target="_self">' + media.title + '</a></td>' +
                                '<td>' + media.year + '</td>' +
                                '<td><a class="table-imdb-link" href="https://www.imdb.com/title/' + media.imdbId + '/" target="_blank">IMDB</a></td>' +
                                '<td>⭐' + media.imdbRating + '</td>' +
                                '<td><select id="u_m_user_rating_select_' + media.userMediaId + '" disabled="" data-oldvalue="' + media.userRating + '">' +
                                '<option value="NoRating" ' + (media.userRating === 0 ? ' selected' : '') + '>Not rated</option>' +
                                '<option value="OneStar" ' + (media.userRating === 1 ? ' selected' : '') + '>1 star</option>' +
                                '<option value="TwoStars" ' + (media.userRating === 2 ? ' selected' : '') + '>2 stars</option>' +
                                '<option value="ThreeStars" ' + (media.userRating === 3 ? ' selected' : '') + '>3 stars</option>' +
                                '<option value="FourStars" ' + (media.userRating === 4 ? ' selected' : '') + '>4 stars</option>' +
                                '<option value="FiveStars" ' + (media.userRating === 5 ? ' selected' : '') + '>5 stars</option></select></td>' +
                                '<td><input type="text" id="u_m_reviewtext_textfield_' + media.userMediaId + '" value="' + (media.reviewText ?? '') + '" disabled="" data-oldvalue="' + media.reviewText + '"/></td>' +
                                '<td><input type="text" id="u_m_note_textfield_' + media.userMediaId + '" value="' + (media.note ?? '') + '" disabled="" data-oldvalue="' + media.note + '"/></td>' +
                                '<td class="actions">' +
                                '<div id="u_m_edit_' + media.userMediaId + '" onclick="editUserMediaReview(' + media.userMediaId + ')" title="Edit">✏️</div>' +
                                '<div id="u_m_delete_' + media.userMediaId + '" onclick="deleteUserMediaReview(' + media.userMediaId + ')" title="Delete">️🗑️</div>' +
                                '<div id="u_m_confirm_' + media.userMediaId + '" onclick="updateUserMediaReview(' + media.userMediaId + ')" title="Confirm" style="display:none;">✔️</div>' +
                                '<div id="u_m_cancel_' + media.userMediaId + '" onclick="cancelUserMediaReview(' + media.userMediaId + ')" title="Cancel" style="display:none;">❌</div>' +

                                '</td>' +
                                '</tr>';
                        });
                    })
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


////////////////////////////////////////// Edit Cancel Delete Confirm  //////////////////////////////////////////

const editUserMediaReview = (id) => {
    // status select
    document.getElementById('u_m_status_select_' + id).disabled = false;
    // my rating select
    document.getElementById('u_m_user_rating_select_' + id).disabled = false;
    // reviewtext textfield
    document.getElementById('u_m_reviewtext_textfield_' + id).disabled = false;
    // note textfield
    document.getElementById('u_m_note_textfield_' + id).disabled = false;
    // action buttons
    document.getElementById('u_m_edit_' + id).style.display = 'none';
    document.getElementById('u_m_delete_' + id).style.display = 'none';
    document.getElementById('u_m_confirm_' + id).style.display = 'inline-block';
    document.getElementById('u_m_cancel_' + id).style.display = 'inline-block';
}

const cancelUserMediaReview = (id) => {
    // status select
    document.getElementById('u_m_status_select_' + id).selectedIndex = document.getElementById('u_m_status_select_' + id).getAttribute('data-oldvalue');
    document.getElementById('u_m_status_select_' + id).disabled = true;
    // my rating select
    document.getElementById('u_m_user_rating_select_' + id).selectedIndex = document.getElementById('u_m_user_rating_select_' + id).getAttribute('data-oldvalue');
    document.getElementById('u_m_user_rating_select_' + id).disabled = true;
    // reviewtext textfield
    //alert('reviewtext = ' + document.getElementById('u_m_reviewtext_textfield_' + id).getAttribute('data-oldvalue'))
    document.getElementById('u_m_reviewtext_textfield_' + id).value = document.getElementById('u_m_reviewtext_textfield_' + id).getAttribute('data-oldvalue');
    document.getElementById('u_m_reviewtext_textfield_' + id).disabled = true;
    // note textfield
    document.getElementById('u_m_note_textfield_' + id).value = document.getElementById('u_m_note_textfield_' + id).getAttribute('data-oldvalue');
    document.getElementById('u_m_note_textfield_' + id).disabled = true;
    // action buttons
    document.getElementById('u_m_edit_' + id).style.display = 'inline-block';
    document.getElementById('u_m_delete_' + id).style.display = 'inline-block';
    document.getElementById('u_m_confirm_' + id).style.display = 'none';
    document.getElementById('u_m_cancel_' + id).style.display = 'none';
}

const deleteUserMediaReview = (id) => {
    if (confirm("Do you really want to delete this record?") == true) {
        confirmDeleteUserMediaReview(id);
    }
};

const confirmDeleteUserMediaReview = (id) => {
    fetch('https://localhost:' + user.localhost + '/UserMedia/' + id + '/Delete',
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
                message(`UserMedia (ID:${id}) deleted successfully`);
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

const updateUserMediaReview = (id) => {
    let newObject_UpdateUserMediaReview = {};
    newObject_UpdateUserMediaReview['userMediaId'] = id;
    newObject_UpdateUserMediaReview['userMediaStatus'] = document.getElementById('u_m_status_select_' + id).value;
    newObject_UpdateUserMediaReview['userRating'] = document.getElementById('u_m_user_rating_select_' + id).value;
    newObject_UpdateUserMediaReview['reviewText'] = document.getElementById('u_m_reviewtext_textfield_' + id).value;
    newObject_UpdateUserMediaReview['note'] = document.getElementById('u_m_note_textfield_' + id).value;

    fetch('https://localhost:' + user.localhost + '/UpdateUserMediaAndReview',
        {
            method: 'put',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': "Bearer " + user.token
            },
            body: JSON.stringify(newObject_UpdateUserMediaReview)
        })
        .then(res => {
            console.log(res)
            if (res.ok) {
                message('User media updated successfully')
                document.getElementById('u_m_status_select_' + id).setAttribute('data-oldvalue', document.getElementById('u_m_status_select_' + id).selectedIndex);
                document.getElementById('u_m_user_rating_select_' + id).setAttribute('data-oldvalue', document.getElementById('u_m_user_rating_select_' + id).selectedIndex);
                document.getElementById('u_m_reviewtext_textfield_' + id).setAttribute('data-oldvalue', document.getElementById('u_m_reviewtext_textfield_' + id).value);
                document.getElementById('u_m_note_textfield_' + id).setAttribute('data-oldvalue', document.getElementById('u_m_note_textfield_' + id).value);
                cancelUserMediaReview(id);
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





