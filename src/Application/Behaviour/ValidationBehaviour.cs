using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviour
{
    public class ValidationBehaviour<TRequest, TRespose> : IPipelineBehavior<TRequest, TRespose> where TRequest : IRequest<TRespose>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TRespose> Handle(TRequest request, RequestHandlerDelegate<TRespose> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failure = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if ((failure.Count != 0))
                {
                    throw new ValidationException(failure);
                }
            }

            return await next();
        }
    }
}
