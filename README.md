# PV179-BookHub



## Overview

BookHub is a comprehensive digital platform designed for the BookHub company, a renowned seller of books across various genres. The application facilitates easy browsing and purchasing of books, allowing customers to sort and filter by authors, publishers, and genres. Built using ASP.NET Core and Entity Framework Core, it provides a robust and efficient system for managing books, authors, publishers, and user ratings.

The website is deployed and published in a Kubernetes cluster. Use this link to evaluate the app without having to setup your Postgres instance and clone this repo: https://bookhub.dyn.cloud.e-infra.cz/
## Features

- Book Management: Enables users to browse, purchase, and view books. Each book has properties like name, price, stock in storage, overall rating, genres, publisher, and authors.
- Author and Publisher Management: Provides administrative privileges to view, add, update, and delete authors and publishers.
- User Account Management: Allows customers to create accounts, review their purchase history, rate books, and create wishlists.
- Rating Management: Facilitates users to rate books and leave comments. The rating value and comments can be updated.

## Use Case Diagram
![Use Case Diagram](Use Case Diagram.png)

## Architecture
The application is built using ASP.NET Core as the framework, which is a cross-platform framework for building modern cloud-based internet connected applications like web apps, IoT apps, and mobile backends. It uses Entity Framework Core as the Object-Relational Mapper (ORM) to handle database operations.

## Components
The application has several components each handling a specific functionality:

- Book Management: This component is responsible for all operations related to books. It allows users to view, add, update, and delete books. Each book has properties like name, price, stock in storage, overall rating, genres, publisher, and authors.
- Author and Publisher Management: This component handles all operations related to authors and publishers. It allows users to view, add, update, and delete authors and publishers.
- Rating Management: This is where users can rate books and leave comments. The rating value and comments can be updated.
- Request Logging: All requests to the application are logged with details like the request method and path. The logs are stored in a text file.

## Data Flow
When a user interacts with the application, the request is first logged by the Request Logging component. Then, depending on the request, it's routed to the appropriate component. For instance, if a user wants to update a book's details, the request is handled by the Book Management component. After processing the request, the updated data is stored in the database using Entity Framework Core.

## Database Schema
![ERD Diagram](Entity Relationship Diagram1.png)

## Setup and Installation

1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Install the required dependencies using the command dotnet restore.
4. Rename `appsettings_template.json` to `appsettings.json`. The template is striped of any secrets, you need to provide your own or ask me for credentials to our DB.
5. Build the project using the command dotnet build.
6. Setup an instance of Postgres database
7. Run the project using the command dotnet run.

## Usage
The application exposes several endpoints for interacting with the books, authors, publishers, and ratings. Here are some examples:
- GET /Books: Fetches a list of all books.
- POST /Books: Adds a new book.
- PUT /UpdateBook/{id}: Updates the details of a book with the given ID.
- DELETE /Books/{id}: Deletes the book with the given ID.

Similar endpoints are available for authors, publishers, and ratings.

At this point the main website only supports registering users without admin privileges. 
Therefore for testing with https://bookhub.dyn.cloud.e-infra.cz/swagger or your local deployment use one of the seeded admin users. 
For example username: "betatesting", password: "Aa123!".

Here are the steps to authorize:

1. Open `/api/Auth/login` endpoint and enter the following credentials:
    - userName: "beth"
    - password: "Aa123!".
2. Copy the generated token.
3. Click on the authorize button
4. Enter `Bearer {TOKEN}` and confirm.
5. Now you've been successfully authorized. You can use any endpoint.  

## Deployment
The application is deployed using CERIT-SC's Kubernetes cluster: https://bookhub.dyn.cloud.e-infra.cz/ for your convenience.

If you would like to deploy the app locally, you can either build your own Docker image or deploy it to a bare metal server.

In either case you need to run `dotnet publish -c Release` to build the app in release mode. Then:
- for bare metal deployment run `dotnet BookHub.dll`.
- for Docker run `docker build -t bookhub-image .` and then `docker run -p 80:80 --name bookhub bookhub-image`
***
