using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Target10._9.Business.Services
{
    public interface IValidationService
    {
        Task ValidateAsync<TCommand>(TCommand command, CancellationToken cancellationToken);
    }

    public class ValidationService(IServiceProvider serviceProvider) : IValidationService
    {
        async Task IValidationService.ValidateAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
        {
            var validators = serviceProvider.GetServices<IValidator<TCommand>>();
            if (validators == null || !validators.Any())
                return;

            // Initie un contexte de validation
            var context = new ValidationContext<TCommand>(command);
            context.RootContextData.Add("input", command);

            // Génère les task de chaque cycle de validation
            var validations = validators.Select(v => v.ValidateAsync(context, cancellationToken));

            // Attends que chaque cycle de validation soit terminé
            var rawResults = await Task.WhenAll(validations);

            // Retravaille le résultat brut de chaque cycle de validation
            var failures = rawResults
                .SelectMany(result => result.Errors)
                .Where(result => result != null);

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }
    }

}