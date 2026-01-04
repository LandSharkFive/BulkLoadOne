
using static BulkLoadOne.BulkLoader;

namespace BulkLoadOne
{
    public class BulkLoader
    {
        // Works for Order 4 and above. Normally, Order is a private constant.
        private int Order { get; set; }
        private double LeafFactor { get; set; }
        private double IndexFactor { get; set; }

        public class Node
        {
            public int Id { get; set; }
            public List<int> Keys = new();
            public List<int> ChildIds = new();
            public bool IsLeaf { get; set; }
        }

        public class LevelEntry
        {
            public int Key { get; set; }
            public int NodeId { get; set; }
        }

        private Dictionary<int, Node> DiskMock = new();
        private int IdCounter = 0;


        public BulkLoader(int order = 60, double leafFactor = 0.8, double indexFactor = 0.8)
        {
            Order = order;
            LeafFactor = leafFactor;
            IndexFactor = indexFactor;
        }

        public int BulkLoad(List<int> sortedKeys)
        {
            // Ensure we don't round down to 0 for very small orders/factors
            int leafMax = (int)Math.Max(1, (Order - 1) * LeafFactor);
            int indexMax = (int)Math.Max(1, (Order - 1) * IndexFactor);
            List<Node> spine = new List<Node>();

            foreach (int key in sortedKeys)
            {
                AddKeyToSpine(0, key, spine, leafMax, indexMax);
            }

            return FinalizeTree(spine);
        }

        private void AddKeyToSpine(int level, int key, List<Node> spine, int leafMax, int indexMax)
        {
            if (spine.Count <= level)
            {
                spine.Add(new Node { IsLeaf = (level == 0) });
            }

            var node = spine[level];
            int limit = node.IsLeaf ? leafMax : indexMax;

            if (node.Keys.Count < limit)
            {
                node.Keys.Add(key);
            }
            else
            {
                // 1. Node is full. Save it.
                int finishedId = Save(node);

                // 2. Clear this level for new data
                spine[level] = new Node { IsLeaf = (level == 0) };

                // 3. Promote the separator AND the ID of the node we just finished
                PromoteToParent(level + 1, key, finishedId, spine, leafMax, indexMax);
            }
        }

        private void PromoteToParent(int level, int separator, int leftChildId, List<Node> spine, int lMax, int iMax)
        {
            if (spine.Count <= level)
            {
                spine.Add(new Node { IsLeaf = false });
            }

            var parent = spine[level];

            // Attach the child that was just completed
            parent.ChildIds.Add(leftChildId);

            if (parent.Keys.Count < iMax)
            {
                parent.Keys.Add(separator);
            }
            else
            {
                // Internal split
                int finishedId = Save(parent);
                spine[level] = new Node { IsLeaf = false };
                PromoteToParent(level + 1, separator, finishedId, spine, lMax, iMax);
            }
        }

        private int FinalizeTree(List<Node> spine)
        {
            if (spine.Count == 0) return -1;

            int lastId = -1;

            for (int i = 0; i < spine.Count; i++)
            {
                var node = spine[i];

                // 1. Mandatory Adoption: If we have a child from below, it MUST be attached.
                if (lastId != -1)
                {
                    node.ChildIds.Add(lastId);
                }

                // 2. The Final Separator Logic: 
                // If we have N keys and N children, the last key is '20'.
                // It needs a child to its right. Since there are no more keys,
                // we just leave the tree in a 'Bulk Load Stable' state.
                // N keys and N+1 children is the goal.

                // 3. Skip saving only if the node is a 'Ghost' (empty leaf at the very end)
                if (node.IsLeaf && node.Keys.Count == 0 && i < spine.Count - 1)
                {
                    continue;
                }

                lastId = Save(node);
            }

            return lastId;
        }


        /// <summary>
        /// Allocate one page.  Write node to disk.
        /// </summary>
        private int Save(Node n)
        {
            n.Id = ++IdCounter;
            DiskMock[n.Id] = n;
            return n.Id;
        }

        private Node DiskRead(int i)
        {
            return DiskMock[i];
        }

