namespace Mocking.WebApi.Interfaces
{
    public interface IMailService
    {
        bool Authenticate();
        bool IndependentAuthenticate();
    }
}
