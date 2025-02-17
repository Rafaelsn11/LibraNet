# LibraNet

LibraNET is a .NET-based API for managing libraries, book editions, media formats, and loans. It allows users to register books, handle multiple editions, manage media types, and track loans and returns.

## Features

- Manage multiple **Books**, **Editions**, **Users** and **Media formats**.
- Handle **loan** and **return** operations for book editions.
- Track users' loans and manage their activity.

## Technologies

- **.NET Core** (API framework)
- **Entity Framework Core** (ORM for database management)
- **PostgreSQL** (or any database supported by EF Core)
- **Docker Compose** (for container orchestration)

## Project Structure

- **Models:**
  - `Media`: Represents different formats (paper, pdf, audio, etc.).
  - `Book`: Holds details about the book itself (title, subject).
  - `Edition`: Represents a specific edition of a book, with details such as the year and loan status.
  - `User`: Represents a library user who can borrow and return books.
  - `Role`: Represents a role within the system.
  - `UserRole`: Links users with their role.

## How to Use

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/LibraNET.git
2. Create appsettings.Development.json according to Template.
3. Create a .env file and set the following values according to your application needs:
    ```
    POSTGRES_USER=value
    POSTGRES_PASSWORD=value

    CONNECTION_STRINGS_DEFAULT=value

    SETTINGS_ADMIN_EMAIL=value
    SETTINGS_ADMIN_PASSWORD=value
    SETTINGS_PASSWORD_ADDITIONAL_KEY=value
    SETTINGS_JWT_EXPIRATION_TIME_MINUTES=0
    SETTINGS_JWT_SIGNING_KEY=value

    PGADMIN_DEFAULT_EMAIL=value
    PGADMIN_DEFAULT_PASSWORD=value
4. Make sure you have docker installed, if you don't have it installed:
    ```
    https://www.docker.com/get-started/
5. Run this command to start the application containers.
    ```sh
    docker compose up -d
6. Run this command to remove containers, volumes and networks created by Compose, when you want to stop the application
    ```sh
    docker compose down -v
