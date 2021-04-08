﻿using System.Drawing;

namespace ArrowLine.Line
{
    public class SolidLine : AbstractArrow, ILine
    {
        public SolidLine(Pen pen, Point startPoint, Point endPoint)
        {
            _pen = pen;
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawLines(_pen, CreateArrowLine());
        }

        public Point[] CreateArrowLine()
        {
            return new Point[] {
                _startPoint,
                new Point(_startPoint.X, _endPoint.Y),
                _endPoint
            };
        }
    }
}
