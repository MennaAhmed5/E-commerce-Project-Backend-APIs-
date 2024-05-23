# E-commerce-Project-Backend-APIs

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
  - [API Endpoints](#api-endpoints)
    - [Product Endpoints](#product-endpoints)
    - [Category Endpoints](#category-endpoints)
    - [Cart Endpoints](#cart-endpoints)
    - [Order Endpoints](#order-endpoints)
    - [User Endpoints](#user-endpoints)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Introduction
This project is an e-commerce application that allows users to manage their shopping cart and place orders. It includes functionalities for adding items to the cart, updating item quantities, creating orders based on cart items, and managing products, categories, and users.

## Features
- User authentication and authorization
- Manage products and categories
- Add items to the cart
- Edit item quantities in the cart
- Remove items from the cart
- Place orders based on cart contents
- View order history

## Technologies Used
- **Backend**: ASP.NET Core
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **API Documentation**: Swagger

## Getting Started

### Prerequisites
- .NET SDK 
- SQL Server

### Installation
1. **Clone the repository**:
    ```sh
    git clone https://github.com/MennaAhmed5/E-commerce-Project-Backend-APIs-.git
    cd your-repo
    ```

2. **Backend Setup**:
    - Navigate to the backend project directory:
        ```sh
        cd BackendProjectDirectory
        ```
    - Restore NuGet packages:
        ```sh
        dotnet restore
        ```
    - Update the `appsettings.json` file with your SQL Server connection string.
    - Apply migrations to set up the database:
        ```sh
        dotnet ef database update
        ```
    - Run the backend server:
        ```sh
        dotnet run
        ```
## Usage

### API Endpoints

#### Product Endpoints
- `GET /api/products`: Retrieve a list of products
- `GET /api/products/{id}`: Retrieve a specific product by ID
- `POST /api/products`: Create a new product
- `PUT /api/products/{id}`: Update a product by ID
- `DELETE /api/products/{id}`: Delete a product by ID

#### Category Endpoints
- `GET /api/categories`: Retrieve a list of categories
- `GET /api/categories/{id}`: Retrieve a specific category by ID
- `POST /api/categories`: Create a new category
- `PUT /api/categories/{id}`: Update a category by ID
- `DELETE /api/categories/{id}`: Delete a category by ID

#### Cart Endpoints
- `GET /api/cart`: Retrieve the user's cart
- `POST /api/cart`: Add an item to the cart
- `PUT /api/cart`: Edit an item in the cart
- `DELETE /api/cart/{itemId}`: Remove an item from the cart

#### Order Endpoints
- `GET /api/orders`: Retrieve user orders
- `POST /api/orders`: Place an order

#### User Endpoints
- `POST /api/users/Register`: Register a new user
- `POST /api/users/Login`: Authenticate a user and retrieve a token

## API Documentation
The API documentation is available via Swagger. Once the backend server is running, navigate to `http://localhost:7002/swagger` (adjust the URL according to your server configuration).

## Contributing
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a pull request.

## Contact
- **Your Name** - [mennaahmed1566924@gmail.com](mailto:mennaahmed1566924@gmail.com)
- **GitHub**: [https://github.com/MennaAhmed5](https://github.com/MennaAhmed5)


- 
