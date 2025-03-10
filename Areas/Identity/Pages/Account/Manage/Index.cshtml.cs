using GalaxyForum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IUserEmailStore<ApplicationUser> _emailStore;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserStore<ApplicationUser> userStore,
        ILogger<IndexModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _logger = logger;
    }

    public string Username { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ImageFilename { get; set; }

    public class InputModel
    {
        [Required]
        [Display(Name = "Display Name")]
        public string Name { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile Image { get; set; }
    }

    private async Task LoadAsync(ApplicationUser user)
    {
        var userName = await _userManager.GetUserNameAsync(user);
        var name = user.Name;
        var location = user.Location;
        var imageFilename = user.ImageFilename;

        Username = userName;

        Input = new InputModel
        {
            Name = name,
            Location = location,
            Image = null // Image is not loaded here
        };

        ImageFilename = imageFilename;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }

        user.Name = Input.Name;
        user.Location = Input.Location;

        if (Input.Image != null)
        {
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(Input.Image.FileName)}";
            var filePath = Path.Combine("wwwroot/images/profiles", uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Input.Image.CopyToAsync(stream);
            }
            user.ImageFilename = "/images/profiles/" + uniqueFileName;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your profile has been updated";
        return RedirectToPage();
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<ApplicationUser>)_userStore;
    }
}
