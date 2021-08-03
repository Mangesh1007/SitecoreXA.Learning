using Sitecore.Pipelines;
using Sitecore.XA.Foundation.RenderingVariants.Pipelines.RenderVariantField;
using Sitecore.XA.Foundation.Variants.Abstractions.Fields;
using Sitecore.XA.Foundation.Variants.Abstractions.Models;
using Sitecore.XA.Foundation.Variants.Abstractions.Pipelines.RenderVariantField;
using Sitecore.XA.Foundation.Variants.Extension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Sitecore.XA.Foundation.Variants.Extension.Renderer
{
    public class RenderNestedHtmlTag : RenderRenderingVariantFieldProcessor
    {
        public override Type SupportedType => typeof(VariantNestedHtmlTag);
        public override RendererMode RendererMode => (RendererMode)1;

        public override void RenderField(RenderVariantFieldArgs args)
        {
            var variantField = args.VariantField as VariantNestedHtmlTag;
            if (variantField != null)
            {
                Control control = null;
                if (variantField.SectionFields.Any())
                {
                    control = new Control();
                    foreach (BaseVariantField sectionField in variantField.SectionFields)
                    {
                        RenderVariantFieldArgs variantFieldArgs = new RenderVariantFieldArgs()
                        {
                            VariantField = sectionField,
                            Item = args.Item,
                            HtmlHelper = args.HtmlHelper,
                            IsControlEditable = args.IsControlEditable,
                            IsFromComposite = args.IsFromComposite,
                            RendererMode = args.RendererMode,
                            Model = args.Model,
                            HrefOverrideFunc = args.HrefOverrideFunc,
                            Parameters = args.Parameters,
                            RenderingWebEditingParams = args.RenderingWebEditingParams
                        };
                        this.PipelineManager.Run("renderVariantField", (PipelineArgs)variantFieldArgs);
                        if (variantFieldArgs.ResultControl != null)
                            control.Controls.Add(variantFieldArgs.ResultControl);
                    }
                }

                var items = variantField.HTMLCollection
                                        .AllKeys
                                        .SelectMany(
                                             variantField.HTMLCollection.GetValues,
                                             (k, v) => new { key = k, value = v }
                                         );
                if (!items.Any())
                    return;


                string _master = "{0}";
                foreach (var row in items)
                {
                    if (!string.IsNullOrEmpty(row.key))
                    {
                        var tag = new HtmlGenericControl(row.key.Split('_')[0]);
                        if (row.value.Split('|').Any())
                        {
                            foreach (var seg in row.value.Split('|'))
                            {
                                tag.Attributes.Add(seg.Split('=')[0], (!string.IsNullOrEmpty(seg.Split('=')[1]) ? seg.Split('=')[1] : string.Empty));
                            }
                        }
                        tag.InnerText = "{0}";
                        _master = string.Format(_master, this.RenderControl(tag));
                    }

                }

                if (control != null)
                    _master = string.Format(_master, this.RenderControl(control));
                else
                    _master = _master.Replace("{0}", string.Empty);

                var topcontrol = new HtmlGenericControl("section");
                topcontrol.InnerHtml = _master;

                args.ResultControl = topcontrol;
                args.Result = this.RenderControl(args.ResultControl);
            }
        }
 
    }
}