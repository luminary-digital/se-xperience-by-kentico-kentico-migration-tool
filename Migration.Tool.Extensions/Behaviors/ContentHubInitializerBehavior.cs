using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Migration.Tool.Common.Abstractions;
using Migration.Tool.Extensions.Initializers;
using Migration.Tool.KXP.Api;

namespace Migration.Tool.Extensions.Behaviors;

/// <summary>
/// MediatR pipeline behavior that ensures content hub is initialized after XbyK API is ready.
/// This behavior must run AFTER XbyKApiContextBehavior to ensure the CMS IoC container is initialized.
/// Uses lazy service resolution to avoid resolving ContentHubInitializer before API initialization.
/// </summary>
public class ContentHubInitializerBehavior<TRequest, TResponse>(
    ILogger<ContentHubInitializerBehavior<TRequest, TResponse>> logger,
    KxpApiInitializer kxpApiInitializer,
    IServiceProvider serviceProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : CommandResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Ensure XbyK API is initialized first (this is critical!)
        if (!kxpApiInitializer.EnsureApiIsInitialized())
        {
            logger.LogError("Cannot initialize content hub - XbyK API initialization failed");
            return await next();
        }

        // NOW it's safe to resolve ContentHubInitializer because API is initialized
        try
        {
            var contentHubInitializer = serviceProvider.GetRequiredService<ContentHubInitializer>();
            await contentHubInitializer.EnsureContentHubStructure();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to initialize content hub structure, continuing with migration...");
            // Don't throw - allow migration to continue even if folder creation fails
        }

        // Continue with the rest of the pipeline
        return await next();
    }
}
