﻿using System;
using Xunit;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    public class UInt32FilterTests
    {
        #region GetNumericValue()

        [Fact]
        public void GetNumericValue_ParsesValue()
        {
            UInt32Filter filter = new UInt32Filter();
            filter.Value = "4294967200";

            Object actual = filter.GetNumericValue();
            UInt32 expected = 4294967200;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetNumericValue_NotValidValue_ReturnsNull()
        {
            UInt32Filter filter = new UInt32Filter();
            filter.Value = "-1";

            Assert.Null(filter.GetNumericValue());
        }

        #endregion
    }
}
