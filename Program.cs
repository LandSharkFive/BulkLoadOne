namespace BulkLoadOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var loader = new BulkLoader(4, 1.0, 1.0);
            List<int> keys = new();

            int maxCount = 21;

            for (int i = 1; i < maxCount; i++)
            {
                keys.Add(i);
            }

            int rootId = loader.BulkLoad(keys);

            Console.WriteLine($"Bulk Load Complete. Root ID: {rootId}");
            loader.PrintTree(rootId, 0);
            loader.DumpFile();
            Console.WriteLine();
            loader.PrintKeys(rootId);
            var result = loader.GetKeys(rootId);

            foreach (var key in keys)
            {
                if (!result.Contains(key))
                {
                    Console.WriteLine($"Key Missing {key}");
                }
            }

            Console.WriteLine();
            Console.Write("Size ");
            Console.WriteLine(result.Count == keys.Count);

            loader.CheckTree();
        }


        static void WeirdOne()
        {
            // Weird Case.  Look at Key 20 and it's children. Maybe redistribute them.
            var loader = new BulkLoader(4, 1.0, 1.0);
            List<int> keys = new();

            int maxCount = 21;

            for (int i = 1; i < maxCount; i++)
            {
                keys.Add(i);
            }

            int rootId = loader.BulkLoad(keys);

            Console.WriteLine($"Bulk Load Complete. Root ID: {rootId}");
            loader.PrintTree(rootId, 0);
            loader.DumpFile();
            Console.WriteLine();
            loader.PrintKeys(rootId);
            var result = loader.GetKeys(rootId);

            foreach (var key in keys)
            {
                if (!result.Contains(key))
                {
                    Console.WriteLine($"Key Missing {key}");
                }
            }

            Console.WriteLine();
            Console.Write("Size ");
            Console.WriteLine(result.Count == keys.Count);

            loader.CheckTree();
        }

    }
}
