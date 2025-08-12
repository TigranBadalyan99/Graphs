# MyGraph – Graph Algorithms in C#

`MyGraph` is a C# class implementing common graph algorithms for directed graphs.  
It supports traversals, pathfinding, cycle detection, topological sorting, and more.

## Features
- **Graph construction** with adjacency list
- **DFS** (Depth-First Search)
- **BFS** (Breadth-First Search)
- **Pathfinding**:
  - Shortest path (BFS-based)
  - All possible paths
  - Nodes at a given level
- **Cycle detection** (DFS & Kahn’s Algorithm)
- **Topological sort** (Kahn’s Algorithm)
- **Strongly Connected Components** (Kosaraju’s Algorithm – WIP)
- Graph printing utility

## Example Usage
```csharp
var g = new MyGraph(5);
g.AddEdge(0, 1);
g.AddEdge(0, 2);
g.AddEdge(1, 3);
g.AddEdge(2, 4);

Console.WriteLine("DFS:");
g.DFS();

Console.WriteLine("\nBFS:");
g.BFS();

Console.WriteLine("\nShortest Path 0 -> 4:");
var path = g.GetShortestPathFromSrcToDst(0, 4);
Console.WriteLine(string.Join(" -> ", path));
