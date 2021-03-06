﻿using MazeWpfApp.ViewModels;
using MazeWpfApp.Views;
using Q_Learning;
using System;
using System.Collections.Generic;

namespace MazeWpfApp.Helpers
{
    /// <summary>
    /// Generic constructor for maze walls.
    /// </summary>
    public class MazeConstructor
    {
        private readonly MazeSettings _Settings;
        private readonly Maze _Maze;

        public MazeConstructor(MazeSettings settings)
        {
            _Settings = settings;
            _Maze = _Settings.Maze;
        }

        public IEnumerable<WallView> GetMazeWallsViews()
        {
            var walls = new List<WallView>();

            for (int i = 0; i < _Maze.QuantityOfSquares; i++)
            {
                for(int j = 0; j < _Maze.QuantityOfSquares; j++)
                {
                    if (ShouldBeWallBetween(i, j))
                    {
                        var (x1, y1, x2, y2) = GetEndpointsOfWall(i, j);
                        var wall = new WallView(new WallViewModel(x1, y1, x2, y2));
                        walls.Add(wall);
                    }
                }
            }

            return walls;
        }

        private bool ShouldBeWallBetween(int firstSquareIndex, int secondSquareIndex)
        {
            var indexesAreNotTheSame = firstSquareIndex != secondSquareIndex;
            var squaresAreNeighbors = (firstSquareIndex - secondSquareIndex == 1) ||
                                      (secondSquareIndex - firstSquareIndex == 1) ||
                                      (firstSquareIndex - secondSquareIndex == _Maze.QuantityOfColumns) ||
                                      (secondSquareIndex - firstSquareIndex == _Maze.QuantityOfColumns);
            var moveIsNotAllowed = _Maze.Matrix[firstSquareIndex][secondSquareIndex] == 0;

            return indexesAreNotTheSame && squaresAreNeighbors && moveIsNotAllowed;
        }

        private (double x1, double y1, double x2, double y2) GetEndpointsOfWall(int firstSquareId, int secondSquareId)
        {
            (double x, double y) firstSquareTopLeftCornerPosition = GetPositionOfSquareTopLeftCorner(firstSquareId);
            double x1 = -1, y1 = -1, x2 = -1, y2 = -1;

            //top wall of first square
            if(firstSquareId == (secondSquareId + _Maze.QuantityOfColumns))
            {
                x1 = firstSquareTopLeftCornerPosition.x;
                y1 = firstSquareTopLeftCornerPosition.y;
                x2 = firstSquareTopLeftCornerPosition.x + _Maze.SizeOfCell;
                y2 = firstSquareTopLeftCornerPosition.y;
            }

            //bottom wall of first square
            if(firstSquareId == (secondSquareId - _Maze.QuantityOfColumns))
            {
                x1 = firstSquareTopLeftCornerPosition.x;
                y1 = firstSquareTopLeftCornerPosition.y + _Maze.SizeOfCell;
                x2 = firstSquareTopLeftCornerPosition.x + _Maze.SizeOfCell;
                y2 = firstSquareTopLeftCornerPosition.y + _Maze.SizeOfCell;
            }

            //left wall of first square
            if(firstSquareId == (secondSquareId + 1))
            {
                x1 = firstSquareTopLeftCornerPosition.x;
                y1 = firstSquareTopLeftCornerPosition.y;
                x2 = firstSquareTopLeftCornerPosition.x;
                y2 = firstSquareTopLeftCornerPosition.y + _Maze.SizeOfCell;
            }

            //right wall of first square
            if(firstSquareId == (secondSquareId - 1))
            {
                x1 = firstSquareTopLeftCornerPosition.x + _Maze.SizeOfCell;
                y1 = firstSquareTopLeftCornerPosition.y;
                x2 = firstSquareTopLeftCornerPosition.x + _Maze.SizeOfCell;
                y2 = firstSquareTopLeftCornerPosition.y + _Maze.SizeOfCell;
            }

            return (x1, y1, x2, y2);
        }

        private (double x, double y) GetPositionOfSquareTopLeftCorner(int index)
        {
            var matrix = GetLatticeMatrix();

            for (int row = 0; row < _Maze.QuantityOfRows; row++)
            {
                for (int column = 0; column < _Maze.QuantityOfColumns; column++)
                {
                    if(matrix[row,column] == index)
                    {
                        double x = (column * _Maze.SizeOfCell) + _Settings.StartXPos;
                        double y = (row * _Maze.SizeOfCell) + _Settings.StartYPos;
                         
                        return (x, y);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private int[,] GetLatticeMatrix()
        {
            int[,] matrix = new int[_Maze.QuantityOfRows,_Maze.QuantityOfColumns];
            int index = 0;
            
            for(int row = 0; row < _Maze.QuantityOfRows; row++)
            {
                for(int column = 0; column < _Maze.QuantityOfColumns; column++)
                {
                    matrix[row,column] = index;
                    index++;
                }
            }

            return matrix;
        }
    }
}
