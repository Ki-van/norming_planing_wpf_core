using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace norming_planing_wpf_core.Attributes
{
    public enum ParamAffilation
    {
        Detail,
        Assembly
    }
    public class MarkComponentParamAttribute: Attribute
    {
       
        public string Label { get; set; }
        public ParamAffilation ParamAffilation { get; set; }

        public MarkComponentParamAttribute(string label, ParamAffilation paramAffilation)
        {
            Label = label;
            ParamAffilation = paramAffilation;
        }

        public MarkComponentParamAttribute(string label)
        {
            Label = label;
            ParamAffilation = ParamAffilation.Detail;
        }
    }
}
