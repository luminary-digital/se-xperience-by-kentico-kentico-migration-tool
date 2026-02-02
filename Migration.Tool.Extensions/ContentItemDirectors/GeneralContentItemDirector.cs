using Migration.Tool.Common;
using Migration.Tool.Common.Services;
using Migration.Tool.Extensions.Constants;
using Migration.Tool.Source.Mappers.ContentItemMapperDirectives;

namespace Migration.Tool.Extensions.ContentItemDirectors;

/// <summary>
/// Responsible for directing content items into a workspace (Clipsal and PDL specific workspaces)
/// and creating folder hierarchies for content hub items.
/// Please not that if you are migrating specific content that needs it's own director logic, ensure the options are not called for the specific items.
/// </summary>
/// <param name="contentFolderService"></param>
/// <param name="workspaceService"></param>
/// <param name="toolConfiguration"></param>
public class GeneralContentItemDirector(
    ContentFolderService contentFolderService,
    WorkspaceService workspaceService,
    ToolConfiguration toolConfiguration) : ContentItemDirectorBase
{
    public override void Direct(MediaContentItemSource source, IBaseContentItemActionProvider options)
    {
        if (source.SourceSite.SiteDisplayName == SiteConstants.ClipsalSiteDisplayName)
        {
            options.OverrideWorkspace(WorkspaceConstants.ClipsalWorkspaceName, WorkspaceConstants.ClipsalWorkspaceDisplayName);
        }
        else
        {
            options.OverrideWorkspace(WorkspaceConstants.PdlWorkspaceName, WorkspaceConstants.PdlWorkspaceDisplayName);
        }
    }

    public override void Direct(ContentItemSource source, IContentItemActionProvider options)
    {
        // Handle only content to be converted to Content Hub items
        if (!toolConfiguration.ClassNamesConvertToContentHub.Contains(source.SourceClassName))
        {
            return;
        }

        // TODO Switch to switch statement in the future
        if (source.SourceClassName == FaqConstants.FaqItemCustomTable)
        {
            EnsureFaqItemPath(options);
            return;
        }

        // Leftover content items not caught earlier above and are not pages enter here.
        if (source.SourceSite?.SiteDisplayName == null || source.SourceNode == null)
        {
            // TODO log director didn't run for content item.
            return;
        }

        // Determine workspace based on site
        Guid? workspaceGuid;
        if (source.SourceSite.SiteDisplayName == SiteConstants.ClipsalSiteDisplayName)
        {
            workspaceGuid = workspaceService.EnsureWorkspace(WorkspaceConstants.ClipsalWorkspaceName, WorkspaceConstants.ClipsalWorkspaceDisplayName).GetAwaiter().GetResult();
            options.OverrideWorkspace(WorkspaceConstants.ClipsalWorkspaceName, WorkspaceConstants.ClipsalWorkspaceDisplayName);
        }
        else
        {
            workspaceGuid = workspaceService.EnsureWorkspace(WorkspaceConstants.PdlWorkspaceName, WorkspaceConstants.PdlWorkspaceDisplayName).GetAwaiter().GetResult();
            options.OverrideWorkspace(WorkspaceConstants.PdlWorkspaceName, WorkspaceConstants.PdlWorkspaceDisplayName);
        }

        if (workspaceGuid.HasValue)
        {
            // Remove the last path segment to get the parent folder path
            var nodePath = source.SourceNode.NodeAliasPath.TrimEnd('/');
            var lastSlashIndex = nodePath.LastIndexOf('/');
            var parentPath = lastSlashIndex >= 0 ? nodePath[..lastSlashIndex] : string.Empty;

            // Build absolute path: /{SiteName}/{ParentPath}
            var absolutePath = $"/{parentPath.TrimStart('/')}".TrimEnd('/');

            // Create folder structure
            var pathTemplate = ContentFolderService.StandardPathTemplate(source.SourceSite.SiteName, absolutePath, workspaceGuid.Value);
            contentFolderService.EnsureFolderStructure(pathTemplate, workspaceGuid.Value).GetAwaiter().GetResult();

            options.OverrideContentFolder(absolutePath);
        }
    }

    private void EnsureFaqItemPath(IContentItemActionProvider options)
    {
        // Build absolute path: /{SiteName}/{ParentPath}
        var absolutePath = $"/{FaqConstants.FaqContentFolderName}";

        // Create folder structure
        var pathTemplate = ContentFolderService.StandardPathTemplate("Shared", absolutePath, workspaceService.FallbackWorkspace.Value.WorkspaceGUID);
        contentFolderService.EnsureFolderStructure(pathTemplate).GetAwaiter().GetResult();

        options.OverrideContentFolder(absolutePath);
    }
}
