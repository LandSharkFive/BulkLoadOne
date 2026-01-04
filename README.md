# Bulk Loader for Vanilla Plain BTree

This is a .NET 8 Console-mode application which loads vanilla plain BTrees, with separator keys, with various orders (degrees) and fill factors.  A hard disk is simulated, using a mock.  The tree can be printed by physical disk node, or by levels. 

## Install and Build

The is a C# Console-mode Project.  Use Visual Studio 2022 and above to compile.  

## Unit Tests

Several unit tests are enclosed.  Trees of various sizes and orders (degrees) are built and tested.  Invalid Trees are detected using a Node Checker.  The Node Checker checks for Key and ChildIds violations of the basic Order Rules for leaves and internal nodes. There are unit tests which check fill factors.  There are unit tests for checking if the tree is well-formed.  

## Default Tree

The default tree is Order 60, with an 80% leaf fill and an 80% internal node fill.  The nodes are deliberately underfilled for safety reasons.

## Purpose

The purpose of the bulk loader is to remove dead space (unused nodes) in the tree.  A balanced tree is produced.  The bulk loader is quick.  Runtime is O(N) and Space is O(Log N), where N is the number of keys.  In practice, space is a constant O(1), since the number of levels in the tree is a very small number.  Three levels are common.  There are O(Log N) levels, where N is generally 3 for order > 10 keys per node.

## Sanity Checks

All nodes in the Tree are checked, including the root node.

* If leaf node, then node.Keys.Count > 0.
* If leaf mode, then node.Keys.Count < Order.
* If leaf node, then node.ChildIds.Count == 0.
* If lndex node, then node.ChildsIds.Count > 0.
* If lndex node, node.ChildIds.Count <= Order.


