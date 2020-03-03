using System.Collections.Generic;

namespace PitFortress
{
    public class Helpers
    {
        public static IEnumerable<int> MakeId()
        {
            var index = 1;

            while (true)
            {
                yield return index++;
            }
        }
    }
}