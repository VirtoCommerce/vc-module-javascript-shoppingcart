var ShoppingCart = require('../shoppingCart/shoppingCart.pageObject.js');
var HomePage = function () {
    this.shoppingCart = new ShoppingCart();

    this.buyButton = element(by.id('buyButton'))
}
HomePage.prototype.clickBuyButton = function () {
    var button = this.buyButton;
    button.isPresent().then(function(present) {
        if (present)
            button.click();
    });
}
module.exports = HomePage;