using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Abstractions.Validators;
using Calculator.Models;
using Moq;

namespace CalculatorTests.Mocks
{
    static class ArithmeticValidatorMock
    {
        private static IArithmeticValidator<CalculatorValue> _mock;

        public static IArithmeticValidator<CalculatorValue> GetMock()
        {
           if(_mock == null)
               SetMock();
           return _mock;
        }

        private static void SetMock()
        {
            var mock = new Mock<IArithmeticValidator<CalculatorValue>>();

            mock.Setup(foo => foo
                    .ValidateIsNull(It.Is<CalculatorValue>(cv => !string.IsNullOrEmpty(cv.Name) && cv.Name != "NULL")))
                .Returns(false);

            mock.Setup(foo => foo.ValidateIsNull(null)).Returns(true);

            mock.Setup(foo => foo
                .ValidateIsEnumerableEmpty(It.Is<IEnumerable<CalculatorValue>>(a => a.ToArray().Length > 0)))
                .Returns(false);

            mock.Setup(foo => foo
                    .ValidateIsEnumerableEmpty(It.Is<IEnumerable<CalculatorValue>>(a => a.ToArray().Length == 0)))
                .Returns(true);

            mock.Setup(foo => foo.ValidateIsEnumerableEmpty(null)).Returns(true);

            _mock = mock.Object;
        }
    }
}
