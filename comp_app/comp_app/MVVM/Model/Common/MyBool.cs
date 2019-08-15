using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace comp_app.MVVM.Model.Common
{
    public sealed class __bool : IConvertible
    {
        public __bool()
        {
            __value = false;
        }

        public __bool(bool val)
        {
            __value = val;
        }

        private Boolean __value;

        public TypeCode GetTypeCode()
        {
            return TypeCode.Boolean;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return __value;
        }
        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }        
        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public float ToSingle(IFormatProvider provider)
        {            
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            return __value ? "1" : "0";
        }

        public override string ToString()
        {
            return __value ? "1" : "0";
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(__value, conversionType);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }



        public static implicit operator __bool(Boolean value)
        {            
            return new __bool() { __value = value };
        }

        public static implicit operator __bool(String value)
        {
            return false;//new __bool() { __value = (new String[] { "True", "true", "TRUE", "1" }).Any(arr_val => arr_val == value) };
        }
        
        public static implicit operator string(__bool value)
        {
            return (new String[] { "True", "true", "TRUE", "1" }).Any(arr_val => arr_val == value) ? "1" : "0";
        }



    }    

    public static class StringExtensions
    {
        public static string ToString(this __bool value)
        {
            return value.ToString();
        }
    }
}
