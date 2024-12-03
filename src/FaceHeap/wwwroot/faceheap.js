window.loading = false;

window.loadDeveloper = async function(developer) {
    const response = await fetch(`/developers/${developer}/popularity`);

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    const popularity = await response.text();

    document.getElementById(developer).textContent = popularity;
    document.getElementById(`${developer}-head`).style.height = popularity + '%';
}

window.loadAllDevelopers = async function() {
    if (!window.loading && !document.hidden) {
        window.loading = true;

        try {
            await Promise.all([
                window.loadDeveloper('bryden'),
                window.loadDeveloper('luke')
            ]);
        }
        catch (e) {
            console.error(e);

            // Recover and retry
        }
        finally {
            window.loading = false;
        }
    }
}

window.loadAllDevelopersLoop = async function() {
    await window.loadAllDevelopers();
    
    setTimeout(window.loadAllDevelopersLoop, 500);
}

window.voteDeveloper = async function(developer) {
    const response = await fetch(`/developers/${developer}/popularity`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({})
    });

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    await window.loadAllDevelopers();
}

_ = window.loadAllDevelopersLoop();