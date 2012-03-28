using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AStar_Incomplete
{
    public class Map : DrawableGameComponent
    {
        private readonly int _columnCount;
        private readonly int _rowCount;
        private readonly int _cellSize;

        private Texture2D _texture;
        private List<int> _path = new List<int>();
        private bool[] _cells;

        private SpriteBatch _spriteBatch;
        private bool _draggingStartCell;
        private bool _draggingTargetCell;
        private Color _gridLineColor = new Color(30, 30, 30);

        public Map(int rowCount, int columnCount, int cellSize, Game game)
            : base(game)
        {
            _rowCount = rowCount;
            _columnCount = columnCount;
            _cellSize = cellSize;

            _cells = new bool[_columnCount * _rowCount];
        }


        public int StartCell { get; private set; }
        public int TargetCell { get; private set; }


        public override void Initialize()
        {
            base.Initialize();

            _spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            StartCell = 0;
            TargetCell = (_rowCount * _columnCount) - 1;

            for (var i = 0; i < _columnCount * _rowCount; i++)
            {
                _cells[i] = true;
            }

            _texture = new Texture2D(Game.GraphicsDevice, 1, 1);
            _texture.SetData(new[] { Color.White });
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton != ButtonState.Pressed)
            {
                _draggingStartCell = false;
                _draggingTargetCell = false;
                return;
            }

            if (IsMouseOverGrid(mouseState) == false)
                return;

            var index = (mouseState.X / _cellSize) + (mouseState.Y / _cellSize) * _columnCount;

            if (index == StartCell)
            {
                _draggingStartCell = true;
            }
            else if (index == TargetCell)
            {
                _draggingTargetCell = true;
            }

            if (_draggingStartCell)
            {
                StartCell = index;
                return;
            }

            if (_draggingTargetCell)
            {
                TargetCell = index;
                return;
            }

            _cells[index] = false;
        }

        private bool IsMouseOverGrid(MouseState mouseState)
        {
            return mouseState.X > 0 && mouseState.X < _columnCount * _cellSize &&
                   mouseState.Y > 0 && mouseState.Y < _rowCount * _cellSize;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            DrawGridLines();
            DrawCells();
        }

        private void DrawGridLines()
        {
            for (int i = 0; i <= _columnCount; i++)
            {
                var linepositionRectangle = new Rectangle(i * _cellSize, 0, 1, _cellSize * _rowCount);
                _spriteBatch.Draw(_texture, linepositionRectangle, _gridLineColor);
            }

            for (int i = 0; i <= _rowCount; i++)
            {
                var linepositionRectangle = new Rectangle(0, i * _cellSize, _cellSize * _rowCount, 1);
                _spriteBatch.Draw(_texture, linepositionRectangle, _gridLineColor);
            }
        }

        private void DrawCells()
        {
            for (var i = 0; i < _columnCount; i++)
            {
                for (var j = 0; j < _rowCount; j++)
                {
                    var cellIndex = (i * _columnCount) + j;
                    if (_path.Contains(cellIndex))
                        _spriteBatch.Draw(_texture, new Rectangle(j * _cellSize + 1, i * _cellSize + 1, _cellSize - 1, _cellSize - 1),
                                          Color.Blue);

                    if (cellIndex == StartCell)
                        _spriteBatch.Draw(_texture, new Rectangle(j * _cellSize + 1, i * _cellSize + 1, _cellSize - 1, _cellSize - 1),
                                          Color.Green);

                    if (cellIndex == TargetCell)
                        _spriteBatch.Draw(_texture, new Rectangle(j * _cellSize + 1, i * _cellSize + 1, _cellSize - 1, _cellSize - 1),
                                          Color.Red);

                    if (_cells[cellIndex])
                        continue;

                    _spriteBatch.Draw(_texture, new Rectangle(j * _cellSize + 1, i * _cellSize + 1, _cellSize - 1, _cellSize - 1),
                                      Color.White);
                }
            }
        }

        public int GetAdjacentCellIndex(int cellIndex, int columnOffset, int rowOffset)
        {
            var x = cellIndex % _columnCount;
            var y = cellIndex / _columnCount;

            if ((x + columnOffset < 0 || x + columnOffset > _columnCount - 1) ||
                (y + rowOffset < 0 || y + rowOffset > _rowCount - 1))
                return -1;

            if (_cells[((y + rowOffset) * _columnCount) + (x + columnOffset)] == false)
                return -1;

            return cellIndex + columnOffset + (rowOffset * _columnCount);
        }

        public int GetDistance(int startCell, int endCell)
        {
            var startX = startCell % _columnCount;
            var startY = startCell / _columnCount;

            var endX = endCell % _columnCount;
            var endY = endCell / _columnCount;

            return Math.Abs(startX - endX) + Math.Abs(startY - endY);
        }

        public void HighlightPath(Node node)
        {
            var currentNode = node;
            while (currentNode != null)
            {
                _path.Add(currentNode.CellIndex);
                currentNode = currentNode.Parent;
            }
        }

        public void Reset()
        {
            _path.Clear();

            for (var i = 0; i < _columnCount * _rowCount; i++)
            {
                _cells[i] = true;
            }
        }
    }
}