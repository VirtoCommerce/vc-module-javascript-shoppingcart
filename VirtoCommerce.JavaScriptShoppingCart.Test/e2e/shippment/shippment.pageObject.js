require('../helpers/options.helpers.js');

var Billing = require('../billing/billing.pageObject.js');

var Shippment = function () {
    this.billing = new Billing();
    this.shippmentOptions = element.all(by.repeater('method in value'));
    this.nextButton = element(by.id('nextButton'));
}
Shippment.prototype.setShippmentByIndex = function(index) {
    this.shippmentOptions.get(index).click();
}
module.exports = Shippment;