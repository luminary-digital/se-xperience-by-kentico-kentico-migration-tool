using Migration.Tool.Common;
using Migration.Tool.Common.Services;
using Migration.Tool.Source.Mappers.ContentItemMapperDirectives;

namespace Migration.Tool.Extensions.ContentItemDirectors;

/// <summary>
/// Responsible for directing content items into a folder hierarchy that mirrors their source structure.
/// </summary>
/// <param name="contentFolderService"></param>
/// <param name="workspaceService"></param>
public class FolderHierarchyContentItemDirector(ContentFolderService contentFolderService, WorkspaceService workspaceService, ToolConfiguration toolConfiguration) : ContentItemDirectorBase
{
    public override void Direct(ContentItemSource source, IContentItemActionProvider options)
    {
        // Only run this directive if the source is being converted to a content item
        if (!toolConfiguration.ClassNamesConvertToContentHub.Contains(source.SourceClassName))
        {
            return;
        }

        var workspaceGuid = workspaceService.FallbackWorkspace.Value.WorkspaceGUID;
        
        // Remove the last path segment to get the parent folder path
        var nodePath = source.SourceNode.NodeAliasPath.TrimEnd('/');
        var lastSlashIndex = nodePath.LastIndexOf('/');
        var parentPath = lastSlashIndex >= 0 ? nodePath[..lastSlashIndex] : string.Empty;
        
        // Build absolute path: /{SiteName}/{ParentPath}
        var absolutePath = $"/{source.SourceSite.SiteName}/{parentPath.TrimStart('/')}".TrimEnd('/');
        
        // Create folder structure
        var pathTemplate = ContentFolderService.StandardPathTemplate(source.SourceSite.SiteName, absolutePath, workspaceGuid);
        contentFolderService.EnsureFolderStructure(pathTemplate, workspaceGuid).GetAwaiter().GetResult();
        
        options.OverrideContentFolder(absolutePath);
    }
}
