using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameExpress.Core.Items;

namespace GameExpress.Editor.Pages
{
    public partial class TimeLinePanel : Panel
    {
        /// <summary>
        /// Die Daten
        /// </summary>
        [NonSerialized]
        private ICollection<ItemVisualInstance> m_data = new List<ItemVisualInstance>();

        /// <summary>
        /// Event, welches beim Ändern der Zeit aufgerufen wird
        /// </summary>
        public event EventHandler<ChangedTimeEventArgs> ChangedTime;

        /// <summary>
        /// Event, welches beim Ändern der Objektauswahl ausgelöst wird
        /// </summary>
        public event EventHandler<ChanegedSelectetItemArgs> ChanegedSelectetItem;

        /// <summary>
        /// Liefert oder setzt die Daten
        /// </summary>
        [Browsable(false)]
        public ICollection<ItemVisualInstance> Data
        {
            get { return m_data; }
            set { m_data = value; Refresh(); }
        }

        /// <summary>
        /// Liefert oder setzt die Zeit
        /// </summary>
        public ulong Time { get; set; }

        /// <summary>
        /// Liefert oder setzt die Objektauswahl
        /// </summary>
        public ItemVisualInstance SelectedItem { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TimeLinePanel()
        {
            InitializeComponent();

            Data = new List<ItemVisualInstance>();
        }

        /// <summary>
        /// Liefert die X-Koordinate der Zeitmarkierung
        /// </summary>
        /// <returns></returns>
        private int GetTimeMarkerPos()
        {
            return (int)Time;
        }

        /// <summary>
        /// Löst das ChangeTime-Event aus 
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected virtual void OnChangedTime(ChangedTimeEventArgs e)
        {
            if (ChangedTime != null)
            {
                ChangedTime(this, e);
            }

            Refresh();
        }
        
        /// <summary>
        /// Löst das ChanegedSelectetItem-Event aus 
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected virtual void OnChanegedSelectetItem(ChanegedSelectetItemArgs e)
        {
            if (ChanegedSelectetItem != null)
            {
                ChanegedSelectetItem(this, e);
            }

            Refresh();
        }

        /// <summary>
        /// Wird vor dem ersten Anzeigen aufgerufen
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Data.Count > 0)
            {
                SelectedItem = Data.FirstOrDefault();
                OnChanegedSelectetItem(new ChanegedSelectetItemArgs() { Item = SelectedItem });
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das horizontale Linial gezeichnet werden soll
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnPaintRuler(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, m_ruler.DisplayRectangle);

            using (Brush brush = new SolidBrush(Color.LightGray))
            {
                buffer.Graphics.FillRectangle(brush, m_ruler.DisplayRectangle);
            }

            using (Brush brush = new SolidBrush(Color.White))
            {
                buffer.Graphics.FillRectangle(brush, new Rectangle(0, 2, m_ruler.Width, m_ruler.ClientSize.Height - 4));
            }

            using (Pen pen = new Pen(Color.Black, 1))
            {
                int count = 0;

                for (int i = 0; i < m_ruler.ClientSize.Width; i += 10)
                {
                    if (count % 10 == 0)
                    {
                        buffer.Graphics.DrawLine(pen, new Point(i, 3), new Point(i, m_ruler.ClientSize.Height - 4));
                    }
                    else
                    {
                        buffer.Graphics.DrawLine(pen, new Point(i, 8), new Point(i, m_ruler.ClientSize.Height - 8));
                    }

                    count++;
                }
            }

            // Zeitpositionsmarke anzeigen
            using (Brush brush = new SolidBrush(Color.DarkRed))
            {
                var mode = buffer.Graphics.SmoothingMode;
                buffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int x = GetTimeMarkerPos();
                Point[] p = new Point[4];

                p[0] = new Point(x - 5, 2);
                p[1] = new Point(p[0].X + 10, 2);
                p[2] = new Point(p[0].X + (p[1].X - p[0].X)/2, 16);
                p[3] = new Point(p[0].X, p[0].Y);

                buffer.Graphics.FillPolygon(brush, p);

                buffer.Graphics.SmoothingMode = mode;
            }

            buffer.Render();
        }

