using System.Collections.Generic;
using System.Collections.Specialized;
using Sitecore.XA.Foundation.RenderingVariants.Fields;
using Sitecore.XA.Foundation.Variants.Abstractions.Fields;
using Sitecore.Data.Items;

namespace Sitecore.XA.Foundation.Variants.Extension.Models
{
    public class VariantNestedHtmlTag : RenderingVariantFieldBase
    {
        public IEnumerable<BaseVariantField> SectionFields { get; set; }
        public NameValueCollection HTMLCollection { get; set; }
        public VariantNestedHtmlTag(Item variantItem) : base(variantItem)
        {
            //this.HTMLCollection = WebUtil.ParseUrlParameters(((BaseItem)variantItem).Fields[Constants.Constants.RenderingVariants.Fields.NestingHTMLReferenceTag.HTMLCollection]?.Value);
        }
    }
}