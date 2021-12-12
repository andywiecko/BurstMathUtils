namespace andywiecko.BurstMathUtils
{
    public static partial class MathUtils
    {
        /// <summary>
        /// Utility function for id enumeration in bilateral interleaving order, 
        /// e.g. sequence of <paramref name="id"/>=0, 1, 2, 3, 4, 5 (<paramref name="count"/>=6),
        /// will be enumerated in the following way: 0, 5, 1, 4, 2, 3.
        /// </summary>
        public static int BilateralInterleavingId(int id, int count) => (id & 1) == 0 ? id >> 1 : count - 1 - (id >> 1);
    }
}