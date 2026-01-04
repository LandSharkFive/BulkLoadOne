
namespace BulkLoadOne
{
    public class Util
    {

        public static List<int> GetBigUniqueRandomNumbers(int count)
        {
            var random = new Random();
            var set = new HashSet<int>();

            while (set.Count < count)
            {
                set.Add(random.Next());
            }

            return set.ToList();
        }

        public static List<int> GetUniqueRandomNumbers(int count, int maxValue)
        {
            var random = new Random();
            var set = new HashSet<int>();

            while (set.Count < count)
            {
                set.Add(random.Next(maxValue));
            }

            return set.ToList();
        }

        /// <summary>
        /// Does the list have any duplicates?
        /// </summary>
        /// <param name="source">List</param>
        /// <returns>bool</returns>
        public static bool HasDuplicate(List<int> source)
        {
            var set = new HashSet<int>();
            foreach (var item in source)
            {
                if (!set.Add(item))
                    return true;
            }
            return false;
        }

    }
}
