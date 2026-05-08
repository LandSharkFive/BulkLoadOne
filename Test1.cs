using BulkLoadOne;

namespace BulkTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestDefaultMedium()
        {
            var loader = new BulkLoader();
            List<int> a = new();

            int maxCount = 300;

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
        public void TestSmallOne()
        {
            var loader = new BulkLoader(10);
            List<int> a = new();

            int maxCount = 300;

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
        public void TestOrderTenFullyPacked()
        {
            var loader = new BulkLoader(10, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 300;

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
        public void TestMediumFullyPacked()
        {
            var loader = new BulkLoader(60, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 500;

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
        public void TestOrderFourFullyPacked()
        {
            var loader = new BulkLoader(4, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 50;

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
