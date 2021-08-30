# Documentation

This documentation is very simplistic, just describing the business flow implemented in this sample 

# Diagrams

**[Event Storming](https://github.com/ggimenes/sample-dotnet/tree/main/doc/diagrams/event-storming)**

## Requirements
Payment flow

Customer
 - see products listing with prices
 - select products and quantities and put then into the cart
 - cart checkout
 - inform payment data
 - confirm payment
 - check stock of products beign decreased

Obs: 
 - System should reserve stock when a product is selected
 - System should validate payment data on anti-fraud system
 - System should descrease stock when payment has been approved
 - System should create a new invoice when payment has been approved
 - System should start shipping process when payment has been approved
 - the user must receive a feedback on the payment flow in at maximun 1 second
 - the anti-fraud system is a vendor and takes almost 1 second to respond

## Use cases

Customer
1. see products listing
	  1. home
 
2. add products on cart
	  1. see products listing
	  2. click on add button
	  3. system: displays message product added
	  4. system: updates the listing

3. remove products from cart
    1. call use case 2
    2. access cart page
    3. click on remove product button
    4. system: displays message product removed
    5. system: updates the cart listing
  
4. increase / decrease product quantity on cart
    1. call use case 2
    2. access cart page
    3. click on increase / decrease quantity
    4. system: reserves / releases reservation as needed
  
5. advance cart checkout
    1. call use case 2
    2. system: shows payment confirmation page

6. confirm payment
    1. call use case 5
    2. user inform card data
    3. user click on confirm payment button
    4. system: starts payment flow
    5. system: validates basic card information
    6. system: calls anti-fraud system
    7. system: calls payment system to effective the payment	
    8. system: calls stock system to decrease stock number
    9. system: shows success message

7. confirm payment, invalid card
    1. call use case 5
    2. user inform card data
    3. user click on confirm payment button
    4. system: starts payment flow
    5. system: validates basic card information
    6. system: calls anti-fraud system
    7. anti-fraud: retrieves invalid	
    8. system: refound client if payment was done
    9. system: shows invalid card error
