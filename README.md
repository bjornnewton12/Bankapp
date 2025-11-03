# Bankapp

## Overview

This project is a Blazor WebAssembly project that simulates a simple banking environment.  
Users can create, view, and manage bank accounts, perform withdrawals, deposits, and transfers, and review transaction history. Everything is stored locally in the browser using Local Storage.

This project demonstrates:
* Components, forms, and dependency injection.
* Practical banking logic including account management and transaction tracking.
* Data persistence using JSON serialization and 'IJSRuntime' for local storage access.

## How to Use the Project
Once the application is running, you can navigate through the following pages:

* 🏠 Home
  * The landing page.
 
* 🏦 New Account (/accounts)
  * Create a new bank account by specifying:
    * Name.
    * Account Type (Deposit or Savings).
    * Starting Balance.
  * View all created accounts with their type, balance, and last update time.
 
 * 👤 My Accounts
   * Displays all your accounts in a table.
   * Shows balance, interest rate, accumulated interest, and last updated date.
   * Accounts with a zero balance can be removed.
  
 * 💸 Withdraw & Deposit (/withdraw-deposit)
   * Withdraw from or deposit into an existing account.
   * Displays real-time success or error messages.
   * Updates balances immediately after each transaction.
  
 * 🔁 New Transaction (/newTransaction
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
       * Transaction type
       * Balance after transaction
   * All actions (create, withdraw, deposit, transfer) automatically update the local storage and persist even after reloading the browser.

## Credits

Developed by: bjornnewton12
- Technologies Used:
- Blazor WebAssembly (.NET 8)
- C#
- JavaScript Interop (IJSRuntime)
- Local Storage API
- HTML & CSS
