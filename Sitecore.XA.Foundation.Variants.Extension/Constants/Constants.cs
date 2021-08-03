using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace Sitecore.XA.Foundation.Variants.Extension.Constants
{
    public static partial class Constants
    {
        public static partial class RenderingVariants
        {
            public static partial class Templates
            {
                public static ID NestingHTMLReferenceTag { get; } = new ID("{9E35E0DF-27CB-4C84-B086-540366387A93}");
            }

            public static partial class Fields
            {
                public static class NestingHTMLReferenceTag
                {
                    public static ID HTMLCollection { get; } = new ID("{B2811840-95A6-4BDC-AF0F-C180CCF41836}");
                }
            }
        }
    }
}