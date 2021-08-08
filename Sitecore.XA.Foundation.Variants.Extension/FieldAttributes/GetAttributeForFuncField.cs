using Sitecore.Data.Items;
using Sitecore.XA.Foundation.RenderingVariants.Pipelines.GetAttributeTokenValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.XA.Foundation.Variants.Extension.FieldAttributes
{
    public class GetAttributeForFuncField : GetAttributeTokenValueProcessor<KeyValuePair<string, string>>
    {

        public override string GetAttributeToken(KeyValuePair<string, string> field)
        {

            if (string.IsNullOrEmpty(field.Key) || string.IsNullOrEmpty(field.Value))
                return null;

            if (!field.Value.StartsWith("Func"))
                return field.Value;

            var currentItem = Sitecore.Context.Database.GetItem(field.Key);
            return this.EvalFunc(currentItem, field.Value);

        }

        private string EvalFunc(Item currentItem, string FnConvention)
        {
            string output = string.Empty;
            FunctionFormat functionFormat = new FunctionFormat(FnConvention);
            string Fname = FnConvention.Split('{')[0];
            switch (Fname)
            {
                case "FuncIndex":
                    int counter = 1;
                    foreach (Item _it in currentItem.Parent.Children)
                    {
                        if (_it.ID.ToString() == currentItem.ID.ToString())
                            break;

                        counter++;
                    }
                    if (counter.ToString() == functionFormat.CompareValue)
                        output = functionFormat.TrueConditionValue;
                    else
                        output = functionFormat.FalseConditionValue;
                    break;
                case "FuncBool":
                    if (currentItem.Fields[functionFormat.FuncType].Value == functionFormat.CompareValue)
                        output = functionFormat.TrueConditionValue;
                    else
                        output = functionFormat.FalseConditionValue;
                    break;
                case "FuncContains":
                    if (currentItem.Fields[functionFormat.FuncType].Value.Contains(functionFormat.CompareValue))
                        output = functionFormat.TrueConditionValue;
                    else
                        output = functionFormat.FalseConditionValue;
                    break;
                case "FuncExtactMatch":
                    if (currentItem.Fields[functionFormat.FuncType].Value == functionFormat.CompareValue)
                        output = functionFormat.TrueConditionValue;
                    else
                        output = functionFormat.FalseConditionValue;
                    break;
                default:
                    break;
            }
            return output;
        }
    }

    public class FunctionFormat
    {
        public FunctionFormat()
        {

        }
        public FunctionFormat(string funcConvention)
        {
            this.FuncType = funcConvention.Split('{')[1].Split('?')[0].Split('=')[0];
            this.CompareValue = funcConvention.Split('{')[1].Split('?')[0].Split('=')[1];
            this.TrueConditionValue = funcConvention.Split('?')[1].Split(':')[0].Replace("'", "").Replace("\"", "").Replace("{", "").Replace("}", "");
            this.FalseConditionValue = funcConvention.Split('?')[1].Split(':')[1].Replace("'", "").Replace("\"", "").Replace("{", "").Replace("}", ""); ;
        }
        public string TrueConditionValue { get; set; }
        public string FalseConditionValue { get; set; }
        public string FuncType { get; set; }
        public string CompareValue { get; set; }
    }
}