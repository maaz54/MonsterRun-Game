using UnityEngine;

namespace Extensions
{
    public static class Extension
    {
        /// <summary>
        /// Calculates the nth number in the Fibonacci sequence.
        /// </summary>
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

        /// <summary>
        /// Formats the given time in minutes and seconds for display.
        /// </summary>
        public static string DisplayTime(this float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

}