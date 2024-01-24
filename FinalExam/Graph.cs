using System;
using System.Collections.Generic;
using System.IO;

namespace FinalExam
{
    internal class Graph
    {
        private LinkedList<int>[] adjList;
        private LinkedList<Tuple<int, int>> edgeList;
        public int NumVertices { get; set; }
        private int[] color;

        public Graph()
        {
            NumVertices = 0;
        }

        internal void AdjencyList(string fname)
        {
            ReadAdjList(fname);
            WriteALVertexDegree(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteALVertexDegree(string fname)
        {
            using (var file = new StreamWriter(fname))
            {
                file.WriteLine(NumVertices);
                for (var i = 0; i < NumVertices; i++)
                    file.Write(adjList[i].Count + " ");
                file.WriteLine();
            }
        }

        private void ReadAdjList(string fname)
        {
            var lines = File.ReadAllLines(fname);
            NumVertices = int.Parse(lines[0].Trim());
            adjList = new LinkedList<int>[NumVertices];
            for (var i = 0; i < NumVertices; i++)
            {
                adjList[i] = new LinkedList<int>();
                var line = lines[i + 1].Split(' ');
                for (var j = 0; j < line.Length; j++)
                {
                    var v = int.Parse(line[j].Trim()) - 1;
                    adjList[i].AddLast(v);
                }
            }
        }

        internal void ConvertAdjListToEdgeList(string inputFileName, string outputFileName)
        {
            ReadAdjList(inputFileName);
            ConvertAdjListToEdgeList();
            WriteEdgeList(outputFileName);
        }

        private void ConvertAdjListToEdgeList()
        {
            edgeList = new LinkedList<Tuple<int, int>>();
            for (var i = 0; i < NumVertices; i++)
                foreach (var vertex in adjList[i])
                    if (i < vertex)
                        edgeList.AddLast(new Tuple<int, int>(i + 1, vertex + 1));
        }

        private void WriteEdgeList(string fileName)
        {
            using (var file = new StreamWriter(fileName))
            {
                file.WriteLine($"{NumVertices} {edgeList.Count}");
                foreach (var edge in edgeList) file.WriteLine($"{edge.Item1} {edge.Item2}");
            }
        }

        internal void CountConnectedComponents(string inputFileName, string outputFileName)
        {
            ReadAdjList(inputFileName);
            var components = GetConnectedComponents();
            WriteConnectedComponents(outputFileName, components);
        }

        private LinkedList<LinkedList<int>> GetConnectedComponents()
        {
            var components = new LinkedList<LinkedList<int>>();
            color = new int[NumVertices];
            for (var i = 0; i < NumVertices; i++)
                if (color[i] == 0)
                {
                    var component = BFS(i);
                    components.AddLast(component);
                }

            return components;
        }

        private LinkedList<int> BFS(int s)
        {
            var component = new LinkedList<int>();
            var queue = new Queue<int>();
            queue.Enqueue(s);
            color[s] = 1;
            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                component.AddLast(v);
                foreach (var w in adjList[v])
                    if (color[w] == 0)
                    {
                        queue.Enqueue(w);
                        color[w] = 1;
                    }
            }

            return component;
        }

        private void WriteConnectedComponents(string fileName, LinkedList<LinkedList<int>> components)
        {
            using (var file = new StreamWriter(fileName))
            {
                file.WriteLine(components.Count);
            }
        }
    }
}