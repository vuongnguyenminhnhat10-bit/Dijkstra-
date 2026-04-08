using System;
using System.Collections.Generic;

namespace DoAnCuoiKy_Dijkstra
{
    public class Graph
    {
        private MyLinkedList<Vertex> vertices;

        public Graph()
        {
            vertices = new MyLinkedList<Vertex>();
        }

        public MyLinkedList<Vertex> Vertices
        {
            get { return vertices; }
        }

        public int VertexCount()
        {
            return vertices.Count;
        }

        public void AddVertex(string id, string name, double x, double y)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new Exception("Mã địa điểm không hợp lệ.");

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên địa điểm không hợp lệ.");

            id = id.Trim();
            name = name.Trim();

            if (FindVertexById(id) != null)
                throw new Exception("Mã địa điểm đã tồn tại.");

            Vertex vertex = new Vertex(id, name, x, y);
            vertices.AddLast(vertex);
        }

        public Vertex FindVertexById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            id = id.Trim();

            ListNode<Vertex> current = vertices.Head;

            while (current != null)
            {
                if (current.Data != null && current.Data.Id == id)
                    return current.Data;

                current = current.Next;
            }

            return null;
        }

        public Vertex GetVertexAt(int index)
        {
            if (index < 0 || index >= vertices.Count)
                throw new Exception("Chỉ số đỉnh không hợp lệ.");

            return vertices.GetAt(index);
        }

        public int GetVertexIndex(Vertex vertex)
        {
            if (vertex == null)
                return -1;

            ListNode<Vertex> current = vertices.Head;
            int index = 0;

            while (current != null)
            {
                if (current.Data != null && current.Data.Id == vertex.Id)
                    return index;

                current = current.Next;
                index++;
            }

            return -1;
        }

        public bool HasEdge(Vertex from, Vertex to)
        {
            if (from == null || to == null)
                return false;

            if (from.Edges == null || from.Edges.Head == null)
                return false;

            ListNode<Edge> current = from.Edges.Head;

            while (current != null)
            {
                if (current.Data != null &&
                    current.Data.Destination != null &&
                    current.Data.Destination.Id == to.Id)
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public double CalculateEuclideanDistance(Vertex a, Vertex b)
        {
            if (a == null || b == null)
                throw new Exception("Đỉnh không hợp lệ.");

            double dx = a.X - b.X;
            double dy = a.Y - b.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void AddUndirectedEdge(string id1, string id2)
        {
            if (string.IsNullOrWhiteSpace(id1) || string.IsNullOrWhiteSpace(id2))
                throw new Exception("Mã địa điểm không hợp lệ.");

            id1 = id1.Trim();
            id2 = id2.Trim();

            if (id1 == id2)
                throw new Exception("Không thể nối một địa điểm với chính nó.");

            Vertex v1 = FindVertexById(id1);
            Vertex v2 = FindVertexById(id2);

            if (v1 == null || v2 == null)
                throw new Exception("Một trong hai địa điểm không tồn tại.");

            if (HasEdge(v1, v2) || HasEdge(v2, v1))
                throw new Exception("Cạnh đã tồn tại.");

            double distance = CalculateEuclideanDistance(v1, v2);

            v1.Edges.AddLast(new Edge(v2, distance));
            v2.Edges.AddLast(new Edge(v1, distance));
        }

        public void AddDirectedEdge(string fromId, string toId)
        {
            if (string.IsNullOrWhiteSpace(fromId) || string.IsNullOrWhiteSpace(toId))
                throw new Exception("Mã địa điểm không hợp lệ.");

            fromId = fromId.Trim();
            toId = toId.Trim();

            if (fromId == toId)
                throw new Exception("Không thể nối một địa điểm với chính nó.");

            Vertex from = FindVertexById(fromId);
            Vertex to = FindVertexById(toId);

            if (from == null || to == null)
                throw new Exception("Một trong hai địa điểm không tồn tại.");

            if (HasEdge(from, to))
                throw new Exception("Cạnh đã tồn tại.");

            double distance = CalculateEuclideanDistance(from, to);

            from.Edges.AddLast(new Edge(to, distance));
        }
    }
}
