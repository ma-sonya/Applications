//const { error } = require("jquery");

const uri = 'api/Categories';
let categories = [];

function getCategories() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayCategories(data))
        .catch(error => console.error("Unable to get categories.", error));
}

function addCategory() {
    const addNameTextbox = document.getElementById('add-name');
    const addInfoTextbox = document.getElementById('add-description');

    const category = {
        name: addNameTextbox.value.trim(),
        description: addInfoTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
        .then(response => response.json())
        .then(() => {
            getCategories();
            addNameTextbox.value = '';
            addInfoTextbox.value = '';
        })
        .catch(error => console.error('Unable to add category.', error));
    document.getElementById('add-name').value = '';
}

function deleteCategory(id) {
    const category = categories.find(category => category.id === id);
    if (confirm(`Are you sure want to kick ${category.name}?!`)) {
        fetch(`${uri}/${id}`, {
            method: 'DELETE'
        })
            .then(() => getCategories())
            .catch(error => console.error('Unable to delete category.', error));
    }
}

function displayEditForm(id) {
    const category = categories.find(category => category.id == id);

    document.getElementById('edit-id').value = category.id;
    document.getElementById('edit-name').value = category.name;
    document.getElementById('edit-description').value = category.description;
    document.getElementById('editForm').style.display = 'block';
}

function updateCategory() {
    const categoryId = document.getElementById('edit-id').value;
    const category = {
        id: parseInt(categoryId, 10),
        name: document.getElementById('edit-name').value.trim(),
        description: document.getElementById('edit-description').value.trim()
    };

    fetch(`${uri}/${categoryId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
        .then(() => getCategories())
        .catch(error => console.error('Unable to update category.', error));

    //closeInput();
    document.getElementById('edit-id').value = 0;
    document.getElementById('edit-name').value = '';
    document.getElementById('edit-description').value = '';

    return false;
}   

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCategories(data) {
    const tBody = document.getElementById('categories');
    tBody.innerHTML = "";

    const button = document.createElement('button');

    data.forEach(category => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Ред.';
        editButton.setAttribute('onclick', `displayEditForm(${category.id})`);
        editButton.setAttribute('style', 'color: green;');


        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Знищити';
        deleteButton.setAttribute('onclick', `deleteCategory(${category.id})`);
        deleteButton.setAttribute('style', 'color: red;');

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(category.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(category.description);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    categories = data;
}