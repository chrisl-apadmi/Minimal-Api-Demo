﻿using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MinimalApi.Clean.Api.Filters.Validation;

public static class ValidationFilter
{
    public static EndpointFilterDelegate ValidationFilterFactory(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
    {
        var validationDescriptors = GetValidators(context.MethodInfo, context.ApplicationServices);

        if (validationDescriptors.Any())
        {
            return invocationContext => Validate(validationDescriptors, invocationContext, next);
        }

        return invocationContext => next(invocationContext);
    }

    private static IEnumerable<ValidationDescriptor> GetValidators(MethodInfo methodInfo, IServiceProvider serviceProvider)
    {
        foreach (var item in methodInfo.GetParameters().Select((parameter, index) => new { parameter, index }))
        {
            if (item.parameter.GetCustomAttribute<ValidateAttribute>() is not null)
            {
                var validatorType = typeof(IValidator<>).MakeGenericType(item.parameter.ParameterType);
                var validator = serviceProvider.GetService(validatorType) as IValidator;

                if (validator is not null)
                {
                    yield return new ValidationDescriptor { ArgumentIndex = item.index, ArgumentType = item.parameter.ParameterType, Validator = validator };
                }
            }
        }
    }

    private static async ValueTask<object> Validate(IEnumerable<ValidationDescriptor> validationDescriptors, EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next)
    {
        foreach (var descriptor in validationDescriptors)
        {
            var argument = invocationContext.Arguments[descriptor.ArgumentIndex];

            if (argument is not null)
            {
                var validationResult = await descriptor.Validator.ValidateAsync(
                    new ValidationContext<object>(argument)
                );

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
            }
        }

        return await next.Invoke(invocationContext);
    }
}