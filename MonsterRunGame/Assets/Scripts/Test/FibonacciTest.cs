using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Extensions;

namespace Tests
{
    [TestFixture]
    public class FibonacciTest
    {
        [Test]
        public void Fibonacci_CorrectValue_ReturnsExpected()
        {
            int result = 10.GetFibonacciSequence();
            Assert.AreEqual(55, result);
        }
    }
}