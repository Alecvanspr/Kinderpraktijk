const uri = 'api/Afspraak';
let afspraken = [];
var resultstring = "";

function getAfspraken()
{
    fetch(uri)
        .then(response => response.json())
        .then(data => displayAfspraken(data))
        .catch(error => console.error('Het is niet gelukt om de afspraken op te halen.', error));
}

function addAfspraak() {
    const beschrijvingTextBox = document.getElementById('add-beschrijving');
    const startTijdBox = document.getElementById('add-starttijd');
    const duurIntBox = document.getElementById('add-duur');

    const afspraak = {
        beschrijving: beschrijvingTextBox.value.trim(),
        startTijd: startTijdBox.value,
        Duur: duurIntBox.value
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(afspraak)
    })
        .then(response => response.json())
        .then(() => {
            getAfspraken();
            beschrijvingTextBox.value = '';
        })
        .catch(error => console.error('Het is niet gelukt om een afspraak toe te voegen!', error))
        alert("De afspraak die u probeert te plannen overlapt een andere afspraak!");
}

function deleteAfspraak(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
    .then(() => getAfspraken())
    .catch(error => console.error('Het is niet gelukt om de afspraak te verwijderen!', error));
}


function displayAfspraken(data)
{
    const tBody = document.getElementById('afspraken');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(afspraak => {
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteAfspraak(${afspraak.id})`);

        let tr = tBody.insertRow();

        let td0 = tr.insertCell(0);
        let afspraakNode = document.createTextNode(afspraak.startTijd);
        td0.appendChild(afspraakNode);

        let td1 = tr.insertCell(1);
        let duurNode = document.createTextNode(afspraak.duur);
        td1.appendChild(duurNode);

        let td2 = tr.insertCell(2);
        let textNode = document.createTextNode(afspraak.beschrijving);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(3);
        td3.appendChild(deleteButton);
    })
}