        private void DiskWrite(Node n)
        {
            DiskMock[n.Id] = n;
        }

        /// <summary>
        /// Walk the tree by levels starting in the root node. Print the keys.
        /// </summary>
        public void PrintTree(int nodeId, int indent)
        {
            if (!DiskMock.ContainsKey(nodeId))
                return;

            var node = DiskMock[nodeId];
            string space = new string(' ', indent * 4);
            Console.WriteLine($"{space}Node {node.Id} ({(node.IsLeaf ? "L" : "I")}): Keys [{string.Join(", ", node.Keys)}]");

            if (!node.IsLeaf)
            {
                foreach (var childId in node.ChildIds)
                {
                    PrintTree(childId, indent + 1);
                }
            }
        }

        /// <summary>
        /// Dump each physical node from the disk. Print IsLeaf, Keys and ChildIds.
        /// </summary>
        public void DumpFile()
        {
            Console.WriteLine("--- PHYSICAL DISK DUMP ---");
            // Start from 0 (or wherever your first node is) up to Manager.TotalPages
            for (int i = 0; i <= IdCounter; i++)
            {
                try
                {
                    Node node = DiskMock[i];
                    Console.Write($"Page {i}: ");
                    Console.Write($"(IsLeaf: {node.IsLeaf}) ");
                    Console.Write(" Keys [");
                    List<string> key = new();
                    for (int j = 0; j < node.Keys.Count; j++)
                    {
                        key.Add(node.Keys[j].ToString());
                    }
                    Console.Write(string.Join(", ", key));
                    Console.Write("] ");
                    Console.Write(" ChildIds [");
                    List<string> childId = new();
                    for (int k = 0; k < node.ChildIds.Count; k++)
                    {
                        childId.Add(node.ChildIds[k].ToString());
                    }
                    Console.Write(string.Join(", ", childId));
                    Console.WriteLine("] ");
                }
                catch { }
            }
        }


        public void PrintKeys(int nodeId)
        {
            if (nodeId < 0 || !DiskMock.ContainsKey(nodeId))
                return;

            var node = DiskMock[nodeId];

            foreach (int key in node.Keys)
            {
                Console.Write(key + " ");
            }

            foreach (int id in node.ChildIds)
            {
                PrintKeys(id);
            }
        }

        List<int> AllKeys;

        public List<int> GetKeys(int nodeId)
        {
            AllKeys = new();
            GetKeysRecursive(nodeId);
            return AllKeys;
        }

        private void GetKeysRecursive(int nodeId)
        {
            if (nodeId < 0 || !DiskMock.ContainsKey(nodeId))
            {
                return;
            }

            var node = DiskMock[nodeId];
            AllKeys.AddRange(node.Keys);

            foreach (int id in node.ChildIds)
            {
                if (id != -1)
                {
                    GetKeysRecursive(id);
                }
            }
        }


        public bool CheckTree()
        {
            // Start from 0 (or wherever your first node is) up to TotalPages
            var maxKVP = DiskMock.MaxBy(kvp => kvp.Key);
            int maxId = Convert.ToInt32(maxKVP.Key);
            for (int i = 0; i <= maxId; i++)
            {
                try
                {
                    if (DiskMock.ContainsKey(i))
                    {

                        Node node = DiskMock[i];
                        if (CheckNode(node) == false)
                        {
                            Console.WriteLine($"Bad Node {i}");
                            return false;
                        }
                    }
                }
                catch { }
            }

            return true;
        }

        /// <summary>
        /// Sanity Check for nodes.
        /// </summary>
        public bool CheckNode(Node node)
        {
            if (node.IsLeaf)
            {
                // leaf
                if (node.Keys.Count == 0)
                    return false;
                if (node.Keys.Count > Order - 1)
                    return false;
                if (node.ChildIds.Count != 0)
                    return false;
            }
            else
            {
                // internal node
                if (node.ChildIds.Count == 0)
                    return false;
                if (node.ChildIds.Count > Order)
                    return false;
            }

            return true;
        }

    }
}
