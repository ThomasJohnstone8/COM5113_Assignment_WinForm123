using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace COM5113_Assignment_WinForm.MapInfo
{
    public static class MapRenderer
    {
        public static void DrawMap(Graphics g, Map map, Rectangle area)
        {
            if (map == null)
                return;

            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.None;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int rows = map.Rows;
            int cols = map.Cols;

            // finds the maximum cell size that fits in the area
            int cellW = area.Width / cols;
            int cellH = area.Height / rows;
           
            int cellSize = Math.Min(cellW, cellH);
            if (cellSize <= 0) return;

            // Compute total used space
            int gridWidth = cols * cellSize;
            int gridHeight = rows * cellSize;

            // Center the grid in the panel
            int offsetX = (area.Width - gridWidth) / 2;
            int offsetY = (area.Height - gridHeight) / 2;

            // Draw terrain cells
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Color color = map.Grid[r, c] switch
                    {
                        1 => Color.White,
                        2 => Color.Green,
                        3 => Color.Blue,
                        0 => Color.Black,
                        _ => Color.Gray
                    };

                    Rectangle rect = new Rectangle(
                        offsetX + c * cellSize,
                        offsetY + r * cellSize,
                        cellSize,
                        cellSize);

                    using (var brush = new SolidBrush(color))
                        g.FillRectangle(brush, rect);

                    g.DrawRectangle(Pens.DarkGray, rect);
                }
            }

            // start position is yellow
            int sx = offsetX + map.Start.Col * cellSize;
            int sy = offsetY + map.Start.Row * cellSize;

            using (var startBrush = new SolidBrush(Color.Yellow))
                g.FillEllipse(startBrush, sx, sy, cellSize, cellSize);

            // the goal postion is red
            int gx = offsetX + map.Goal.Col * cellSize;
            int gy = offsetY + map.Goal.Row * cellSize;

            using (var goalBrush = new SolidBrush(Color.Red))
                g.FillEllipse(goalBrush, gx, gy, cellSize, cellSize);

        }
    }
}
