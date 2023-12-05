using Microsoft.AspNetCore.Mvc;

namespace powerpage.ViewModels.Identity;

public class RoleViewModel
{
    [HiddenInput]
    public string Id { set; get; }

    [Required(ErrorMessage = "(*)")]
    [Display(Name = "نام نقش")]
    public string Name { set; get; }
}