# VirtoCommerce.JavascriptShoppingCart

VirtoCommerce.JavascriptShoppingCart module represents checkout which can be used on any site.

Selling online is by no means an easy task. We are always thinking how we can help you to sell more. Your Storefront or Mobile application are only ones of your possible sales channels. Right now VirtoCommerce makes it easy to add ecommerce functionality to share and sell your products on any website.

![vcsalechannels](/docs/media/vcsaleschannels.png "Sales Channels")

You have designed an awesome blog with Wordpress or other platform, and now you can start making money by adding easy-to-use VirtoCommerce JavaScriptShoppingCart. It works by offering a small piece of code, which can be implemented on your site to turn any element into a Buy button.

# Installation

1. Installing the module:
  * Automatically: in VC Manager go to Configuration -> Modules -> JavaScriptShoppingCart management module -> Install
  * Manually: download module zip package from https://github.com/VirtoCommerce/vc-module-javascript-shoppingcart/releases. In VC Manager go to Configuration -> Modules -> Advanced -> upload module package -> Install.

2. Setting up crawling

  In VC Manager go to Settings -> JavaScriptShoppingCart -> General. Here you should setup whitelist of hosts that can be crawled.

  ![whitelistplatform](/docs/media/whitelistplatform.png "Setting whitelist in platform")

  Then in [Web.Config](https://github.com/VirtoCommerce/vc-platform/blob/master/VirtoCommerce.Platform.Web/Web.config#L60) set the same values as in whitelist in platform.
  
  We are using [referer header analysis](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referer) in our backend to determine from which page the request has been made. You need to make sure that the protocol security level is the same for the platform and your site.

3. Including JS/CSS files

  Open a page on your site which you want to add ecommerce functionality and include the VirtoCommerce JavaScriptShoppingCart javascript and CSS files before the closing head tag.

  ```html
  <link href="http://demo.virtocommerce.com/admin/styles/vc-shopping-cart" rel="stylesheet">
  <script type="text/javascript" src="http://demo.virtocommerce.com/admin/scripts/vc-shopping-cart"></script>
  ```

  JavaScriptShoppingCart build using AngularJS. You should add ng-app and ng-controller to html and body tags accordingly. Also there is a special component “vc-cart” which handle all operations with shopping cart. You should add all required setting to this component which include: cart name, store-id, api-url, currency-code, culture.

  ```html
  <html ng-app="virtoCommerce.cartModule">
  ...
  <body ng-controller="virtoCommerce.cartModule.cartController">
  ...
  <vc-cart name="default" store-id="Clothing" api-url="http://demo.virtocommerce.com/admin/" currency-code="USD" culture="en-us"/>
  ```

4. Defining products

  Now that you have included the files, you simply need to define the products on your website. A product is described using a simple HTML syntax. You should add vc-buy-button directive for every product element.

  ```html
  <button type="button" vc-buy-button
                vc-item-name="Neon Coral Tropical Floral Print Bandeau Pleated Dress"
                vc-image-url="https://demovc1.blob.core.windows.net/catalog/355792584/355792584_.jpg"
                vc-catalog-id="b61aa9d1d0024bc4be12d79bf5786e9f"
                vc-sku="344394719"
                vc-quantity="1"
                vc-list-price="25.00"
                vc-currency="USD"
                vc-product-id="31bb21bf0b144b68a9d731374c1a95e8"
                class="javascript-shoppingcart-add-item btn">
            Buy the lamp
        </button>
  ```

  In attributes you should provide the required product information: name, quantity, listPrice, currency, productId, sku, catalogId, imageUrl.

# Customization

The best way to customize VirtoCommerce JavaScript Shopping Cart is to fork the repository and start working from it.

After cloning the repository, you will have the following files tree:

![scriptstructure](/docs/media/scriptstructure.png)

VirtoCommerce JavaScript Shopping Cart has a component-based application structure. Various components can be adjusted to your needs in Scripts/Checkout folder. In addition, you can adjust Content/checkout.css file that contains all styles.

# Wordpress JavaScript Shopping Cart Setup

Wordpress.org is the most popular CMS. This section aims to demonstrate the simplicity of Virtocommerce JavaScript Shopping Cart that will let you transform your Wordpress.org website into a full-fledged e-commerce platform.

We assume that you already have a WordPress.org site installed and that you know the basics.

First of all, you'll need to add the Virtocommerce JavaScript Shopping Cart JavaScript file and stylesheet to your template.

Add them in the header.php file located in /wp-content/themes/{your_theme}/header.php.

![wordpress](/docs/media/wordpress.png)

Then you should create a blog post and add buy button.

![worpderss-create-blog-post](/docs/media/worpderss-create-blog-post.png)

Now, if you navigate to this blog post, you should be able to buy it!

You should have something that looks like this:

![wordpress-blog-post](/docs/media/wordpress-blog-post.png)

When you click the button you will see a modal which contain standard checkout with all available steps.

![wordpress-checkout](/docs/media/wordpress-checkout.png)


# License
Copyright (c) Virto Solutions LTD.  All rights reserved.

Licensed under the Virto Commerce Open Software License (the "License"); you
may not use this file except in compliance with the License. You may
obtain a copy of the License at

http://virtocommerce.com/opensourcelicense

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied.
