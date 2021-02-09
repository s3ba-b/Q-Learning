﻿using MazeWpfApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeWpfApp.ViewModels
{
    public class CellViewModel
    {
        private readonly MazeSettings _Settings;

        public CellViewModel(int id, double topLeftX, double topLeftY, int size, MazeSettings settings)
        {
            Id = id;
            Height = size;
            Width = size;
            _Settings = settings;
            Content = CreateCell(topLeftX, topLeftY);
        }

        public int Id { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public Grid Content { get; set; }
        
        private Grid CreateCell(double topLeftX, double topLeftY)
        {
            var grid = new Grid();

            var shiftBeyondCornersInX = Width - 1;
            var shiftBeyondCornersInY = Height - 1;

            Line topLine = GetLine(topLeftX, topLeftY, topLeftX + shiftBeyondCornersInX, topLeftY);
            grid.Children.Add(topLine);

            Line botLine = GetLine(topLeftX, topLeftY + shiftBeyondCornersInY, topLeftX + shiftBeyondCornersInX, topLeftY + shiftBeyondCornersInY);
            grid.Children.Add(botLine);

            Line leftLine = GetLine(topLeftX, topLeftY, topLeftX, topLeftY + shiftBeyondCornersInY);
            grid.Children.Add(leftLine);

            Line rightLine = GetLine(topLeftX + shiftBeyondCornersInX, topLeftY, topLeftX + shiftBeyondCornersInX, topLeftY + shiftBeyondCornersInY);
            grid.Children.Add(rightLine);

            UIElement backgroundRectangle = GetBackgroundRectangle(topLeftX, topLeftY, topLeftX + shiftBeyondCornersInX, topLeftY + shiftBeyondCornersInY);
            grid.Children.Add(backgroundRectangle);

            return grid;
        }

        private UIElement GetBackgroundRectangle(double topLeftX, double topLeftY, double bottomRightX, double bottomRightY)
        {
            Canvas canvas = new Canvas();
            canvas.Background = Brushes.Red;
            canvas.Margin = new Thickness(topLeftX, topLeftY, _Settings.WindowWidth - bottomRightX, _Settings.WindowHeight - bottomRightY);
            canvas.Visibility = Visibility.Visible;
            canvas.MaxHeight = Height;
            canvas.MinHeight = Height;
            canvas.MaxWidth = Width;
            canvas.MinWidth = Width;

            return canvas;
        }

        private static Line GetLine(double x1, double y1, double x2, double y2)
        {
            var line = new Line();

            line.X1 = x1;
            line.Y1 = y1;

            line.X2 = x2;
            line.Y2 = y2;

            line.Stroke = Brushes.Gray;
            line.StrokeThickness = 0.5;

            return line;
        }
    }
}
