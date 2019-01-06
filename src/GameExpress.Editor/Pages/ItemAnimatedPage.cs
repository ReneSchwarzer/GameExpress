using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GameExpress.Core.Structs;
using GameExpress.Core.Items;
using System.Threading;

namespace GameExpress.Editor.Pages
{
    public partial class ItemAnimatedPage : ItemContainerPage
    {
        /// <summary>
        /// Liefert oder setzt die Zeit
        /// </summary>
        private Time Time { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemAnimatedPage()
            : this(null)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item"></param>
        public ItemAnimatedPage(IItem item)
            :base(item)
        {
            InitializeComponent();

            var i = Item as ItemVisualAnimated;
            if (i == null) return;

            m_timeLinePanel.Data = i.InstanceItems;
            m_timeLinePanel.ChangedTime += OnChanegdTime;

            Time = new Time();

            Controls.SetChildIndex(this.m_toolStrip, 3);
            Controls.SetChildIndex(this.m_timeLinePanel, 2);
            Controls.SetChildIndex(this.m_splitter, 1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Sichtbarkeit ändert
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (m_playToolStripButton.Checked)
            {
                m_playToolStripButton.Checked = false;
            }
        }

        /// <summary>
        /// Wird bei ersten Mal anzeigen aufgerufen
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_toolStrip.ImageScalingSize = new Size(16, 16);
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Zeit von einer anderen Komponente geändert wurde
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        void OnChanegdTime(object sender, ChangedTimeEventArgs e)
        {
            OnChangeActiveItem(new ChangeActiveItemEventArgs() { Item = e.KeyFrame != null ? (IItem)e.KeyFrame : e.Item != null ? (IItem)e.Item : Item} );

            if (!m_playToolStripButton.Checked)
            {
                Time = new Time(e.Time);
            }

            Refresh();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Item visuell dargestellt werden soll
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnPaintItem(PaintViewEventArgs e)
        {
            var objectState = Item as ItemVisualAnimated;
            if (objectState == null) return;

            var uc = new UpdateContext()
            {
                Time = Time,
                Designer = !m_playToolStripButton.Checked
            };

            objectState.Update(uc);

            var pc = new PresentationContext(e.Graphics);
            pc.Matrix *= Matrix3D.Translation(e.ViewArea.Location);
            pc.Matrix *= Matrix3D.Scaling(e.Zoom, e.Zoom);
            pc.Designer = !m_playToolStripButton.Checked;
            pc.Time = Time;
            
            objectState.Presentation(pc);

            if (m_playToolStripButton.Checked)
            {
                m_timeLinePanel.Time = (ulong)objectState.LocalTime(Time);
            }

            // Handle zeichnen für Größenenderung der Objekte
            if (m_timeLinePanel.SelectedItem != null)
            {
                var keyFrame = m_timeLinePanel.SelectedItem.GetKeyFrame(pc.Time.Ticks);

                if (keyFrame != null)
                {
                    if (!PullFrame.IsDragAndDrop)
                    {
                        PullFrame.Enable = true;

                        // Berechnet die kleinen Ziehpunkte für das vergrößern von Objekten
                        Point p0 = new Point(0, 0);
                        Point pa = new Point(m_timeLinePanel.SelectedItem.ObjectSize.Width + PullFrame.HandleSize, 0);
                        Point pb = new Point(0, m_timeLinePanel.SelectedItem.ObjectSize.Height + PullFrame.HandleSize);
                        Matrix3D m = pc.Matrix * keyFrame.Matrix;
                        p0 = m.Transform(p0);
                        pa = m.Transform(pa);
                        pb = m.Transform(pb);
                        PullFrame.Set(p0, new Point(pa.X - p0.X, pa.Y - p0.Y), new Point(pb.X - p0.X, pb.Y - p0.Y));
                    }
                }
                else
                {
                    PullFrame.Enable = false;
                }
            }

            base.OnPaintItem(e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Größe des Items ermittelt werden muss
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnRetrieveItemSize(RetrieveItemSizeEventArgs e)
        {
            var item = Item as ItemVisualAnimated;
            if (item == null) return;

            e.Size = item.Size;
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Objektauswahl ändert
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnChangedSelectedItem(object sender, ChanegedSelectetItemArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Diese Methode wird aufgerufen, wenn der Benutzer die Maustaste drückt.
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer die Maus über die Ansicht bewegt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Cursor cur = PullFrame.GetCursor(e.Location);

            if (cur != null)
            {
                Cursor = cur;
            }
            else
            {
                Cursor = Cursors.Default;
            }

            if (PullFrame.MouseMove(e.Location))
            {
                var selectedItem = m_timeLinePanel.SelectedItem;

                if (selectedItem != null)
                {
                    var keyFrame = selectedItem.GetKeyFrame(m_timeLinePanel.Time);

                    if (keyFrame != null)
                    {
                        PointF p0, pa, pb;
                        PullFrame.Get(out p0, out pa, out pb);

                        keyFrame.Matrix = new Matrix3D
                             (
                                 pa.X / selectedItem.Size.Width, pa.Y / selectedItem.Size.Width, 0,
                                 pb.X / selectedItem.Size.Height, pb.Y / selectedItem.Size.Height, 0,
                                 p0.X - ItemViewRect.Location.X, p0.Y - ItemViewRect.Location.Y, 1
                             );
                    }
                }

                Refresh();
            }       
        }

        /// <summary>
        /// Wird beim drücken einer Maustaste aufgerufen
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (PullFrame.MouseButtonDown(e.Location))
            {
                RefreshItem();
            }
            else
            {
                OnChangeActiveItem(new ChangeActiveItemEventArgs() { Item = Item });
            }
        }

        /// <summary>
        /// Wird beim loslassen der Maustaste gedrückt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (PullFrame.MouseButtonUp(e.Location))
            {
                Refresh();
            }
        }

        /// <summary>
        /// Wird beim doppelklicken ausgelöst
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Wird zum starten des Timers aufgerufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Das Eventargument</param>
        private void OnPlay(object sender, EventArgs e)
        {
            var locking = false;

            m_playToolStripButton.Checked = !m_playToolStripButton.Checked;

            ThreadPool.QueueUserWorkItem((x) => 
            {
                while (m_playToolStripButton.Checked)
                {
                    Time.AddTick(1);

                    if (!locking && Created)
                    {
                        locking = true;

                        ThreadPool.QueueUserWorkItem((y) =>
                        {
                            ExecuteSecure(() => { Refresh(); });

                            locking = false;
                        });
                    }

                    Thread.Sleep(10);
                }
            });
        }
    }
}
