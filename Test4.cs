using BulkLoadOne;
using System.Runtime.CompilerServices;

namespace BulkTest
{
    [TestClass]
    public sealed class Test4
    {
        [TestMethod]
        public void WeirdOneSmall()
        {
            // Fully Packed. Look at Key 20 and it's children.
            var loader = new BulkLoader(4, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 21;

            for (int i = 1; i < maxCount; i++)
            {
                a.Add(i);
            }

            int rootId = loader.BulkLoad(a);
            var result = loader.GetKeys(rootId);

            Assert.AreEqual(a.Count, result.Count);
            Assert.IsFalse(Util.HasDuplicate(result));

            foreach (var key in a)
            {
                Assert.IsTrue(result.Contains(key));
                if (result.Contains(key) == false)
                {
                    Console.WriteLine($"Missing Key: {key}");
                }
            }

            Assert.IsTrue(loader.CheckTree());
        }

        [TestMethod]
        public void ScrambleOneSmall()
        {
            Random rnd = new Random();
            
            // Fully Packed. Look at Key 20 and it's children.
            var loader = new BulkLoader(4, 1.0, 1.0);

            int maxCount = 21;

            List<int> a = Util.GetUniqueRandomNumbers(maxCount, 1000000);
            a = a.Distinct().OrderBy(x => x).ToList();
            Console.WriteLine(a.Count);

            int rootId = loader.BulkLoad(a);
            var result = loader.GetKeys(rootId);

            Assert.AreEqual(a.Count, result.Count);
            Assert.IsFalse(Util.HasDuplicate(result));

            foreach (var key in a)
            {
                Assert.IsTrue(result.Contains(key));
                if (result.Contains(key) == false)
                {
                    Console.WriteLine($"Missing Key: {key}");
                }
            }

            Assert.IsTrue(loader.CheckTree());
        }

        [TestMethod]
        public void ScrambleTwoMedium()
        {
            Random rnd = new Random();

            var loader = new BulkLoader(32);

            int maxCount = 400;

            List<int> a = Util.GetBigUniqueRandomNumbers(maxCount);
            a = a.Distinct().OrderBy(x => x).ToList();
            Console.WriteLine(a.Count);

            int rootId = loader.BulkLoad(a);
            var result = loader.GetKeys(rootId);

            Assert.AreEqual(a.Count, result.Count);
            Assert.IsFalse(Util.HasDuplicate(result));

            foreach (var key in a)
            {
                Assert.IsTrue(result.Contains(key));
                if (result.Contains(key) == false)
                {
                    Console.WriteLine($"Missing Key: {key}");
                }
            }

            Assert.IsTrue(loader.CheckTree());
        }


    }
}
