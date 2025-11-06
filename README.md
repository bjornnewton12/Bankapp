# Bankapp

## Project description
This project is a Blazor WebAssembly project that simulates a simple banking environment.  
Users can:
* Create, view, and manage bank accounts.
* Perform withdrawals, deposits, and transfers.
* Review transaction history.
* Everything is stored locally in the browser using Local Storage.

## This project demonstrates:
### Dependency injection
Implemented across the app to provide services, for example AccountService, AuthenticationService and StorageService. This increases maintainability by avoiding hard-coded dependecies.
### Local Storage via IJSRuntime
Used for data persistence so that user's accounts and transactions remain intact after reloading the page. By accessing it through IJSRuntime it enables a safe interaction between C# and Javascript without me actually writing any Javascript.
### JSON Serialization
Used for storing and retrieving data in a readable way. It also simulates real-world data exchange, making this concept app somewhat mirror that of real a banking API.
### Component-based Architecture
BK Bank is structured into reusable Razor components and services. Each pages is responsbile for its own logic and interactions, while still relying on shared services e.g. data storage and business operations.
### Simple Authentication
A simple username and pin requirement demonstrates an authentication flow, without the need of a database or external API. This keeps the focus on understanding authentication management.
### UI design
BK Bank's interface uses simple HTML and CSS styling, making the logic and interactivity the main focus rather than a complex design system.

## How to Use the Project
### Use the following credentials to log in:
* Username: arber
* PIN: 27237

### App Navigation
* 🏠 Home
  * The landing page introducing BK Bank.

* 🏦 New Account (/accounts)
  * Create a new bank account by specifying:
  * Name
  * Account Type (Deposit or Savings)
  * Starting Balance
  * View all created accounts with their type, balance, and last update time.

* 👤 My Accounts (/myAccounts)
  * Displays all your accounts in a table.
  * Shows balance, interest rate, accumulated interest, and last updated date.
  * Accounts with a zero balance can be removed.

* 💸 Withdraw & Deposit (/withdraw-deposit)
  * Withdraw funds from or deposit funds into an existing account.
  * Displays real-time success or error messages.
  * Updates balances immediately after each transaction.

* 🔁 New Transaction (/newTransaction)
  * Transfer funds between two existing accounts.
  * Prevents invalid transactions (same account or empty selection).
  * Displays confirmation messages after successful transfers.

* 📜 History (/history)
  * View all transactions for a selected account.
    * Filter by:
    * Date range
    * Transaction type
  * Sort by:
    * Date
    * Amount
    * Transaction Type
    * Balance After Transaction
  * All actions (create, withdraw, deposit, transfer) automatically update the local storage and persist even after reloading the browser.

* 🛑Logout

## Credits
* Developed by: bjornnewton12
* Technologies Used:
* Blazor WebAssembly (.NET 8)
* C#
* JavaScript Interop (IJSRuntime)
* Local Storage API
* HTML & CSS