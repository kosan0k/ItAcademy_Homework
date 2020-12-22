using System;

namespace Lesson5_Homework
{
    public class StudyTask
    {
        public Span<int> GetSubArrayWithMaxSumOfElements(int[] array) 
        {
            int maxSum = 0;
            int currentSum;
            int firstItemIndex = 0;
            int lastItemIndex = 0;


            for (int index = 0; index < array.Length; index++)
            {
                currentSum = array[index];

                for (int innerIndex = index + 1; innerIndex < array.Length - 1; innerIndex++) 
                {
                    currentSum += array[innerIndex];
                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                        firstItemIndex = index;
                        lastItemIndex = innerIndex;
                    }
                }
            }

            var resultSpan = new Span<int>(array, firstItemIndex, (lastItemIndex - firstItemIndex) + 1); //avoiding array copy
            return resultSpan;
        }
    }
}
