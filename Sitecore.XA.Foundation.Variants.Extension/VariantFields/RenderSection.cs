using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Pipelines;
using Sitecore.XA.Foundation.RenderingVariants.Pipelines.GetAttributeTokenValue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
namespace Sitecore.XA.Foundation.Variants.Extension.VariantFields
{
    public class RenderSection  : Sitecore.XA.Foundation.RenderingVariants.Pipelines.RenderVariantField.RenderSection
    {
        protected override string GetAttributeTokenValue(string fieldName, Item item)
        {
            Field field = item.Fields[fieldName];
            if (field == null && !fieldName.StartsWith("Func"))
                return string.Empty;

            GetAttributeTokenValueArgs attributeTokenValueArgs = null;

            if (fieldName.StartsWith("Func"))
                attributeTokenValueArgs = new GetAttributeTokenValueArgs()
                {
                    Item = item,
                    Field = new KeyValuePair<string, string>(item.ID.ToString(), fieldName)
                };
            else
                attributeTokenValueArgs = new GetAttributeTokenValueArgs(item, field);

            CorePipeline.Run("getAttributeTokenValue", (PipelineArgs)attributeTokenValueArgs);
            return attributeTokenValueArgs.Result;
        }
    }
}