using Microsoft.AspNetCore.Mvc;
using Service_Lib.Implementations;
using Starter_DB.Models;

namespace AspnetCoreMvcStarter.Controllers
{
  public class OTPController : Controller
  {

    private readonly ValidationService _validationService;
   private readonly OTPService _otpService;

    public OTPController(ValidationService validationService, OTPService otpService)
    {
      _validationService = validationService;
     _otpService = otpService;
    }

    public IActionResult Index()
    {
      return View(new Otprequest());
    }

    [HttpPost]
    public IActionResult GenerayteOTP(Otprequest request)
    {
      if (!ModelState.IsValid)
      {
        return View("Index", request);
      }

      var (isValid, type, error) = _validationService.ValidateContact(request.ContactValue);

      if (!isValid)
      {
        ModelState.AddModelError("Contact", error);
        return View("Index", request);
      }

      // Generate OTP
      var otp = _otpService.GenerateOtp();

      // In a real application, you would send the OTP via email or SMS here
      // For demonstration, we'll store it in TempData
      TempData["SuccessMessage"] = $"OTP sent to your {type}. For demo purposes, OTP is: {otp}";

      return RedirectToAction("Verify", new { contact = request.ContactValue });
    }

    public IActionResult Verify(string contact)
    {
      ViewBag.Contact = contact;
      return View();
    }

    [HttpPost]
    public IActionResult VerifyOTP(string contact, string otp)
    {
      if (string.IsNullOrEmpty(otp))
      {
        ModelState.AddModelError("", "Please enter the OTP");
        ViewBag.Contact = contact;
        return View("Verify");
      }

      bool isValid = true;//_otpService.ValidateOTP(contact, otp);

      if (!isValid)
      {
        ModelState.AddModelError("", "Invalid or expired OTP");
        ViewBag.Contact = contact;
        return View("Verify");
      }

      TempData["SuccessMessage"] = "OTP verified successfully!";
      return RedirectToAction("Index");
    }
  }
}
