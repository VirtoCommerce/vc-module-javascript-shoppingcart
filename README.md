# VirtoCommerce.JavascriptShoppingCart

VirtoCommerce.JavascriptShoppingCart module represents checkout which can be used on any site.

Selling online is by no means an easy task. We are always thinking how we can help you to sell more. Your Storefront or Mobile application are only ones of your possible sales channels. Right now VirtoCommerce makes it easy to add ecommerce functionality to share and sell your products on any website.

![vcsalechannels](https://cloud.githubusercontent.com/assets/16013311/18511407/d76a3c14-7a83-11e6-9c07-c09c1c7f0289.PNG)

You have designed an awesome blog with Wordpress or other platform, and now you can start making money by adding easy-to-use VirtoCommerce JavaScriptShoppingCart. It works by offering a small piece of code, which can be implemented on your site to turn any element into a Buy button.

# Installation

1. Installing the module:
  * Automatically: in VC Manager go to Configuration -> Modules -> JavaScriptShoppingCart management module -> Install
  * Manually: download module zip package from https://github.com/VirtoCommerce/vc-module-javascript-shoppingcart/releases. In VC Manager go to Configuration -> Modules -> Advanced -> upload module package -> Install.

2. Setting up security

  In VC Manager go to Configuration -> Security -> Roles -> Create Security Role. This role allows access to Cart module web api.

  ![createjavascriptshoppingcartsecurityrole](https://cloud.githubusercontent.com/assets/16013311/18511921/d28b12ec-7a86-11e6-88d2-a046067792f4.png)

  Then in VC Manager go to Configuration -> Security -> Users and create a user who has JavaScriptShoppingCart security role created in the previous step.

  ![createjavascriptshoppingcartsecurityuser](https://cloud.githubusercontent.com/assets/16013311/18511944/eab3641e-7a86-11e6-85fe-8999c6d99b9e.png)

  When you have created the user you should open API Key widget and create Simple Api Key.

  ![createjavascriptshoppingcartsecuritysimplekey](https://cloud.githubusercontent.com/assets/16013311/18511964/0137502e-7a87-11e6-8eca-868cf0afae64.png)

3. Including JS/CSS files

  Open a page on your site which you want to add ecommerce functionality and include the VirtoCommerce JavaScriptShoppingCart javascript and CSS files before the closing head tag.

  ```html
  <link href="http://demo.virtocommerce.com/admin/styles/vc-shopping-cart" rel="stylesheet">
  <script type="text/javascript" src="http://demo.virtocommerce.com/admin/scripts/vc-shopping-cart"></script>
  ```

  JavaScriptShoppingCart build using AngularJS. You should add ng-app and ng-controller to html and body tags accordingly. Also there is a special component “vc-cart” which handle all operations with shopping cart. You should add all required setting to this component which include: cart name, api-key, store-id, api-url, currency-code, culture.

  ```html
  <html ng-app="virtoCommerce.cartModule">
  ...
  <body ng-controller="virtoCommerce.cartModule.cartController">
  ...
  <vc-cart name="default" api-key="1fdbd73305bb4a3389fceed7feb9bab1" store-id="Clothing" api-url="http://demo.virtocommerce.com/admin/" currency-code="USD" culture="en-us"/>
  ```

4. Defining products

  Now that you have included the files, you simply need to define the products on your website. A product is described using a simple HTML syntax.
  
  ```html
  <button id="buyButton" type="button" class="javascript-shoppingcart-add-item btn" ng-click="carts.default.addLineItem({name: 'Handcrafted lamp', quantity: 1, listPrice: '250.00', currency: 'USD', productId: '1', sku: 'PH1231G2', catalogId: 'Hand-made', imageUrl: 'https://virtocommercedemo1.blob.core.windows.net/catalog/1435269990000_1163371.jpg'}); openCheckout();">Buy the lamp</button>
  ```
  
  In parameters you should provide the required product information: name, quantity, listPrice, currency, productId, sku, catalogId, imageUrl.

# Customization

The best way to customize VirtoCommerce JavaScript Shopping Cart is to fork the repository and start working from it.

After cloning the repository, you will have the following files tree: 

![scriptstructure](https://cloud.githubusercontent.com/assets/16013311/18512465/8f32edfa-7a89-11e6-8cc7-cbe42a16baeb.png)

VirtoCommerce JavaScript Shopping Cart has a component-based application structure. Various components can be adjusted to your needs in Scripts/Checkout folder. In addition, you can adjust Content/checkout.css file that contains all styles.

# Wordpress JavaScript Shopping Cart Setup

Wordpress.org is the most popular CMS. This section aims to demonstrate the simplicity of Virtocommerce JavaScript Shopping Cart that will let you transform your Wordpress.org website into a full-fledged e-commerce platform.

We assume that you already have a WordPress.org site installed and that you know the basics.

First of all, you'll need to add the Virtocommerce JavaScript Shopping Cart JavaScript file and stylesheet to your template.

Add them in the header.php file located in /wp-content/themes/{your_theme}/header.php.

![wordpress](https://cloud.githubusercontent.com/assets/16013311/18549886/857e655a-7b51-11e6-9d5e-f43344d6f9c2.png)

Then you should create a blog post and add buy button.

![worpderss-create-blog-post](https://cloud.githubusercontent.com/assets/16013311/18549914/aaab1eb8-7b51-11e6-9d02-5efb52574a08.png)

Now, if you navigate to this blog post, you should be able to buy it!

You should have something that looks like this:

![wordpress-blog-post](https://cloud.githubusercontent.com/assets/16013311/18549893/938759f4-7b51-11e6-92cd-25a6346beb88.png)

When you click the button you will see a modal which contain standard checkout with all available steps.

![wordpress-checkout](https://cloud.githubusercontent.com/assets/16013311/18549930/c01ecb00-7b51-11e6-8562-f53c0cdbbf02.png)


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
