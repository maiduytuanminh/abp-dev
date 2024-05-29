(function () {

    const stateKey = 'authentication-state-id';

    window.addEventListener('load', function () {
        if (!ss || !ss.currentUser) {
            return;
        }

        if (!ss.currentUser.isAuthenticated) {
            localStorage.removeItem(stateKey);
        } else {
            localStorage.setItem(stateKey, ss.currentUser.id);
        }

        window.addEventListener('storage', function (event) {

            if (event.key !== stateKey || event.oldValue === event.newValue) {
                return;
            }

            if (event.oldValue || !event.newValue) {
                window.location.reload();
            } else {
                location.assign('/')
            }
        });
    });

}());