document.getElementById('loginForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const messageDiv = document.getElementById('message');

    try {
        const response = await fetch('http://captiveportal.cu:8000/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password })
        });

        if (!response.ok) {
            const error = await response.json();
            messageDiv.style.display = 'block';
            messageDiv.style.color= 'red'
            messageDiv.textContent = error.message || 'Login failed';
        }
        else{
            messageDiv.textContent = 'Logged in!!';
            messageDiv.style.color= 'green'
            messageDiv.style.display = 'block';
        }
        
    } catch (error) {
        messageDiv.textContent = `Some error has ocurred`;
        messageDiv.style.display = 'block';
    }
});

document.getElementById('logoutBtn').addEventListener('click', async function() {
    const messageDiv = document.getElementById('message');
    try {
        const response = await fetch('http://captiveportal.cu:8000/logout', {
            method: 'POST',
            credentials: 'include'
        });
        if (response.ok) {
            messageDiv.textContent = 'Logged out!';
            messageDiv.style.color = 'green';
        } else {
            messageDiv.textContent = 'Logout failed';
            messageDiv.style.color = 'red';
        }
    } catch (error) {
        messageDiv.textContent = 'Error during logout';
        messageDiv.style.color = 'red';
    }
    messageDiv.style.display = 'block';
});