        /// <summary>
        /// Wird aufgerufen, wenn Objektliste gezeichnet werden soll
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnPaintObjectList(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, m_objectListPanel.DisplayRectangle);

            var mode = buffer.Graphics.SmoothingMode;
            buffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Hintergrund
            using (Brush brush = new SolidBrush(System.Drawing.SystemColors.Control))
            {
                buffer.Graphics.FillRectangle(brush, m_objectListPanel.DisplayRectangle);
            }

            for (int i = 0; i < Data.Count; i++)
            {
                var data = Data.Skip(i).FirstOrDefault();
                Rectangle r = new Rectangle(0, (i * 16), m_objectListPanel.DisplayRectangle.Width - 1, 16);

                // Auswahl
                if (SelectedItem == data)
                {
                    buffer.Graphics.FillRectangle(SystemBrushes.Highlight, r);
                }

                using (Pen pen = new Pen(System.Drawing.SystemColors.ButtonShadow))
                {
                    buffer.Graphics.DrawLine(pen, new Point(r.Left - 1, r.Bottom), new Point(r.Width - 1, r.Bottom));
                }
                
                using (Brush brush = new SolidBrush(ForeColor))
                {
                    StringFormat sf = new StringFormat();
                    sf.Trimming = StringTrimming.EllipsisWord;

                    buffer.Graphics.DrawString(data.Name, Font, brush, new Rectangle(r.Left + 10, r.Top, r.Width - 32, 16), sf);
                }

                using (Brush brush = new SolidBrush(!data.Lock ? Color.Red : Color.White))
                {
                    buffer.Graphics.FillEllipse(brush, r.Right - 32 + 5, r.Top + 3, 7, 7);
                }

                using (Brush brush = new SolidBrush(!data.Enable ? Color.Green : Color.White))
                {
                    buffer.Graphics.FillEllipse(brush, r.Right - 16 + 5, r.Top + 3, 7, 7);
                }
            }

            buffer.Graphics.SmoothingMode = mode;

            buffer.Render();
        }

        /// <summary>
        /// Wird aufgerufen, wenn Zeitliste gezeichnet werden soll
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnPaintTimeList(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, m_timeListPanel.DisplayRectangle);

            // Hintergrund
            using (Brush brush = new SolidBrush(System.Drawing.SystemColors.Window))
            {
                buffer.Graphics.FillRectangle(brush, m_timeListPanel.DisplayRectangle);
            }

            for (int i = 0; i < Data.Count; i++)
            {
                var data = Data.Skip(i).FirstOrDefault();
                Rectangle r = new Rectangle(0, (i * 16), m_timeListPanel.DisplayRectangle.Width - 1, 16);

                // Hintergrundgitter
                using (Pen pen = new Pen(SystemColors.ButtonShadow))
                {
                    buffer.Graphics.DrawLine(pen, new Point(r.Left - 1, r.Bottom), new Point(r.Width - 1, r.Bottom));

                    for (int j = 0; j < (m_timeListPanel.DisplayRectangle.Width / 10) + 1; j++)
                    {
                        //if (j % 100 == 0)
                        {
                            //buffer.Graphics.FillRectangle(new Rectangle((i * 10), r.Top, (i * 10) + 10, rc.bottom), &highlightBrush);
                        }

                        //j += m_minFrameTime;

                        if (j > 0)
                        {
                            buffer.Graphics.DrawLine(pen, r.Left + j * 10, r.Top, r.Left + j * 10, r.Bottom);
                        }
                    }
                }

                // Keyframes
                using (Brush brush = new SolidBrush(SystemColors.Info))
                {
                    using (Pen pen = new Pen(SystemColors.MenuHighlight))
                    {
                        ItemVisualKeyFrame lastKeyFrame = null;

                        foreach (var k in data.KeyFrames)
                        {
                            Rectangle r1 = new Rectangle((int)k.From + 1, r.Top, (int)(Math.Min(k.Duration, (ulong)r.Width)) - 1, r.Height);

                            buffer.Graphics.FillRectangle(brush, r1);
                            
                            if (lastKeyFrame != null && lastKeyFrame.Tweening)
                            {
                                var mode = buffer.Graphics.SmoothingMode;
                                buffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                                buffer.Graphics.DrawLine(pen, lastKeyFrame.From + lastKeyFrame.Duration, r.Top + (r.Height / 2), k.From, r.Top + (r.Height / 2));
                                buffer.Graphics.FillEllipse(brush, new RectangleF(lastKeyFrame.From + lastKeyFrame.Duration - 3, r.Top + (r.Height / 2) - 3, 6, 6));
                                buffer.Graphics.DrawEllipse(pen, new Rectangle((int)(lastKeyFrame.From + lastKeyFrame.Duration - 3), (int)(r.Top + (r.Height / 2) - 3), 6, 6));

                                buffer.Graphics.DrawLine(pen, k.From - 4, r.Top + (r.Height / 2) - 3, k.From, r.Top + (r.Height / 2));
                                buffer.Graphics.DrawLine(pen, k.From - 4, r.Top + (r.Height / 2) + 3, k.From, r.Top + (r.Height / 2));

                                buffer.Graphics.SmoothingMode = mode;
                            }

                            lastKeyFrame = k;
                        }
                    }
                }
            }

