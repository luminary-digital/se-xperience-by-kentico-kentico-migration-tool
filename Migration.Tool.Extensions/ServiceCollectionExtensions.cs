using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Migration.Tool.Extensions.Behaviors;
using Migration.Tool.Extensions.ClassMappings;
using Migration.Tool.Extensions.CommunityMigrations;
using Migration.Tool.Extensions.ContentItemDirectors;
using Migration.Tool.Extensions.CustomWidgetMigrations;
using Migration.Tool.Extensions.DefaultMigrations;
using Migration.Tool.Extensions.Initializers;
using Migration.Tool.KXP.Api.Services.CmsClass;
using Migration.Tool.Source.Mappers.ContentItemMapperDirectives;

namespace Migration.Tool.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseCustomizations(this IServiceCollection services)
    {
        services.AddTransient<IFieldMigration, AssetMigration>();
        services.AddTransient<IFieldMigration, SampleTextMigration>();

        services.AddTransient<IWidgetPropertyMigration, WidgetFileMigration>();
        services.AddTransient<IWidgetPropertyMigration, WidgetPathSelectorMigration>();
        services.AddTransient<IWidgetPropertyMigration, WidgetPageSelectorMigration>();

        #region Custom Migrations
        // Widget property migrations
        services.AddTransient<IWidgetPropertyMigration, WidgetPageSelectorToCombinedSelectorMigration>();
        
        // Content item directors
        services.AddTransient<ContentItemDirectorBase, GeneralContentItemDirector>();

        
        #endregion

        #region Initializers
        // Register content hub initializer as singleton to maintain state across pipeline invocations
        services.AddSingleton<ContentHubInitializer>();
        
        // Register the MediatR behavior to run the initializer AFTER XbyK API is initialized
        // Must be registered AFTER UseKsToolCore() so it runs after XbyKApiContextBehavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ContentHubInitializerBehavior<,>));
        #endregion
        
        // Comprehensive mapping for all 20 SE page types that inherit from SE.BasePage
        services.AddAllSEPageTypesReusableSchemaMapping();

        // Old sample mapping - replaced by comprehensive mapping above
        // services.AddSEOMetaDataReusableSchemaMapping();

        // services.AddClassMergeExample();
        // services.AddClassMergeExampleAsReusable();
        // services.AddSimpleRemodelingSample();
        // services.AddReusableRemodelingSample();
        // services.AddReusableSchemaIntegrationSample();
        // services.AddReusableSchemaAutoGenerationSample();
        // services.AddTransient<ContentItemDirectorBase, SamplePageToWidgetDirector>();
        // services.AddTransient<ContentItemDirectorBase, SampleChildLinkDirector>();

        // Routing content items to prefabricated content types (i.e., types not created by Migration Tool --page-types CLI argument)
        //
        // The following two methods may be combined, but one particular content type should be covered by only one of them.
        //
        //   1. Content item director method is applicable if each target field has a matching source field that has the same name.
        //      You may use JsonBasedTypeRemapDirector or drive the mapping directly from your own director using IContentItemActionProvider.OverrideTargetType method.
        //
        //      services.AddTransient<ContentItemDirectorBase>(sp => new JsonBasedTypeRemapDirector("migration-mapping.json"));
        //
        //   2. Custom mapping method gives you the highest flexibility if the prefabricated type doesn't match the source type exactly.
        //
        //      services.AddMappingToPrefabricatedContentTypeSample();

        return services;
    }
}
