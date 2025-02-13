﻿#nullable enable
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;
using System.Globalization;


namespace XAFVectorSearch.Blazor.Server.Helpers;

public static class ServiceCollectionExtensions
{
    public static T? ConfigureAndGet<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class
    {
        var section = configuration.GetSection(sectionName);
        var settings = section.Get<T>();
        services.Configure<T>(section);

        return settings;
    }

    public static IServiceCollection AddRequestLocalization(this IServiceCollection services, params string[] cultures)
        => services.AddRequestLocalization(cultures, null);

    public static IServiceCollection AddRequestLocalization(this IServiceCollection services, IEnumerable<string> cultures, Action<IList<IRequestCultureProvider>>? providersConfiguration)
    {
        var supportedCultures = cultures.Select(c => new CultureInfo(c)).ToList();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.DefaultRequestCulture = new RequestCulture(supportedCultures.First());
            providersConfiguration?.Invoke(options.RequestCultureProviders);
        });

        return services;
    }

    public static IServiceCollection Replace<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TService : class
        where TImplementation : class, TService
    {
        services.Replace(new ServiceDescriptor(serviceType: typeof(TService), implementationType: typeof(TImplementation), lifetime));

        return services;
    }

    public static IServiceCollection AddDefaultProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                var statusCode = context.ProblemDetails.Status.GetValueOrDefault(StatusCodes.Status500InternalServerError);

                context.ProblemDetails.Type ??= $"https://httpstatuses.io/{statusCode}";
                context.ProblemDetails.Title ??= ReasonPhrases.GetReasonPhrase(statusCode);
                context.ProblemDetails.Instance ??= context.HttpContext.Request.Path;
                context.ProblemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);
            };
        });

        return services;
    }

    public static IServiceCollection AddDefaultExceptionHandler(this IServiceCollection services)
    {
        // Ensures that the ProblemDetails service is registered.
        services.AddProblemDetails();

        services.AddExceptionHandler<DefaultExceptionHandler>();
        return services;
    }
}
