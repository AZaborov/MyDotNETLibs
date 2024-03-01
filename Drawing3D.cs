using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TM_L2_Zaborov
{
    class Drawing3D
    {
        public double AngleX { get; set; }
        public double AngleY { get; set; }
        public double AngleZ { get; set; }

        public Point Project(double[,] vector, int multx, int multy)
        {
            double[,] Rotated = MultiplyVectors(GetRotationMatX(), vector);
            Rotated = MultiplyVectors(GetRotationMatY(), Rotated);
            Rotated = MultiplyVectors(GetRotationMatZ(), Rotated);

            int X = (int)(Rotated[0, 0] * multx);
            int Y = (int)(Rotated[1, 0] * multy);

            return new Point(X, Y);
        }

        public double GetDistanceFromCamera(double[,] vector)
        {
            double[,] Rotated = MultiplyVectors(GetRotationMatX(), vector);
            Rotated = MultiplyVectors(GetRotationMatY(), Rotated);
            Rotated = MultiplyVectors(GetRotationMatZ(), Rotated);

            double[,] Projected = MultiplyVectors(GetProjectionMat(), Rotated);

            return Projected[1, 0];
        }

        private double[,] GetProjectionMat() => new double[,]
        {
            { 1, 0, 0 },
            { 0, 1, 0 }
        };

        private double[,] GetRotationMatX() => new double[,]
        {
            { 1, 0, 0 },
            { 0, Math.Cos(AngleX), -Math.Sin(AngleX) },
            { 0, Math.Sin(AngleX), Math.Cos(AngleX) },
        };

        private double[,] GetRotationMatY() => new double[,]
        {
            { Math.Cos(AngleY), 0, -Math.Sin(AngleY) },
            { 0, 1, 0 },
            { Math.Sin(AngleY), 0, Math.Cos(AngleY) },
        };

        private double[,] GetRotationMatZ() => new double[,]
        {
            { Math.Cos(AngleZ), -Math.Sin(AngleZ), 0 },
            { Math.Sin(AngleZ), Math.Cos(AngleZ), 0 },
            { 0, 0, 1 }
        };

        private double[,] MultiplyVectors(double[,] vec1, double[,] vec2)
        {
            double[,] Result = new double[vec1.GetLength(0), vec2.GetLength(1)];

            for (int i = 0; i < vec1.GetLength(0); i++)
                for (int j = 0; j < vec2.GetLength(1); j++)
                    for (int k = 0; k < vec2.GetLength(0); k++)
                        Result[i, j] += vec1[i, k] * vec2[k, j];

            return Result;
        }
    }
}
