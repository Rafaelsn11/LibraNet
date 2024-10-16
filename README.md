# LibraNet

LibraNET is a .NET-based API for managing libraries, book editions, media formats, and loans. It allows users to register books, handle multiple editions, manage media types, and track loans and returns.

## Features

- Manage multiple **Books**, **Editions**, and **Media formats**.
- Handle **loan** and **return** operations for book editions.
- Track users' loans and manage their activity.

## Technologies

- **.NET Core** (API framework)
- **Entity Framework Core** (ORM for database management)
- **SQL Server** (or any database supported by EF Core)

## Project Structure

- **Models:**
  - `Media`: Represents different formats (paper, pdf, audio, etc.).
  - `Book`: Holds details about the book itself (title, subject).
  - `Edition`: Represents a specific edition of a book, with details such as the year and loan status.
  - `User`: Represents a library user who can borrow and return books.

## How to Use

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/LibraNET.git
