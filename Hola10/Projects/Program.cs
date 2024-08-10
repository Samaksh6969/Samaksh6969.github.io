using System;
using System.Collections.Generic;

class Program
{
    public static void Main()
    {
        // Create an instance of your Graph class
        Graph graph = new Graph();

        // Add vertices and edges to the graph
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddVertex(4);

        graph.AddEdge(0, 1);
        //graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 4);
        graph.AddEdge(0, 3); // Adding an extra edge for testing

        // Choose start and end vertices for testing
        int startVertex = 0;
        int endVertex = 2;

        // Call the DijkstraShortestPath method
        List<int> shortestPath = graph.DijkstraShortestPath(startVertex, endVertex);

        // Check the result and print it
        if (shortestPath != null)
        {
            Console.WriteLine("Shortest Path from " + startVertex + " to " + endVertex + ":");
            foreach (int vertex in shortestPath)
            {
                Console.Write(vertex + " -> ");
            }
            Console.WriteLine("Distance: " + (shortestPath.Count - 1)); // The distance is the number of edges
        }
        else
        {
            Console.WriteLine("No path exists from " + startVertex + " to " + endVertex);
            Console.ReadLine();
        }
    }
}
