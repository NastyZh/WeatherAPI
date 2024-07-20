namespace Weather;

public interface IAuthorizationService
{
    bool Authorize(string token);
}
