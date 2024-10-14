using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcStarter.Models
{
  public class BankPayment_aura
  {
    public int Id { get; set; }
    public string? Supplier { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal? Total { get; set; }
    public String? Status { get; set; }
  }
}
