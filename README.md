Here’s a `README.md` file for the `ProductsController` implementation you provided:

```markdown
# Product Management API

This API is part of the Product Management system, providing endpoints for managing products, including retrieving, adding, updating, and deleting products. The API uses role-based authorization to ensure that only authorized users can perform certain actions.

## Prerequisites

- .NET 8.0 SDK
- A configured and running SQL Server database (or an in-memory database for testing)
- Visual Studio 2022 (or later) or any other preferred IDE

## Installation

1. **Clone the Repository**

   Clone the repository to your local machine:

   ```bash
   git clone https://github.com/yourusername/ProductManagementAPI.git
   cd ProductManagementAPI
   ```

2. **Restore Packages**

   Navigate to the project directory and restore the NuGet packages:

   ```bash
   dotnet restore
   ```

3. **Configure the Database**

   Update the connection string in `appsettings.json` with your database details.

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "YourConnectionStringHere"
   }
   ```

4. **Run Migrations**

   Apply the migrations to create the necessary database tables:

   ```bash
   dotnet ef database update
   ```

## API Endpoints

### **ProductsController**

This controller provides endpoints for managing products in the system.

- **GET /products/{id}**
  
  Retrieves the details of a single product by ID.

  **Authorization:** Admin, User

  **Parameters:**
  
  - `id` (query parameter) - The ID of the product.

  **Response:**
  
  Returns the product details with VAT calculated.

  **Example:**
  
  ```bash
  GET /products/1
  ```

- **GET /products**
  
  Retrieves a list of all products.

  **Authorization:** Admin, User

  **Response:**
  
  Returns a list of products with VAT calculated.

  **Example:**
  
  ```bash
  GET /products
  ```

- **POST /products**
  
  Adds a new product to the system.

  **Authorization:** Admin

  **Body:**
  
  - `ProductAddRequest` - The request body containing product details.

  **Response:**
  
  Returns the added product details.

  **Example:**
  
  ```bash
  POST /products
  {
      "name": "Product Name",
      "price": 100,
      "quantity": 10
  }
  ```

- **PUT /products/{id}**
  
  Updates an existing product.

  **Authorization:** Admin

  **Parameters:**
  
  - `id` (query parameter) - The ID of the product to update.

  **Body:**
  
  - `ProductUpdateRequest` - The request body containing updated product details.

  **Response:**
  
  Returns the updated product details.

  **Example:**
  
  ```bash
  PUT /products/1
  {
      "name": "Updated Product Name",
      "price": 120,
      "quantity": 15
  }
  ```

- **DELETE /products/{id}**
  
  Deletes a product by ID.

  **Authorization:** Admin

  **Parameters:**
  
  - `id` (query parameter) - The ID of the product to delete.

  **Response:**
  
  Returns the deleted product details.

  **Example:**
  
  ```bash
  DELETE /products/1
  ```

## Role-Based Authorization

The API uses a custom attribute `AuthorizeRole` to enforce role-based authorization. The roles supported are:

- `Admin` - Full access to all endpoints (CRUD operations).
- `User` - Read-only access to products.

### **Custom Filter Attributes**

The API uses a custom filter attribute, `AuthorizeRole`, to handle role-based access control:

- **`[AuthorizeRole(UserRole.Admin, UserRole.User)]`** - Allows access to both Admin and User roles.
- **`[AuthorizeRole(UserRole.Admin)]`** - Restricts access to Admin role only.

## Services

### **IProductService**

The `IProductService` interface defines methods for product-related operations:

- `Task<ProductVAT> GetProductByIdAsync(long id);`
- `Task<List<ProductVAT>> GetProductsAsync();`
- `Task<Product> AddProductAsync(long userId, ProductAddRequest request);`
- `Task<Product> UpdateProductAsync(long userId, ProductUpdateRequest request);`
- `Task<Product> DeleteProductAsync(long userId, long id);`

### **Models**

- **`ProductAddRequest`** - Model for adding a new product.
- **`ProductUpdateRequest`** - Model for updating an existing product.
- **`ProductVAT`** - Model for product details including VAT.

## Running the Application

Run the application using the following command:

```bash
dotnet run
```

You can now use tools like Postman or Swagger (if configured) to interact with the API.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

```

This `README.md` provides an overview of the API, its endpoints, and how to use it. It also explains the role-based authorization implemented using custom attributes. Make sure to replace placeholders such as `"YourConnectionStringHere"` with actual values specific to your environment.
