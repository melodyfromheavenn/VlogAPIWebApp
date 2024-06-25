const uri = 'api/Users';
let users = [];

function getUsers() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayUsers(data))
        .catch(error => console.error('Не вдалося отримати користувачів.', error));
}

function addUser() {
    const addFullNameTextbox = document.getElementById('add-fullname');
    const addCityTextbox = document.getElementById('add-city');
    const addStatusTextbox = document.getElementById('add-status');

    const fullName = addFullNameTextbox.value.trim();
    const city = addCityTextbox.value.trim();
    const status = addStatusTextbox.value.trim();

    if (!fullName || !city || !status) {
        alert('Будь ласка, заповніть всі поля');
        return;
    }

    const user = {
        fullName: fullName,
        city: city,
        status: status,
        posts: [],
        friends: [],
        sentFriendRequests: [],
        receivedFriendRequests: []
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => response.json())
        .then(() => {
            getUsers();
            addFullNameTextbox.value = '';
            addCityTextbox.value = '';
            addStatusTextbox.value = '';
        })
        .catch(error => console.error('Не вдалося додати користувача.', error));
}

function deleteUser(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getUsers())
        .catch(error => console.error('Не вдалося видалити користувача.', error));
}

function displayEditForm(id) {
    const user = users.find(user => user.userId === id);

    document.getElementById('edit-id').value = user.userId;
    document.getElementById('edit-fullname').value = user.fullName;
    document.getElementById('edit-city').value = user.city;
    document.getElementById('edit-status').value = user.status;
    document.getElementById('editUser').style.display = 'block';
}

function updateUser() {
    const userId = document.getElementById('edit-id').value;
    const fullName = document.getElementById('edit-fullname').value.trim();
    const city = document.getElementById('edit-city').value.trim();
    const status = document.getElementById('edit-status').value.trim();

    if (!fullName || !city || !status) {
        alert('Будь ласка, заповніть всі поля');
        return;
    }

    const user = {
        userId: parseInt(userId, 10),
        fullName: fullName,
        city: city,
        status: status,
        posts: [],
        friends: [],
        sentFriendRequests: [],
        receivedFriendRequests: []
    };

    fetch(`${uri}/${userId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(() => getUsers())
        .catch(error => console.error('Не вдалося оновити користувача.', error));

    closeInput();
    return false;
}

function closeInput() {
    document.getElementById('editUser').style.display = 'none';
}

function _displayUsers(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(user => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Редагувати';
        editButton.setAttribute('onclick', `displayEditForm(${user.userId})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Видалити';
        deleteButton.setAttribute('onclick', `deleteUser(${user.userId})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(user.fullName);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeCity = document.createTextNode(user.city);
        td2.appendChild(textNodeCity);

        let td3 = tr.insertCell(2);
        let textNodeStatus = document.createTextNode(user.status);
        td3.appendChild(textNodeStatus);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });

    users = data;
}

document.addEventListener('DOMContentLoaded', (event) => {
    getUsers();
});
