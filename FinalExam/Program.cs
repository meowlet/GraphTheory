namespace FinalExam
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var graph = new Graph();

            // Yêu cầu 1: Tính bậc của các đỉnh từ danh sách kề
            graph.AdjencyList("DanhSachKe.INP");

            // Yêu cầu 2: Chuyển danh sách kề thành danh sách cạnh
            graph.ConvertAdjListToEdgeList("DSKe2Canh.INP", "DSKe2Canh.OUT");

            // Yêu cầu 3: Đếm số lượng miền liên thông
            graph.CountConnectedComponents("DemLienThong.INP", "DemLienThong.OUT");
        }
    }
}