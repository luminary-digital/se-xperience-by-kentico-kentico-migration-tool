using Migration.Tool.Common;
using Migration.Tool.Common.Services;
using Migration.Tool.Extensions.Constants;
using Migration.Tool.Source.Mappers.ContentItemMapperDirectives;

namespace Migration.Tool.Extensions.ContentItemDirectors;

/// <summary>
/// Responsible for directing content items into a workspace (Clipsal and PDL specific workspaces)
/// and creating folder hierarchies for content hub items.
/// </summary>
/// <param name="contentFolderService"></param>
/// <param name="workspaceService"></param>
/// <param name="toolConfiguration"></param>
public class WorkspaceContentItemDirector(ContentFolderService contentFolderService, WorkspaceService workspaceService, ToolConfiguration toolConfiguration) : ContentItemDirectorBase
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
        
        // TODO: This needs to be extended so that it can handle content items that are not pages (not assigned to a specific site).
        if (source.SourceSite?.SiteDisplayName == null || source.SourceNode == null)
        {
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

        // Only create folder hierarchy if the source is being converted to a content hub item
        if (toolConfiguration.ClassNamesConvertToContentHub.Contains(source.SourceClassName) && workspaceGuid.HasValue)
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
}
