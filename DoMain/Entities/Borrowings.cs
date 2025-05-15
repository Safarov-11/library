namespace DoMain.Entities;

public class Borrowings
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime BorrowDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(10);
    public DateTime? ReturnDate { get; set; }
    public decimal Fine { get; set; }
    
}
