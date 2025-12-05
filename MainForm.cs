using COM5113_Assignment_WinForm.Algorithms;
using COM5113_Assignment_WinForm.MapInfo;
using COM5113_Assignment_WinForm.Path;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace COM5113_Assignment_WinForm
{
    public partial class MainForm : Form
    {
        private Map? loadedMap;
        private LinkedList<Coord>? foundPath = null;
        private string? currentMapName;

        public MainForm()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Font = new Font("Segoe UI", 12F);

            // Add algorithms to the dropdown
            cmbAlgorithms.Items.Clear();
            cmbAlgorithms.Items.Add("BFS");
            cmbAlgorithms.Items.Add("DFS");
            cmbAlgorithms.Items.Add("HILL");
            cmbAlgorithms.Items.Add("BESTFIRST");
            cmbAlgorithms.Items.Add("DIJKSTRA");
            cmbAlgorithms.Items.Add("ASTAR");

            cmbAlgorithms.SelectedIndex = 0; // select the first option by default
        }

        private void BtnLoadMap_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files|*.txt";
            openFileDialog1.Title = "Select Map File";


            // opens the file explorer tab
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loadedMap = Map.LoadFromFile(openFileDialog1.FileName);

                // saves map name
                currentMapName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

                foundPath = null;
                mapPanel.Invalidate();

            }
        }

        private void SavePathToFile(string algorithmName, LinkedList<Coord> path)
        {
            // save path to file
            if (currentMapName == null)
                return;

            string outputFile = $"{currentMapName}Path_{algorithmName}.txt";

            using StreamWriter writer = new StreamWriter(outputFile);

            var node = path.Head;

            // write each coordinate in the path
            while (node != null)
            {
                writer.WriteLine($"{node.Value.Row} {node.Value.Col}");
                node = node.Next;
            }

            // If using A*, add the number of times the open list was ordered
            if (algorithmName.ToUpper() == "ASTAR" && AStar.OpenSortCount > 0)
            {
                writer.WriteLine($"Open List Sorted: {AStar.OpenSortCount} times");
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (loadedMap == null)
            {
                MessageBox.Show("Load a map first.");
                return;
            }

            // Get the selected algorithm name from the UI
            var sel = cmbAlgorithms.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(sel))
            {
                MessageBox.Show("Select an algorithm.");
                return;
            }

            string algorithmName = sel;

            // Create the pathfinder instance from the factory
            PathFinderInterface pathfinder = PathFinderFactory.Create(algorithmName);

            // Prepare inputs for the algorithm
            int[,] grid = loadedMap.Grid;
            Coord start = loadedMap.Start;
            Coord goal = loadedMap.Goal;

            LinkedList<Coord> path = new LinkedList<Coord>();

            // Run the chosen algorithm
            bool success = pathfinder.FindPath(grid, start, goal, ref path);

            if (!success)
            {
                foundPath = null;

                // ? If the algorithm provides a message, display it
                if (pathfinder is Dijkstra d && !string.IsNullOrEmpty(d.LastErrorMessage))
                    MessageBox.Show(d.LastErrorMessage);
                else
                    MessageBox.Show("No path found.");
                    return;
            }
            else
            {
                foundPath = path;
                SavePathToFile(algorithmName, path);
                MessageBox.Show($"Path saved to {currentMapName}Path_{algorithmName}.txt");
            }

            mapPanel.Invalidate(); // redraw to show the path
        }

        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            if (loadedMap == null)
                return;

            // Draw the map
            MapRenderer.DrawMap(e.Graphics, loadedMap, mapPanel.ClientRectangle);

            // Draw the path if we have one
            if (foundPath != null)
                DrawPath(e.Graphics);
        }

        private void DrawPath(Graphics g)
        {
            if (loadedMap == null || foundPath == null || foundPath.IsEmpty)
                return;

            int rows = loadedMap.Rows;
            int cols = loadedMap.Cols;

            Rectangle area = mapPanel.ClientRectangle;

            int cellW = area.Width / cols;
            int cellH = area.Height / rows;
            int cellSize = Math.Min(cellW, cellH);

            int gridWidth = cols * cellSize;
            int gridHeight = rows * cellSize;

            int offsetX = (area.Width - gridWidth) / 2;
            int offsetY = (area.Height - gridHeight) / 2;

            using Pen pen = new Pen(Color.Magenta, 3);

            // go through the linked list and draw lines between successive coordinates
            var node = foundPath.Head;
            LinkedListNode<Coord>? prev = null;

            while (node != null)
            {
                if (prev != null)
                {
                    Point p1 = new Point(
                        offsetX + prev.Value.Col * cellSize + cellSize / 2,
                        offsetY + prev.Value.Row * cellSize + cellSize / 2);

                    Point p2 = new Point(
                        offsetX + node.Value.Col * cellSize + cellSize / 2,
                        offsetY + node.Value.Row * cellSize + cellSize / 2);

                    g.DrawLine(pen, p1, p2);
                }

                prev = node;
                node = node.Next;
            }
        }
    }
}
