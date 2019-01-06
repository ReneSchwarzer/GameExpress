using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameExpress.Editor.Pages
{
    /// <summary>
    ///  Diese Klasse repräsentiert ein Rahmen, der durch Ziehpunkte (Handle) in der Größe 
    ///  und Drehung verändert werden kann.
    ///  x-------x--------x
    ///  |				  |
    ///  |				  |
    ///  |				  |
    ///  x       x		  x
    ///  |				  |
    ///  |			      |
    ///  |				  |
    ///  x-------x--------x
    /// </summary>
    public class PullFrame
    {
        public class DragAndDrop 
        {
		    public enum DragAndDropState { None, Move, TopLeft, Top, TopRight, Left, Center, Right, BottomLeft, Bottom, BottomRight };
		    public DragAndDropState State { get; set; }
		    public Size Offset { get; set; }
		    public float m_va;			// Verhältnis der Komponenten des a-Vektors
		    public float m_vb;			// Verhältnis der Komponenten des b-Vektors
	    }

        public enum PullFrameState { None, Size, Rotate };

        /// <summary>
        /// Die Größe der Handels
        /// </summary>
        public const int HandleSize = 4;

        /// <summary>
        /// Liefert oder setzt ob das PullFrame aktiv ist.
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Liefert oder setzt den Zustand
        /// </summary>
        public PullFrameState State { get; set; }

        /// <summary>
        /// Ermittelt ob Drag & Drop aktiv
        /// </summary>
        public bool IsDragAndDrop { get { return m_dragAndDrop.State != PullFrame.DragAndDrop.DragAndDropState.None; } }

        /// <summary>
        /// Darg and Drop - Eigenschaften
        /// </summary>
        private DragAndDrop m_dragAndDrop = new DragAndDrop();

        /// <summary>
        /// Liefert oder setzt die obere Linke Ecke
        /// </summary>
        protected PointF HandleTopLeft { get; set; }
        protected PointF HandleTop { get; set; }
        protected PointF HandleTopRight { get; set; }
        protected PointF HandleLeft { get; set; }
        protected PointF HandleBottomLeft { get; set; }
        protected PointF HandleBottom { get; set; }
        protected PointF HandleBottomRight { get; set; }
        protected PointF HandleRight { get; set; }
        protected PointF HandleCenter { get; set; }

        /// <summary>
        /// Liefert oder setzt den Drehpunkt
        /// </summary>
        protected PointF Origin { get; set; }

        /// <summary>
        ///  Handle Zustand wechseln
        /// </summary>
        public void NextHandleState()
        {
            switch (State)
            {
                case PullFrameState.None:
                    State = PullFrameState.Size;
                    break;
                case PullFrameState.Size:
                    State = PullFrameState.Rotate;
                    break;
                case PullFrameState.Rotate:
                default:
                    State = PullFrameState.None;
                    break;
            }
        }

        /// <summary>
        /// Dreht einen Punkt p um den Punkt origin. Die Gradzahl der Umdrehung 
        /// wird durch die Punkte p1 und p2 angegeben
        /// Alle Angaben beziehen sich auf die normalen Fensterkoordinaten,
        /// wobei Oben-Links 0,0 ist.
        /// </summary>
        /// <param name="p">Der zu drehende Punkt</param>
        /// <param name="origin">Der Drehpunkt</param>
        /// <param name="p1">Richtungsvektor a</param>
        /// <param name="p2">Richtungsvektor b</param>
        /// <returns>gedrehter Punkt in Fensterkoordinaten</returns>
        protected static PointF RotatePoint(PointF p, PointF origin, PointF p1, PointF p2) 
        {
	        // Drehwinkel bestimmen
	        double wzOP = (p1.X - origin.X == 0) ? Math.PI/2 : Math.Atan((origin.Y - p1.Y)/(double)(p1.X - origin.X));
	        double wzORP = (p2.X - origin.X == 0)? Math.PI/2 : Math.Atan((origin.Y - p2.Y)/(double)(p2.X - origin.X));
	        if (p1.X - origin.X <= 0) wzOP += Math.PI;
	        if (p2.X - origin.X <= 0) wzORP += Math.PI;
	        double dw = wzOP - wzORP;  
	
	        double x = /*Round(*/((p.X-origin.X)*Math.Cos(dw))+((origin.Y-p.Y)*Math.Sin(dw))/*)*/;
	        double y = /*Round(*/((origin.Y-p.Y)*Math.Cos(dw))-((p.X-origin.X)*Math.Sin(dw))/*)*/;
															  
	        return new PointF((float)(x + origin.X), (float)(origin.Y - y));
        }    

        /// <summary>
        /// Ermittelt den Maus-Cursor
        /// </summary>
        /// <param name="point">Die Mausposition</param>
        /// <returns>der Cursor, wenn Maus über ein Handle ist, null sonst</returns>
        public Cursor GetCursor(Point point)
        {
            if (!Enable) return null;
            
            switch (State)
            {
                case PullFrameState.Size:
                    switch (m_dragAndDrop.State)
                    {
                        case DragAndDrop.DragAndDropState.None:
                            {
                                if (PtInRect(HandleTopLeft, point))
                                {
                                    return Cursors.SizeNWSE;
                                }
                                else if (PtInRect(HandleTop, point))
                                {
                                    return Cursors.SizeNS;
                                }
                                else if (PtInRect(HandleTopRight, point))
                                {
                                    return Cursors.SizeNESW;
                                }
                                else if (PtInRect(HandleLeft, point))
                                {
                                    return Cursors.SizeWE;
                                }
                                else if (PtInRect(HandleBottomLeft, point))
                                {
                                    return Cursors.SizeNESW;
                                }
                                else if (PtInRect(HandleBottom, point))
                                {
                                    return Cursors.SizeNS;
                                }
                                else if (PtInRect(HandleBottomRight, point))
                                {
                                    return Cursors.SizeNWSE;
                                }
                                else if (PtInRect(HandleRight, point))
                                {
                                    return Cursors.SizeWE;
                                }
                            }
                            break;
                        // TopLeft
                        case DragAndDrop.DragAndDropState.TopLeft:
                            return Cursors.SizeNWSE;
                        // Top
                        case DragAndDrop.DragAndDropState.Top:
                            return Cursors.SizeNS;
                        // TopRight
                        case DragAndDrop.DragAndDropState.TopRight:
                            return Cursors.SizeNESW;
                        // Left
                        case DragAndDrop.DragAndDropState.Left:
                            return Cursors.SizeWE;
                        // BottomLeft
                        case DragAndDrop.DragAndDropState.BottomLeft:
                            return Cursors.SizeNESW;
                        // Bottom
                        case DragAndDrop.DragAndDropState.Bottom:
                            return Cursors.SizeNS;
                        // BottomRight	
                        case DragAndDrop.DragAndDropState.BottomRight:
                            return Cursors.SizeNWSE;
                        // Right
                        case DragAndDrop.DragAndDropState.Right:
                            return Cursors.SizeWE;
                        default:
                            return null;
                    }
                    
                    break;
                case PullFrameState.Rotate:
                    if (PtInRect(HandleTopLeft, point))
                    {
                        using (MemoryStream ms = new MemoryStream(global::GameExpress.Editor.Properties.Resources.rotate))
                        {
                            return new Cursor(ms);
                        }
                    }
                    else if (PtInRect(HandleTop, point))
                    {
                        return Cursors.SizeWE; 
                    }
                    else if (PtInRect(HandleTopRight, point))
                    {
                        using (MemoryStream ms = new MemoryStream(global::GameExpress.Editor.Properties.Resources.rotate))
                        {
                            return new Cursor(ms);
                        }
                    }
                    else if (PtInRect(HandleLeft, point))
                    {
                        return Cursors.SizeNS;
                    }
                    else if (PtInRect(HandleBottomLeft, point))
                    {
                        using (MemoryStream ms = new MemoryStream(global::GameExpress.Editor.Properties.Resources.rotate))
                        {
                            return new Cursor(ms);
                        }
                    }
                    else if (PtInRect(HandleBottom, point))
                    {
                        return Cursors.SizeWE;
                    }
                    else if (PtInRect(HandleBottomRight, point))
                    {
                        using (MemoryStream ms = new MemoryStream(global::GameExpress.Editor.Properties.Resources.rotate))
                        {
                            return new Cursor(ms);
                        }
                    }
                    else if (PtInRect(HandleRight, point))
                    {
                        return Cursors.SizeNS;
                    }
                    break;
            }

            return null;
        }

        /// <summary>
        /// Prüft ob der Punkt im einem Hanle ist
        /// </summary>
        /// <param name="p1">Das Handle</param>
        /// <param name="p2">der zu prüfende Punkt</param>
        /// <returns>true wenn erfolgreich, fals sonst</returns>
        private static RectangleF HandleRect(PointF p)
        {
            return new RectangleF(p.X - HandleSize, p.Y - HandleSize, HandleSize, HandleSize); 
        }
        
        /// <summary>
        /// Prüft ob der Punkt im einem Hanle ist
        /// </summary>
        /// <param name="p1">Das Handle</param>
        /// <param name="p2">der zu prüfende Punkt</param>
        /// <returns>true wenn erfolgreich, fals sonst</returns>
        private static bool PtInRect(PointF p1, PointF p2)
        {
            RectangleF rect = HandleRect(p1);

            return ((p2.X >= rect.X) && (p2.X <= (rect.X + rect.Width)) &&
                    (p2.Y >= rect.Y) && (p2.Y <= (rect.Y + rect.Height)));
        }

        /// <summary>
        /// Prüft ob der Punkt im einem Hanle ist
        /// </summary>
        /// <param name="p1">Das Handle</param>
        /// <returns>true wenn erfolgreich, fals sonst</returns>
        private static Point HandleCenterPoint(PointF p)
        {
            RectangleF rect = HandleRect(p);

            return new Point((int)(rect.X + rect.Width / 2), (int)(rect.Y + rect.Height / 2));
        }

        /// <summary>
        /// Handle zeichnen
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
	        // Brush
            //using (Brush hbrush = )
            {
                //using (Pen pen = SystemPens.Highlight)
                {
                    // Handle zeichnen für Größenenderung der Objekte
                    if (Enable)
                    {
                        switch (State)
                        {
                            case PullFrameState.None:
                                g.DrawLine(SystemPens.Highlight, HandleTopLeft, HandleTopRight);
                                g.DrawLine(SystemPens.Highlight, HandleTopRight, HandleBottomRight);
                                g.DrawLine(SystemPens.Highlight, HandleTopLeft, HandleBottomLeft);
                                g.DrawLine(SystemPens.Highlight, HandleBottomLeft, HandleBottomRight);
                                break;
                            case PullFrameState.Size:
                                g.FillRectangle(SystemBrushes.Highlight, HandleRect(HandleTopLeft));
                                g.FillRectangle(SystemBrushes.Highlight, HandleRect(HandleTop));
                                g.FillRectangle(SystemBrushes.Highlight, HandleRect(HandleTopRight));
                                g.FillRectangle(SystemBrushes.Highlight, HandleRect(HandleLeft));
                                g.FillRectangle(SystemBrushes.Highlight, HandleRect(HandleBottomLeft));
                                g.FillRectangle(SystemBrushes.Highlight, HandleRect(HandleBottom));
                                g.FillRectangle(SystemBrushes.Highlight, HandleRect(HandleBottomRight));
                                g.FillRectangle(SystemBrushes.Highlight, HandleRect(HandleRight));
                                break;
                            case PullFrameState.Rotate:
                                g.FillEllipse(SystemBrushes.Highlight, HandleRect(HandleTopLeft));
                                g.FillEllipse(SystemBrushes.Highlight, HandleRect(HandleTop));
                                g.FillEllipse(SystemBrushes.Highlight, HandleRect(HandleTopRight));
                                g.FillEllipse(SystemBrushes.Highlight, HandleRect(HandleLeft));
                                g.FillEllipse(SystemBrushes.Highlight, HandleRect(HandleBottomLeft));
                                g.FillEllipse(SystemBrushes.Highlight, HandleRect(HandleBottom));
                                g.FillEllipse(SystemBrushes.Highlight, HandleRect(HandleBottomRight));
                                g.FillEllipse(SystemBrushes.Highlight, HandleRect(HandleRight));
                                //pDC->Ellipse(HANDLERECT(m_dpTopLeft));
                                //pDC->FillRect(HANDLERECT(m_dpTop), &hbrush);
                                //pDC->Ellipse(HANDLERECT(m_dpTopRight));
                                //pDC->FillRect(HANDLERECT(m_dpLeft), &hbrush);
                                //pDC->Ellipse(HANDLERECT(m_dpBottomLeft));
                                //pDC->FillRect(HANDLERECT(m_dpBottom), &hbrush);
                                //pDC->Ellipse(HANDLERECT(m_dpBottomRight));
                                //pDC->FillRect(HANDLERECT(m_dpRight), &hbrush);
                                //pDC->SelectObject(op);
                                //pDC->SelectObject(ob);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Setzt die Frameposiion
        /// </summary>
        /// <param name="p0">Linke obere Ecke</param>
        /// <param name="pa">Richtingsvektor a</param>
        /// <param name="pb">Richtungsvektor b</param>
        public void Set(PointF p0, PointF pa, PointF pb)
        {
	        HandleTopLeft = new PointF(p0.X, p0.Y);
            HandleTop = new PointF(p0.X + (pa.X / 2), p0.Y + (pa.Y/2));
            HandleTopRight = new PointF(p0.X + pa.X, p0.Y + pa.Y);
            HandleLeft = new PointF(p0.X + (pb.X / 2), p0.Y + (pb.Y/2));
            HandleBottomLeft = new PointF(p0.X + pb.X, p0.Y + pb.Y);
            HandleBottom = new PointF(p0.X + (pa.X / 2) + pb.X, p0.Y + (pa.Y/2) + pb.Y);
            HandleBottomRight = new PointF(p0.X + pa.X + pb.X, p0.Y + pa.Y + pb.Y);
            HandleRight = new PointF(p0.X + pa.X + (pb.X / 2), p0.Y + pa.Y + (pb.Y/2));
        }

        /// <summary>
        /// Liefert die Koordinaten in Form von Vektoren des Frames
        /// </summary>
        /// <param name="p0">der Ortsvektor</param>
        /// <param name="pa">der erste Richtungsvektor</param>
        /// <param name="pb">der zweite Richtungsvektor</param>
        public void Get(out PointF p0, out PointF pa, out PointF pb)
        {
	        p0 = HandleTopLeft;
            pa = new PointF(HandleTopRight.X - p0.X, HandleTopRight.Y - p0.Y);
            pb = new PointF(HandleBottomLeft.X - p0.X, HandleBottomLeft.Y - p0.Y);
        }

        /// <summary>
        /// Wird beim Drücken der linken Maustaste aufgerufen.
        /// </summary>
        /// <param name="point">Enthält die X- und Y-Koordinaten des Mauscursors relativ zur oberen linken Ecke des Fensters.</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public bool MouseButtonDown(Point point)
        {
            if (!Enable) return false;

            PointF p0, pa, pb;
            Get(out p0, out pa, out pb);

            // Drag & Drop zurücksetzen
            m_dragAndDrop.State = DragAndDrop.DragAndDropState.None;

            // Auf PullHandle geklickt
            // TopLeft
            if (PtInRect(HandleTopLeft, point))
            {
                m_dragAndDrop.State = DragAndDrop.DragAndDropState.TopLeft;
                m_dragAndDrop.Offset = new Size(point.X - HandleCenterPoint(HandleTopLeft).X, point.Y - HandleCenterPoint(HandleTopLeft).Y);

                m_dragAndDrop.m_va = (pa.X != 0) ? pa.Y / (float)pa.X : 0;
                m_dragAndDrop.m_vb = (pb.Y != 0) ? pb.X / (float)pb.Y : 0;

                return true;
            }
            // Top
            else if (PtInRect(HandleTop, point))
            {
                m_dragAndDrop.State = DragAndDrop.DragAndDropState.Top;
                m_dragAndDrop.Offset = new Size(point.X - HandleCenterPoint(HandleTop).X, point.Y - HandleCenterPoint(HandleTop).Y);

                m_dragAndDrop.m_va = 0;
                m_dragAndDrop.m_vb = (pb.Y != 0) ? pb.X / (float)pb.Y : 0;

                return true;
            }
            // TopRight
            else if (PtInRect(HandleTopRight, point))
            {
                m_dragAndDrop.State = DragAndDrop.DragAndDropState.TopRight;
                m_dragAndDrop.Offset = new Size(point.X - HandleCenterPoint(HandleTopRight).X, point.Y - HandleCenterPoint(HandleTopRight).Y);

                m_dragAndDrop.m_va = (pa.X != 0) ? pa.Y / (float)pa.X : 0;
                m_dragAndDrop.m_vb = (pb.Y != 0) ? pb.X / (float)pb.Y : 0;

                return true;
            }
            // Left
            else if (PtInRect(HandleLeft, point))
            {
                m_dragAndDrop.State = DragAndDrop.DragAndDropState.Left;
                m_dragAndDrop.Offset = new Size(point.X - HandleCenterPoint(HandleLeft).X, point.Y - HandleCenterPoint(HandleLeft).Y);

                m_dragAndDrop.m_va = (pa.X != 0) ? pa.Y / (float)pa.X : 0;
                m_dragAndDrop.m_vb = 0;

                return true;
            }
            // BottomLeft
            else if (PtInRect(HandleBottomLeft, point))
            {
                m_dragAndDrop.State = DragAndDrop.DragAndDropState.BottomLeft;
                m_dragAndDrop.Offset = new Size(point.X - HandleCenterPoint(HandleBottomLeft).X, point.Y - HandleCenterPoint(HandleBottomLeft).Y);

                m_dragAndDrop.m_va = (pa.X != 0) ? pa.Y / (float)pa.X : 0;
                m_dragAndDrop.m_vb = (pb.Y != 0) ? pb.X / (float)pb.Y : 0;

                return true;
            }
            // Bottom
            else if (PtInRect(HandleBottom, point))
            {
                m_dragAndDrop.State = DragAndDrop.DragAndDropState.Bottom;
                m_dragAndDrop.Offset = new Size(point.X - HandleCenterPoint(HandleBottom).X, point.Y - HandleCenterPoint(HandleBottom).Y);

                m_dragAndDrop.m_va = 0;
                m_dragAndDrop.m_vb = (pb.Y != 0) ? pb.X / (float)pb.Y : 0;

                return true;
            }
            // BottomRight
            else if (PtInRect(HandleBottomRight, point))
            {
                m_dragAndDrop.State = DragAndDrop.DragAndDropState.BottomRight;
                m_dragAndDrop.Offset = new Size(point.X - HandleCenterPoint(HandleBottomRight).X, point.Y - HandleCenterPoint(HandleBottomRight).Y);

                m_dragAndDrop.m_vb = (pb.Y != 0) ? pb.X / (float)pb.Y : 0;
                m_dragAndDrop.m_va = (pa.X != 0) ? pa.Y / (float)pa.X : 0;

                return true;
            }
            // Right
            else if (PtInRect(HandleRight, point))
            {
                m_dragAndDrop.State = DragAndDrop.DragAndDropState.Right;
                m_dragAndDrop.Offset = new Size(point.X - HandleCenterPoint(HandleRight).X, point.Y - HandleCenterPoint(HandleRight).Y);

                m_dragAndDrop.m_va = (pa.X != 0) ? pa.Y / (float)pa.X : 0;
                m_dragAndDrop.m_vb = 0;

                return true;
            }
            // Ursprung
            //CRect rc = m_bottomLeft;
            //rc.left += m_origin.left;
            //rc.top -= m_origin.top; 
            //rc.right = rc.left + 8;
            //rc.bottom = rc.top + 8;
            //if (rc.PtInRect(point)) {
            //	m_trackandrop = 8;
            //       m_offset.x = point.x - m_origin.CenterPoint().x;
            //	m_offset.y = point.y - m_origin.CenterPoint().y;
            //}

            return false;
        }
  
        /// <summary>
        /// wird bei Mausbewegungen aufgerufen.
        /// </summary>
        /// <param name="point">Enthält die X- und Y-Koordinaten des Mauscursors relativ zur oberen linken Ecke des Fensters.</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public bool MouseMove(Point point)
        {   
	        if (!Enable || m_dragAndDrop.State == DragAndDrop.DragAndDropState.None) return false;

            PointF p = new PointF(point.X - m_dragAndDrop.Offset.Width, point.Y - m_dragAndDrop.Offset.Height);

        	PointF p0, pa, pb;
	        Get(out p0, out pa, out pb);	
	
	        switch (State) 
            {
		        case PullFrameState.Size:
                    switch (m_dragAndDrop.State) 
                    {
                        // TopLeft
                        case DragAndDrop.DragAndDropState.TopLeft:
                            //for (int i=0; i<10; i++) 
                            {
                                // Top
                                float x = p0.X + pb.X;
                                pb.Y = pb.Y - (p.Y - p0.Y);
                                pb.X = pb.Y * m_dragAndDrop.m_vb;
                                p0.Y = p.Y;
                                p0.X = x - pb.X;
                                // left
                                float y = p0.Y + pa.Y;
                                pa.X = pa.X - (p.X - p0.X);
                                pa.Y = pa.X * m_dragAndDrop.m_va;
                                p0.X = p.X;
                                p0.Y = y - pa.Y;
                            }
                            break;
                        // Top
                        case DragAndDrop.DragAndDropState.Top:
                            {
                                float x = p0.X + pb.X;
                                pb.Y = pb.Y - (p.Y - (pa.Y/2) - p0.Y);	 
                                pb.X = pb.Y * m_dragAndDrop.m_vb;
                                p0.Y = p.Y - pa.Y/2;  
                                p0.X = x - pb.X;
                            }
                            break;
                        // TopRight
                        case DragAndDrop.DragAndDropState.TopRight:
                            //for (int i=0; i<10; i++) 
                            {
                                float x = p0.X + pb.X;
                                pb.Y = pb.Y - (p.Y - pa.Y - p0.Y);	 
                                pb.X = pb.Y * m_dragAndDrop.m_vb;
                                p0.Y = p.Y - pa.Y;  
                                p0.X = x - pb.X;   
                                pa.X = p.X - p0.X; 
                                pa.Y = pa.X * m_dragAndDrop.m_va;
                            }
                            break;
                        // Left
                        case DragAndDrop.DragAndDropState.Left:
                            {
                                float y = p0.Y + pa.Y;
                                pa.X = pa.X - (p.X - pb.X/2 - p0.X); 
                                pa.Y = pa.X * m_dragAndDrop.m_va;
                                p0.X = p.X - pb.X/2;
                                p0.Y = y - pa.Y;
                            }
                            break;
                        // BottomLeft
                        case DragAndDrop.DragAndDropState.BottomLeft:
                            //for (int i=0; i<10; i++) 
                            {
                                float y = p0.Y + pa.Y;
                                pa.X = pa.X - (p.X - pb.X - p0.X); 
                                pa.Y = pa.X * m_dragAndDrop.m_va;
                                p0.X = p.X - pb.X;   
                                p0.Y = y - pa.Y;
                                pb.Y = p.Y - p0.Y;
                                pb.X = pb.Y * m_dragAndDrop.m_vb;
                            }
                            break;
                        // Bottom
                        case DragAndDrop.DragAndDropState.Bottom:
                            { 
                                pb.X = (long)(p.Y - p0.Y - pa.Y/2) * m_dragAndDrop.m_vb;
                                pb.Y = p.Y - p0.Y - (pa.Y/2);
                            }
                            break;
                        // BottomRight	
                        case DragAndDrop.DragAndDropState.BottomRight:
                            //for (int i=0; i<10; i++) 
                            {
                                pa.Y = (p.X - p0.X - pb.X) * m_dragAndDrop.m_va;
                                pa.X = p.X - p0.X - pb.X;
                                pb.X = (p.Y - p0.Y - pa.Y) * m_dragAndDrop.m_vb;
                                pb.Y = p.Y - p0.Y - pa.Y;
                            }
                            break;
                        // Right
                        case DragAndDrop.DragAndDropState.Right:
                            {
                                pa.Y = (long)((p.X - p0.X - (pb.X/2)) * m_dragAndDrop.m_va);
                                pa.X = p.X - p0.X - (pb.X/2); 
                            }
                            break;
                        default:
                            return false;
                    }	
			        break;
		        case PullFrameState.Rotate:
                    switch (m_dragAndDrop.State) 
                    {
                        // TopLeft	
                        case DragAndDrop.DragAndDropState.TopLeft:
                            {
                                //        if ((nFlags & MK_CONTROL) == MK_CONTROL) {
                                //            CDoublePoint pc(p0.x + pa.x + pb.x, p0.y + pa.y + pb.y);
                                //            CDoublePoint po(p0.x + pa.x/2 + pb.x/2, p0.y + pa.y/2 + pb.y/2);
                                //            pa = GetRotatePoint(CDoublePoint(pa.x + po.x, po.y + pa.y), po, pc, p);
                                //            pa.x -= po.x; 
                                //            pa.y -= po.y;
                                //            pb = GetRotatePoint(CDoublePoint(pb.x + po.x, po.y + pb.y), po, pc, p);
                                //            pb.x -= po.x; 
                                //            pb.y -= po.y ;
                                //            p0 = GetRotatePoint(p0, po, pc, p);
                                //        } else {	 
                                // In Fensterkoordinaten umwandeln
                                PointF pc = new PointF(p0.X + pa.X + pb.X, p0.Y + pa.Y + pb.Y);
                                pa = RotatePoint(new PointF(pa.X + pc.X, pc.Y + pa.Y), pc, p0, p);
                                pa.X -= pc.X; 
                                pa.Y -= pc.Y;
                                pb = RotatePoint(new PointF(pb.X + pc.X, pc.Y + pb.Y), pc, p0, p);
                                pb.X -= pc.X; 
                                pb.Y -= pc.Y;
                                p0 = RotatePoint(p0, pc, p0, p);
                                //        }
                            }
                            break;
                        // Top
                        case DragAndDrop.DragAndDropState.Top:
                            {
                                pb.X -= p.X - p0.X - pa.X/2;
                                p0.X -= (p0.X - p.X) + pa.X/2;
                            }
                            break;
                       // TopRight
                       case DragAndDrop.DragAndDropState.TopRight:
                           {
                                //        // Drehen
                                //        if ((nFlags & MK_CONTROL) == MK_CONTROL) {
                                //            CDoublePoint pc(p0.x + pa.x + pb.x, p0.y - pa.y - pb.y);
                                //            CDoublePoint po(p0.x + pa.x/2 + pb.x/2, p0.y - pa.y/2 - pb.y/2);
                                //            pa = GetRotatePoint(CDoublePoint(pa.x + po.x, po.y - pa.y), po, pc, p);
                                //            pa.x -= po.x; 
                                //            pa.y = po.y - pa.y;
                                //            pb = GetRotatePoint(CDoublePoint(pb.x + po.x, po.y - pb.y), po, pc, p);
                                //            pb.x -= po.x; 
                                //            pb.y = po.y - pb.y;
                                //            p0 = GetRotatePoint(p0, po, pc, p);
                                //        } else {	 
                                            // In Fensterkoordinaten umwandeln
                                            PointF db = new PointF(p0.X + pa.X, p0.Y + pa.Y);
                                            PointF po = new PointF(p0.X + pb.X, p0.Y + pb.Y);
                                            pa = RotatePoint(new PointF(pa.X + po.X, po.Y + pa.Y), po, db, p);
                                            pa.X -= po.X; 
                                            pa.Y -= po.Y;
                                            pb = RotatePoint(new PointF(pb.X + po.X, po.Y + pb.Y), po, db, p);
                                            pb.X -= po.X; 
                                            pb.Y -= po.Y;
                                            p0 = RotatePoint(p0, po, db, p);
                                //        }
                            }
                            break;
                    //    // Left
                    //    case DRAGANDDROP::LEFT:
                    //        pa.y -= p.y - p0.y - pb.y/2;
                    //        p0.y += (p.y - p0.y) - pb.y/2;
                    //        break;
                    //    // Bottomleft
                    //    case DRAGANDDROP::BOTTOMLEFT:
                    //        // Drehen
                    //        if ((nFlags & MK_CONTROL) == MK_CONTROL) {
                    //            CDoublePoint pc(p0.x + pa.x + pb.x, p0.y - pa.y - pb.y);
                    //            CDoublePoint po(p0.x + pa.x/2 + pb.x/2, p0.y - pa.y/2 - pb.y/2);
                    //            pa = GetRotatePoint(CDoublePoint(pa.x + po.x, po.y - pa.y), po, pc, p);
                    //            pa.x -= po.x; 
                    //            pa.y = po.y - pa.y;
                    //            pb = GetRotatePoint(CDoublePoint(pb.x + po.x, po.y - pb.y), po, pc, p);
                    //            pb.x -= po.x; 
                    //            pb.y = po.y - pb.y;
                    //            p0 = GetRotatePoint(p0, po, pc, p);
                    //        } else {	 
                    //            // In Fensterkoordinaten umwandeln
                    //            CDoublePoint db(p0.x + pb.x, p0.y + pb.y);
                    //            CDoublePoint po(p0.x + pa.x, p0.y + pa.y);
                    //            pa = GetRotatePoint(CDoublePoint(pa.x + po.x, po.y + pa.y), po, db, p);
                    //            pa.x -= po.x; 
                    //            pa.y -= po.y;
                    //            pb = GetRotatePoint(CDoublePoint(pb.x + po.x, po.y + pb.y), po, db, p);
                    //            pb.x -= po.x; 
                    //            pb.y -= po.y;
                    //            p0 = GetRotatePoint(p0, po, db, p);
                    //        }
                    //        break;
                    //    // Bottom
                    //    case DRAGANDDROP::BOTTOM:
                    //        pb.x = p.x - p0.x - pa.x/2;
                    //        break;
                    //    // ButtomRight
                    //    case DRAGANDDROP::BOTTOMRIGHT:
                    //        // Drehen
                    //        if ((nFlags & MK_CONTROL) == MK_CONTROL) {
                    //            CDoublePoint pc(p0.x + pa.x + pb.x, p0.y - pa.y - pb.y);
                    //            CDoublePoint po(p0.x + pa.x/2 + pb.x/2, p0.y - pa.y/2 - pb.y/2);
                    //            pa = GetRotatePoint(CDoublePoint(pa.x + po.x, po.y - pa.y), po, pc, p);
                    //            pa.x -= po.x; 
                    //            pa.y = po.y - pa.y;
                    //            pb = GetRotatePoint(CDoublePoint(pb.x + po.x, po.y - pb.y), po, pc, p);
                    //            pb.x -= po.x; 
                    //            pb.y = po.y - pb.y;
                    //            p0 = GetRotatePoint(p0, po, pc, p);
                    //        } else {	 
                    //            // In Fensterkoordinaten umwandeln
                    //            CDoublePoint pc(p0.x + pa.x + pb.x, p0.y + pa.y + pb.y);
                    //            pa = GetRotatePoint(CDoublePoint(pa.x + p0.x, p0.y + pa.y), p0, pc, p);
                    //            pa.x -= p0.x; 
                    //            pa.y -= p0.y;
                    //            pb = GetRotatePoint(CDoublePoint(pb.x + p0.x, p0.y + pb.y), p0, pc, p);
                    //            pb.x -= p0.x; 
                    //            pb.y -= p0.y;
                    //        }
                    //        break;
                    //    // Right
                    //    case DRAGANDDROP::RIGHT:
                    //        pa.y = p.y - p0.y - pb.y/2;
                    //        break;
                    //    default:
                    //        return false;
                    }
                    ////// Ursprung für das drehen  verschieben
                    ////if (m_trackandrop == 8) {
                    ////	m_origin.left = p.x - 4;
                    ////	m_origin.top = -p.y - p0.y + 4;
                    ////	m_origin.right = m_origin.left + 8;
                    ////	m_origin.bottom = m_origin.top + 8;
                    ////	//hCursor = AfxGetApp()->LoadCursor(IDC_ORIGIN);
                    ////}	
		        break;
		        default:
			        return false;
	        }
	
	        Set(p0, pa, pb);
	        
            return true;
	        //m_pParentWnd->SendMessage(OnSizeHandle(p0, pa, pb);
        }

        /// <summary>
        /// Wird beim Lösen der Maustaste aufgerufen.
        /// </summary>
        /// <param name="point">Enthält die X- und Y-Koordinaten des Mauscursors relativ zur oberen linken Ecke des Fensters.</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public bool MouseButtonUp(Point point)
        {
	        // Drag und Drop aufheben
            m_dragAndDrop.State = DragAndDrop.DragAndDropState.None;

	        return true;
        }
    }
}
