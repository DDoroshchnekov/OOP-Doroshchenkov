using MyProject.Domain;

namespace MyProject.Tests;

public class BookTests
{
    [Fact]
    public void Book_Should_Be_Available_After_Creation()
    {
        var book = new Book("Test", "Author");

        Assert.True(book.IsAvailable);
    }

    [Fact]
    public void Borrow_Should_Change_Status()
    {
        var book = new Book("Test", "Author");

        book.Borrow();

        Assert.False(book.IsAvailable);
    }

    [Fact]
    public void Borrow_Twice_Should_Throw()
    {
        var book = new Book("Test", "Author");

        book.Borrow();

        Assert.Throws<InvalidOperationException>(() => book.Borrow());
    }

    [Fact]
    public void Empty_Title_Should_Throw()
    {
        Assert.Throws<ArgumentException>(() =>
            new Book("", "Author"));
    }

    [Fact]
    public void Empty_Author_Should_Throw()
    {
        Assert.Throws<ArgumentException>(() =>
            new Book("Title", ""));
    }
}