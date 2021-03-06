﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qellatalo.Nin.TheEyes
{
    internal class TransparentForm : Form
    {
        internal static Color TransparentKey { get; set; } = Color.LimeGreen;
        private Graphics g;
        internal TransparentForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = TransparentKey;
            TransparencyKey = BackColor;
            ShowInTaskbar = false;
            TopMost = true;
            Size = SystemInformation.VirtualScreen.Size;
            Location = SystemInformation.VirtualScreen.Location;
            g = CreateGraphics();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    if (!Visible)
        //    {
        //        base.OnPaintBackground(e);
        //    }
        //}

        internal void Clear()
        {
            Refresh();
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Visible = false;
                });
            }
            else
            {
                Visible = false;
            }
        }

        public override void Refresh()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(base.Refresh));
            }
            else
            {
                base.Refresh();
            }
        }

        internal void Highlight(Area area, Pen pen)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Visible = true;
                });
            }
            else
            {
                Visible = true;
            }
            using(Bitmap display = area.GetDisplayingImage()) { 
                g.DrawImage(display, area.Rectangle.Location);
            }
            g.DrawRectangle(pen, area.Rectangle);
        }

        internal void Caption(Area area, String str, Font font, Brush brush)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Visible = true;
                });
            }
            else
            {
                Visible = true;
            }
            using (Bitmap display = area.GetDisplayingImage())
            {
                g.DrawImage(display, area.Rectangle.Location);
            }
            g.DrawString(str, font, brush, area.Rectangle.Location);
        }

        internal void Highlight(Area area, Brush brush)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Visible = true;
                });
            }
            else
            {
                Visible = true;
            }
            using (Bitmap display = area.GetDisplayingImage())
            {
                g.DrawImage(display, area.Rectangle.Location);
            }
            g.FillRectangle(brush, area.Rectangle);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Clear();
        }

        protected override void Dispose(bool disposing)
        {
            g.Dispose();
            base.Dispose(disposing);
        }
    }
}
