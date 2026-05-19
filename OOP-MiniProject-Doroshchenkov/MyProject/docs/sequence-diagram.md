```mermaid
sequenceDiagram

actor User
participant Console
participant LibraryService
participant Repository

User->>Console: Borrow book
Console->>LibraryService: BorrowBook(bookId)
LibraryService->>Repository: GetBookById()
Repository-->>LibraryService: Book
LibraryService->>Book: Borrow()
LibraryService-->>Console: Success
Console-->>User: Book borrowed
```