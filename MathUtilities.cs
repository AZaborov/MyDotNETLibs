using System;
using System.Text.RegularExpressions;

namespace MathUtilitiesLibrary
{
    static class MathUtilities
    {
        // Функции активации
        public static double Sigmoid(double value) => 1.0d / (1.0d + (double)Math.Exp(-value));
        
        public static double[] Softmax(double[] values)
        {
            double[] Exps = new double[values.Length];
            for (int i = 0; i < Exps.Length; i++)
                Exps[i] = Math.Exp(values[i]);

            double ExpsSum = 0;
            for (int i = 0; i < Exps.Length; i++)
                ExpsSum += Exps[i];

            double[] Results = new double[Exps.Length];
            for (int i = 0; i < Results.Length; i++)
                Results[i] = Exps[i] / ExpsSum;

            return Results;
        }

        public static double ReLU(double value) => value < 0 ? 0 : value;
        
        // Преобразовать число из одного диапазона в другой
        public static double ConvertRange(double originalStart, double originalEnd, double newStart, double newEnd, double value)
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return newStart + ((value - originalStart) * scale);
        }

        // Округлить вещественное число до определённого числа цифр после запятой
        public static string Truncate(double value, int precision)
        {
            string format = $"{{0:f{(precision < 17 ? 17 : precision)}}}";
            string str = string.Format(format, value);
            Regex regex = new Regex(@"-?\d+[\.\,]\d" + $"{{{precision}}}");
            return regex.Match(str).Value;
        }
    }
}
