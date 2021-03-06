using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.UI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember-me")]
        public bool RememberMe { get; set; }
    }
}
