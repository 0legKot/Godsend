var IdentityUser = /** @class */ (function () {
    function IdentityUser(id, name, email, birth, registrationDate, rating, token) {
        if (id === void 0) { id = ''; }
        if (name === void 0) { name = ''; }
        if (email === void 0) { email = ''; }
        if (birth === void 0) { birth = ''; }
        if (registrationDate === void 0) { registrationDate = ''; }
        if (rating === void 0) { rating = 0; }
        if (token === void 0) { token = ''; }
        this.id = id;
        this.name = name;
        this.email = email;
        this.birth = birth;
        this.registrationDate = registrationDate;
        this.rating = rating;
        this.token = token;
    }
    return IdentityUser;
}());
export { IdentityUser };
//# sourceMappingURL=user.model.js.map