namespace GameOfLife.Common.Extensions;

public static class ArrayExtensions
{
    public static T[,] ToMatrix<T>(this T[][] array)
    {
        var width = array.Length;
        var height = 0;

        foreach (var row in array)
            height = Math.Max(height, row.Length);

        var bArray = new T[width, height];

        for (var i = 0; i < array.Length; i++)
            for (var j = 0; j < array[i].Length; j++)
                bArray[i, j] = array[i][j];

        return bArray;
    }

    public static T[][] ToArrayOfArray<T>(this T[,] array)
    {
        var arrayOfArray = new T[array.GetLength(0)][];

        for (var i = 0; i < array.GetLength(0); i++)
        {
            arrayOfArray[i] = new T[array.GetLength(1)];

            for (var j = 0; j < array.GetLength(1); j++)
            {
                arrayOfArray[i][j] = array[i, j];
            }
        }

        return arrayOfArray;
    }
}

