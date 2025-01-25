using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntToArray 
{
    public int[] DigitsArray { get; private set; }
    public IntToArray(int num)
    {
        DigitsArray = IntToIntArray(num);
    }

    
    private int[] IntToIntArray(int num)
    {
        if (num == 0)
            return new int[1] { 0 };

        List<int> digits = new List<int>();

        while (num != 0)
        {
            digits.Add(num % 10);
            num /= 10;
        }

        int[] array = digits.ToArray();
        System.Array.Reverse(array);

        return array;
    }


}
