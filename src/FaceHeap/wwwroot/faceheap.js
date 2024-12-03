window.loading = false;

window.loadDeveloper = async function(developer) {
    const response = await fetch(`/developers/${developer}/popularity`);

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    const popularity = await response.text();

    document.getElementById(developer).textContent = popularity;
    document.getElementById(`${developer}-head`).style.width = popularity + '%';
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
    
    setTimeout(window.loadAllDevelopers, 500);
}

window.voteDeveloper = async function(developer, vote) {
    const response = await fetch(`/developers/${developer}/popularity`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ vote: vote })
    });

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    await window.loadDeveloper(developer);
}

_ = window.loadAllDevelopers();