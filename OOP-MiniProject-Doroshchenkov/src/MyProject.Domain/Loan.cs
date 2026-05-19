namespace MyProject.Domain;

public class Loan
{
    public Guid BookId { get; }
    public Guid UserId { get; }
    public DateTime LoanDate { get; }

    public Loan(Guid bookId, Guid userId)
    {
        BookId = bookId;
        UserId = userId;
        LoanDate = DateTime.Now;
    }
}