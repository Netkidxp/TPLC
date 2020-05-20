using FoamLib.UI.Post.Display;
using FoamLib.UI.Post.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Model
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PressureDropProperty
    {
        private string unitName1 = "";
        private string unitName2 = "";

        public static List<string> unitNames = new List<string>();
        private FoamUnitManager unitManager = null;

        [DisplayName("Unit1 Name"),TypeConverter(typeof(UnitNameConverter))]
        public string UnitName1
        {
            get => unitName1;
            set
            {
                unitName1 = value;
                PressureDrop = CalculatePressureDrop();
            }
        }

        [DisplayName("Unit2 Name"), TypeConverter(typeof(UnitNameConverter))]
        public string UnitName2
        {
            get => unitName2;
            set
            {
                unitName2 = value;
                PressureDrop = CalculatePressureDrop();
            }
        }

        [DisplayName("Pressure Drop (Unit1 - Unit2)"),ReadOnly(true)]
        public double PressureDrop { get; set; }

        [Browsable(false)]
        public FoamUnitManager UnitManager
        {
            get => unitManager;
            set
            {
                unitManager = value;
                unitNames.Clear();
                if (unitManager != null)
                {
                    List<DatasetUnit> us = unitManager.GetAll2DUnit();
                    foreach (DatasetUnit u in us)
                        unitNames.Add(u.Name);
                }
            }

        }

        public double CalculatePressureDrop()
        {
            if (UnitManager == null)
                return 0;
            return UnitManager.CalculatePressDrop(unitName1, unitName2);
        }
    }

    public class UnitNameConverter : StringConverter
    {
        //true enable,false disable
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(PressureDropProperty.unitNames); //编辑下拉框中的items
        }

        //true: disable text editting.    false: enable text editting;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
