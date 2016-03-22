using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC5Course.Models
{
    internal class 至少須包含兩個空白Attribute : DataTypeAttribute
    {

        public 至少須包含兩個空白Attribute() :base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            string str = (string)value;

            return str.Count(_p => _p == ' ') == 2;
        }
    }
}