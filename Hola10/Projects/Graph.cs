using System.Collections.Generic;
using System.Linq;

class Graph
{
    // Private field to store the adjacency list
    public Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();

    // Method to add a new vertex to the graph
    public bool AddVertex(int vertex)
    {
        // Include a check to see if the vertex already exists in the graph
        if (!adjList.ContainsKey(vertex))
        {
            adjList[vertex] = new List<int>();
            return true; // Successfully added the vertex
        }
        else
        {
            return false; // Vertex already exists
        }
    }

    // Method to add a new edge between two vertices in the graph
    public bool AddEdge(int source, int destination)
    {
        // Add a check if an edge already exists between these two vertices
        if (adjList.ContainsKey(source) && adjList.ContainsKey(destination))
        {
            if (!adjList[source].Contains(destination))
            {
                adjList[source].Add(destination);
                return true; // Successfully added the edge
            }
            else
            {
                return false; // Edge already exists
            }
        }
        else
        {
            return false; // Source or destination vertex does not exist
        }
    }

    // Method to get the neighbors of a given vertex
    public List<int> GetNeighbors(int vertex)
    {
        return adjList[vertex];
    }

    // Method to get the count of vertices in the graph
    public int GetVerticesCount()
    {
        return adjList.Count;
    }

    public bool RemoveVertex(int vertex)
    {
        if (adjList.ContainsKey(vertex))
        {
            // Remove edges associated with the vertex
            foreach (int neighbor in adjList[vertex])
            {
                adjList[neighbor].Remove(vertex);
            }

            // Remove the vertex itself
            adjList.Remove(vertex);
            return true; // Successfully removed the vertex
        }
        else
        {
            return false; // Vertex does not exist
        }
    }

    public bool RemoveEdge(int source, int destination)
    {
        if (adjList.ContainsKey(source) && adjList.ContainsKey(destination))
        {
            if (adjList[source].Contains(destination))
            {
                adjList[destination].Remove(source);
                adjList[source].Remove(destination);

                return true; // Successfully removed the edge
            }
            else
            {
                return false; // Edge does not exist
            }
        }
        else
        {
            return false; // Source or destination vertex does not exist
        }
    }




    // ... (existing code)

    // Method to find the shortest path between two vertices using Dijkstra's algorithm
    public List<int> DijkstraShortestPath(int startVertex, int endVertex)
    {
        // Check if the start and end vertices exist in the graph
        if (!adjList.ContainsKey(startVertex) || !adjList.ContainsKey(endVertex))
        {
            return null; // Either the start or end vertex does not exist
        }

        // Create a dictionary to store the distance from the start vertex to each vertex
        Dictionary<int, int> distance = new Dictionary<int, int>();

        // Initialize all distances to infinity, except for the start vertex which is 0
        foreach (var vertex in adjList.Keys)
        {
            distance[vertex] = int.MaxValue;
        }
        distance[startVertex] = 0;

        // Create a priority queue to store vertices to be explored
        var priorityQueue = new SortedSet<int>(Comparer<int>.Create((v1, v2) => distance[v1] - distance[v2]));

        // Add all vertices to the priority queue
        foreach (var vertex in adjList.Keys)
        {
            priorityQueue.Add(vertex);
        }

        // Create a dictionary to store the previous vertex in the shortest path
        Dictionary<int, int> previousVertex = new Dictionary<int, int>();

        while (priorityQueue.Count > 0)
        {
            // Get the vertex with the smallest distance
            int currentVertex = priorityQueue.First();
            priorityQueue.Remove(currentVertex);

            // If the current vertex is the end vertex, we have found the shortest path
            if (currentVertex == endVertex)
            {
                return ReconstructPath(previousVertex, startVertex, endVertex);
            }

            // Update distances to neighbors
            foreach (var neighbor in adjList[currentVertex])
            {
                int tentativeDistance = distance[currentVertex] + 1; // Assuming unweighted edges

                if (tentativeDistance < distance[neighbor])
                {
                    distance[neighbor] = tentativeDistance;
                    previousVertex[neighbor] = currentVertex;

                    // Update the priority queue
                    priorityQueue.Remove(neighbor);
                    priorityQueue.Add(neighbor);
                }
            }
        }

        return null; // No path exists between the start and end vertices
    }

    private List<int> ReconstructPath(Dictionary<int, int> previousVertex, int startVertex, int endVertex)
    {
        var path = new List<int>();
        int current = endVertex;

        while (previousVertex.ContainsKey(current))
        {
            path.Add(current);
            current = previousVertex[current];
        }

        path.Add(startVertex); // Add the startVertex to the path

        path.Reverse(); // Reverse the path to get it from start to end

        return path;
    }



}
