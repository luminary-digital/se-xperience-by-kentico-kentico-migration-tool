using CMS.DataEngine;
using Microsoft.Extensions.DependencyInjection;
using Migration.Tool.Common.Builders;
using Migration.Tool.KXP.Api.Auxiliary;

namespace Migration.Tool.Extensions.ClassMappings;

/// <summary>
/// Comprehensive reusable schema mapping for all SE page types that inherit from SE.BasePage
/// </summary>
public static class AllSEPageTypesReusableSchemaMapping
{
    public static IServiceCollection AddAllSEPageTypesReusableSchemaMapping(this IServiceCollection serviceCollection)
    {
        const string schemaName = "SE.SEOMetadata";

        // Create reusable schema for SEO metadata fields from SE.BasePage
        var sb = new ReusableSchemaBuilder(schemaName, "SEO Metadata", "Reusable schema that defines common SEO metadata fields from SE.BasePage");
        
        // TODO: Instead of doing this build the schema manually and include any additional fields that need to be merged into the schema (keywords for example)
        
        sb.ConvertFrom("SE.BasePage", x => x);
        

        // ==================== SE.Home ====================
        var mHome = new MultiClassMapping("SE.Home", target =>
        {
            target.ClassName = "SE.Home";
            target.ClassTableName = "SE_Home";
            target.ClassDisplayName = "Home";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mHome.BuildField("HomeID").AsPrimaryKey();
        mHome.UseResusableSchema(schemaName);
        
        // Reusable schema fields
        mHome.BuildField("SocialShareTitle").SetFrom("SE.Home", "SocialShareTitle");
        mHome.BuildField("SocialShareDescription").SetFrom("SE.Home", "SocialShareDescription");
        mHome.BuildField("SocialShareImage").SetFrom("SE.Home", "SocialShareImage");
        mHome.BuildField("IncludeInSiteMap").SetFrom("SE.Home", "IncludeInSiteMap");
        
        // Page-specific fields
        mHome.BuildField("Title").SetFrom("SE.Home", "Title", isTemplate: true);

        // ==================== SE.ArticlePage ====================
        var mArticlePage = new MultiClassMapping("SE.ArticlePage", target =>
        {
            target.ClassName = "SE.ArticlePage";
            target.ClassTableName = "SE_ArticlePage";
            target.ClassDisplayName = "Article Page";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mArticlePage.BuildField("ArticlePageID").AsPrimaryKey();
        mArticlePage.UseResusableSchema(schemaName);
        
        // Reusable schema fields
        mArticlePage.BuildField("SocialShareTitle").SetFrom("SE.ArticlePage", "SocialShareTitle");
        mArticlePage.BuildField("SocialShareDescription").SetFrom("SE.ArticlePage", "SocialShareDescription");
        mArticlePage.BuildField("SocialShareImage").SetFrom("SE.ArticlePage", "SocialShareImage");
        mArticlePage.BuildField("IncludeInSiteMap").SetFrom("SE.ArticlePage", "IncludeInSiteMap");
        
        // Page-specific fields
        mArticlePage.BuildField("ArticleTitle").SetFrom("SE.ArticlePage", "ArticleTitle", isTemplate: true);
        mArticlePage.BuildField("ArticleSubTitle").SetFrom("SE.ArticlePage", "ArticleSubTitle", isTemplate: true);
        mArticlePage.BuildField("BannerImage").SetFrom("SE.ArticlePage", "BannerImage", isTemplate: true);
        mArticlePage.BuildField("BannerAltImage").SetFrom("SE.ArticlePage", "BannerAltImage", isTemplate: true);
        mArticlePage.BuildField("ReadTime").SetFrom("SE.ArticlePage", "ReadTime", isTemplate: true);
        mArticlePage.BuildField("DatePublished").SetFrom("SE.ArticlePage", "DatePublished", isTemplate: true);
        mArticlePage.BuildField("AuthorName").SetFrom("SE.ArticlePage", "AuthorName", isTemplate: true);
        mArticlePage.BuildField("AuthorRole").SetFrom("SE.ArticlePage", "AuthorRole", isTemplate: true);
        mArticlePage.BuildField("AuthorImg").SetFrom("SE.ArticlePage", "AuthorImg", isTemplate: true);
        mArticlePage.BuildField("AuthorAltImg").SetFrom("SE.ArticlePage", "AuthorAltImg", isTemplate: true);

        // ==================== SE.AssessmentToolFormPage ====================
        var mAssessmentToolFormPage = new MultiClassMapping("SE.AssessmentToolFormPage", target =>
        {
            target.ClassName = "SE.AssessmentToolFormPage";
            target.ClassTableName = "se_AssessmentToolForm";
            target.ClassDisplayName = "Assessment Tool Form Page";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mAssessmentToolFormPage.BuildField("AssessmentToolFormID").AsPrimaryKey();
        mAssessmentToolFormPage.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mAssessmentToolFormPage.BuildField("SocialShareTitle").SetFrom("SE.AssessmentToolFormPage", "SocialShareTitle");
        mAssessmentToolFormPage.BuildField("SocialShareDescription").SetFrom("SE.AssessmentToolFormPage", "SocialShareDescription");
        mAssessmentToolFormPage.BuildField("SocialShareImage").SetFrom("SE.AssessmentToolFormPage", "SocialShareImage");
        mAssessmentToolFormPage.BuildField("IncludeInSiteMap").SetFrom("SE.AssessmentToolFormPage", "IncludeInSiteMap");

        // ==================== SE.AssessmentToolResultpage ====================
        var mAssessmentToolResultpage = new MultiClassMapping("SE.AssessmentToolResultpage", target =>
        {
            target.ClassName = "SE.AssessmentToolResultpage";
            target.ClassTableName = "SE_AssessmentToolResultpage";
            target.ClassDisplayName = "Assessment Tool Result Page";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mAssessmentToolResultpage.BuildField("AssessmentToolResultpageID").AsPrimaryKey();
        mAssessmentToolResultpage.UseResusableSchema(schemaName);
        
        // Reusable schema fields
        mAssessmentToolResultpage.BuildField("SocialShareTitle").SetFrom("SE.AssessmentToolResultpage", "SocialShareTitle");
        mAssessmentToolResultpage.BuildField("SocialShareDescription").SetFrom("SE.AssessmentToolResultpage", "SocialShareDescription");
        mAssessmentToolResultpage.BuildField("SocialShareImage").SetFrom("SE.AssessmentToolResultpage", "SocialShareImage");
        mAssessmentToolResultpage.BuildField("IncludeInSiteMap").SetFrom("SE.AssessmentToolResultpage", "IncludeInSiteMap");
        
        // Page-specific fields
        mAssessmentToolResultpage.BuildField("HeroOverlayIsBreadcrumbVisible").SetFrom("SE.AssessmentToolResultpage", "HeroOverlayIsBreadcrumbVisible", isTemplate: true);
        mAssessmentToolResultpage.BuildField("HeroOverlayColorway").SetFrom("SE.AssessmentToolResultpage", "HeroOverlayColorway", isTemplate: true);
        mAssessmentToolResultpage.BuildField("HeroOverlayFallbackTitle").SetFrom("SE.AssessmentToolResultpage", "HeroOverlayFallbackTitle", isTemplate: true);
        mAssessmentToolResultpage.BuildField("HeroOverlayImageSubTitle").SetFrom("SE.AssessmentToolResultpage", "HeroOverlayImageSubTitle", isTemplate: true);
        mAssessmentToolResultpage.BuildField("HeroOverlayImageMediaPerformanceSeeker").SetFrom("SE.AssessmentToolResultpage", "HeroOverlayImageMediaPerformanceSeeker", isTemplate: true);
        mAssessmentToolResultpage.BuildField("HeroOverlayImageMediaDesignDictator").SetFrom("SE.AssessmentToolResultpage", "HeroOverlayImageMediaDesignDictator", isTemplate: true);
        mAssessmentToolResultpage.BuildField("HeroOverlayImageMediaCastleCreator").SetFrom("SE.AssessmentToolResultpage", "HeroOverlayImageMediaCastleCreator", isTemplate: true);
        mAssessmentToolResultpage.BuildField("HeroOverlayImageIsGradient").SetFrom("SE.AssessmentToolResultpage", "HeroOverlayImageIsGradient", isTemplate: true);
        mAssessmentToolResultpage.BuildField("CTAButtons").SetFrom("SE.AssessmentToolResultpage", "CTAButtons", isTemplate: true);
        mAssessmentToolResultpage.BuildField("EmailFormPersonalisedGreeting").SetFrom("SE.AssessmentToolResultpage", "EmailFormPersonalisedGreeting", isTemplate: true);
        mAssessmentToolResultpage.BuildField("EmailFormMainText").SetFrom("SE.AssessmentToolResultpage", "EmailFormMainText", isTemplate: true);
        mAssessmentToolResultpage.BuildField("EmailFormOptInHeader").SetFrom("SE.AssessmentToolResultpage", "EmailFormOptInHeader", isTemplate: true);
        mAssessmentToolResultpage.BuildField("EmailFormOptInText").SetFrom("SE.AssessmentToolResultpage", "EmailFormOptInText", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYouPersonalisedHeader").SetFrom("SE.AssessmentToolResultpage", "ThankYouPersonalisedHeader", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYouMainText").SetFrom("SE.AssessmentToolResultpage", "ThankYouMainText", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYouSecondaryHeader").SetFrom("SE.AssessmentToolResultpage", "ThankYouSecondaryHeader", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYou1IconBulletInfoSVGIcon").SetFrom("SE.AssessmentToolResultpage", "ThankYou1IconBulletInfoSVGIcon", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYou1IconBulletInfoHeading").SetFrom("SE.AssessmentToolResultpage", "ThankYou1IconBulletInfoHeading", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYou1IconBulletInfoInfoText").SetFrom("SE.AssessmentToolResultpage", "ThankYou1IconBulletInfoInfoText", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYou1IconBulletInfoButtonText").SetFrom("SE.AssessmentToolResultpage", "ThankYou1IconBulletInfoButtonText", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYou2IconBulletInfoSVGIcon").SetFrom("SE.AssessmentToolResultpage", "ThankYou2IconBulletInfoSVGIcon", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYou2IconBulletInfoHeading").SetFrom("SE.AssessmentToolResultpage", "ThankYou2IconBulletInfoHeading", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYou2IconBulletInfoInfoText").SetFrom("SE.AssessmentToolResultpage", "ThankYou2IconBulletInfoInfoText", isTemplate: true);
        mAssessmentToolResultpage.BuildField("ThankYou2IconBulletInfoButtonText").SetFrom("SE.AssessmentToolResultpage", "ThankYou2IconBulletInfoButtonText", isTemplate: true);

        // ==================== SE.CashSales ====================
        var mCashSales = new MultiClassMapping("SE.CashSales", target =>
        {
            target.ClassName = "SE.CashSales";
            target.ClassTableName = "SE_CashSales";
            target.ClassDisplayName = "Cash Sales";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSales.BuildField("CashSalesID").AsPrimaryKey();
        mCashSales.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSales.BuildField("SocialShareTitle").SetFrom("SE.CashSales", "SocialShareTitle");
        mCashSales.BuildField("SocialShareDescription").SetFrom("SE.CashSales", "SocialShareDescription");
        mCashSales.BuildField("SocialShareImage").SetFrom("SE.CashSales", "SocialShareImage");
        mCashSales.BuildField("IncludeInSiteMap").SetFrom("SE.CashSales", "IncludeInSiteMap");

        // ==================== SE.CashSalesAccount ====================
        var mCashSalesAccount = new MultiClassMapping("SE.CashSalesAccount", target =>
        {
            target.ClassName = "SE.CashSalesAccount";
            target.ClassTableName = "SE_CashSalesAccount";
            target.ClassDisplayName = "Cash Sales Account";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesAccount.BuildField("CashSalesAccountID").AsPrimaryKey();
        mCashSalesAccount.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesAccount.BuildField("SocialShareTitle").SetFrom("SE.CashSalesAccount", "SocialShareTitle");
        mCashSalesAccount.BuildField("SocialShareDescription").SetFrom("SE.CashSalesAccount", "SocialShareDescription");
        mCashSalesAccount.BuildField("SocialShareImage").SetFrom("SE.CashSalesAccount", "SocialShareImage");
        mCashSalesAccount.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesAccount", "IncludeInSiteMap");

        // ==================== SE.CashSalesAccountAddressForm ====================
        var mCashSalesAccountAddressForm = new MultiClassMapping("SE.CashSalesAccountAddressForm", target =>
        {
            target.ClassName = "SE.CashSalesAccountAddressForm";
            target.ClassTableName = "SE_CashSalesAccountAddressForm";
            target.ClassDisplayName = "Cash Sales Account Address Form";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesAccountAddressForm.BuildField("CashSalesAccountAddressFormID").AsPrimaryKey();
        mCashSalesAccountAddressForm.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesAccountAddressForm.BuildField("SocialShareTitle").SetFrom("SE.CashSalesAccountAddressForm", "SocialShareTitle");
        mCashSalesAccountAddressForm.BuildField("SocialShareDescription").SetFrom("SE.CashSalesAccountAddressForm", "SocialShareDescription");
        mCashSalesAccountAddressForm.BuildField("SocialShareImage").SetFrom("SE.CashSalesAccountAddressForm", "SocialShareImage");
        mCashSalesAccountAddressForm.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesAccountAddressForm", "IncludeInSiteMap");

        // ==================== SE.CashSalesAccountAddressList ====================
        var mCashSalesAccountAddressList = new MultiClassMapping("SE.CashSalesAccountAddressList", target =>
        {
            target.ClassName = "SE.CashSalesAccountAddressList";
            target.ClassTableName = "SE_CashSalesAccountAddressList";
            target.ClassDisplayName = "Cash Sales Account Address List";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesAccountAddressList.BuildField("CashSalesAccountAddressListID").AsPrimaryKey();
        mCashSalesAccountAddressList.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesAccountAddressList.BuildField("SocialShareTitle").SetFrom("SE.CashSalesAccountAddressList", "SocialShareTitle");
        mCashSalesAccountAddressList.BuildField("SocialShareDescription").SetFrom("SE.CashSalesAccountAddressList", "SocialShareDescription");
        mCashSalesAccountAddressList.BuildField("SocialShareImage").SetFrom("SE.CashSalesAccountAddressList", "SocialShareImage");
        mCashSalesAccountAddressList.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesAccountAddressList", "IncludeInSiteMap");

        // ==================== SE.CashSalesAccountOrderDetails ====================
        var mCashSalesAccountOrderDetails = new MultiClassMapping("SE.CashSalesAccountOrderDetails", target =>
        {
            target.ClassName = "SE.CashSalesAccountOrderDetails";
            target.ClassTableName = "SE_CashSalesAccountOrderDetails";
            target.ClassDisplayName = "Cash Sales Account Order Details";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesAccountOrderDetails.BuildField("CashSalesAccountOrderDetailsID").AsPrimaryKey();
        mCashSalesAccountOrderDetails.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesAccountOrderDetails.BuildField("SocialShareTitle").SetFrom("SE.CashSalesAccountOrderDetails", "SocialShareTitle");
        mCashSalesAccountOrderDetails.BuildField("SocialShareDescription").SetFrom("SE.CashSalesAccountOrderDetails", "SocialShareDescription");
        mCashSalesAccountOrderDetails.BuildField("SocialShareImage").SetFrom("SE.CashSalesAccountOrderDetails", "SocialShareImage");
        mCashSalesAccountOrderDetails.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesAccountOrderDetails", "IncludeInSiteMap");

        // ==================== SE.CashSalesAccountOrderHistory ====================
        var mCashSalesAccountOrderHistory = new MultiClassMapping("SE.CashSalesAccountOrderHistory", target =>
        {
            target.ClassName = "SE.CashSalesAccountOrderHistory";
            target.ClassTableName = "SE_CashSalesAccountOrderHistory";
            target.ClassDisplayName = "Cash Sales Account Order History";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesAccountOrderHistory.BuildField("CashSalesAccountOrderHistoryID").AsPrimaryKey();
        mCashSalesAccountOrderHistory.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesAccountOrderHistory.BuildField("SocialShareTitle").SetFrom("SE.CashSalesAccountOrderHistory", "SocialShareTitle");
        mCashSalesAccountOrderHistory.BuildField("SocialShareDescription").SetFrom("SE.CashSalesAccountOrderHistory", "SocialShareDescription");
        mCashSalesAccountOrderHistory.BuildField("SocialShareImage").SetFrom("SE.CashSalesAccountOrderHistory", "SocialShareImage");
        mCashSalesAccountOrderHistory.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesAccountOrderHistory", "IncludeInSiteMap");

        // ==================== SE.CashSalesCart ====================
        var mCashSalesCart = new MultiClassMapping("SE.CashSalesCart", target =>
        {
            target.ClassName = "SE.CashSalesCart";
            target.ClassTableName = "SE_CashSalesCart";
            target.ClassDisplayName = "Cash Sales Cart";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesCart.BuildField("CashSalesCartID").AsPrimaryKey();
        mCashSalesCart.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesCart.BuildField("SocialShareTitle").SetFrom("SE.CashSalesCart", "SocialShareTitle");
        mCashSalesCart.BuildField("SocialShareDescription").SetFrom("SE.CashSalesCart", "SocialShareDescription");
        mCashSalesCart.BuildField("SocialShareImage").SetFrom("SE.CashSalesCart", "SocialShareImage");
        mCashSalesCart.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesCart", "IncludeInSiteMap");

        // ==================== SE.CashSalesCheckout ====================
        var mCashSalesCheckout = new MultiClassMapping("SE.CashSalesCheckout", target =>
        {
            target.ClassName = "SE.CashSalesCheckout";
            target.ClassTableName = "SE_CashSalesCheckout";
            target.ClassDisplayName = "Cash Sales Checkout";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesCheckout.BuildField("CashSalesCheckoutID").AsPrimaryKey();
        mCashSalesCheckout.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesCheckout.BuildField("SocialShareTitle").SetFrom("SE.CashSalesCheckout", "SocialShareTitle");
        mCashSalesCheckout.BuildField("SocialShareDescription").SetFrom("SE.CashSalesCheckout", "SocialShareDescription");
        mCashSalesCheckout.BuildField("SocialShareImage").SetFrom("SE.CashSalesCheckout", "SocialShareImage");
        mCashSalesCheckout.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesCheckout", "IncludeInSiteMap");

        // ==================== SE.CashSalesLogin ====================
        var mCashSalesLogin = new MultiClassMapping("SE.CashSalesLogin", target =>
        {
            target.ClassName = "SE.CashSalesLogin";
            target.ClassTableName = "SE_CashSalesLogin";
            target.ClassDisplayName = "Cash Sales Login";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesLogin.BuildField("CashSalesLoginID").AsPrimaryKey();
        mCashSalesLogin.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesLogin.BuildField("SocialShareTitle").SetFrom("SE.CashSalesLogin", "SocialShareTitle");
        mCashSalesLogin.BuildField("SocialShareDescription").SetFrom("SE.CashSalesLogin", "SocialShareDescription");
        mCashSalesLogin.BuildField("SocialShareImage").SetFrom("SE.CashSalesLogin", "SocialShareImage");
        mCashSalesLogin.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesLogin", "IncludeInSiteMap");

        // ==================== SE.CashSalesPayment ====================
        var mCashSalesPayment = new MultiClassMapping("SE.CashSalesPayment", target =>
        {
            target.ClassName = "SE.CashSalesPayment";
            target.ClassTableName = "SE_CashSalesPayment";
            target.ClassDisplayName = "Cash Sales Payment";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesPayment.BuildField("CashSalesPaymentID").AsPrimaryKey();
        mCashSalesPayment.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesPayment.BuildField("SocialShareTitle").SetFrom("SE.CashSalesPayment", "SocialShareTitle");
        mCashSalesPayment.BuildField("SocialShareDescription").SetFrom("SE.CashSalesPayment", "SocialShareDescription");
        mCashSalesPayment.BuildField("SocialShareImage").SetFrom("SE.CashSalesPayment", "SocialShareImage");
        mCashSalesPayment.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesPayment", "IncludeInSiteMap");

        // ==================== SE.CashSalesSummary ====================
        var mCashSalesSummary = new MultiClassMapping("SE.CashSalesSummary", target =>
        {
            target.ClassName = "SE.CashSalesSummary";
            target.ClassTableName = "SE_CashSalesSummary";
            target.ClassDisplayName = "Cash Sales Summary";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCashSalesSummary.BuildField("CashSalesSummaryID").AsPrimaryKey();
        mCashSalesSummary.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mCashSalesSummary.BuildField("SocialShareTitle").SetFrom("SE.CashSalesSummary", "SocialShareTitle");
        mCashSalesSummary.BuildField("SocialShareDescription").SetFrom("SE.CashSalesSummary", "SocialShareDescription");
        mCashSalesSummary.BuildField("SocialShareImage").SetFrom("SE.CashSalesSummary", "SocialShareImage");
        mCashSalesSummary.BuildField("IncludeInSiteMap").SetFrom("SE.CashSalesSummary", "IncludeInSiteMap");

        // ==================== SE.CompleteUserRegistrationPage ====================
        var mCompleteUserRegistrationPage = new MultiClassMapping("SE.CompleteUserRegistrationPage", target =>
        {
            target.ClassName = "SE.CompleteUserRegistrationPage";
            target.ClassTableName = "SE_CompletUserRegistrationPage";
            target.ClassDisplayName = "Complete User Registration Page";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mCompleteUserRegistrationPage.BuildField("CompletUserRegistrationPageID").AsPrimaryKey();
        mCompleteUserRegistrationPage.UseResusableSchema(schemaName);
        
        // Reusable schema fields
        mCompleteUserRegistrationPage.BuildField("SocialShareTitle").SetFrom("SE.CompleteUserRegistrationPage", "SocialShareTitle");
        mCompleteUserRegistrationPage.BuildField("SocialShareDescription").SetFrom("SE.CompleteUserRegistrationPage", "SocialShareDescription");
        mCompleteUserRegistrationPage.BuildField("SocialShareImage").SetFrom("SE.CompleteUserRegistrationPage", "SocialShareImage");
        mCompleteUserRegistrationPage.BuildField("IncludeInSiteMap").SetFrom("SE.CompleteUserRegistrationPage", "IncludeInSiteMap");
        
        // Page-specific fields
        mCompleteUserRegistrationPage.BuildField("TempField").SetFrom("SE.CompleteUserRegistrationPage", "TempField", isTemplate: true);

        // ==================== SE.FaqDetailPage ====================
        var mFaqDetailPage = new MultiClassMapping("SE.FaqDetailPage", target =>
        {
            target.ClassName = "SE.FaqDetailPage";
            target.ClassTableName = "SE_FaqDetailPage";
            target.ClassDisplayName = "FAQ Detail Page";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mFaqDetailPage.BuildField("FaqDetailPageID").AsPrimaryKey();
        mFaqDetailPage.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mFaqDetailPage.BuildField("SocialShareTitle").SetFrom("SE.FaqDetailPage", "SocialShareTitle");
        mFaqDetailPage.BuildField("SocialShareDescription").SetFrom("SE.FaqDetailPage", "SocialShareDescription");
        mFaqDetailPage.BuildField("SocialShareImage").SetFrom("SE.FaqDetailPage", "SocialShareImage");
        mFaqDetailPage.BuildField("IncludeInSiteMap").SetFrom("SE.FaqDetailPage", "IncludeInSiteMap");

        // ==================== SE.GenericPage ====================
        var mGenericPage = new MultiClassMapping("SE.GenericPage", target =>
        {
            target.ClassName = "SE.GenericPage";
            target.ClassTableName = "SE_GenericPage";
            target.ClassDisplayName = "Generic Page";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mGenericPage.BuildField("GenericPageID").AsPrimaryKey();
        mGenericPage.UseResusableSchema(schemaName);
        
        // Reusable schema fields only
        mGenericPage.BuildField("SocialShareTitle").SetFrom("SE.GenericPage", "SocialShareTitle");
        mGenericPage.BuildField("SocialShareDescription").SetFrom("SE.GenericPage", "SocialShareDescription");
        mGenericPage.BuildField("SocialShareImage").SetFrom("SE.GenericPage", "SocialShareImage");
        mGenericPage.BuildField("IncludeInSiteMap").SetFrom("SE.GenericPage", "IncludeInSiteMap");

        // ==================== SE.ProductCatalogueLandingPage ====================
        var mProductCatalogueLandingPage = new MultiClassMapping("SE.ProductCatalogueLandingPage", target =>
        {
            target.ClassName = "SE.ProductCatalogueLandingPage";
            target.ClassTableName = "SE_ProductCatalogueLandingPage";
            target.ClassDisplayName = "Product Catalogue Landing Page";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mProductCatalogueLandingPage.BuildField("ProductCatalogueLandingPageID").AsPrimaryKey();
        mProductCatalogueLandingPage.UseResusableSchema(schemaName);
        
        // Reusable schema fields
        mProductCatalogueLandingPage.BuildField("SocialShareTitle").SetFrom("SE.ProductCatalogueLandingPage", "SocialShareTitle");
        mProductCatalogueLandingPage.BuildField("SocialShareDescription").SetFrom("SE.ProductCatalogueLandingPage", "SocialShareDescription");
        mProductCatalogueLandingPage.BuildField("SocialShareImage").SetFrom("SE.ProductCatalogueLandingPage", "SocialShareImage");
        mProductCatalogueLandingPage.BuildField("IncludeInSiteMap").SetFrom("SE.ProductCatalogueLandingPage", "IncludeInSiteMap");
        
        // Page-specific fields
        mProductCatalogueLandingPage.BuildField("Title").SetFrom("SE.ProductCatalogueLandingPage", "Title", isTemplate: true);
        mProductCatalogueLandingPage.BuildField("NavId").SetFrom("SE.ProductCatalogueLandingPage", "NavId", isTemplate: true);
        mProductCatalogueLandingPage.BuildField("ComingSoon").SetFrom("SE.ProductCatalogueLandingPage", "ComingSoon", isTemplate: true);
        mProductCatalogueLandingPage.BuildField("Checksum_ProductCategoryNavTree").SetFrom("SE.ProductCatalogueLandingPage", "Checksum_ProductCategoryNavTree", isTemplate: true);
        mProductCatalogueLandingPage.BuildField("Checksum_RangeNavTree").SetFrom("SE.ProductCatalogueLandingPage", "Checksum_RangeNavTree", isTemplate: true);
        mProductCatalogueLandingPage.BuildField("Icon").SetFrom("SE.ProductCatalogueLandingPage", "Icon", isTemplate: true);

        // ==================== SE.ProductCatalogueListingPage ====================
        var mProductCatalogueListingPage = new MultiClassMapping("SE.ProductCatalogueListingPage", target =>
        {
            target.ClassName = "SE.ProductCatalogueListingPage";
            target.ClassTableName = "SE_ProductCatalogueListingPage";
            target.ClassDisplayName = "Product Catalogue Listing Page";
            target.ClassType = ClassType.CONTENT_TYPE;
            target.ClassContentTypeType = ClassContentTypeType.WEBSITE;
        });

        mProductCatalogueListingPage.BuildField("ProductCatalogueListingPageID").AsPrimaryKey();
        mProductCatalogueListingPage.UseResusableSchema(schemaName);
        
        // Reusable schema fields
        mProductCatalogueListingPage.BuildField("SocialShareTitle").SetFrom("SE.ProductCatalogueListingPage", "SocialShareTitle");
        mProductCatalogueListingPage.BuildField("SocialShareDescription").SetFrom("SE.ProductCatalogueListingPage", "SocialShareDescription");
        mProductCatalogueListingPage.BuildField("SocialShareImage").SetFrom("SE.ProductCatalogueListingPage", "SocialShareImage");
        mProductCatalogueListingPage.BuildField("IncludeInSiteMap").SetFrom("SE.ProductCatalogueListingPage", "IncludeInSiteMap");
        
        // Page-specific fields
        mProductCatalogueListingPage.BuildField("Title").SetFrom("SE.ProductCatalogueListingPage", "Title", isTemplate: true);
        mProductCatalogueListingPage.BuildField("ItemsPerPage").SetFrom("SE.ProductCatalogueListingPage", "ItemsPerPage", isTemplate: true);
        mProductCatalogueListingPage.BuildField("NavId").SetFrom("SE.ProductCatalogueListingPage", "NavId", isTemplate: true);
        mProductCatalogueListingPage.BuildField("ComingSoon").SetFrom("SE.ProductCatalogueListingPage", "ComingSoon", isTemplate: true);
        mProductCatalogueListingPage.BuildField("ProductSection").SetFrom("SE.ProductCatalogueListingPage", "ProductSection", isTemplate: true);
        mProductCatalogueListingPage.BuildField("Checksum").SetFrom("SE.ProductCatalogueListingPage", "Checksum", isTemplate: true);
        mProductCatalogueListingPage.BuildField("Checksum_Products").SetFrom("SE.ProductCatalogueListingPage", "Checksum_Products", isTemplate: true);
        mProductCatalogueListingPage.BuildField("Icon").SetFrom("SE.ProductCatalogueListingPage", "Icon", isTemplate: true);

        // Register all mappings
        serviceCollection.AddSingleton<IClassMapping>(mHome);
        serviceCollection.AddSingleton<IClassMapping>(mArticlePage);
        serviceCollection.AddSingleton<IClassMapping>(mAssessmentToolFormPage);
        serviceCollection.AddSingleton<IClassMapping>(mAssessmentToolResultpage);
        serviceCollection.AddSingleton<IClassMapping>(mCashSales);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesAccount);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesAccountAddressForm);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesAccountAddressList);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesAccountOrderDetails);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesAccountOrderHistory);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesCart);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesCheckout);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesLogin);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesPayment);
        serviceCollection.AddSingleton<IClassMapping>(mCashSalesSummary);
        serviceCollection.AddSingleton<IClassMapping>(mCompleteUserRegistrationPage);
        serviceCollection.AddSingleton<IClassMapping>(mFaqDetailPage);
        serviceCollection.AddSingleton<IClassMapping>(mGenericPage);
        serviceCollection.AddSingleton<IClassMapping>(mProductCatalogueLandingPage);
        serviceCollection.AddSingleton<IClassMapping>(mProductCatalogueListingPage);
        
        // Register reusable schema builder
        serviceCollection.AddSingleton<IReusableSchemaBuilder>(sb);

        return serviceCollection;
    }
}
