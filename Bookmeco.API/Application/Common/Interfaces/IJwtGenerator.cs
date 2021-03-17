namespace Application.Common.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(Domain.Entities.User user);
    }
}
