using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GameExpress.Editor.Pages
{
    public partial class ItemPanel : Panel
    {
        /// <summary>
        /// Der Wert der Unendlichkeit
        /// </summary>
        private const int m_infinity = 10000;

        /// <summary>
        /// Event zum zeichnen des Items
        /// </summary>
        public EventHandler<PaintViewEventArgs> PaintView;

        /// <summary>
        /// Event zum ermitteln der Itemgröße
        /// </summary>
        public EventHandler<RetrieveItemSizeEventArgs> RetrieveItemSize;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemPanel()
        {
            InitializeComponent();

            Zoom = 1.0f;
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Größe äandert
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            bool infinity;

            Rectangle itemViewRect = GetItemViewRect(out infinity);

            // Scrollbalken einblenden
            m_verticalScrollBar.Visible = itemViewRect.Height > m_panel.DisplayRectangle.Height;
            m_horizontalScrollBar.Visible = itemViewRect.Width > m_panel.DisplayRectangle.Width;

            // Werte anpassen
            m_horizontalScrollBar.Minimum = -(itemViewRect.Width / 2);
            m_horizontalScrollBar.Maximum = (itemViewRect.Width / 2);
            m_horizontalScrollBar.Value = 0;

            m_verticalScrollBar.Minimum = -(itemViewRect.Height / 2);
            m_verticalScrollBar.Maximum = (itemViewRect.Height / 2);
            m_verticalScrollBar.Value = 0;

            m_panel.Refresh();
            m_horizontalRuler.Refresh();
            m_verticalRuler.Refresh();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Item visuell dargestellt werden soll
        /// </summary>
        /// <param name="e">Eventargumente</param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            var buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, m_panel.DisplayRectangle);

            for (int y = 0; y < m_panel.DisplayRectangle.Height; y += global::GameExpress.Editor.Properties.Resources.hintergrund.Height)
            {
                for (int x = 0; x < m_panel.DisplayRectangle.Width; x += global::GameExpress.Editor.Properties.Resources.hintergrund.Width)
                {
                    buffer.Graphics.DrawImageUnscaled(global::GameExpress.Editor.Properties.Resources.hintergrund, x, y);
                }
            }

            using (var pen = new Pen(Color.Black, 1))
            {
                var infinity = true;
                var itemViewRect = GetItemViewRect(out infinity);

                OnPaintView(new PaintViewEventArgs()
                {
                    Graphics = buffer.Graphics,
                    ViewArea = itemViewRect,
                    Zoom = Zoom
                });

                if (!infinity)
                {
                    buffer.Graphics.DrawRectangle(pen, new Rectangle(itemViewRect.X - 1, itemViewRect.Y - 1, itemViewRect.Width + 2, itemViewRect.Height + 2));
                }
            }

            buffer.Render();
        }

        /// <summary>
        /// Löst das PaintView-Ereignis aus
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected virtual void OnPaintView(PaintViewEventArgs e)
        {
            if (PaintView != null)
            {
                PaintView(this, e);
            }
        }

        /// <summary>
        /// Löst das RetriveItemSize-Ereignis aus
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected virtual void OnRetrieveItemSize(RetrieveItemSizeEventArgs e)
        {
            if (RetrieveItemSize != null)
            {
                RetrieveItemSize(this, e);
            }
        }

        /// <summary>
        /// Ermittelt die Koordinaten des Bereiches indem das Item gezeichnet werden soll
        /// </summary>
        /// <param name="infinity">Es gibt kienen Festen Bereich, indem das Item gezeichnet werden soll. 
        /// Stattdessen kann das Item den gesammten Zeichebreich nutzen</param>
        /// <returns>Das Rechteck, indem das Item gezeichnet werden soll</returns>
        public virtual Rectangle GetItemViewRect(out bool infinity)
        {
            RetrieveItemSizeEventArgs size = new RetrieveItemSizeEventArgs();
            OnRetrieveItemSize(size);

            var sz = new SizeF();
            var pt = new PointF();

            if (size.Size.IsEmpty)
            {
                // Keine Größe angegeben, Infinity-Modus wird aktiv
                sz.Width = m_infinity;
                sz.Height = m_infinity;

                pt.X = 0 - m_horizontalScrollBar.Value;
                pt.Y = 0 - m_verticalScrollBar.Value;

                infinity = true;
            }
            else
            {
                sz = new SizeF(size.Size.Width * Zoom, size.Size.Height * Zoom);

                pt.X = (m_panel.ClientSize.Width / 2) - (sz.Width / 2) - m_horizontalScrollBar.Value;
                pt.Y = (m_panel.ClientSize.Height / 2) - (sz.Height / 2) - m_verticalScrollBar.Value;

                infinity = false;
            }

            return new Rectangle((int)pt.X, (int)pt.Y, (int)sz.Width, (int)sz.Height);
        }

        /// <summary>
        /// Liefert oder setzt den aktuellen Zoom
        /// </summary>
        public float Zoom { get; set; }

        /// <summary>
        /// Wird aufgerufen, wenn das horizontale Linial gezeichnet werden soll
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnPaintHorizontalRuler(object sender, PaintEventArgs e)
        {
            var infinity = false;
            var itemViewRect = GetItemViewRect(out infinity);
            var buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, m_horizontalRuler.DisplayRectangle);

            if (infinity)
            {
                using (Brush brush = new SolidBrush(Color.White))
                {
                    buffer.Graphics.FillRectangle(brush, m_horizontalRuler.DisplayRectangle);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(Color.LightGray))
                {
                    buffer.Graphics.FillRectangle(brush, m_horizontalRuler.DisplayRectangle);
                }

                using (Brush brush = new SolidBrush(Color.White))
                {
                    buffer.Graphics.FillRectangle(brush, new Rectangle(itemViewRect.Left, 2, itemViewRect.Width, m_horizontalRuler.ClientSize.Height - 4));
                }
            }

            using (Pen pen = new Pen(Color.Black, 1))
            {
                int count = 0;

                for (int i = itemViewRect.Left; i < m_horizontalRuler.ClientSize.Width; i += 10)
                {
                    if (count % 10 == 0)
                    {
                        buffer.Graphics.DrawLine(pen, new Point(i, 3), new Point(i, m_horizontalRuler.ClientSize.Height - 4));
                    }
                    else
                    {
                        buffer.Graphics.DrawLine(pen, new Point(i, 8), new Point(i, m_horizontalRuler.ClientSize.Height - 8));
                    }

                    count++;
                }

                count = 1;

                for (int i = itemViewRect.Left - 10; i > 0; i -= 10)
                {
                    if (count % 10 == 0)
                    {
                        buffer.Graphics.DrawLine(pen, new Point(i, 3), new Point(i, m_horizontalRuler.ClientSize.Height - 4));
                    }
                    else
                    {
                        buffer.Graphics.DrawLine(pen, new Point(i, 8), new Point(i, m_horizontalRuler.ClientSize.Height - 8));
                    }

                    count++;
                }
            }

            buffer.Render();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das horizontale Linial gezeichnet werden soll
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnPaintVerticalRuler(object sender, PaintEventArgs e)
        {
            var infinity = false;
            var itemViewRect = GetItemViewRect(out infinity);

            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, m_verticalRuler.DisplayRectangle);

            if (infinity)
            {
                using (Brush brush = new SolidBrush(Color.White))
                {
                    buffer.Graphics.FillRectangle(brush, m_verticalRuler.DisplayRectangle);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(Color.LightGray))
                {
                    buffer.Graphics.FillRectangle(brush, m_verticalRuler.DisplayRectangle);
                }

                using (Brush brush = new SolidBrush(Color.White))
                {
                    buffer.Graphics.FillRectangle(brush, new Rectangle(2, itemViewRect.Top, m_verticalRuler.ClientSize.Width - 4, itemViewRect.Height));
                }
            }

            using (Pen pen = new Pen(Color.Black, 1))
            {
                int count = 0;

                for (int i = itemViewRect.Top; i < m_verticalRuler.ClientSize.Height; i += 10)
                {
                    if (count % 10 == 0)
                    {
                        buffer.Graphics.DrawLine(pen, new Point(3, i), new Point(m_verticalRuler.ClientSize.Width - 4, i));
                    }
                    else
                    {
                        buffer.Graphics.DrawLine(pen, new Point(8, i), new Point(m_verticalRuler.ClientSize.Width - 8, i));
                    }

                    count++;
                }

                count = 1;

                for (int i = itemViewRect.Top - 10; i > 0; i -= 10)
                {
                    if (count % 10 == 0)
                    {
                        buffer.Graphics.DrawLine(pen, new Point(3, i), new Point(m_verticalRuler.ClientSize.Width - 4, i));
                    }
                    else
                    {
                        buffer.Graphics.DrawLine(pen, new Point(8, i), new Point(m_verticalRuler.ClientSize.Width - 8, i));
                    }

                    count++;
                }
            }

            buffer.Render();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer das Bild vergrößern möchte
        /// </summary>
        public void ZoomIn()
        {
            Zoom += 0.1f;
            if (Zoom > 4.0f)
            {
                Zoom = 4.0f;
            }

            m_panel.Refresh();
            m_horizontalRuler.Refresh();
            m_verticalRuler.Refresh();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer das Bild verkleinern möchte
        /// </summary>
        public void ZoomOut()
        {
            Zoom -= 0.1f;
            if (Zoom < 0.1f)
            {
                Zoom = 0.1f;
            }

            m_panel.Refresh();
            m_horizontalRuler.Refresh();
            m_verticalRuler.Refresh();
        }

        /// <summary>
        /// Wird aufgerufen, wenn vertikal gescrollt wurde
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">Eventargumente</param>
        private void OnVerticalScroll(object sender, ScrollEventArgs e)
        {
            m_panel.Refresh();
            m_horizontalRuler.Refresh();
            m_verticalRuler.Refresh();
        }

        /// <summary>
        /// Wird aufgerufen, wenn horizontal gescrollt wurde
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">Eventargumente</param>
        private void OnHorizontalScroll(object sender, ScrollEventArgs e)
        {
            m_panel.Refresh();
            m_horizontalRuler.Refresh();
            m_verticalRuler.Refresh();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer die Maus bewegt
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        /// <summary>
        /// Wird beim drücken einer Maustaste aufgerufen
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        /// <summary>
        /// Wird beim loslassen der Maustaste gedrückt
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        /// <summary>
        /// Wird beim klicken der Maustaste aufgerufen
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        /// <summary>
        /// Wird beim doppelklicken aufgerufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }
    }
}
