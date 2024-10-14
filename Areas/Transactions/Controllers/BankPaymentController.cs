using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Areas.Transactions.Controllers
{
  [Area("Transactions")]
  public class BankPaymentController : Controller
  {
    public IActionResult Index()
    {
      string[] status = ["Paid", "Due", "Cancelled", "Cancelled", "Paid"];
      List<BankPayment_aura> bankPayment_ = new List<BankPayment_aura>();
      for (int i = 0; i < 5; i++)
      {
        BankPayment_aura bp = new BankPayment_aura();
        bp.Id = i+ 1;
        bp.Supplier = "Supplier_" + (i + 1);
        bp.TransactionDate = DateTime.Now;
        bp.DueDate = DateTime.Now;
        bp.Total = 60000;
        bp.Status = status[i];
        bankPayment_.Add(bp);
      }
      return View(bankPayment_);
    }
    public IActionResult Create()
    {
      BankPayment_aura bp = new BankPayment_aura();
      return View("Create", bp);
    }
    public IActionResult Edit(int id)
    {
      BankPayment_aura bp = new BankPayment_aura();
      return View("Create", bp);
    }
  }
}
