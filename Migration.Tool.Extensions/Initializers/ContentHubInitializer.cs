using Microsoft.Extensions.Logging;
using Migration.Tool.Common.Services;
using Migration.Tool.Extensions.Constants;
using Migration.Tool.KXP.Api;

namespace Migration.Tool.Extensions.Initializers;

/// <summary>
/// Initializer that sets up initial content hub folder structure when the migration tool runs.
/// You can follow this structure to have things created or preconfigured to migrate content.
/// </summary>
public class ContentHubInitializer(
    ILogger<ContentHubInitializer> logger,
    KxpApiInitializer kxpApiInitializer,
    ContentFolderService contentFolderService,
    WorkspaceService workspaceService)
{
    private bool initializationCalled;

    /// <summary>
    /// Ensures the content hub folder structure is created. This method is idempotent and safe to call multiple times.
    /// </summary>
    public async Task EnsureContentHubStructure()
    {
        if (initializationCalled)
        {
            logger.LogTrace("Content hub initialization already called, skipping init");
            return;
        }

        // Ensure XbyK API is initialized first
        kxpApiInitializer.EnsureApiIsInitialized();

        logger.LogInformation("Initializing content hub folder structure...");

        try
        {
            // Create Clipsal workspace folders
            var clipsalWorkspaceGuid = await workspaceService.EnsureWorkspace(
                WorkspaceConstants.ClipsalWorkspaceName, 
                WorkspaceConstants.ClipsalWorkspaceDisplayName);

            if (clipsalWorkspaceGuid.HasValue)
            {
                await CreateInitialFolders(clipsalWorkspaceGuid.Value, WorkspaceConstants.ClipsalWorkspaceDisplayName);
                logger.LogInformation("Clipsal workspace folders created successfully");
            }

            // Create PDL workspace folders
            var pdlWorkspaceGuid = await workspaceService.EnsureWorkspace(
                WorkspaceConstants.PdlWorkspaceName, 
                WorkspaceConstants.PdlWorkspaceDisplayName);

            if (pdlWorkspaceGuid.HasValue)
            {
                await CreateInitialFolders(pdlWorkspaceGuid.Value, WorkspaceConstants.PdlWorkspaceDisplayName);
                logger.LogInformation("PDL workspace folders created successfully");
            }

            initializationCalled = true;
            logger.LogInformation("Content hub initialization completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to initialize content hub folder structure");
            throw;
        }
    }

    /// <summary>
    /// Creates the initial folder structure for a workspace.
    /// Customize this method to create your desired folder structure.
    /// </summary>
    private async Task CreateInitialFolders(Guid workspaceGuid, string siteName)
    {
        // Example: Create a root folder structure for organizing migrated content
        // Format: /{FolderName}

        var foldersToCreate = new[] { ContentHubFolderConstants.Assets, ContentHubFolderConstants.ProductsFolderName };

        foreach (var folderPath in foldersToCreate)
        {
            var pathTemplate = ContentFolderService.StandardPathTemplate(siteName, folderPath, workspaceGuid);
            await contentFolderService.EnsureFolderStructure(pathTemplate, workspaceGuid);
            logger.LogDebug("Created folder: {FolderPath} in workspace {WorkspaceGuid}", folderPath, workspaceGuid);
        }
    }
}
