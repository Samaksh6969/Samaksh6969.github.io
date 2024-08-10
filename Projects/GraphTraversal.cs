using System;
using System.Collections.Generic;
using System.Linq;

class GraphTraversal
{
    private bool[] visited;
    private bool[] onPath;
    private bool hasCycle;

    // Depth-First Search (DFS) method to traverse the graph starting from a given vertex
    public void DFS(Graph graph, int start)
    {
        int vertices = graph.GetVerticesCount();
        visited = new bool[vertices];
        onPath = new bool[vertices]; // Initialize onPath array
        hasCycle = false;
        DFSUtil(graph, start);
    }


    // Private helper method for DFS traversal
    private void DFSUtil(Graph graph, int vertex)
    {
        visited[vertex] = true;
        onPath[vertex] = true; // Mark the node as part of the current path

        List<int> neighbors = graph.GetNeighbors(vertex);
        foreach (var neighbor in neighbors)
        {
            if (!visited[neighbor])
            {
                DFSUtil(graph, neighbor);
            }
            else if (onPath[neighbor])
            {
                hasCycle = true; // Cycle detected
                return;
            }
        }

        onPath[vertex] = false; // Remove the node from the current path when backtracking
    }

    // Breadth-First Search (BFS) method to traverse the graph starting from a given vertex
    public void BFS(Graph graph, int start)
    {
        int vertices = graph.GetVerticesCount();
        visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();

        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Any())
        {
            int currentVertex = queue.Dequeue();
            Console.WriteLine("Visited: " + currentVertex);

            List<int> neighbors = graph.GetNeighbors(currentVertex);
            foreach (var neighbor in neighbors)
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    public bool HasPathBetweenVertices(Graph graph, int start, int end)
    {
        int vertices = graph.GetVerticesCount();
        visited = new bool[vertices];

        DFS(graph, start); // Perform a DFS starting from the 'start' vertex

        // Check if the 'end' vertex is visited after DFS
        return visited[end];

    }
    public bool HasCycle(Graph graph)
    {
        int vertices = graph.GetVerticesCount();
        visited = new bool[vertices];
        onPath = new bool[vertices];
        hasCycle = false;

        for (int i = 0; i < vertices; i++)
        {
            if (!visited[i])
            {
                DFSUtil(graph, i);
                if (hasCycle)
                {
                    return true;
                }
            }
        }

        return false; // No cycle detected in the entire graph.
    }
}
