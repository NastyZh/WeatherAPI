namespace Weather
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool Authorize(string token)
        {
            return token == "7268dea1c8d5457bad2150200241307";
        }
    }
    
}

