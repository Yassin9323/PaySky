# Banking System

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [API Documentation](#api-documentation)
  - [Endpoints](#endpoints)
- [Database Schema](#database-schema)
- [License](#license)

---

## Overview

The Banking System project is a RESTful API built with **ASP.NET Core**, designed to manage accounts, transactions, and balances efficiently. It includes features for creating accounts, depositing, withdrawing, and transferring funds. This project serves as an example of modern software development best practices, including the use of **AutoMapper**, **Dependency Injection**, and **EF Core** for database management.

---

## Features

- Account management:
  - Create accounts with unique 9-digit account numbers.
  - Fetch all accounts and their details.
  - Retrieve the balance of a specific account.
- Transaction management:
  - Deposit funds into an account in multiple currencies.
  - Withdraw funds with validation for account type, available balance, and currency.
  - Transfer funds between accounts, handling currency conversion where applicable.
  - Storing each transaction in the database.
- Interest calculation over time, based on the balance.
- Validation for account type and available balance.
- Support for multiple currencies (USD, GBP, EUR, CHF, JPY, AED, SAR, JOD, CAD), with automatic currency conversion.
- Automatic database migrations.
- Comprehensive API documentation.

---

## Technologies Used

- **ASP.NET Core 8.0**
- **Entity Framework Core 8.0**
- **SQLite** for data storage
- **AutoMapper** for object-to-object mapping
- **Dependency Injection** for modular and testable architecture
- **Swagger** for API documentation
- **C# 11**

---

## Setup and Installation

### Prerequisites

1. **.NET SDK** 8.0 installed on your system.
2. **SQLite** for database management (pre-installed in most environments).

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/Yassin9323/PaySky.git
   cd BankingSystem
   ```

2. Install dependencies:

   ```bash
   dotnet restore
   ```

3. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the project:

   ```bash
   dotnet run
   ```

5. Access the API documentation at:

   [http://localhost:5122/swagger/index.html](http://localhost:5122/swagger/index.html)

---

## API Documentation

## You can use it from [http://localhost:5122/swagger/index.html](http://localhost:5122/swagger/index.html)

### Base URL

[http://localhost:5122/api/accounts](http://localhost:5122/api/accounts)

### Endpoints

#### **1. Get All Accounts**

**GET** `/`

- **Response**:
  ```json
  {
    "number_Of_Accounts": 2,
   "data": [
        {
            "accountNumber": "923141323",
            "accountType": "CheckingAccount",
            "_Balance": "$5,500.00"
        },
        {
            "accountNumber": "769241323",
            "accountType": "SavingsAccount",
            "_Balance": "$3,899.32"
        },
  }
  ```

#### **2. Create a New Account**

**POST** `/`

- **Request Body**:
  ```json
  {
    "AccountType": "SavingsAccount"
  }
  ```
- **Response**:
  ```json
  {
    "result": "Successful Operation",
    "data": {
        "accountNumber": "378506558",
        "accountType": "SavingsAccount",
        "_Balance": "$0.00"
    }
  }
  ```

#### **3. Get Account Balance**

**GET** `/{id}/balance`

- **Response**:
  ```json
  {
    "result": "Successful Operation",
    "data": {
        "accountNumber": "231682773",
        "accountType": "SavingsAccount",
        "_Balance": "$2,736.00"
    }
  }
  ```

#### **4. Deposit Funds**

**POST** `/deposit`

- **Request Body**:
  ```json
  {
  	"AccountNumber": "231682773",
      "Balance": 1000.00,
      "Currency": "GBP"
  }
  ```
- **Response**:
  ```json
  {
    "result": "Successful Operation",
    "data": {
        "transactionType": "Deposit",
        "_Amount": "£1,000.00",
        "currency": "GBP",
        "transactionTime": "12/29/2024 9:41:03 PM"
    }
  }
  ```

#### **5. Withdraw Funds**

**POST** `/withdrawal`

- **Request Body**:
  ```json
   {
  	"AccountNumber": "231682773",
      "Balance": 100,
      "Currency": "GBP"
  }
  ```
- **Response**:
  ```json
   {
      "result": "Successful Operation",
      "data": {
          "transactionType": "Withdrawal",
          "_Amount": "£100.00",
          "currency": "GBP",
          "transactionTime": "12/29/2024 9:43:48 PM"
      }
  }
  ```

#### **6. Transfer Funds**

**POST** `/transfer`

- **Request Body**:
  ```json
  {
	"senderAccount": "231682773",
    "receiverAccount": "378506558",
    "Balance": 300.000,
    "Currency": "GBP"
  }
  ```
- **Response**:
  ```json
  {
    "result": "Successful Operation",
    "data": {
        "transactionType": "Transfer",
        "_Amount": "£300.00",
        "currency": "GBP",
        "transactionTime": "12/29/2024 9:54:16 PM"
    }
  }
  ```

---

## Database Schema

### Tables

1. **Accounts**:

   - `Id`: Primary Key
   - `AccountNumber`: Unique 9-digit number
   - `AccountType`: Type of account (e.g., Savings, Checking)
   - `Balance`: Current balance
   - `Currency`: Default currency for the account
   - `CreatedAt`: Date the account was created
   - `LastInterestCalculated`: Date the last interest calculated was created

2. **Transactions**:

   - `Id`: Primary Key
   - `AccountId`: Foreign Key to `Accounts`
   - `TransactionType`: Deposit, Withdrawal, or Transfer
   - `Amount`: Amount of the transaction
   - `Currency`: Currency used in the transaction
   - `TransactionDate`: Date of the transaction
   - `TransferredTo`: For transfers, the target account number

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.
