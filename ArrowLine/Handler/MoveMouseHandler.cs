﻿using ArrowLine.Table.StringData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArrowLine.Selection
{
    public class MoveMouseHandler : IMouseHandler
    {
        Point tmpPoint;
        public void OnMouseDown(AbstractFigure abstractFigure, MouseEventArgs e, Form form, ContextMenuStrip contextMenuStrip)
        {
            //tmpPoint = e.Location;
        }

        public void OnMouseMove(AbstractFigure currentFigure, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnMouseUp(AbstractFigure currentFigure, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnPaint(AbstractFigure currentFigure, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        public IDTO OnToolStripMenuItemAddStringDataTable_Click(StringDataForm stringDataForm)
        {
            throw new NotImplementedException();
        }
    }
}