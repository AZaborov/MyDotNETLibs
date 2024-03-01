/*
 * Библиотека, позволяющая работать с обычными массивами, как с листами
 * Автор: Заборов Артемий
 * Создано: 10.03.2020
 * Последнее обновление: 12.01.2021
 */
using System;

namespace DynamicArrayLibrary
{
    public static class DynamicArray
    {
        /// <summary>
        /// Добавить в конец массива элемент.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        public static T[] Add<T>(this T[] array, T element)
        {
            T[] dummy = array;
            array = new T[dummy.Length + 1];

            for (int i = 0; i < dummy.Length; i++)
                array[i] = dummy[i]; 

            array[array.Length - 1] = element;

            return array;
        }

        /// <summary>
        /// Добавить в конец массива несколько элементов.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="range"></param>
        public static T[] AddRange<T>(this T[] array, T[] range)
        {
            T[] dummy = array;
            array = new T[dummy.Length + range.Length];

            for (int i = 0; i < dummy.Length; i++)
                array[i] = dummy[i];

            for (int i = dummy.Length; i < array.Length; i++)
                array[i] = range[i - dummy.Length];

            return array;
        }

        /// <summary>
        /// Вставить элемент в массив.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <param name="position"></param>
        public static T[] Insert<T>(this T[] array, T element, int position)
        {
            if (position < 0 || position > array.Length)
                throw new Exception("Индекс находился за пределами диапазона массива");

            T[] dummy = array;
            array = new T[dummy.Length + 1];

            for (int i = 0; i < position; i++)
                array[i] = dummy[i];

            array[position] = element;

            for (int i = position + 1; i < array.Length; i++)
                array[i] = dummy[i - 1];

            return array;
        }

        /// <summary>
        /// Убрать элемент из массива.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="position"></param>
        public static T[] RemoveAt<T>(this T[] array, int position)
        {
            if (position < 0 || position > array.Length)
                throw new Exception("Индекс находился за пределами диапазона массива");

            T[] dummy = array;
            array = new T[dummy.Length - 1];

            for (int i = 0; i < position; i++)
                array[i] = dummy[i];

            for (int i = position; i < array.Length; i++)
                array[i] = dummy[i + 1];

            return array;
        }

        /// <summary>
        /// Убрать несколько элементов из массива.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static T[] RemoveRange<T>(this T[] array, int from, int to)
        {
            int range = to - from;

            if (from < 0 || to > array.Length)
                throw new Exception("Индекс находился за пределами диапазона массива");

            if (range < 1)
                throw new Exception("Недопустимое значение диапазона");

            T[] dummy = array;
            array = new T[dummy.Length - range];

            for (int i = 0; i < from; i++)
                array[i] = dummy[i];

            for (int i = to; i < dummy.Length; i++)
                array[i - range] = dummy[i];

            return array;
        }

        /// <summary>
        /// Получить самый маленький элемент массива.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T GetMin<T>(this T[] array) where T: IComparable
        {
            if (array.Length == 0)
                throw new Exception("Массив пуст");

            T CurrentMin = array[0];
            for (int i = 0; i < array.Length; i++)
                if (array[i].CompareTo(CurrentMin) < 0)
                    CurrentMin = array[i];

            return CurrentMin;
        }

        /// <summary>
        /// Получить самый большой элемент массива.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T GetMax<T>(this T[] array) where T : IComparable
        {
            if (array.Length == 0)
                throw new Exception("Массив пуст");

            T CurrentMax = array[0];
            for (int i = 0; i < array.Length; i++)
                if (array[i].CompareTo(CurrentMax) > 0)
                    CurrentMax = array[i];

            return CurrentMax;
        }

        /// <summary>
        /// Получить индекс самого маленького элемента массива.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int GetIndexOfMin<T>(this T[] array) where T : IComparable
        {
            if (array.Length == 0)
                throw new Exception("Массив пуст");

            T CurrentMin = array[0];
            int CurrentMinIndex = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i].CompareTo(CurrentMin) < 0)
                {
                    CurrentMin = array[i];
                    CurrentMinIndex = i;
                }

            return CurrentMinIndex;
        }

        /// <summary>
        /// Получить индекс самого большого элемента массива.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int GetIndexOfMax<T>(this T[] array) where T : IComparable
        {
            if (array.Length == 0)
                throw new Exception("Массив пуст");

            T CurrentMax = array[0];
            int CurrentMaxIndex = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i].CompareTo(CurrentMax) > 0)
                {
                    CurrentMax = array[i];
                    CurrentMaxIndex = i;
                }

            return CurrentMaxIndex;
        }

        private static int Partition<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            int i = start;
            for (int j = start; j <= end; j++)
            {
                if (array[j].CompareTo(array[end]) <= 0)
                {
                    T dummy = array[i];
                    array[i] = array[j];
                    array[j] = dummy;
                    i++;        
                }
            }
            return i - 1;
        }

        // Перегрузка для рекурсии
        private static void QuickSort<T>(T[] array, int start, int end) where T : IComparable<T>
        {                                        
            if (start >= end) return;
            int partition = Partition(array, start, end);
            QuickSort(array, start, partition - 1);
            QuickSort(array, partition + 1, end);
        }

        /// <summary>
        /// Отсортировать массив.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] QuickSort<T>(this T[] array) where T : IComparable<T>
        {
            if (array.Length == 0)
                throw new Exception("Массив пуст");

            int start = 0;
            int end = array.Length - 1;
            int partition = Partition(array, start, end);
            QuickSort(array, start, partition - 1);
            QuickSort(array, partition + 1, end);

            return array;
        }

        /// <summary>
        /// Преобразовать двумерный массив в одномерный.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] Flatten<T>(this T[,] array)
        {
            int size = array.Length;
            T[] result = new T[size];

            int write = 0;

            for (int i = 0; i <= array.GetUpperBound(0); i++)
                for (int z = 0; z <= array.GetUpperBound(1); z++)
                    result[write++] = array[i, z];

            return result;
        }

        /// <summary>
        /// Объединить n-ое количество массивов.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static T[] Concat<T>(params T[][] arrays)
        {
            T[] result = new T[0];

            for (int i = 0; i < arrays.Length; i++)
                result = result.AddRange(arrays[i]);

            return result;
        }

        /// <summary>
        /// Конвертировать массив одного типа в массив другого.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static void ConvertTo<T1, T2>(this T1[] input, out T2[] output)
        {
            output = new T2[input.Length];

            for (int i = 0; i < input.Length; i++)
                output[i] = (T2)Convert.ChangeType(input[i], typeof(T2));
        }

        /// <summary>
        /// Вывести массив в консоль.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void Print<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.Write(array[i].ToString() + " ");

            Console.WriteLine();
        }
    }
}