            // Zeichne Zeitmakierungslinie 
            using (Pen pen = new Pen(Color.DarkRed, 1))
            {
                int x = GetTimeMarkerPos();
                buffer.Graphics.DrawLine(pen, new Point(x, m_objectListPanel.DisplayRectangle.Top), new Point(x, m_objectListPanel.DisplayRectangle.Bottom));
            }

            //using (Brush brush = new SolidBrush(Color.White))
            //{
            //    buffer.Graphics.FillRectangle(brush, new Rectangle(0, 2, m_ruler.Width, m_ruler.ClientSize.Height - 4));
            //}

            //using (Pen pen = new Pen(Color.Black, 1))
            //{
            //    int count = 0;

            //    for (int i = 0; i < m_ruler.ClientSize.Width; i += 10)
            //    {
            //        if (count % 10 == 0)
            //        {
            //            buffer.Graphics.DrawLine(pen, new Point(i, 3), new Point(i, m_ruler.ClientSize.Height - 4));
            //        }
            //        else
            //        {
            //            buffer.Graphics.DrawLine(pen, new Point(i, 8), new Point(i, m_ruler.ClientSize.Height - 8));
            //        }

            //        count++;
            //    }
            //}

            buffer.Render();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer die Zeitmarkierung ändern will
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnClickRuler(object sender, MouseEventArgs e)
        {
            ChangeTime((ulong)e.X);

            if (sender == m_timeListPanel)
            {
                int i = e.Y / 16;

                if (i >= 0 && i < Data.Count)
                {
                    SelectedItem = Data.FirstOrDefault();
                }
                else
                {
                    SelectedItem = null;
                }

                OnChanegedSelectetItem(new ChanegedSelectetItemArgs() { Item = SelectedItem });

                Refresh();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Mauszeiger über den Ruler bewegt wird
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnMouseMoveRuler(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ChangeTime((ulong)e.X);
            }
        }

        /// <summary>
        /// Ändert die Zeit
        /// </summary>
        /// <param name="time">Die neue Zeit</param>
        private void ChangeTime(ulong time)
        {
            Time = time;

            ItemVisualKeyFrame keyFrame = null;

            if (SelectedItem != null)
            {
                foreach (var k in SelectedItem.KeyFrames)
                {
                    if (k.From <= Time && Time <= k.From + k.Duration)
                    {
                        keyFrame = k;
                        break;
                    }
                }
            }

            OnChangedTime(new ChangedTimeEventArgs() { Time = Time, Item = SelectedItem, KeyFrame = keyFrame });
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer ein Objekt aus der Objektliste auswählt
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnObjectListClick(object sender, MouseEventArgs e)
        {
            int i = e.Y / 16;

            if (i >= 0 && i < Data.Count)
            {
                SelectedItem = Data.FirstOrDefault();
            }
            else
            {
                SelectedItem = null;
            }

            OnChanegedSelectetItem(new ChanegedSelectetItemArgs() { Item = SelectedItem });
        }


        /// <summary>
        /// Wird beim erstmaligen Anzeigen aufgerufen
        /// </summary>
        /// <param name="sender">Der Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnLoad(object sender, EventArgs e)
        {
            ChangeTime((ulong)0);
        }
    }
}
