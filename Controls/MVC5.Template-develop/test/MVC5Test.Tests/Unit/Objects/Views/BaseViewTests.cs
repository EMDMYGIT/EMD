﻿using MVC5Test.Objects;
using NSubstitute;
using System;
using Xunit;

namespace MVC5Test.Tests.Unit.Objects
{
    public class BaseViewTests
    {
        private BaseView view;

        public BaseViewTests()
        {
            view = Substitute.For<BaseView>();
        }

        #region CreationDate

        [Fact]
        public void CreationDate_ReturnsSameValue()
        {
            DateTime expected = view.CreationDate;
            DateTime actual = view.CreationDate;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
