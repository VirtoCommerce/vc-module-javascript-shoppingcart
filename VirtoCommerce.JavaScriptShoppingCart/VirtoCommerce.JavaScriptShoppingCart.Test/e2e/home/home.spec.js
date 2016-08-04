var HomePage = require('./home.pageObject.js');

var self = this;
self.homePage = new HomePage();

describe('Buy button', function () {
    beforeAll(function () {
        browser.get(browser.baseUrl);
    });

    it('should buy lamp', function () {
        self.homePage.clickBuyButton();

        var cartPage = self.homePage.shoppingCart;
        cartPage.nextButton.click();

        var address = cartPage.userAddress;
        address.fillAddressForm(browser.params.address);
        address.nextButton.click();

        var shippment = address.shippment;
        shippment.setShippmentByIndex(0);
        shippment.nextButton.click();

        var billing = shippment.billing;
        //billing.useAddress.click();
        billing.nextButton.click();

        var thanks = billing.thanks;
        expect(thanks.orderNumber.isPresent()).toBeTruthy();
        expect(thanks.orderNumber.getText()).not.toBe('');
    })
})