# Bulk Loader for B-Trees

A .NET 8 Console application designed to efficiently bulk-load "plain vanilla" B-Trees. This tool supports configurable orders (degrees), fill factors, and separator keys, using a mocked disk interface to simulate physical storage.



---

## Features
* **Configurable Parameters:** Supports custom B-Tree orders and specific fill factors for both leaf and internal nodes.
* **Disk Simulation:** Uses a mock layer to simulate hard disk node storage and I/O.
* **Visualization:** Options to print the tree structure by physical disk node or by logical levels.
* **Validation:** Includes a **Node Checker** to ensure the tree adheres to B-Tree invariants (Key and ChildId constraints).

## Installation & Build
This is a C# project targeting **.NET 8**.
* **IDE:** Visual Studio 2022 (or higher).
* **Build:** 1. Clone the repository.
  2. Open the solution file.
  3. Run `dotnet build` or use your IDE's Build command.

## Performance & Purpose
The primary goal of this bulk loader is to eliminate "dead space" (unused nodes) and produce a perfectly balanced tree.
* **Time Complexity:** $O(N)$
* **Space Complexity:** $O(\log N)$
  > *Note: In practice, space is effectively $O(1)$ because the tree height remains very small (e.g., 3 levels) even for large datasets with an order > 10.*

## Unit Testing & Sanity Checks
The project includes a comprehensive test suite to ensure trees are well-formed and valid.

### Structural Rules
The **Node Checker** validates the following logic for every node in the tree:

| Node Type | Rule / Constraint |
| :--- | :--- |
| **Leaf Node** | $0 < \text{Keys.Count} < \text{Order}$ |
| **Leaf Node** | $\text{ChildIds.Count} == 0$ |
| **Index Node** | $0 < \text{ChildIds.Count} \le \text{Order}$ |
| **Index Node** | $\text{Keys.Count} = \text{ChildIds.Count} - 1$ |

### Test Coverage
* **Scale Testing:** Building trees of various sizes and degrees.
* **Fill Factor Validation:** Verifying that the 80% default fill factor (or custom inputs) is respected.
* **Integrity Checks:** Detecting Key and ChildId violations against basic B-Tree rules.

## Default Configuration
By default, the application uses:
* **Order:** 60
* **Leaf Fill:** 80%
* **Internal Node Fill:** 80%
* *Nodes are deliberately underfilled to allow for immediate post-load insertions without immediate splits.*

