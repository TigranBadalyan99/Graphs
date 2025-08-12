class MyGraph
{
    public List<List<int>> adjacencyList { get; private set; }
    public MyGraph(int numVertices)
    {
        adjacencyList = new List<List<int>>(numVertices);
        for (int i = 0; i < numVertices; i++)
            adjacencyList.Add(new List<int>());
    }
    


    public void AddEdge(int u, int v)
    {
        if (!adjacencyList[u].Contains(v))
            adjacencyList[u].Add(v);

        // if (!adjacencyList[v].Contains(u))
        //     adjacencyList[v].Add(u);
    }

/////////////Traversals\\\\\\\\\\\\
    public void DFS()
    {
        bool[] visited = new bool[adjacencyList.Count];

        for (int i = 0; i < adjacencyList.Count; i++)
        {
            if (!visited[i])
                DFSRec(i, visited);
        }
    }

    private void DFSRec(int vertex, bool[] visited)
    {
        visited[vertex] = true;
        Console.Write(vertex + "->");

        foreach (int neighbor in adjacencyList[vertex])
        {
            if (!visited[neighbor])
                DFSRec(neighbor, visited);
        }
    }

    public void BFS()
    {
        bool[] visited = new bool[adjacencyList.Count];

        for (int i = 0; i < adjacencyList.Count; i++)
        {
            if (!visited[i])
                BFSIterative(i, visited);
        }
    }

    private void BFSIterative(int start, bool[] visited)
    {
        Queue<int> q = new();
        q.Enqueue(start);
        visited[start] = true;

        while (q.Count > 0)
        {
            int current = q.Dequeue();
            Console.Write(current + "->");

            foreach (int neighbor in adjacencyList[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    q.Enqueue(neighbor);
                }
            }
        }
    }
//////////////////////////

    public bool CanReach(int source, int destination)
    {
        if (source >= adjacencyList.Count || destination >= adjacencyList.Count)
            return false;

        bool[] visited = new bool[adjacencyList.Count];
        Stack<int> st = new();
        st.Push(source);

        while (st.Count > 0)
        {
            int current = st.Pop();

            if (current == destination)
                return true;

            if (!visited[current])
            {
                visited[current] = true;
                foreach (int neighbor in adjacencyList[current])
                {
                    if (!visited[neighbor])
                        st.Push(neighbor);
                }
            }
        }

        return false;
    }

    public List<int>? GetShortestPathFromSrcToDst(int src, int dst)
    {
        if (src >= adjacencyList.Count || dst >= adjacencyList.Count)
            return null;

        bool[] visited = new bool[adjacencyList.Count];
        int[] parent = new int[adjacencyList.Count];
        Array.Fill(parent, -1);

        Queue<int> q = new();
        q.Enqueue(src);
        visited[src] = true;

        while (q.Count > 0)
        {
            int current = q.Dequeue();

            if (current == dst)
                break;

            foreach (int neighbor in adjacencyList[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    parent[neighbor] = current;
                    q.Enqueue(neighbor);
                }
            }
        }

        if (!visited[dst])
            return null;

        List<int> result = new();
        for (int v = dst; v != -1; v = parent[v])
            result.Add(v);

        result.Reverse();
        return result;
    }

    public List<int>? GetNodesAtAGivenLevel(int start, int level)
    {
        if (start >= adjacencyList.Count)
            return null;
        
        List<int> res = new();
        Queue<int> q = new();
        bool[] visited = new bool[adjacencyList.Count];

        q.Enqueue(start);
        visited[start] = true;
        int currentLevel = 0;

        if (level == 0)
        {
            res.Add(start);
            return res;
        }

        while (q.Count > 0)
        {
            int size = q.Count;

            while (size-- > 0)
            {
                int current = q.Dequeue();

                foreach (int neighbor in adjacencyList[current])
                {
                    if (!visited[neighbor])
                    {
                        visited[neighbor] = true;
                        q.Enqueue(neighbor);
                    }
                }
            }

            currentLevel++;

            if (currentLevel == level)
            {
                while (q.Count > 0)
                    res.Add(q.Dequeue());
                    
                break;
            }
        }

        return res;
    }

    public List<List<int>> GetAllPossiblePaths(int src, int dst)
    {
        if (src >= adjacencyList.Count || dst >= adjacencyList.Count || src < 0 || dst < 0)
            return new List<List<int>>();

        List<List<int>> allPaths = new();
        List<int> currentPath = new();
        bool[] visited = new bool[adjacencyList.Count];

        DFSPaths(src, dst, visited, currentPath, allPaths);
        return allPaths;
    }

    private void DFSPaths(int src, int dst, bool[] visited, List<int> path, List<List<int>> allPaths)
    {
        visited[src] = true;
        path.Add(src);

        if (src == dst)
        {
            allPaths.Add(new List<int>(path));
        }
        else
        {
            foreach (var neighbor in adjacencyList[src])
            {
                if (!visited[neighbor])
                {
                    DFSPaths(neighbor, dst, visited, path, allPaths);
                }
            }
        }

        path.RemoveAt(path.Count - 1);
        visited[src] = false;
    }

    public bool HasCycle()
    {
        bool[] visited = new bool[adjacencyList.Count];
        bool[] recStack = new bool[adjacencyList.Count];

        for (int v = 0; v < adjacencyList.Count; v++)
        {
            if (!visited[v])
            {
                if (DFSHasCycle(v, visited, recStack))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    public bool DFSHasCycle(int vertex, bool[] visited, bool[] inStack)
    {
        visited[vertex] = true;
        inStack[vertex] = true;

        foreach (var neighbor in adjacencyList[vertex])
        {
            if (!visited[neighbor])
            {
                if (DFSHasCycle(neighbor, visited, inStack))
                    return true;
            }

            if (inStack[neighbor])
                return true;
        }

        inStack[vertex] = false;

        return false;
    }

    public List<int> KahnToplogicalSort(List<List<int>> graph)
    {
        int n = graph.Count;
        int[] inDegree = new int[n];

        for (int u = 0; u < n; u++)
        {
            foreach (int neighbor in graph[u])
                inDegree[neighbor]++;
        }

        Queue<int> q = new();

        for (int i = 0; i < n; i++)
        {
            if (inDegree[i] == 0)
                q.Enqueue(i);
        }

        List<int> result = new();

        while (q.Count > 0)
        {
            int u = q.Dequeue();
            result.Add(u);

            foreach (int neighbor in graph[u])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                    q.Enqueue(neighbor);
            }
        }

        if (result.Count != n)
            throw new Exception("The graph has a cycle");


        return result;
    }

    private void DFSKosaraju(int v, bool[] visited, Stack<int> stack)
    {
        visited[v] = true;

        foreach (var neighbor in adjacencyList[v])
        {
            if (!visited[neighbor])
                DFSKosaraju(neighbor, visited, stack);
        }

        stack.Push(v);
    }

    private List<List<int>> TransposeGraph()
    {
        int n = adjacencyList.Count;
        List<List<int>> transpose = new();

        for (int i = 0; i < n; i++)
            transpose.Add(new List<int>());

        for (int v = 0; v < n; v++)
        {
            foreach (var neighbor in adjacencyList[v])
            {
                transpose[neighbor].Add(v);
            }
        }

        return transpose;
    }

    private void DFS2(int v, bool[] visited, List<int> component)
    {
        visited[v] = true;
        component.Add(v);

        foreach (var neighbor in adjacencyList[v])
        {
            if (!visited[neighbor])
                DFS2(neighbor, visited, component);
        }
    }

    public List<List<int>> FindSCC()
    {
        int n = adjacencyList.Count;
        bool[] visited = new bool[n];
        Stack<int> st = new();

        for (int i = 0; i < n; i++)
            if (!visited[i])
                MyGraph.DFSKosataju();
    }


    public void Print()
    {
        for (int i = 0; i < adjacencyList.Count; i++)
        {
            Console.Write($"Key: {i} -> {string.Join(", ", adjacencyList[i])} \n");
        }
    }
}
