using CMS.ContentEngine;
using CMS.Core;
using Microsoft.Extensions.Logging;
using Migration.Tool.Common.Enumerations;
using Migration.Tool.Common.Services;
using Migration.Tool.KXP.Api.Services.CmsClass;
using Migration.Tool.Source.Services.Model;
using Newtonsoft.Json.Linq;

/// <summary>
/// Migrates widget properties from Kentico 13 Page Selector to Combined Selector format.
/// </summary>
/// <param name="spoiledGuidContext"></param>
/// <param name="logger"></param>
public class WidgetPageSelectorToCombinedSelectorMigration(
    ISpoiledGuidContext spoiledGuidContext,
    ILogger<WidgetPageSelectorToCombinedSelectorMigration> logger) : IWidgetPropertyMigration
{
    private const string MigratedComponent = Kx13FormComponents.Kentico_PageSelector;
    
    /// <summary>
    /// Set of properties to be migrated from Page Selector to Combined Selector
    /// </summary>
    private static readonly HashSet<string> MigratedProperties = new(StringComparer.InvariantCultureIgnoreCase)
    {
        "datasource"
    };

    // Set higher priority (lower number) than migrations you want to override
    public int Rank => 100;

    // This migration should happen only for Page selector properties
    public bool ShallMigrate(WidgetPropertyMigrationContext context, string propertyName)
        => MigratedComponent.Equals(context.EditingFormControlModel?.FormComponentIdentifier, StringComparison.InvariantCultureIgnoreCase);

    // Define the property migration
    public Task<WidgetPropertyMigrationResult> MigrateWidgetProperty(
        string key, JToken? value, WidgetPropertyMigrationContext context)
    {
        // Only migrate properties defined in MigratedProperties
        if (!MigratedProperties.Contains(key))
        {
            return Task.FromResult(new WidgetPropertyMigrationResult(value));
        }

        (int siteId, _) = context;

        // Read the KX13 value if it's not empty
        if (value?.ToObject<List<PageSelectorItem>>() is { Count: > 0 } items)
        {
            // Map each page selector object to a content item reference - the target data type
            var result = items.Select(pageSelectorItem => new ContentItemReference
            {
                // Retrieve the correct GUID of the migrated page item
                Identifier = spoiledGuidContext.EnsureNodeGuid(pageSelectorItem.NodeGuid, siteId)
            }).ToList();

            // Serialize and return the new structure
            var resultAsJToken = JToken.FromObject(result);
            return Task.FromResult(new WidgetPropertyMigrationResult(resultAsJToken));
        }
        else
        {
            logger.LogError("Failed to parse '{ComponentName}' json {Json}", MigratedComponent, value?.ToString() ?? "<null>");

            // Leave value as it is
            return Task.FromResult(new WidgetPropertyMigrationResult(value));
        }
    }
} 
