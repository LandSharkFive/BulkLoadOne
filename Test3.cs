using BulkLoadOne;

namespace BulkTest
{
    [TestClass]
    public sealed class Test3
    {
        [TestMethod]
        public void TestBigFullyPacked()
        {
            var loader = new BulkLoader(60, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 1000;

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
        public void TestBigMediumPack()
        {
            var loader = new BulkLoader(32, 0.7, 0.7);
            List<int> a = new();

            int maxCount = 1000;

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
        public void TestBigThreeHalfPack()
        {
            var loader = new BulkLoader(32, 0.5, 0.5);
            List<int> a = new();

            int maxCount = 1000;

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
        public void TestBigOne()
        {
            var loader = new BulkLoader(100, 0.9, 0.9);
            List<int> a = new();

            int maxCount = 1000;

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
        public void TestBigTwo()
        {
            var loader = new BulkLoader(48, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 1000;

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


    }
}
