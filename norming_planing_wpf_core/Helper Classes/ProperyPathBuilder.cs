using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace norming_planing_wpf_core
{
    internal  class ProperyPathCollection
    {
        private static  List<PropertyPathFormat> properies = new List<PropertyPathFormat>();
        public static PropertyPathFormat? AddPropertyPathFormat(string format, int typeCode)
        {
            PropertyPathFormat? property = null;
            if (IsTypeCodeUnique(typeCode))
            {
                property = new PropertyPathFormat(format, typeCode);
                properies.Add(property);    
            }

            return property;
        }

        public static string GetPropertyPath(int typeCode, params object[] values)
        {
            return String.Format(properies.First(p => p.TypeCode == typeCode).Format, values);
        }
        private static bool IsTypeCodeUnique(int typeCode)
        {
            return true;
        }
  
    }

    public class PropertyPathFormat
    {
        public PropertyPathFormat(string format, int typeCode)
        {
            Format = format;
            TypeCode = typeCode;
        }

        public string Format { get; set; }
        public int TypeCode { get; set; }
    }
}
