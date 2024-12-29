# Banking System

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [API Documentation](#api-documentation)
  - [Endpoints](#endpoints)
- [Usage Examples](#usage-examples)
- [Database Schema](#database-schema)
- [Contributing](#contributing)
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
  - Deposit funds into an account.
  - Withdraw funds with validation for account type and available balance.
  - Transfer funds between accounts.
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
   git clone <repository-url>
   cd BankingSystem
   ```
````

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

   ```
   http://localhost:<port>/swagger
   ```

---

## API Documentation

### Base URL

```
http://localhost:<port>/api/accounts
```

### Endpoints

#### **1. Get All Accounts**

**GET** `/`

- **Response**:
  ```json
  {
    "NumberOfAccounts": 2,
    "Data": [
      {
        "AccountNumber": "123456789",
        "AccountType": "SavingsAccount",
        "Balance": 1000.5
      },
      {
        "AccountNumber": "987654321",
        "AccountType": "CheckingAccount",
        "Balance": 200.75
      }
    ]
  }
  ```

#### **2. Create a New Account**

**POST** `/`

- **Request Body**:
  ```json
  {
    "AccountNumber": "",
    "AccountType": "SavingsAccount",
    "Balance": 100.5
  }
  ```
- **Response**:
  ```json
  {
    "Result": "Successful Operation",
    "Data": {
      "AccountNumber": "123456789",
      "AccountType": "SavingsAccount",
      "Balance": 100.5
    }
  }
  ```

#### **3. Get Account Balance**

**GET** `/{id}/balance`

- **Response**:
  ```json
  {
    "Result": "Successful Operation",
    "Data": {
      "AccountNumber": "123456789",
      "AccountType": "SavingsAccount",
      "Balance": 1000.5
    }
  }
  ```

#### **4. Deposit Funds**

**POST** `/deposit`

- **Request Body**:
  ```json
  {
    "AccountNumber": "123456789",
    "Balance": 500.0
  }
  ```
- **Response**:
  ```json
  {
    "Result": "Successful Operation",
    "Data": {
      "AccountNumber": "123456789",
      "AccountType": "SavingsAccount",
      "Balance": 1500.5
    }
  }
  ```

#### **5. Withdraw Funds**

**POST** `/withdrawal`

- **Request Body**:
  ```json
  {
    "AccountNumber": "123456789",
    "Balance": 200.0
  }
  ```
- **Response**:
  ```json
  {
    "Result": "Successful Operation",
    "Data": {
      "AccountNumber": "123456789",
      "AccountType": "SavingsAccount",
      "Balance": 800.5
    }
  }
  ```

#### **6. Transfer Funds**

**POST** `/transfer`

- **Request Body**:
  ```json
  {
    "SenderAccount": "123456789",
    "ReceiverAccount": "987654321",
    "Balance": 100.0
  }
  ```
- **Response**:
  ```json
  {
    "Result": "Successful Operation",
    "Data": {
      "SenderAccount": "123456789",
      "ReceiverAccount": "987654321",
      "Balance": 100.0
    }
  }
  ```

---

## Usage Examples

### Using Postman or curl

- **Get All Accounts**:
  ```bash
  curl -X GET http://localhost:<port>/api/accounts
  ```
- **Create Account**:
  ```bash
  curl -X POST http://localhost:<port>/api/accounts \
      -H "Content-Type: application/json" \
      -d '{"AccountType":"SavingsAccount","Balance":100.00}'
  ```

---

## Database Schema

### Tables

1. **Accounts**:

   - `Id`: Primary Key
   - `AccountNumber`: Unique 9-digit number
   - `AccountType`: Type of account (e.g., Savings, Checking)
   - `Balance`: Current balance
   - `CreatedAt`: Date the account was created

2. **Transactions**:

   - `Id`: Primary Key
   - `AccountId`: Foreign Key to `Accounts`
   - `TransactionType`: Deposit, Withdrawal, or Transfer
   - `Amount`: Amount of the transaction
   - `TransactionDate`: Date of the transaction
   - `TransferredTo`: For transfers, the target account number

---

## Contributing

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Submit a pull request with detailed changes.

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.

```

```
