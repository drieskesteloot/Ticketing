using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticketing.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "EmailIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessageResourceName = "IncorrectEmail",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PasswordIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.TicketingUI))]
        public string Password { get; set; }

        [Display(Name = "LoginRemember", ResourceType = typeof(Resources.TicketingUI))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "FirstName", ResourceType = typeof(Resources.TicketingUI))]
        [Required(ErrorMessageResourceName = "FirstNameIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        public string Voornaam { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.TicketingUI))]
        [Required(ErrorMessageResourceName = "LastNameIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        public string Achternaam { get; set; }

        [Display(Name = "Telefoonnummer")]
        public string TelefoonNummer { get; set; }

        [Required(ErrorMessageResourceName = "CellPhoneNumberIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        [StringLength(13, ErrorMessageResourceName = "IncorrectCellPhoneNumber", 
            ErrorMessageResourceType = typeof(Resources.TicketingUI), MinimumLength = 13)]
        [Display(Name = "CellphoneNumber", ResourceType = typeof(Resources.TicketingUI))]
        public string GsmNummer { get; set; }

        [Display(Name = "ResponsibleName", ResourceType = typeof(Resources.TicketingUI))]
        public string VerantwoordelijkeId { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.TicketingUI))]
        public bool Actief { get; set; }

        [Required(ErrorMessageResourceName = "EmailIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        [EmailAddress(ErrorMessageResourceName = "IncorrectEmail",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PasswordIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        [StringLength(100, ErrorMessageResourceName = "PasswordLength",
            ErrorMessageResourceType = typeof(Resources.TicketingUI), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.TicketingUI))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.TicketingUI))]
        [Compare("Password", ErrorMessageResourceName = "PasswordCompare",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
