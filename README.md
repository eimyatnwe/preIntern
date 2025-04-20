# preIntern
A full-stack library management system designed to streamline the management of books, users, and borrowing activities.

# Features
• User Authentication: 
- Secure login and registration for users and administrators.

• Book Management: 
- Add, update, delete, and view books in the library.

• Borrowing System: 
- Users can borrow books with tracking of due dates.

• Admin Dashboard: 
- Administrators can manage users and books.

• Responsive UI: 
- User-friendly interface compatible with various devices.

### Default Credentials

#### Administrator
	•	Email: admin@librarytwo.com
	•	Password: Admin123@

#### Regular User
	•	Email: def@gmail.com
	•	Password: user123@

#### Users can register for a new account via the registration page.

### Setup Instructions

#### Prerequisites
• .NET SDK (version compatible with the project)
• Node.js and npm (for Angular frontend)
• Angular CLI

#### Run the API
dotnet run

#### Run the Angular application:
ng serve

### API Endpoints Overview

#### Authentication
• POST /api/auth/login: 
 - Authenticate user and return JWT.
• POST /api/auth/register:
 - Register a new user.

#### Books
• GET /api/books: 
- Retrieve a list of all books.
• GET /api/books/{id}:
- Retrieve details of a specific book.
• POST /api/books:
- Add a new book (Admin only).
• PUT /api/books/{id}:
- Update book information (Admin only).
• DELETE /api/books/{id}:
- Delete a book (Admin only).

#### Borrowing
• POST /api/borrowRecord: 
- Borrow a book.
• GET /api/borrowRecord:
- Retrieve borrowed records.
