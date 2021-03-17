﻿using FluentValidation;

namespace Application.Common.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                    .NotEmpty()
                    .MinimumLength(3).WithMessage("Password must be at least 3 characters")
                //.Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase character")
                //.Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase character")
                //.Matches("[0-9]").WithMessage("Password must contain at least 1 number")
                //.Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 non alphanumeric character")
                ;
            return options;
        }
    }
}
