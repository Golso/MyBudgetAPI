using FluentValidation;
using MyBudgetApi.Data.Context;
using MyBudgetApi.Data.Dtos;
using System.Linq;

namespace MyBudgetApi.Data
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(BudgetDbContext budgetDbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.FirstName).MinimumLength(3);

            RuleFor(x => x.LastName).MinimumLength(2);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = budgetDbContext.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken.");
                }
            });
        }
    }
}
