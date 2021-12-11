namespace andywiecko.BurstMathUtils
{
    public static partial class MathUtils
    {
        // TODO: Add summary
        public static int BilateralInterleavingId(this int id, int length) => (id & 1) == 0 ? id >> 1 : length - 1 - (id >> 1);
    }
}