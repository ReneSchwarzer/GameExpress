using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GameExpress.Core.Items;
using GameExpress.Core;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace GameExpress.Editor.Pages
{
    public partial class ItemPage : Page
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich das aktive Item geändert hat
        /// </summary>
        public event EventHandler<ChangeActiveItemEventArgs> ChangeActiveItemEvent;

        /// <summary>
        /// Event zum zeichnen des Items
        /// </summary>
        public EventHandler<PaintViewEventArgs> PaintItem;

        /// <summary>
        /// Event zum ermitteln der Itemgröße
        /// </summary>
        public EventHandler<RetrieveItemSizeEventArgs> RetrieveItemSize;

        /// <summary>
        /// Mausevent
        /// </summary>
        public EventHandler<MouseEventArgs> MouseMoveItem;

        /// <summary>
        /// Mausevent
        /// </summary>
        public EventHandler<MouseEventArgs> MouseDownItem;

        /// <summary>
        /// Mausevent
        /// </summary>
        public EventHandler<MouseEventArgs> MouseUpItem;

        /// <summary>
        /// Mausevent
        /// </summary>
        public EventHandler<MouseEventArgs> MouseClickItem;

        /// <summary>
        /// Mausevent
        /// </summary>
        public EventHandler<MouseEventArgs> MouseDoubleClickItem;

        /// <summary>
        /// Liefert den aktuellen Zoom
        /// </summary>
        public float Zoom { get { return m_panel.Zoom; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemPage()
        {
            InitializeComponent();

            m_panel.PaintView += OnPaintItem;
            m_panel.RetrieveItemSize += OnRetrieveItemSize;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item"></param>
        public ItemPage(IItem item)
            :this()
        {
            Item = item;

            if (Item != null)
            {
                item.PropertyChanged += OnPropertyChanged;
            }
                
        }

        /// <summary>
        /// Tritt ein, wenn der Benutzer das Formular lädt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "10 %", Value = 0.1f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "25 %", Value = 0.25f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "50 %", Value = 0.5f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "75 %", Value = 0.75f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "100 %", Value = 0.1f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "125 %", Value = 1.25f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "150 %", Value = 1.5f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "200 %", Value = 2.0f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "400 %", Value = 4.0f });
            m_zoomComboBox.Items.Add(new ComboBoxItem<float>() { Name = "800 %", Value = 8.0f });

            m_zoomComboBox.SelectedIndex = 4;

            Visible = false;
        }

        /// <summary>
        /// Löst das VisibleChanged-Event aus
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                OnChangeActiveItem(new ChangeActiveItemEventArgs() { Item = Item });
            }
        }

        /// <summary>
        /// Zeigt an das sich Eigenschaften eines Items sich geändert haben
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Löst das ChangeActiveItemEvent aus.
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected virtual void OnChangeActiveItem(ChangeActiveItemEventArgs e)
        {
            ChangeActiveItemEvent?.Invoke(this, e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Item visuell dargestellt werden soll
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPaintItem(object sender, PaintViewEventArgs e)
        {
            OnPaintItem(e);
        }

        /// <summary>
        /// Löst das RetrieveItemSizeEvent aus
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnPaintItem(PaintViewEventArgs e)
        {
            PaintItem?.Invoke(this, e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Größe des Items ermittelt werden muss
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnRetrieveItemSize(object sender, RetrieveItemSizeEventArgs e)
        {
            OnRetrieveItemSize(e);
        }

        /// <summary>
        /// Löst das RetrieveItemSizeEvent aus
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnRetrieveItemSize(RetrieveItemSizeEventArgs e)
        {
            RetrieveItemSize?.Invoke(this, e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer die Maus über die Ansicht bewegt
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnMouseMoveItem(object sender, MouseEventArgs e)
        {
            OnMouseMoveItem(e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer die Maus über die Ansicht bewegt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnMouseMoveItem(MouseEventArgs e)
        {
            MouseMoveItem?.Invoke(this, e);
        }

        /// <summary>
        /// Wird beim drücken einer Maustaste aufgerufen
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnMouseDownItem(object sender, MouseEventArgs e)
        {
            OnMouseDownItem(e);
        }

        /// <summary>
        /// Wird beim drücken einer Maustaste aufgerufen
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnMouseDownItem(MouseEventArgs e)
        {
            MouseDownItem?.Invoke(this, e);
        }

        /// <summary>
        /// Wird beim loslassen der Maustaste gedrückt
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnMouseUpItem(object sender, MouseEventArgs e)
        {
            OnMouseUpItem(e);
        }

        /// <summary>
        /// Wird beim loslassen der Maustaste gedrückt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnMouseUpItem(MouseEventArgs e)
        {
            MouseUpItem?.Invoke(this, e);
        }

        /// <summary>
        /// Wird beim klicken ausgelöst
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnMouseClickItem(object sender, MouseEventArgs e)
        {
            OnMouseClickItem(e);
        }

        /// <summary>
        /// Wird beim klicken ausgelöst
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnMouseClickItem(MouseEventArgs e)
        {
            MouseClickItem?.Invoke(this, e);
        }

        /// <summary>
        /// Wird beim doppelklicken ausgelöst
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnMouseDoubleClickItem(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClickItem(e);
        }

        /// <summary>
        /// Wird beim doppelklicken ausgelöst
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnMouseDoubleClickItem(MouseEventArgs e)
        {
            MouseDoubleClickItem?.Invoke(this, e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich der Zoom ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnZoomValueChanged(object sender, EventArgs e)
        {
            var numbers = Regex.Match(m_zoomComboBox.Text.Trim(), "^[0-9]*")?.Value;
            var value = Convert.ToInt32(numbers);

            m_panel.Zoom = (float)value / 100;
            m_panel.Refresh();
        }

        /// <summary>
        /// Aktuakisiert die Itemansicht
        /// </summary>
        public void RefreshItem()
        {
            m_panel.Refresh();
        }

        /// <summary>
        /// Ermittelt die Koordinaten des Bereiches indem das Item gezeichnet werden soll
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle ItemViewRect
        {
            get
            {
                var infinity = false;
                return m_panel.GetItemViewRect(out infinity);
            }
        }

        /// <summary>
        /// Liefert oder setzt die Item-Eigenschaft
        /// </summary>
        public IItem Item { get; set; }

        
    }
}
