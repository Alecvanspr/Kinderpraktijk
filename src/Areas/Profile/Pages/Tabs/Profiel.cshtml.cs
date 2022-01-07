using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using src.Areas.Identity.Data;
using src.Areas.Profile.ViewModels;

namespace src.Areas.Profile.Pages.Tabs
{
    public class ProfielModel : PageModel
    {
        [BindProperty]
        public ProfileViewModel ProfileViewModel { get; set; }
        public new srcUser User { get; set; }
        public void OnGet()
        {
        }

    }
}
