using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOTP_LR_2
{
    class FigureDirector
    {
        public Shape MakeFigure(FigureBuilder pFigureBuilder , List<Point> lpPoints, SolidColorBrush pColor)
        {
            pFigureBuilder.SetPoints(lpPoints);
            pFigureBuilder.SetFill(pColor);
            return pFigureBuilder.m_pFigure;
        }
    }
    abstract class FigureBuilder
    {
        public Shape m_pFigure { get;  set; }
        public void CreateBread(Shape pFigure)
        {
            m_pFigure = pFigure;
        }
        public abstract void SetPoints(List<Point> lpPoints);
        public abstract void SetFill(SolidColorBrush pColor);
    }
    class TrangleBuilder : FigureBuilder
    {
        Polygon m_pTrangle = new Polygon();
        public TrangleBuilder()
        {
            Reset();                        
        }
        private void Reset()
        {
            m_pFigure = m_pTrangle;
        }
        public override void SetPoints(List<Point> lpPoints)
        {
            for(int i = 0; i < lpPoints.Count; i ++)
            {
                m_pTrangle.Points.Add(lpPoints[i]);
            }
        }
        public override void SetFill(SolidColorBrush pColor)
        {
            m_pFigure.Fill = pColor;
        }
    }

    class RectangleBuilder : FigureBuilder
    {
        Rectangle m_pRectangle = new Rectangle();
        public RectangleBuilder()
        {
            Reset();
        }
        private void Reset()
        {
            m_pFigure = m_pRectangle;
        }
        public override void SetPoints(List<Point> lpPoints)
        {
            m_pRectangle.Width = Math.Abs(lpPoints[0].X) + Math.Abs(lpPoints[1].X);
            m_pRectangle.Height = Math.Abs(lpPoints[0].Y) + Math.Abs(lpPoints[1].Y);
        }
        public override void SetFill(SolidColorBrush pColor)
        {
            m_pFigure.Fill = pColor;
        }
    }

    class CircleBuilder : FigureBuilder
    {
        Ellipse m_pEllipse = new Ellipse();
        public CircleBuilder()
        {
            Reset();
        }
        private void Reset()
        {
            m_pFigure = m_pEllipse;
        }
        public override void SetPoints(List<Point> lpPoints)
        {
            m_pEllipse.Width = m_pEllipse.Height = Math.Sqrt(Math.Pow(lpPoints[0].X, 2) + Math.Pow(lpPoints[0].Y, 2));
        }
        public override void SetFill(SolidColorBrush pColor)
        {
            m_pFigure.Fill = pColor;
        }
    }
}
