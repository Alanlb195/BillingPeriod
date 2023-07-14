using System;
using System.Collections.Generic;

namespace BillingPeriod.Services.Pascal
{
    public static class PascalTriangleService
    {
        public static List<List<int>> GeneratePascalTriangle(int numRows)
        {
            List<List<int>> triangle = new List<List<int>>();

            for (int i = 0; i < numRows; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j <= i; j++)
                {
                    if (j == 0 || j == i)
                    {
                        row.Add(1);
                    }
                    else
                    {
                        int left = triangle[i - 1][j - 1];
                        int right = triangle[i - 1][j];
                        row.Add(left + right);
                    }
                }
                triangle.Add(row);
            }

            return triangle;
        }
    }
}
