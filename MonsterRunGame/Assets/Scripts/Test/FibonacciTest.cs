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
        /// <summary>
        /// Unit tests for the Fibonacci sequence calculation.
        /// </summary>
        [Test]
        public void Fibonacci_CorrectValue_ReturnsExpected()
        {
            int input = 10;
            int result = input.GetFibonacciSequence();
            Assert.AreEqual(55, result);
        }
    }
}