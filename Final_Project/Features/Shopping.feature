﻿Feature: Shopping

To be able to add an item to your basket and apply a coupon to checkout at the end.

Background:
	Given When I am in the login page
	And I provide my login details
		| Username      | Password          |
		| test@mail.com | HelloPassword123; |

@tag1
Scenario: Add an item to the basket and apply a coupon
	When I add an item called 'item' into my basket
	And apply a coupon 'edgewords'
	Then it should apply a discount of "15"% off from the subtotal

@tag1
Scenario: Add an item to the basket and go through the checkout
	When I add an item called 'item' into my basket
	And place an order with vaild billing details
		| First Name | Last Name | Street Address | Town  | Postcode | Phone     |
		| AJ         | Lee       | 19 Dust Road   | Fleet | PA21 2BE | 077718291 |
	Then a order number would appear in the users account

#ctrl k + d

#Thigs to do Screenshot, discount code, console log, conversions, button checkout, clear basket  