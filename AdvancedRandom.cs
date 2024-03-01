using System;
using System.Security.Cryptography;

namespace AdvancedRandomLibrary
{
    static class AdvancedRandom
    {
        /// <summary>
        /// Сгенерировать случайное целое число, включая минимум и не включая максимум
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
            if (min >= max)
                throw new Exception("Неправильный ввод");

            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            max -= 1;
            var bytes = new byte[sizeof(int)];
            generator.GetNonZeroBytes(bytes);
            var val = BitConverter.ToInt32(bytes, 0);
            var result = ((val - min) % (max - min + 1) + (max - min + 1)) % (max - min + 1) + min;
            return result;
        }

        /// <summary>
        /// Сгенерировать случайное вещественное число от 0 до 1, включая 0 и не включая 1
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double NextDouble()
        {
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            var bytes = new byte[sizeof(double)];
            generator.GetNonZeroBytes(bytes);
            var val = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            var result = val / (double)(1UL << 53);
            return result;
        }

        /// <summary>
        /// Сгенерировать истину с определённой вероятностью
        /// </summary>
        /// <returns></returns>
        public static bool NextBool(int probabilty)
        {
            if (probabilty < 0 || probabilty > 100)
                throw new Exception("Неправильная вероятность");

            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            int min = 0;
            int max = 99;
            var bytes = new byte[sizeof(int)];
            generator.GetNonZeroBytes(bytes);
            var val = BitConverter.ToInt32(bytes, 0);
            var result = ((val - min) % (max - min + 1) + (max - min + 1)) % (max - min + 1) + min;
            return result <= probabilty;
        }
    }
}
