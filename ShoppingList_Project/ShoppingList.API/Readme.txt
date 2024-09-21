

                      ****************** Shopping List Project Documentation *******************

1. Overview
   - This document outlines the setup and usage of the Shopping List project using a database-first approach.

2. Prerequisites
   - Visual Studio or VS Code
   - .NET SDK
   - SQL Server

3. Step 1: Configure Database Connection
   - Edit appsettings.Development.json:
     DefaultConnection: "Server=[servername];Integrated Security=true;Initial Catalog=ShoppingList;TrustServerCertificate=true;"

4. Step 2: Run the Project
   - Open the project in Visual Studio or VS Code.
   - Run the application it automatically generate the database and seed data.

5. step 3: Enter the Email and Password in to the login end point and get the JWT token.

6. Step 4: Authenticate User
   - Enter the JWT authentication token in the provided text box and authenticate.

7. Step 5: Share Shopping List
   - Use the /share endpoint with the following parameters:
     {
       "listId": "1",
       "sharedWith": "user email",
       "permission": "read/write"
     }

8. Step 6: View Shared Shopping Lists
   - Use the /shared/{userId} endpoint with userId in string(GUID type) format, get the id from the database user's table.

8. Step 6: Unit Testing
   - Run the four included unit tests.

Conclusion
   - Follow the steps to use the application.
