using FluentValidation;

namespace Application.UserCompanies.Commands.CreateUserCompany
{
    public class CreateUserCompanyValidator : AbstractValidator<CreateUserCompanyCommand>
    {
        public CreateUserCompanyValidator()
        {
            RuleFor(x => x.AccessTypeId).NotNull();
            RuleFor(x => x.CompanyId).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
