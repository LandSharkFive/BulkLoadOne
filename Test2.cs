using BulkLoadOne;

namespace BulkTest
{
    [TestClass]
    public sealed class Test2
    {
        [TestMethod]
        public void TestOrderFourSplitOne()
        {
            var loader = new BulkLoader(4, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 20;

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
        public void TestOrderFourSplitTwo()
        {
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
        public void TestOrderFourSplitThree()
        {
            var loader = new BulkLoader(4, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 22;

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
        public void TestOrderFourSplitFour()
        {
            var loader = new BulkLoader(4, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 23;

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
        public void TestOrderFourSplitFive()
        {
            var loader = new BulkLoader(4, 1.0, 1.0);
            List<int> a = new();

            int maxCount = 24;

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
