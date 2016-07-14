var Thanks = require('../thanks/thanks.pageObject.js');
var Billing = function () {
    this.thanks = new Thanks();
    this.useAddress = element(by.id('BillingAddressEqualsShipping'));
    this.nextButton = element(by.id('nextButton'));
}
module.exports = Billing;