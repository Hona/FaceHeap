window.developerReloadIntervalMs = 500;

window.loadDeveloper = function(developer) {
    const request = new XMLHttpRequest();
    request.open('GET', '/developers/' + developer + '/popularity', true);
    request.onload = function() {
        if (request.status >= 200 && request.status < 400) {
            document.getElementById(developer).textContent = request.responseText;
        }
        else {
            document.getElementById(developer).textContent = 'Error';
        }
    };
    request.send();
}

window.loadAllDevelopers = function() {
    // Only hit the server if the tab is active
    if (!document.hidden) {
        window.loadDeveloper('bryden');
        window.loadDeveloper('luke');
    }
    
    // Call every x interval, even when hidden to keep data fresh
    setTimeout(loadAllDevelopers, window.developerReloadIntervalMs)
}

window.voteDeveloper = function(developer, vote) {
    const request = new XMLHttpRequest();
    request.open('POST', '/developers/' + developer + '/popularity', true);
    request.setRequestHeader('Content-Type', 'application/json');
    request.onload = function() {
        if (request.status >= 200 && request.status < 400) {
            window.loadDeveloper(developer);
        }
        else {
            document.getElementById(developer).textContent = 'Error';
        }
    };
    request.send(JSON.stringify({ vote: vote }));
}

window.loadAllDevelopers();