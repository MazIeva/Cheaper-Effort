using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Security.Cryptography.X509Certificates;

namespace Cheaper_Effort.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Login Login { get; set; }
        public String UserName { get; set; }
        public String Greeting { get; set; }
        public String FullGreeting { get; set; }

        public void OnGet()
        {
            Random rnd = new Random();

            String Greeting = "Welcome".AddAdjective(rnd.Next(1,7));

            if (User.Identity.IsAuthenticated)
            {
                UserName = User.Identity.Name;
                FullGreeting = AddName(Greeting, UserName);
            }
            else
            {
                FullGreeting = AddName(CurrentGreeting: Greeting);
            }

        }
        public static string AddName(string CurrentGreeting, string UserName = "User")
        {
            string FullString = CurrentGreeting + " " + UserName;

            return FullString;
        }
    }
}

public static class StringExtensions
{
    public static string AddAdjective(this String Greeting, int number)
    {
        String[] Adjectives = new String [] { "beautiful", "graceful", "lovely", "joyful", "amazing", "our favourite", "the one and only"};

        String answer = Greeting + " " + Adjectives[number];
        
        return answer;
    }
}