using System;
using System.IO;

namespace COM5113_Assignment_WinForm.MapInfo
{
    public class Map
    {
        public int[,] Grid { get; }
        public Coord Start { get; }
        public Coord Goal { get; }

        public int Rows => Grid.GetLength(0);
        public int Cols => Grid.GetLength(1);

        // ? CORRECT CONSTRUCTOR (matches LoadFromFile)
        private Map(int[,] grid, Coord start, Coord goal)
        {
            Grid = grid;
            Start = start;
            Goal = goal;
        }

        public static Map LoadFromFile(string filepath)
        {
            var lines = File.ReadAllLines(filepath);

            if (lines.Length < 4)
                throw new Exception("Invalid map file format: not enough lines.");

            // First line -> rows cols (but be tolerant if this is wrong)
            var sizeParts = lines[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (sizeParts.Length < 2)
                throw new Exception("Invalid map file format: first line must contain two integers for rows and columns.");

            int parsedRows = int.Parse(sizeParts[0]);
            int parsedCols = int.Parse(sizeParts[1]);

            // Second line -> start
            var startParts = lines[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Coord start = new Coord(
                int.Parse(startParts[0]),
                int.Parse(startParts[1])
            );

            // Third line -> goal
            var goalParts = lines[2].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Coord goal = new Coord(
                int.Parse(goalParts[0]),
                int.Parse(goalParts[1])
            );

            // Determine data-derived dimensions
            int dataRowsAvailable = lines.Length - 3;

            // Look at the first data row to infer column count
            var firstRowTokens = lines[3].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int actualColsInFirstRow = firstRowTokens.Length;

            int rows = parsedRows;
            int cols = parsedCols;

            // If header disagrees with data, attempt to correct common issues:
            // 1) Header swapped rows/cols (common mistake) -> detect if first row token count equals parsedRows
            if (actualColsInFirstRow != parsedCols && actualColsInFirstRow == parsedRows)
            {
                // swap
                rows = parsedCols;
                cols = parsedRows;
            }
            else if (dataRowsAvailable != parsedRows || actualColsInFirstRow != parsedCols)
            {
                // Fallback: prefer the data we actually have
                rows = dataRowsAvailable;
                cols = actualColsInFirstRow;
            }

            if (rows <= 0 || cols <= 0)
                throw new Exception($"Invalid map dimensions inferred: rows={rows}, cols={cols}.");

            if (lines.Length < 3 + rows)
                throw new Exception($"Not enough data rows. Expected {rows} data rows but file has {dataRowsAvailable}.");

            int[,] grid = new int[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                var rowData = lines[3 + r]
                    .Trim()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (rowData.Length != cols)
                    throw new Exception($"Row {r} does not contain {cols} columns (found {rowData.Length}).");

                for (int c = 0; c < cols; c++)
                    grid[r, c] = int.Parse(rowData[c]);
            }

            // Validate start/goal are in bounds
            if (start.Row < 0 || start.Col < 0 || start.Row >= rows || start.Col >= cols)
                throw new Exception($"Start coordinate {start.Row} {start.Col} is out of bounds for map size {rows}x{cols}.");

            if (goal.Row < 0 || goal.Col < 0 || goal.Row >= rows || goal.Col >= cols)
                throw new Exception($"Goal coordinate {goal.Row} {goal.Col} is out of bounds for map size {rows}x{cols}.");

            // Validate start/goal are not walls (0 means wall)
            if (grid[start.Row, start.Col] == 0)
                throw new Exception($"Start coordinate {start.Row} {start.Col} is a wall (value 0). Please choose a walkable start.");

            if (grid[goal.Row, goal.Col] == 0)
                throw new Exception($"Goal coordinate {goal.Row} {goal.Col} is a wall (value 0). Please choose a walkable goal.");

            // ? THIS NOW MATCHES THE CONSTRUCTOR
            return new Map(grid, start, goal);
        }
    }
}
