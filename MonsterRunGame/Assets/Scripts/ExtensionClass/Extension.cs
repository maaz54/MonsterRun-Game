using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Extensions
{
    public static class Extension
    {
        public static int GetFibonacciSequence(this int sequenceNumber)
        {
            if (sequenceNumber <= 0)
            {
                return -1; // Error condition
            }

            if (sequenceNumber == 1 || sequenceNumber == 2)
            {
                return 1; // The first two Fibonacci numbers are 1
            }

            int a = 1;
            int b = 1;
            int result = 0;

            for (int i = 3; i <= sequenceNumber; i++)
            {
                result = a + b;
                a = b;
                b = result;
            }
            return result;
        }
    }

}