// Fetch user data from the backend API
fetch('/api/users')
    .then(response => response.json())  // Convert response to JSON
    .then(users => {
        // Get the table body where rows will be appended
        const userTableBody = document.getElementById('userTableBody');
        
        // Iterate over each user received from the backend
        users.forEach(user => {
            // Construct table rows for each user
            const row = `
                <tr>
                    <td><input type="checkbox" class="user-checkbox" value="${user.id}"></td>
                    <td>${user.userName}</td>
                    <td>${user.email}</td>
                    <td>${user.lastLoginTime}</td>
                    <td>${user.isBlocked ? 'Blocked' : 'Active'}</td>
                </tr>
            `;
            // Append the row to the table body
            userTableBody.innerHTML += row;
        });
    })
    .catch(error => {
        console.error('Error fetching users:', error);  // Handle any errors
    });
