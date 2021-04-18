﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArrowLine
{
    public abstract class AbstractTable : AbstractFigure
    {
        protected TextBox title;
        protected int width = 140;
        protected int height = 60;
        protected int heightStringRectangle = 20;
        protected int stepDownLine = 20;
        protected int stepDownFieldPoint = 20;
        protected int stepDownPropertyPoint = 22;
        protected int stepDownMethodPoint = 24;
        protected bool selected;
        protected List<string> fields;
        protected List<string> properties;
        protected List<string> methods;
        protected List<Rectangle> fieldRectangles;
        protected List<Rectangle> propertieRectangles;
        protected List<Rectangle> methodRectangles;
        protected List<LineInTable> linesInTable;
        public Rectangle objectRectangle;
        protected Rectangle stringRectangle;
        public List<Rectangle> highlightRectangles = new List<Rectangle>();
        public Font font;
        public StringFormat format;
        public Pen whitePen;
        public SolidBrush solidBrush;

        public AbstractTable()
        {
            objectRectangle = new Rectangle();
            objectRegion = new Region();

            font = new Font("Arial", 12);
            format = new StringFormat();
            whitePen = new Pen(Color.White, 2);////////////////
            solidBrush = new SolidBrush(Color.Black);//////Pen.Color
            linesInTable = new List<LineInTable>();
            fields = new List<string>();
            fieldRectangles = new List<Rectangle>();
            properties = new List<string>();
            propertieRectangles = new List<Rectangle>();
            methods = new List<string>();
            methodRectangles = new List<Rectangle>();
            format.Alignment = StringAlignment.Center;

            LineInTable lineInTable = new LineInTable();

            for (int i = 0; i < 3; i++)
            {
                linesInTable.Add(lineInTable);
            }
        }
        public virtual Rectangle ObjectRectangle
        {
            get { return objectRectangle; }
            set { objectRectangle = value; }
        }
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public virtual void DrawOverlay(Graphics g)
        {
           
            if (selected)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(objectRectangle.Left - 8, objectRectangle.Top - 8, 8, 8));
                g.FillRectangle(Brushes.Black, new Rectangle(objectRectangle.Right, objectRectangle.Top - 8, 8, 8));
                g.FillRectangle(Brushes.Black, new Rectangle(objectRectangle.Left - 8, objectRectangle.Bottom, 8, 8));
                g.FillRectangle(Brushes.Black, new Rectangle(objectRectangle.Right, objectRectangle.Bottom, 8, 8));

          
                g.FillRectangle(Brushes.Black, new Rectangle(objectRectangle.Left + objectRectangle.Width / 2 - 4, objectRectangle.Top - 8, 8, 8));
                g.FillRectangle(Brushes.Black, new Rectangle(objectRectangle.Left - 8, objectRectangle.Top + objectRectangle.Height / 2 - 4, 8, 8));
                g.FillRectangle(Brushes.Black, new Rectangle(objectRectangle.Left + objectRectangle.Width / 2 - 4, objectRectangle.Bottom, 8, 8));
                g.FillRectangle(Brushes.Black, new Rectangle(objectRectangle.Right, objectRectangle.Top + objectRectangle.Height / 2 - 4, 8, 8));

                g.DrawRectangle(Pens.CadetBlue, objectRectangle);
            }
        }
        public virtual bool HitTest(Point pt)
        {
            return objectRegion.IsVisible(pt);
        }

        public virtual bool HitTest(Rectangle r)
        {
            return objectRegion.IsVisible(r);
        }

        public virtual void AddField(Pen pen, Graphics graphics)
        {
            stepDownPropertyPoint += 20;
            stepDownMethodPoint += 20;
            format.Alignment = StringAlignment.Near;

            graphics.DrawRectangle(whitePen, objectRectangle);

            if (methodRectangles.Count != 0)
            {
                ClearArea(graphics, methodRectangles);
                ReDrawArea(graphics, methodRectangles, methods);
            }

            if (propertieRectangles.Count != 0)
            {
                ClearArea(graphics, propertieRectangles);
                ReDrawArea(graphics, propertieRectangles, properties);

                DrawHorizontalLine(pen, graphics, lineIndex: 2, stepDownPropertyPoint);
            }

            DrawStringRectangle(graphics, font, format, "Field", heightStringRectangle,
                stepDownPoint: stepDownFieldPoint += 20);

            fields.Add("Field");
            fieldRectangles.Add(stringRectangle);

            objectRectangle.Height += heightStringRectangle;
            graphics.DrawRectangle(pen, objectRectangle);

            DrawHorizontalLine(pen, graphics, lineIndex: 1, stepDownFieldPoint);
        }

        public virtual void AddProperty(Pen pen, Graphics graphics)
        {
            stepDownMethodPoint += 20;

            format.Alignment = StringAlignment.Near;

            graphics.DrawRectangle(whitePen, objectRectangle);

            if (methodRectangles.Count != 0)
            {
                ClearArea(graphics, methodRectangles);
                ReDrawArea(graphics, methodRectangles, methods);
            }

            DrawStringRectangle(graphics, font, format, "Property", heightStringRectangle,
                stepDownPoint: stepDownPropertyPoint += 20);

            properties.Add("Property");
            propertieRectangles.Add(stringRectangle);

            objectRectangle.Height += heightStringRectangle;
            graphics.DrawRectangle(pen, objectRectangle);

            DrawHorizontalLine(pen, graphics, lineIndex: 2, stepDownPropertyPoint);
        }

        public virtual void AddMethod(Pen pen, Graphics graphics)
        {
            format.Alignment = StringAlignment.Near;

            graphics.DrawRectangle(whitePen, objectRectangle);

            DrawStringRectangle(graphics, font, format, "Method", heightStringRectangle,
                stepDownPoint: stepDownMethodPoint += 20);

            methods.Add("Method");
            methodRectangles.Add(stringRectangle);

            objectRectangle.Height += heightStringRectangle;
            graphics.DrawRectangle(pen, objectRectangle);
        }

        //public override void Draw(Pen pen, Graphics graphics)
        //{
        //}
        protected abstract void Resize();
        protected abstract void Move();
        protected virtual void DrawStringRectangle(
            Graphics graphics, Font font, StringFormat format, string text, int heightStringRectangle, int stepDownPoint)
        {
            stringRectangle = new Rectangle(_startPoint.X, _startPoint.Y + stepDownPoint, width, heightStringRectangle);

            graphics.DrawRectangle(whitePen, _startPoint.X, _startPoint.Y + stepDownPoint, width, heightStringRectangle);
            graphics.DrawString(text, font, solidBrush, stringRectangle, format);
        }

        protected virtual void ReDrawArea(
            Graphics graphics, List<Rectangle> stringRectangles, List<string> stringData)
        {
            for (int i = 0; i < stringRectangles.Count; i++)
            {
                stringRectangle = new Rectangle(stringRectangles[i].X,
                    stringRectangles[i].Y + 20, width, heightStringRectangle);

                stringRectangles[i] = stringRectangle;

                graphics.DrawRectangle(whitePen, stringRectangles[i].X, stringRectangles[i].Y, width, heightStringRectangle);
                graphics.DrawString(stringData[i], font, solidBrush, stringRectangles[i], format);
            }
        }

        protected virtual void ClearArea(Graphics graphics, List<Rectangle> stringRectangles)
        {
            Point[] points = new Point[] {
                new Point(stringRectangles[0].X,
                    stringRectangles[0].Y),

                new Point(stringRectangles[0].X + width,
                    stringRectangles[0].Y),

                new Point(stringRectangles[stringRectangles.Count - 1].X + width,
                    stringRectangles[stringRectangles.Count - 1].Y + 20),

                new Point(stringRectangles[stringRectangles.Count - 1].X,
                    stringRectangles[stringRectangles.Count - 1].Y + 20),
            };

            SolidBrush shadowBrush = new SolidBrush(Color.White);

            graphics.DrawPolygon(whitePen, points);
            graphics.FillPolygon(shadowBrush, points);
        }

        protected virtual void DrawHorizontalLine(Pen pen, Graphics graphics, int lineIndex, int stepDownPoint)
        {
            linesInTable[lineIndex]._startLinePoint.X = _startPoint.X;
            linesInTable[lineIndex]._startLinePoint.Y = _startPoint.Y + stepDownPoint + heightStringRectangle;
            linesInTable[lineIndex]._endLinePoint.X = _startPoint.X + width;
            linesInTable[lineIndex]._endLinePoint.Y = _startPoint.Y + stepDownPoint + heightStringRectangle;
            graphics.DrawLine(pen, linesInTable[lineIndex]._startLinePoint, linesInTable[lineIndex]._endLinePoint);
        }
    }
}
