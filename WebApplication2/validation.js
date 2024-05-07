function validateSignupForm() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    return validateUsername(username) && validatePassword(password);
}
function validateUpdateInfoForm() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var isValid = true;

    if (username.length != 0) {
        isValid = validateUsername(username);
    }
    if (password.length != 0) {
        isValid = isValid && validatePassword(password);
    }

    return isValid;
}

function validateUsername(username) {
    // Checks username length
    if (username.length < 3 || username.length > 20) {
       alert("Username must be between 3 and 20 characters long!");
        return false;
    }

    // Checks if username contains illegal characters
    if (!/^[a-zA-Z0-9_]+$/.test(username)) {
        alert("Username can only contain letters, numbers, and underscores!");
        return false;
    }

    return true;
}

function validatePassword(password) {
    // Checks password length
    if (password.length < 8 || password.length > 50) {
        alert("Password must be between 8 and 50 characters long!");
        return false;
    }

    // Checks for an uppercase letter
    if (!/[A-Z]/.test(password)) {
        alert("Password must contain at least one uppercase letter!");
        return false;
    }

    // Checks for a lowercase letter
    if (!/[a-z]/.test(password)) {
        alert("Password must contain at least one lowercase letter!");
        return false;
    }

    // Checks for a number
    if (!/[0-9]/.test(password)) {
        alert("Password must contain at least one number!");
        return false;
    }

    // Checks for a special character
    if (!/[^a-zA-Z0-9]/.test(password)) {
        alert("Password must contain at least one special character!");
        return false;
    }

    return true;
}
