var UserAddress = require('../userAddress/userAddress.pageObject.js');
var ShoppingCart = function () {
    this.userAddress = new UserAddress();

    this.nextButton = element(by.id('nextButton'))
}
module.exports = ShoppingCart;