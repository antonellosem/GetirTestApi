# Getir Test API
## The Challenge
In this backend development assignment, we would like you to create a simple online banking API app. Gringotts Bank is a bank that has an online branch for wizards to do some account transactions. Gringotts Bank is known for its goblin-made, secure, and consistent accounting structure so account transaction consistency is the first priority for their operations.
## Requirements
1. DotNet 5
2. Relational / Non-Relational Database (Relational choosen)
3. Restful Endpoints
4. Clean Code
5. Brief explanation of your design
## MUST HAVE Requirements
1. New Customer Endpoint (Will persist brief information about customers)
2. New Account Endpoint (Will persist new account information and current balance of the account)
3. New Account Transaction Endpoint for adding and withdrawing money. This action needs to update the balance of the account.
4. List accounts of a customer (Will query all accounts of the customer)
5. View the account details (View all transactions of the customer between a time period)
6. Validations - Please be sure your system is error-proof.
7. Responses - Please define success and error response models and use them
8. Authentication - Please secure endpoints (for ex. bearer token)
## Nice to Have's
1. Logging - Log all changes on entities. (Which user made specific changes and when)
2. Containerize - Please containerize your app
3. Open API Specification - Please use Swagger (authentication not needed)
4. Deployment - You are free to use any cloud provider. We recommend Heroku since it is easy and free.
## Design
The application design follows n-tier architecture model, each module has its own responsibility, and based on this concept the solution projects are defined as:
1. Host Layer: takes care of application hosting and REST endpoints definition
2. Application Layer: implements handlers on command and domain events and the logic of business validation
3. Infratructure Layer: defines repositories and db contexts, including infastructure patterns (in this case Specification)
4. Domain Layer: collects domain entities on the customer root aggregate for this bounded context; defines also domain events
5. Abstraction Layer: defines project abstractions and shared contracts

> The choise to use Domain Driven Design broght me to identify one Root Aggregate on the Customer entity, with multiple Accounts as pure Entity and so Transactions as Value Objects. 
> DDD was used to show how data driven approach can be used to implement business logic, and in the current implementation the balance is updated using a domain event handler.

## Dev Notes
- SqlServer as Relational Database (DB automatically created once connection string operationsContext is set properly)
- Used Swashbuckle to browse the endpoints
- No Authentication implemented
- Very basic implementation of user tracking using User parameter on POST endpoints
- Logging implemented but not initialized
- Fluent Validation used for DTOs validation

## Endpoints and Validation Rules
##### POST /customers
###### Create a new Customer
- user: the user requesting the service (must be not null)
- nationalId: the national identifier used as unique code for the customer (must be not null, must be unique in the Customer table)
- birthdate: the birth date of customer (must be not null, customer must be adult with age +18)
- firstName: the customer first name (must be not null)
- lastName: the customer last name (must be not null)
> Returns the customerId as customer identifier
#### POST /accounts
###### Create a new account for a specific customer
- user: the user requesting the service (must be not null)
- customerId: the customer identifier (must be not null, must be a valid customerId)
- iban: the account number with IBAN (must be not null, must implement a generic regular expression for Multi Country IBAN, egs: FR7630006000011234567890189, RO09BCYP0000001234567890, SA4420000001234567891234, ES7921000813610123456789, CH5604835012345678009, GB98MIDL07009312345678)
> Returns the accountId as account identifier
#### GET /accounts
###### Gets the list of accounts by customer
- customerId: the customer identifier (must be not null, must be a valid customerId)
> Returns the list of accounts with: (1) balance, (2) the IBAN, (3) when it was created and (4) who created it
#### POST /accounts/transactions
##### Create a new transaction for a specific account
- user: the user requesting the service (must be not null)
- accountId: the account identifier (must be not null, must be a valid accountId)
- isWithdrawal: true if it's a withdrawal, false if the transaction adds money to the account (must be not null)
- amount: the amount involved in the transaction as positive value (must be not null, must be in the range ]0, 1M[, the decimal part must have no more than 2 decimal digits)
> Returns the account balance
#### GET /accounts/transactions
###### Gets the list of transactions by account within a time range
- accountId: the account identifier (must be not null, must be a valid accountId)
- from: the start date of time range (must be not null, must be previous the end date)
- to: the end of time range (must be not null, must be subsequent the start date)
> From and To CANNOT be equal
> Returns the list of transactions