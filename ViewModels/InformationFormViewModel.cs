using System.ComponentModel.DataAnnotations;

namespace DemoDownloadPage.ViewModels
    {
    public class InformationFormViewModel
        {
        [Required]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Organization Address")]
        public string OrganizationAddress { get; set; }

        [Required(ErrorMessage = "Verify Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Verify Mobile Number")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Invalid Contact No")]
        public string Mobile { get; set; }
        }
    }
