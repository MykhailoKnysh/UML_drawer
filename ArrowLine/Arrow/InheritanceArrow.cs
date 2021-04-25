﻿using ArrowLine.CapArrow;
using ArrowLine.Line;
using System.Drawing;
using System;
using ArrowLine.Abstract;

namespace ArrowLine.Arrow
{
    public class InheritanceArrow : AbstractFigure
    {
        public InheritanceArrow(FigureType type)
        {
            Type = type;
        }

        public override void Draw()
        {
            SetDelta();

            AbstractLine line = new SolidLine(StartPoint, EndPoint);
            line.Draw();
            AbstractCapArrow arrowCap = new CloseCapArrow(StartPoint, EndPoint);
            arrowCap.Draw();

        }
    }
}
