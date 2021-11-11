using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TaskyJ.Interface.Windows
{
    public class ImageAndTextItem
    {
        private const int MarginWidth = 2;
        private const int MarginHeight = 2;

        public Image Picture;
        public int Id;
        public string Text;
        public Font Font;

        public ImageAndTextItem(Image picture, int id, string text, Font font)
        {
            Picture = picture;
            Id = id;
            Text = text;
            Font = font;
        }

        private int Width, Height;
        private bool SizeCalculated = false;
        public void MeasureItem(MeasureItemEventArgs e)
        {
            if (!SizeCalculated)
            {
                SizeCalculated = true;
                SizeF text_size = e.Graphics.MeasureString(Text, Font);
                Height = 2 * MarginHeight + (int)Math.Max(Picture.Height, text_size.Height);
                Width = (int)(4 * MarginWidth + Picture.Width + text_size.Width);
            }
            e.ItemWidth = Width;
            e.ItemHeight = Height;
        }

        public void DrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            float hgt = e.Bounds.Height - 2 * MarginHeight;
            float scale = hgt / Picture.Height;
            float wid = Picture.Width * scale;
            RectangleF rect = new RectangleF(
                e.Bounds.X + MarginWidth,
                e.Bounds.Y + MarginHeight,
                wid, hgt);
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.DrawImage(Picture, rect);
            string visible_text = Text;
            if (e.Bounds.Height < Picture.Height)
                visible_text = Text.Substring(0, Text.IndexOf('\n'));
            wid = e.Bounds.Width - rect.Right - 3 * MarginWidth;
            rect = new RectangleF(rect.Right + 2 * MarginWidth, rect.Y, wid, hgt);
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                e.Graphics.DrawString(visible_text, Font, Brushes.Black, rect, sf);
            }
            //e.Graphics.DrawRectangle(Pens.Blue, Rectangle.Round(rect));
            //e.DrawFocusRectangle();
        }
    }
}
