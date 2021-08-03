
using System.Collections.Generic;
using Sitecore.XA.Foundation.Variants.Abstractions.Pipelines.ParseVariantFields;
using Sitecore.Data;
using Sitecore.XA.Foundation.Variants.Extension.Models;
using Sitecore.Web;
using Sitecore.XA.Foundation.Variants.Abstractions.Fields;
using Sitecore.XA.Foundation.Variants.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Sitecore.XA.Foundation.Variants.Extension.Parser
{
    public class ParseNestedHtmlTag : ParseVariantFieldProcessor
    {
        public override ID SupportedTemplateId => Constants.Constants.RenderingVariants.Templates.NestingHTMLReferenceTag;

        public override void TranslateField(ParseVariantFieldArgs args)
        {
            ParseVariantFieldArgs variantFieldArgs = args;
            var variantHtmlTag = new VariantNestedHtmlTag(args.VariantItem) { Tag = "section" };
            variantHtmlTag.HTMLCollection = WebUtil.ParseUrlParameters(args.VariantItem.Fields[Constants.Constants.RenderingVariants.Fields.NestingHTMLReferenceTag.HTMLCollection]?.Value);
            variantHtmlTag.SectionFields = args.VariantItem.Children.Count > 0 ? (IEnumerable<BaseVariantField>)((IVariantFieldParser)ServiceProviderServiceExtensions.GetService<IVariantFieldParser>(ServiceLocator.ServiceProvider)).ParseVariantFields(args.VariantItem, args.VariantRootItem, false) : (IEnumerable<BaseVariantField>)new List<BaseVariantField>();

            variantFieldArgs.TranslatedField = variantHtmlTag;
        }
    }
}