namespace Tournaments.Standard
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using Tournaments.Graphics;

    public class SystemGraphics : IGraphics
    {
        private System.Drawing.Graphics baseGraphics;

        public SystemGraphics(System.Drawing.Graphics baseGraphics)
        {
            this.baseGraphics = baseGraphics ?? throw new ArgumentNullException(nameof(baseGraphics));
        }

        public SystemGraphics(System.Drawing.Image targetImage)
        {
            if (targetImage == null)
            {
                throw new InvalidOperationException("targetImage");
            }

            this.baseGraphics = System.Drawing.Graphics.FromImage(targetImage);
        }

        public SystemGraphics()
        {
            this.baseGraphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1));
        }

        public Region Clip
        {
            get
            {
                return this.baseGraphics.Clip;
            }

            set
            {
                this.baseGraphics.Clip = value;
            }
        }

        public RectangleF ClipBounds
        {
            get
            {
                return this.baseGraphics.ClipBounds;
            }
        }

        public CompositingMode CompositingMode
        {
            get
            {
                return this.baseGraphics.CompositingMode;
            }

            set
            {
                this.baseGraphics.CompositingMode = value;
            }
        }

        public CompositingQuality CompositingQuality
        {
            get
            {
                return this.baseGraphics.CompositingQuality;
            }

            set
            {
                this.baseGraphics.CompositingQuality = value;
            }
        }

        public float DpiX
        {
            get
            {
                return this.baseGraphics.DpiX;
            }
        }

        public float DpiY
        {
            get
            {
                return this.baseGraphics.DpiY;
            }
        }

        public InterpolationMode InterpolationMode
        {
            get
            {
                return this.baseGraphics.InterpolationMode;
            }

            set
            {
                this.baseGraphics.InterpolationMode = value;
            }
        }

        public bool IsClipEmpty
        {
            get
            {
                return this.baseGraphics.IsClipEmpty;
            }
        }

        public bool IsVisibleClipEmpty
        {
            get
            {
                return this.baseGraphics.IsVisibleClipEmpty;
            }
        }

        public float PageScale
        {
            get
            {
                return this.baseGraphics.PageScale;
            }

            set
            {
                this.baseGraphics.PageScale = value;
            }
        }

        public GraphicsUnit PageUnit
        {
            get
            {
                return this.baseGraphics.PageUnit;
            }

            set
            {
                this.baseGraphics.PageUnit = value;
            }
        }

        public PixelOffsetMode PixelOffsetMode
        {
            get
            {
                return this.baseGraphics.PixelOffsetMode;
            }

            set
            {
                this.baseGraphics.PixelOffsetMode = value;
            }
        }

        public Point RenderingOrigin
        {
            get
            {
                return this.baseGraphics.RenderingOrigin;
            }

            set
            {
                this.baseGraphics.RenderingOrigin = value;
            }
        }

        public SmoothingMode SmoothingMode
        {
            get
            {
                return this.baseGraphics.SmoothingMode;
            }

            set
            {
                this.baseGraphics.SmoothingMode = value;
            }
        }

        public int TextContrast
        {
            get
            {
                return this.baseGraphics.TextContrast;
            }

            set
            {
                this.baseGraphics.TextContrast = value;
            }
        }

        public TextRenderingHint TextRenderingHint
        {
            get
            {
                return this.baseGraphics.TextRenderingHint;
            }

            set
            {
                this.baseGraphics.TextRenderingHint = value;
            }
        }

        public Matrix Transform
        {
            get
            {
                return this.baseGraphics.Transform;
            }

            set
            {
                this.baseGraphics.Transform = value;
            }
        }

        public RectangleF VisibleClipBounds
        {
            get
            {
                return this.baseGraphics.VisibleClipBounds;
            }
        }

        public void AddMetafileComment(byte[] data)
        {
            this.baseGraphics.AddMetafileComment(data);
        }

        public GraphicsContainer BeginContainer()
        {
            return this.baseGraphics.BeginContainer();
        }

        public GraphicsContainer BeginContainer(Rectangle destination, Rectangle source, GraphicsUnit unit)
        {
            return this.baseGraphics.BeginContainer(destination, source, unit);
        }

        public GraphicsContainer BeginContainer(RectangleF destination, RectangleF source, GraphicsUnit unit)
        {
            return this.baseGraphics.BeginContainer(destination, source, unit);
        }

        public void Clear(Color color)
        {
            this.baseGraphics.Clear(color);
        }

        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
        {
            this.baseGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize);
        }

        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
        {
            this.baseGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize, copyPixelOperation);
        }

        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize)
        {
            this.baseGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize);
        }

        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
        {
            this.baseGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
        }

        public void Dispose()
        {
            this.baseGraphics.Dispose();
        }

        public void DrawArc(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle)
        {
            this.baseGraphics.DrawArc(pen, rectangle, startAngle, sweepAngle);
        }

        public void DrawArc(Pen pen, RectangleF rectangle, float startAngle, float sweepAngle)
        {
            this.baseGraphics.DrawArc(pen, rectangle, startAngle, sweepAngle);
        }

        public void DrawArc(Pen pen, float ellipseX, float ellipseY, float width, float height, float startAngle, float sweepAngle)
        {
            this.baseGraphics.DrawArc(pen, ellipseX, ellipseY, width, height, startAngle, sweepAngle);
        }

        public void DrawArc(Pen pen, int ellipseX, int ellipseY, int width, int height, int startAngle, int sweepAngle)
        {
            this.baseGraphics.DrawArc(pen, ellipseX, ellipseY, width, height, startAngle, sweepAngle);
        }

        public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
        {
            this.baseGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        {
            this.baseGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            this.baseGraphics.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
        }

        public void DrawBeziers(Pen pen, Point[] points)
        {
            this.baseGraphics.DrawBeziers(pen, points);
        }

        public void DrawBeziers(Pen pen, PointF[] points)
        {
            this.baseGraphics.DrawBeziers(pen, points);
        }

        public void DrawClosedCurve(Pen pen, Point[] points)
        {
            this.baseGraphics.DrawClosedCurve(pen, points);
        }

        public void DrawClosedCurve(Pen pen, PointF[] points)
        {
            this.baseGraphics.DrawClosedCurve(pen, points);
        }

        public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillMode)
        {
            this.baseGraphics.DrawClosedCurve(pen, points, tension, fillMode);
        }

        public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillMode)
        {
            this.baseGraphics.DrawClosedCurve(pen, points, tension, fillMode);
        }

        public void DrawCurve(Pen pen, Point[] points)
        {
            this.baseGraphics.DrawCurve(pen, points);
        }

        public void DrawCurve(Pen pen, PointF[] points)
        {
            this.baseGraphics.DrawCurve(pen, points);
        }

        public void DrawCurve(Pen pen, Point[] points, float tension)
        {
            this.baseGraphics.DrawCurve(pen, points, tension);
        }

        public void DrawCurve(Pen pen, PointF[] points, float tension)
        {
            this.baseGraphics.DrawCurve(pen, points, tension);
        }

        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
        {
            this.baseGraphics.DrawCurve(pen, points, offset, numberOfSegments);
        }

        public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
        {
            this.baseGraphics.DrawCurve(pen, points, offset, numberOfSegments, tension);
        }

        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
        {
            this.baseGraphics.DrawCurve(pen, points, offset, numberOfSegments, tension);
        }

        public void DrawEllipse(Pen pen, Rectangle rectangle)
        {
            this.baseGraphics.DrawEllipse(pen, rectangle);
        }

        public void DrawEllipse(Pen pen, RectangleF rectangle)
        {
            this.baseGraphics.DrawEllipse(pen, rectangle);
        }

        public void DrawEllipse(Pen pen, float ellipseX, float ellipseY, float width, float height)
        {
            this.baseGraphics.DrawEllipse(pen, ellipseX, ellipseY, width, height);
        }

        public void DrawEllipse(Pen pen, int ellipseX, int ellipseY, int width, int height)
        {
            this.baseGraphics.DrawEllipse(pen, ellipseX, ellipseY, width, height);
        }

        public void DrawIcon(Icon icon, Rectangle targetRect)
        {
            this.baseGraphics.DrawIcon(icon, targetRect);
        }

        public void DrawIcon(Icon icon, int iconX, int iconY)
        {
            this.baseGraphics.DrawIcon(icon, iconX, iconY);
        }

        public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
        {
            this.baseGraphics.DrawIconUnstretched(icon, targetRect);
        }

        public void DrawImage(Image image, Point point)
        {
            this.baseGraphics.DrawImage(image, point);
        }

        public void DrawImage(Image image, Point[] destinationPoints)
        {
            this.baseGraphics.DrawImage(image, destinationPoints);
        }

        public void DrawImage(Image image, PointF point)
        {
            this.baseGraphics.DrawImage(image, point);
        }

        public void DrawImage(Image image, PointF[] destinationPoints)
        {
            this.baseGraphics.DrawImage(image, destinationPoints);
        }

        public void DrawImage(Image image, Rectangle rectangle)
        {
            this.baseGraphics.DrawImage(image, rectangle);
        }

        public void DrawImage(Image image, RectangleF rectangle)
        {
            this.baseGraphics.DrawImage(image, rectangle);
        }

        public void DrawImage(Image image, float imageX, float imageY)
        {
            this.baseGraphics.DrawImage(image, imageX, imageY);
        }

        public void DrawImage(Image image, int imageX, int imageY)
        {
            this.baseGraphics.DrawImage(image, imageX, imageY);
        }

        public void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit)
        {
            this.baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit);
        }

        public void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit)
        {
            this.baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceRectangle, sourceUnit);
        }

        public void DrawImage(Image image, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceRectangle, sourceUnit);
        }

        public void DrawImage(Image image, float imageX, float imageY, float width, float height)
        {
            this.baseGraphics.DrawImage(image, imageX, imageY, width, height);
        }

        public void DrawImage(Image image, float imageX, float imageY, RectangleF sourceRectangle, GraphicsUnit sourceUnit)
        {
            this.baseGraphics.DrawImage(image, imageX, imageY, sourceRectangle, sourceUnit);
        }

        public void DrawImage(Image image, int imageX, int imageY, int width, int height)
        {
            this.baseGraphics.DrawImage(image, imageX, imageY, width, height);
        }

        public void DrawImage(Image image, int imageX, int imageY, Rectangle sourceRectangle, GraphicsUnit sourceUnit)
        {
            this.baseGraphics.DrawImage(image, imageX, imageY, sourceRectangle, sourceUnit);
        }

        public void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes)
        {
            this.baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes);
        }

        public void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes)
        {
            this.baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes);
        }

        public void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback)
        {
            this.baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes, callback);
        }

        public void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback)
        {
            this.baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes, callback);
        }

        public void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback, int callbackData)
        {
            this.baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes, callback, callbackData);
        }

        public void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback, int callbackData)
        {
            this.baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes, callback, callbackData);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes, callback);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes, callback);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback, IntPtr callbackData)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes, callback, callbackData);
        }

        public void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback, IntPtr callbackData)
        {
            this.baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes, callback, callbackData);
        }

        public void DrawImageUnscaled(Image image, Point point)
        {
            this.baseGraphics.DrawImageUnscaled(image, point);
        }

        public void DrawImageUnscaled(Image image, Rectangle rectangle)
        {
            this.baseGraphics.DrawImageUnscaled(image, rectangle);
        }

        public void DrawImageUnscaled(Image image, int x, int y)
        {
            this.baseGraphics.DrawImageUnscaled(image, x, y);
        }

        public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
        {
            this.baseGraphics.DrawImageUnscaled(image, x, y, width, height);
        }

        public void DrawImageUnscaledAndClipped(Image image, Rectangle rectangle)
        {
            this.baseGraphics.DrawImageUnscaledAndClipped(image, rectangle);
        }

        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            this.baseGraphics.DrawLine(pen, pt1, pt2);
        }

        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            this.baseGraphics.DrawLine(pen, pt1, pt2);
        }

        public void DrawLine(Pen pen, float startX, float startY, float endX, float endY)
        {
            this.baseGraphics.DrawLine(pen, startX, startY, endX, endY);
        }

        public void DrawLine(Pen pen, int startX, int startY, int endX, int endY)
        {
            this.baseGraphics.DrawLine(pen, startX, startY, endX, endY);
        }

        public void DrawLines(Pen pen, Point[] points)
        {
            this.baseGraphics.DrawLines(pen, points);
        }

        public void DrawLines(Pen pen, PointF[] points)
        {
            this.baseGraphics.DrawLines(pen, points);
        }

        public void DrawPath(Pen pen, GraphicsPath path)
        {
            this.baseGraphics.DrawPath(pen, path);
        }

        public void DrawPie(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle)
        {
            this.baseGraphics.DrawPie(pen, rectangle, startAngle, sweepAngle);
        }

        public void DrawPie(Pen pen, RectangleF rectangle, float startAngle, float sweepAngle)
        {
            this.baseGraphics.DrawPie(pen, rectangle, startAngle, sweepAngle);
        }

        public void DrawPie(Pen pen, float imageX, float imageY, float width, float height, float startAngle, float sweepAngle)
        {
            this.baseGraphics.DrawPie(pen, imageX, imageY, width, height, startAngle, sweepAngle);
        }

        public void DrawPie(Pen pen, int imageX, int imageY, int width, int height, int startAngle, int sweepAngle)
        {
            this.baseGraphics.DrawPie(pen, imageX, imageY, width, height, startAngle, sweepAngle);
        }

        public void DrawPolygon(Pen pen, Point[] points)
        {
            this.baseGraphics.DrawPolygon(pen, points);
        }

        public void DrawPolygon(Pen pen, PointF[] points)
        {
            this.baseGraphics.DrawPolygon(pen, points);
        }

        public void DrawRectangle(Pen pen, Rectangle rectangle)
        {
            this.baseGraphics.DrawRectangle(pen, rectangle);
        }

        public void DrawRectangle(Pen pen, float imageX, float imageY, float width, float height)
        {
            this.baseGraphics.DrawRectangle(pen, imageX, imageY, width, height);
        }

        public void DrawRectangle(Pen pen, int imageX, int imageY, int width, int height)
        {
            this.baseGraphics.DrawRectangle(pen, imageX, imageY, width, height);
        }

        public void DrawRectangles(Pen pen, Rectangle[] rects)
        {
            this.baseGraphics.DrawRectangles(pen, rects);
        }

        public void DrawRectangles(Pen pen, RectangleF[] rects)
        {
            this.baseGraphics.DrawRectangles(pen, rects);
        }

        public void DrawString(string s, Font font, Brush brush, PointF point)
        {
            this.baseGraphics.DrawString(s, font, brush, point);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            this.baseGraphics.DrawString(s, font, brush, layoutRectangle);
        }

        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            this.baseGraphics.DrawString(s, font, brush, x, y);
        }

        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            this.baseGraphics.DrawString(s, font, brush, point, format);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            this.baseGraphics.DrawString(s, font, brush, layoutRectangle, format);
        }

        public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
        {
            this.baseGraphics.DrawString(s, font, brush, x, y, format);
        }

        public void EndContainer(GraphicsContainer container)
        {
            this.baseGraphics.EndContainer(container);
        }

        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback);
        }

        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback);
        }

        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback);
        }

        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback);
        }

        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback);
        }

        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback);
        }

        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, sourceUnit, callback);
        }

        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, sourceUnit, callback);
        }

        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, sourceUnit, callback);
        }

        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, sourceUnit, callback);
        }

        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, sourceUnit, callback);
        }

        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, sourceUnit, callback);
        }

        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, sourceUnit, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, sourceUnit, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, sourceUnit, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, sourceUnit, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, sourceUnit, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, sourceUnit, callback, callbackData);
        }

        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, unit, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, unit, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, unit, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, unit, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, unit, callback, callbackData, imageAttributes);
        }

        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes)
        {
            this.baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, unit, callback, callbackData, imageAttributes);
        }

        public void ExcludeClip(Rectangle rectangle)
        {
            this.baseGraphics.ExcludeClip(rectangle);
        }

        public void ExcludeClip(Region region)
        {
            this.baseGraphics.ExcludeClip(region);
        }

        public void FillClosedCurve(Brush brush, Point[] points)
        {
            this.baseGraphics.FillClosedCurve(brush, points);
        }

        public void FillClosedCurve(Brush brush, PointF[] points)
        {
            this.baseGraphics.FillClosedCurve(brush, points);
        }

        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillMode)
        {
            this.baseGraphics.FillClosedCurve(brush, points, fillMode);
        }

        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillMode)
        {
            this.baseGraphics.FillClosedCurve(brush, points, fillMode);
        }

        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillMode, float tension)
        {
            this.baseGraphics.FillClosedCurve(brush, points, fillMode, tension);
        }

        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillMode, float tension)
        {
            this.baseGraphics.FillClosedCurve(brush, points, fillMode, tension);
        }

        public void FillEllipse(Brush brush, Rectangle rectangle)
        {
            this.baseGraphics.FillEllipse(brush, rectangle);
        }

        public void FillEllipse(Brush brush, RectangleF rectangle)
        {
            this.baseGraphics.FillEllipse(brush, rectangle);
        }

        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            this.baseGraphics.FillEllipse(brush, x, y, width, height);
        }

        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        {
            this.baseGraphics.FillEllipse(brush, x, y, width, height);
        }

        public void FillPath(Brush brush, GraphicsPath path)
        {
            this.baseGraphics.FillPath(brush, path);
        }

        public void FillPie(Brush brush, Rectangle rectangle, float startAngle, float sweepAngle)
        {
            this.baseGraphics.FillPie(brush, rectangle, startAngle, sweepAngle);
        }

        public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            this.baseGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
        }

        public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            this.baseGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
        }

        public void FillPolygon(Brush brush, Point[] points)
        {
            this.baseGraphics.FillPolygon(brush, points);
        }

        public void FillPolygon(Brush brush, PointF[] points)
        {
            this.baseGraphics.FillPolygon(brush, points);
        }

        public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
        {
            this.baseGraphics.FillPolygon(brush, points, fillMode);
        }

        public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
        {
            this.baseGraphics.FillPolygon(brush, points, fillMode);
        }

        public void FillRectangle(Brush brush, Rectangle rectangle)
        {
            this.baseGraphics.FillRectangle(brush, rectangle);
        }

        public void FillRectangle(Brush brush, RectangleF rectangle)
        {
            this.baseGraphics.FillRectangle(brush, rectangle);
        }

        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            this.baseGraphics.FillRectangle(brush, x, y, width, height);
        }

        public void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            this.baseGraphics.FillRectangle(brush, x, y, width, height);
        }

        public void FillRectangles(Brush brush, Rectangle[] rects)
        {
            this.baseGraphics.FillRectangles(brush, rects);
        }

        public void FillRectangles(Brush brush, RectangleF[] rects)
        {
            this.baseGraphics.FillRectangles(brush, rects);
        }

        public void FillRegion(Brush brush, Region region)
        {
            this.baseGraphics.FillRegion(brush, region);
        }

        public void Flush()
        {
            this.baseGraphics.Flush();
        }

        public void Flush(FlushIntention intention)
        {
            this.baseGraphics.Flush(intention);
        }

        public IntPtr GetHdc()
        {
            return this.baseGraphics.GetHdc();
        }

        public Color GetNearestColor(Color color)
        {
            return this.baseGraphics.GetNearestColor(color);
        }

        public void IntersectClip(Rectangle rectangle)
        {
            this.baseGraphics.IntersectClip(rectangle);
        }

        public void IntersectClip(RectangleF rectangle)
        {
            this.baseGraphics.IntersectClip(rectangle);
        }

        public void IntersectClip(Region region)
        {
            this.baseGraphics.IntersectClip(region);
        }

        public bool IsVisible(Point point)
        {
            return this.baseGraphics.IsVisible(point);
        }

        public bool IsVisible(PointF point)
        {
            return this.baseGraphics.IsVisible(point);
        }

        public bool IsVisible(Rectangle rectangle)
        {
            return this.baseGraphics.IsVisible(rectangle);
        }

        public bool IsVisible(RectangleF rectangle)
        {
            return this.baseGraphics.IsVisible(rectangle);
        }

        public bool IsVisible(float x, float y)
        {
            return this.baseGraphics.IsVisible(x, y);
        }

        public bool IsVisible(int x, int y)
        {
            return this.baseGraphics.IsVisible(x, y);
        }

        public bool IsVisible(float x, float y, float width, float height)
        {
            return this.baseGraphics.IsVisible(x, y, width, height);
        }

        public bool IsVisible(int x, int y, int width, int height)
        {
            return this.baseGraphics.IsVisible(x, y, width, height);
        }

        public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
        {
            return this.baseGraphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
        }

        public SizeF MeasureString(string text, Font font)
        {
            return this.baseGraphics.MeasureString(text, font);
        }

        public SizeF MeasureString(string text, Font font, int width)
        {
            return this.baseGraphics.MeasureString(text, font, width);
        }

        public SizeF MeasureString(string text, Font font, SizeF layoutArea)
        {
            return this.baseGraphics.MeasureString(text, font, layoutArea);
        }

        public SizeF MeasureString(string text, Font font, int width, StringFormat format)
        {
            return this.baseGraphics.MeasureString(text, font, width, format);
        }

        public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
        {
            return this.baseGraphics.MeasureString(text, font, origin, stringFormat);
        }

        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
        {
            return this.baseGraphics.MeasureString(text, font, layoutArea, stringFormat);
        }

        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
        {
            return this.baseGraphics.MeasureString(text, font, layoutArea, stringFormat, out charactersFitted, out linesFilled);
        }

        public void MultiplyTransform(Matrix matrix)
        {
            this.baseGraphics.MultiplyTransform(matrix);
        }

        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            this.baseGraphics.MultiplyTransform(matrix, order);
        }

        public void ReleaseHdc()
        {
            this.baseGraphics.ReleaseHdc();
        }

        public void ReleaseHdc(IntPtr hdc)
        {
            this.baseGraphics.ReleaseHdc(hdc);
        }

        public void ReleaseHdcInternal(IntPtr hdc)
        {
            this.baseGraphics.ReleaseHdcInternal(hdc);
        }

        public void ResetClip()
        {
            this.baseGraphics.ResetClip();
        }

        public void ResetTransform()
        {
            this.baseGraphics.ResetTransform();
        }

        public void Restore(GraphicsState graphicsState)
        {
            this.baseGraphics.Restore(graphicsState);
        }

        public void RotateTransform(float angle)
        {
            this.baseGraphics.RotateTransform(angle);
        }

        public void RotateTransform(float angle, MatrixOrder order)
        {
            this.baseGraphics.RotateTransform(angle, order);
        }

        public GraphicsState Save()
        {
            return this.baseGraphics.Save();
        }

        public void ScaleTransform(float sx, float sy)
        {
            this.baseGraphics.ScaleTransform(sx, sy);
        }

        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            this.baseGraphics.ScaleTransform(sx, sy, order);
        }

        public void SetClip(System.Drawing.Graphics g)
        {
            this.baseGraphics.SetClip(g);
        }

        public void SetClip(GraphicsPath path)
        {
            this.baseGraphics.SetClip(path);
        }

        public void SetClip(Rectangle rectangle)
        {
            this.baseGraphics.SetClip(rectangle);
        }

        public void SetClip(RectangleF rectangle)
        {
            this.baseGraphics.SetClip(rectangle);
        }

        public void SetClip(System.Drawing.Graphics g, CombineMode combineMode)
        {
            this.baseGraphics.SetClip(g, combineMode);
        }

        public void SetClip(GraphicsPath path, CombineMode combineMode)
        {
            this.baseGraphics.SetClip(path, combineMode);
        }

        public void SetClip(Rectangle rectangle, CombineMode combineMode)
        {
            this.baseGraphics.SetClip(rectangle, combineMode);
        }

        public void SetClip(RectangleF rectangle, CombineMode combineMode)
        {
            this.baseGraphics.SetClip(rectangle, combineMode);
        }

        public void SetClip(Region region, CombineMode combineMode)
        {
            this.baseGraphics.SetClip(region, combineMode);
        }

        public void TransformPoints(CoordinateSpace destinationSpace, CoordinateSpace sourceSpace, Point[] pts)
        {
            this.baseGraphics.TransformPoints(destinationSpace, sourceSpace, pts);
        }

        public void TransformPoints(CoordinateSpace destinationSpace, CoordinateSpace sourceSpace, PointF[] pts)
        {
            this.baseGraphics.TransformPoints(destinationSpace, sourceSpace, pts);
        }

        public void TranslateClip(float dx, float dy)
        {
            this.baseGraphics.TranslateClip(dx, dy);
        }

        public void TranslateClip(int dx, int dy)
        {
            this.baseGraphics.TranslateClip(dx, dy);
        }

        public void TranslateTransform(float dx, float dy)
        {
            this.baseGraphics.TranslateTransform(dx, dy);
        }

        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            this.baseGraphics.TranslateTransform(dx, dy, order);
        }
    }
}
