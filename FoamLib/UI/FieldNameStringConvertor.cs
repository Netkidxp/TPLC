using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.Model;
namespace FoamLib.UI
{
    public class FieldNameStringConvertor : StringConverter
    {
        //true enable,false disable
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(GlobalParameters.FieldNames); //编辑下拉框中的items
        }

        //true: disable text editting.    false: enable text editting;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
