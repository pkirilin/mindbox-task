using System;

namespace GeometricService.Domain
{
    public static class FigureParametersConverter
    {
        public static byte[] ConvertToBytes(double[] array)
        {
            var bytes = new byte[array.Length * sizeof(double)];
            Buffer.BlockCopy(array, 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static double[] ConvertToDoubleArray(byte[] bytes)
        {
            var array = new double[bytes.Length / sizeof(double)];
            Buffer.BlockCopy(bytes, 0, array, 0, array.Length * sizeof(double));
            return array;
        }
    }
}
