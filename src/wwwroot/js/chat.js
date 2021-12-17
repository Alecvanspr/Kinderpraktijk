//deze wordt enkel gebruikt door de index pagina    
    
    //DIt is voor het aanmaken van een connectie met de ChatHub
    //De naam van de Chathub is case sensitive
    const connection = new signalR.HubConnectionBuilder()
                                    .withUrl("https://localhost:5001/chathub")
                                    .configureLogging(signalR.LogLevel.Information)
                                    .build();

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };
    //Hieronder staat code die mij wat te specifiek leek voor de website
    //hieronder wordt een list aangepast
    connection.on("ReceiveMessage", (user, message) => {
        const encodedMsg = `${user} says ${message}`;
        const li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });
    
    //Hier wordt ervoor gezorgd dat wanneer je op de knop sendButton klikt dat er een bericht verzend
    document.getElementById("sendButton").addEventListener("click", event => {
        const user = document.getElementById("userInput").value; //Hier wordt de user value gepakt
        const message = document.getElementById("messageInput").value; //Hier wordt het bericht gepakt
        connection.invoke("SendMessage", user, message).catch(err => console.error(err));
        event.preventDefault();
    });

    connection.onclose(async () => {
        await start();
    });

    // Start the connection.
    start();


