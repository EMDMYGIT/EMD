﻿using System;

namespace Datalist
{
    public class DatalistColumnAttribute : Attribute
    {
        public Int32 Position { get; set; }
        public Boolean Hidden { get; set; }
        public String Format { get; set; }

        public DatalistColumnAttribute()
        {
        }
        public DatalistColumnAttribute(Int32 position)
        {
            Position = position;
        }
    }
}
