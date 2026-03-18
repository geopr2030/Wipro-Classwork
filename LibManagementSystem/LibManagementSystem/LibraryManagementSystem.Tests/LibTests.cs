namespace LibraryManagementSystem.Tests
{
    [TestClass]
    public class LibTests
    {
        [TestMethod]
        public void AddBook_ShouldAddBookToLibrary()
        {
            var library = new Library();
            var book = new Book("C#", "Abhishek", "123");

            library.AddBook(book);

            Assert.AreEqual(1, library.Books.Count);
        }
        [TestMethod]
        public void RegisterBorrower_ShouldAddBorrower()
        {
            var library = new Library();
            var borrower = new Borrower("Abhi", "CARD1");

            library.RegisterBorrower(borrower);

            Assert.AreEqual(1, library.Borrowers.Count);
        }
        [TestMethod]
        public void BorrowBook_ShouldMarkBookAsBorrowed()
        {
            var library = new Library();
            var book = new Book("C#", "Author", "123");
            var borrower = new Borrower("Abhi", "CARD1");

            library.AddBook(book);
            library.RegisterBorrower(borrower);

            library.BorrowBook("123", "CARD1");

            Assert.IsTrue(book.IsBorrowed);
            Assert.AreEqual(1, borrower.BorrowedBooks.Count);
        }
        [TestMethod]
        public void ReturnBook_ShouldMarkBookAsAvailable()
        {
            var library = new Library();
            var book = new Book("C#", "Author", "123");
            var borrower = new Borrower("Abhi", "CARD1");

            library.AddBook(book);
            library.RegisterBorrower(borrower);

            library.BorrowBook("123", "CARD1");
            library.ReturnBook("123", "CARD1");

            Assert.IsFalse(book.IsBorrowed);
            Assert.AreEqual(0, borrower.BorrowedBooks.Count);
        }
        [TestMethod]
        public void ViewBooks_ShouldReturnAllBooks()
        {
            var library = new Library();
            library.AddBook(new Book("Book1", "A", "1"));
            library.AddBook(new Book("Book2", "B", "2"));

            var books = library.ViewBooks();

            Assert.AreEqual(2, books.Count);
        }
        [TestMethod]
        public void ViewBorrowers_ShouldReturnAllBorrowers()
        {
            var library = new Library();
            library.RegisterBorrower(new Borrower("A", "1"));
            library.RegisterBorrower(new Borrower("B", "2"));

            var borrowers = library.ViewBorrowers();

            Assert.AreEqual(2, borrowers.Count);
        }
    }
}
