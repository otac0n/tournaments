using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tournaments.Graphics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Tournaments.Standard
{
    public class SystemGraphics : IGraphics
    {
        System.Drawing.Graphics baseGraphics;

        public SystemGraphics(System.Drawing.Graphics baseGraphics)
        {
            if (baseGraphics == null)
            {
                throw new ArgumentNullException("baseGraphics");
            }

            this.baseGraphics = baseGraphics;
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
            this.baseGraphics = System.Drawing.Graphics.FromImage(
                new Bitmap(1, 1));
        }

        public Region Clip { get { return baseGraphics.Clip; } set { baseGraphics.Clip = value; } }
        public RectangleF ClipBounds { get { return baseGraphics.ClipBounds; } }
        public CompositingMode CompositingMode { get { return baseGraphics.CompositingMode; } set { baseGraphics.CompositingMode = value; } }
        public CompositingQuality CompositingQuality { get { return baseGraphics.CompositingQuality; } set { baseGraphics.CompositingQuality = value; } }
        public float DpiX { get { return baseGraphics.DpiX; } }
        public float DpiY { get { return baseGraphics.DpiY; } }
        public InterpolationMode InterpolationMode { get { return baseGraphics.InterpolationMode; } set { baseGraphics.InterpolationMode = value; } }
        public bool IsClipEmpty { get { return baseGraphics.IsClipEmpty; } }
        public bool IsVisibleClipEmpty { get { return baseGraphics.IsVisibleClipEmpty; } }
        public float PageScale { get { return baseGraphics.PageScale; } set { baseGraphics.PageScale = value; } }
        public GraphicsUnit PageUnit { get { return baseGraphics.PageUnit; } set { baseGraphics.PageUnit = value; } }
        public PixelOffsetMode PixelOffsetMode { get { return baseGraphics.PixelOffsetMode; } set { baseGraphics.PixelOffsetMode = value; } }
        public Point RenderingOrigin { get { return baseGraphics.RenderingOrigin; } set { baseGraphics.RenderingOrigin = value; } }
        public SmoothingMode SmoothingMode { get { return baseGraphics.SmoothingMode; } set { baseGraphics.SmoothingMode = value; } }
        public int TextContrast { get { return baseGraphics.TextContrast; } set { baseGraphics.TextContrast = value; } }
        public TextRenderingHint TextRenderingHint { get { return baseGraphics.TextRenderingHint; } set { baseGraphics.TextRenderingHint = value; } }
        public Matrix Transform { get { return baseGraphics.Transform; } set { baseGraphics.Transform = value; } }
        public RectangleF VisibleClipBounds { get { return baseGraphics.VisibleClipBounds; } }
        public void AddMetafileComment(byte[] data) { baseGraphics.AddMetafileComment(data); }
        public GraphicsContainer BeginContainer() { return baseGraphics.BeginContainer(); }
        public GraphicsContainer BeginContainer(Rectangle destination, Rectangle source, GraphicsUnit unit) { return baseGraphics.BeginContainer(destination, source, unit); }
        public GraphicsContainer BeginContainer(RectangleF destination, RectangleF source, GraphicsUnit unit) { return baseGraphics.BeginContainer(destination, source, unit); }
        public void Clear(Color color) { baseGraphics.Clear(color); }
        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize) { baseGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize); }
        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation) { baseGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize, copyPixelOperation); }
        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize) { baseGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize); }
        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation) { baseGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation); }
        public void Dispose() { baseGraphics.Dispose(); }
        public void DrawArc(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle) { baseGraphics.DrawArc(pen, rectangle, startAngle, sweepAngle); }
        public void DrawArc(Pen pen, RectangleF rectangle, float startAngle, float sweepAngle) { baseGraphics.DrawArc(pen, rectangle, startAngle, sweepAngle); }
        public void DrawArc(Pen pen, float ellipseX, float ellipseY, float width, float height, float startAngle, float sweepAngle) { baseGraphics.DrawArc(pen, ellipseX, ellipseY, width, height, startAngle, sweepAngle); }
        public void DrawArc(Pen pen, int ellipseX, int ellipseY, int width, int height, int startAngle, int sweepAngle) { baseGraphics.DrawArc(pen, ellipseX, ellipseY, width, height, startAngle, sweepAngle); }
        public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4) { baseGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4); }
        public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4) { baseGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4); }
        public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4) { baseGraphics.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4); }
        public void DrawBeziers(Pen pen, Point[] points) { baseGraphics.DrawBeziers(pen, points); }
        public void DrawBeziers(Pen pen, PointF[] points) { baseGraphics.DrawBeziers(pen, points); }
        public void DrawClosedCurve(Pen pen, Point[] points) { baseGraphics.DrawClosedCurve(pen, points); }
        public void DrawClosedCurve(Pen pen, PointF[] points) { baseGraphics.DrawClosedCurve(pen, points); }
        public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillMode) { baseGraphics.DrawClosedCurve(pen, points, tension, fillMode); }
        public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillMode) { baseGraphics.DrawClosedCurve(pen, points, tension, fillMode); }
        public void DrawCurve(Pen pen, Point[] points) { baseGraphics.DrawCurve(pen, points); }
        public void DrawCurve(Pen pen, PointF[] points) { baseGraphics.DrawCurve(pen, points); }
        public void DrawCurve(Pen pen, Point[] points, float tension) { baseGraphics.DrawCurve(pen, points, tension); }
        public void DrawCurve(Pen pen, PointF[] points, float tension) { baseGraphics.DrawCurve(pen, points, tension); }
        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments) { baseGraphics.DrawCurve(pen, points, offset, numberOfSegments); }
        public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension) { baseGraphics.DrawCurve(pen, points, offset, numberOfSegments, tension); }
        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension) { baseGraphics.DrawCurve(pen, points, offset, numberOfSegments, tension); }
        public void DrawEllipse(Pen pen, Rectangle rectangle) { baseGraphics.DrawEllipse(pen, rectangle); }
        public void DrawEllipse(Pen pen, RectangleF rectangle) { baseGraphics.DrawEllipse(pen, rectangle); }
        public void DrawEllipse(Pen pen, float ellipseX, float ellipseY, float width, float height) { baseGraphics.DrawEllipse(pen, ellipseX, ellipseY, width, height); }
        public void DrawEllipse(Pen pen, int ellipseX, int ellipseY, int width, int height) { baseGraphics.DrawEllipse(pen, ellipseX, ellipseY, width, height); }
        public void DrawIcon(Icon icon, Rectangle targetRect) { baseGraphics.DrawIcon(icon, targetRect); }
        public void DrawIcon(Icon icon, int iconX, int iconY) { baseGraphics.DrawIcon(icon, iconX, iconY); }
        public void DrawIconUnstretched(Icon icon, Rectangle targetRect) { baseGraphics.DrawIconUnstretched(icon, targetRect); }
        public void DrawImage(Image image, Point point) { baseGraphics.DrawImage(image, point); }
        public void DrawImage(Image image, Point[] destinationPoints) { baseGraphics.DrawImage(image, destinationPoints); }
        public void DrawImage(Image image, PointF point) { baseGraphics.DrawImage(image, point); }
        public void DrawImage(Image image, PointF[] destinationPoints) { baseGraphics.DrawImage(image, destinationPoints); }
        public void DrawImage(Image image, Rectangle rectangle) { baseGraphics.DrawImage(image, rectangle); }
        public void DrawImage(Image image, RectangleF rectangle) { baseGraphics.DrawImage(image, rectangle); }
        public void DrawImage(Image image, float imageX, float imageY) { baseGraphics.DrawImage(image, imageX, imageY); }
        public void DrawImage(Image image, int imageX, int imageY) { baseGraphics.DrawImage(image, imageX, imageY); }
        public void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit) { baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit); }
        public void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit) { baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit); }
        public void DrawImage(Image image, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit) { baseGraphics.DrawImage(image, destinationRectangle, sourceRectangle, sourceUnit); }
        public void DrawImage(Image image, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit) { baseGraphics.DrawImage(image, destinationRectangle, sourceRectangle, sourceUnit); }
        public void DrawImage(Image image, float imageX, float imageY, float width, float height) { baseGraphics.DrawImage(image, imageX, imageY, width, height); }
        public void DrawImage(Image image, float imageX, float imageY, RectangleF sourceRectangle, GraphicsUnit sourceUnit) { baseGraphics.DrawImage(image, imageX, imageY, sourceRectangle, sourceUnit); }
        public void DrawImage(Image image, int imageX, int imageY, int width, int height) { baseGraphics.DrawImage(image, imageX, imageY, width, height); }
        public void DrawImage(Image image, int imageX, int imageY, Rectangle sourceRectangle, GraphicsUnit sourceUnit) { baseGraphics.DrawImage(image, imageX, imageY, sourceRectangle, sourceUnit); }
        public void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes) { baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes); }
        public void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes) { baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes); }
        public void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback) { baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes, callback); }
        public void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback) { baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes, callback); }
        public void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback, int callbackData) { baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes, callback, callbackData); }
        public void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback, int callbackData) { baseGraphics.DrawImage(image, destinationPoints, sourceRectangle, sourceUnit, imageAttributes, callback, callbackData); }
        public void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit) { baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit); }
        public void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit) { baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit); }
        public void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes) { baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes); }
        public void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes) { baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes); }
        public void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback) { baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes, callback); }
        public void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback) { baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes, callback); }
        public void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback, IntPtr callbackData) { baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes, callback, callbackData); }
        public void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, System.Drawing.Graphics.DrawImageAbort callback, IntPtr callbackData) { baseGraphics.DrawImage(image, destinationRectangle, sourceLeft, sourceTop, sourceWidth, sourceHeight, sourceUnit, imageAttributes, callback, callbackData); }
        public void DrawImageUnscaled(Image image, Point point) { baseGraphics.DrawImageUnscaled(image, point); }
        public void DrawImageUnscaled(Image image, Rectangle rectangle) { baseGraphics.DrawImageUnscaled(image, rectangle); }
        public void DrawImageUnscaled(Image image, int x, int y) { baseGraphics.DrawImageUnscaled(image, x, y); }
        public void DrawImageUnscaled(Image image, int x, int y, int width, int height) { baseGraphics.DrawImageUnscaled(image, x, y, width, height); }
        public void DrawImageUnscaledAndClipped(Image image, Rectangle rectangle) { baseGraphics.DrawImageUnscaledAndClipped(image, rectangle); }
        public void DrawLine(Pen pen, Point pt1, Point pt2) { baseGraphics.DrawLine(pen, pt1, pt2); }
        public void DrawLine(Pen pen, PointF pt1, PointF pt2) { baseGraphics.DrawLine(pen, pt1, pt2); }
        public void DrawLine(Pen pen, float startX, float startY, float endX, float endY) { baseGraphics.DrawLine(pen, startX, startY, endX, endY); }
        public void DrawLine(Pen pen, int startX, int startY, int endX, int endY) { baseGraphics.DrawLine(pen, startX, startY, endX, endY); }
        public void DrawLines(Pen pen, Point[] points) { baseGraphics.DrawLines(pen, points); }
        public void DrawLines(Pen pen, PointF[] points) { baseGraphics.DrawLines(pen, points); }
        public void DrawPath(Pen pen, GraphicsPath path) { baseGraphics.DrawPath(pen, path); }
        public void DrawPie(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle) { baseGraphics.DrawPie(pen, rectangle, startAngle, sweepAngle); }
        public void DrawPie(Pen pen, RectangleF rectangle, float startAngle, float sweepAngle) { baseGraphics.DrawPie(pen, rectangle, startAngle, sweepAngle); }
        public void DrawPie(Pen pen, float imageX, float imageY, float width, float height, float startAngle, float sweepAngle) { baseGraphics.DrawPie(pen, imageX, imageY, width, height, startAngle, sweepAngle); }
        public void DrawPie(Pen pen, int imageX, int imageY, int width, int height, int startAngle, int sweepAngle) { baseGraphics.DrawPie(pen, imageX, imageY, width, height, startAngle, sweepAngle); }
        public void DrawPolygon(Pen pen, Point[] points) { baseGraphics.DrawPolygon(pen, points); }
        public void DrawPolygon(Pen pen, PointF[] points) { baseGraphics.DrawPolygon(pen, points); }
        public void DrawRectangle(Pen pen, Rectangle rectangle) { baseGraphics.DrawRectangle(pen, rectangle); }
        public void DrawRectangle(Pen pen, float imageX, float imageY, float width, float height) { baseGraphics.DrawRectangle(pen, imageX, imageY, width, height); }
        public void DrawRectangle(Pen pen, int imageX, int imageY, int width, int height) { baseGraphics.DrawRectangle(pen, imageX, imageY, width, height); }
        public void DrawRectangles(Pen pen, Rectangle[] rects) { baseGraphics.DrawRectangles(pen, rects); }
        public void DrawRectangles(Pen pen, RectangleF[] rects) { baseGraphics.DrawRectangles(pen, rects); }
        public void DrawString(string s, Font font, Brush brush, PointF point) { baseGraphics.DrawString(s, font, brush, point); }
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle) { baseGraphics.DrawString(s, font, brush, layoutRectangle); }
        public void DrawString(string s, Font font, Brush brush, float x, float y) { baseGraphics.DrawString(s, font, brush, x, y); }
        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format) { baseGraphics.DrawString(s, font, brush, point, format); }
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format) { baseGraphics.DrawString(s, font, brush, layoutRectangle, format); }
        public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format) { baseGraphics.DrawString(s, font, brush, x, y, format); }
        public void EndContainer(GraphicsContainer container) { baseGraphics.EndContainer(container); }
        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback); }
        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback); }
        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback); }
        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback); }
        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback); }
        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback); }
        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, sourceUnit, callback); }
        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, sourceUnit, callback); }
        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, sourceUnit, callback); }
        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, sourceUnit, callback); }
        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, sourceUnit, callback); }
        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, sourceUnit, callback); }
        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, sourceUnit, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, sourceUnit, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, sourceUnit, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, sourceUnit, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, sourceUnit, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, sourceUnit, callback, callbackData); }
        public void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, unit, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, unit, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationPoint, sourceRectangle, unit, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationPoints, sourceRectangle, unit, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, unit, callback, callbackData, imageAttributes); }
        public void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit unit, System.Drawing.Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes) { baseGraphics.EnumerateMetafile(metafile, destinationRectangle, sourceRectangle, unit, callback, callbackData, imageAttributes); }
        public void ExcludeClip(Rectangle rectangle) { baseGraphics.ExcludeClip(rectangle); }
        public void ExcludeClip(Region region) { baseGraphics.ExcludeClip(region); }
        public void FillClosedCurve(Brush brush, Point[] points) { baseGraphics.FillClosedCurve(brush, points); }
        public void FillClosedCurve(Brush brush, PointF[] points) { baseGraphics.FillClosedCurve(brush, points); }
        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillMode) { baseGraphics.FillClosedCurve(brush, points, fillMode); }
        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillMode) { baseGraphics.FillClosedCurve(brush, points, fillMode); }
        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillMode, float tension) { baseGraphics.FillClosedCurve(brush, points, fillMode, tension); }
        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillMode, float tension) { baseGraphics.FillClosedCurve(brush, points, fillMode, tension); }
        public void FillEllipse(Brush brush, Rectangle rectangle) { baseGraphics.FillEllipse(brush, rectangle); }
        public void FillEllipse(Brush brush, RectangleF rectangle) { baseGraphics.FillEllipse(brush, rectangle); }
        public void FillEllipse(Brush brush, float x, float y, float width, float height) { baseGraphics.FillEllipse(brush, x, y, width, height); }
        public void FillEllipse(Brush brush, int x, int y, int width, int height) { baseGraphics.FillEllipse(brush, x, y, width, height); }
        public void FillPath(Brush brush, GraphicsPath path) { baseGraphics.FillPath(brush, path); }
        public void FillPie(Brush brush, Rectangle rectangle, float startAngle, float sweepAngle) { baseGraphics.FillPie(brush, rectangle, startAngle, sweepAngle); }
        public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle) { baseGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle); }
        public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle) { baseGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle); }
        public void FillPolygon(Brush brush, Point[] points) { baseGraphics.FillPolygon(brush, points); }
        public void FillPolygon(Brush brush, PointF[] points) { baseGraphics.FillPolygon(brush, points); }
        public void FillPolygon(Brush brush, Point[] points, FillMode fillMode) { baseGraphics.FillPolygon(brush, points, fillMode); }
        public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode) { baseGraphics.FillPolygon(brush, points, fillMode); }
        public void FillRectangle(Brush brush, Rectangle rectangle) { baseGraphics.FillRectangle(brush, rectangle); }
        public void FillRectangle(Brush brush, RectangleF rectangle) { baseGraphics.FillRectangle(brush, rectangle); }
        public void FillRectangle(Brush brush, float x, float y, float width, float height) { baseGraphics.FillRectangle(brush, x, y, width, height); }
        public void FillRectangle(Brush brush, int x, int y, int width, int height) { baseGraphics.FillRectangle(brush, x, y, width, height); }
        public void FillRectangles(Brush brush, Rectangle[] rects) { baseGraphics.FillRectangles(brush, rects); }
        public void FillRectangles(Brush brush, RectangleF[] rects) { baseGraphics.FillRectangles(brush, rects); }
        public void FillRegion(Brush brush, Region region) { baseGraphics.FillRegion(brush, region); }
        public void Flush() { baseGraphics.Flush(); }
        public void Flush(FlushIntention intention) { baseGraphics.Flush(intention); }
        public IntPtr GetHdc() { return baseGraphics.GetHdc(); }
        public Color GetNearestColor(Color color) { return baseGraphics.GetNearestColor(color); }
        public void IntersectClip(Rectangle rectangle) { baseGraphics.IntersectClip(rectangle); }
        public void IntersectClip(RectangleF rectangle) { baseGraphics.IntersectClip(rectangle); }
        public void IntersectClip(Region region) { baseGraphics.IntersectClip(region); }
        public bool IsVisible(Point point) { return baseGraphics.IsVisible(point); }
        public bool IsVisible(PointF point) { return baseGraphics.IsVisible(point); }
        public bool IsVisible(Rectangle rectangle) { return baseGraphics.IsVisible(rectangle); }
        public bool IsVisible(RectangleF rectangle) { return baseGraphics.IsVisible(rectangle); }
        public bool IsVisible(float x, float y) { return baseGraphics.IsVisible(x, y); }
        public bool IsVisible(int x, int y) { return baseGraphics.IsVisible(x, y); }
        public bool IsVisible(float x, float y, float width, float height) { return baseGraphics.IsVisible(x, y, width, height); }
        public bool IsVisible(int x, int y, int width, int height) { return baseGraphics.IsVisible(x, y, width, height); }
        public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat) { return baseGraphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat); }
        public SizeF MeasureString(string text, Font font) { return baseGraphics.MeasureString(text, font); }
        public SizeF MeasureString(string text, Font font, int width) { return baseGraphics.MeasureString(text, font, width); }
        public SizeF MeasureString(string text, Font font, SizeF layoutArea) { return baseGraphics.MeasureString(text, font, layoutArea); }
        public SizeF MeasureString(string text, Font font, int width, StringFormat format) { return baseGraphics.MeasureString(text, font, width, format); }
        public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat) { return baseGraphics.MeasureString(text, font, origin, stringFormat); }
        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat) { return baseGraphics.MeasureString(text, font, layoutArea, stringFormat); }
        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled) { return baseGraphics.MeasureString(text, font, layoutArea, stringFormat, out charactersFitted, out linesFilled); }
        public void MultiplyTransform(Matrix matrix) { baseGraphics.MultiplyTransform(matrix); }
        public void MultiplyTransform(Matrix matrix, MatrixOrder order) { baseGraphics.MultiplyTransform(matrix, order); }
        public void ReleaseHdc() { baseGraphics.ReleaseHdc(); }
        public void ReleaseHdc(IntPtr hdc) { baseGraphics.ReleaseHdc(hdc); }
        public void ReleaseHdcInternal(IntPtr hdc) { baseGraphics.ReleaseHdcInternal(hdc); }
        public void ResetClip() { baseGraphics.ResetClip(); }
        public void ResetTransform() { baseGraphics.ResetTransform(); }
        public void Restore(GraphicsState graphicsState) { baseGraphics.Restore(graphicsState); }
        public void RotateTransform(float angle) { baseGraphics.RotateTransform(angle); }
        public void RotateTransform(float angle, MatrixOrder order) { baseGraphics.RotateTransform(angle, order); }
        public GraphicsState Save() { return baseGraphics.Save(); }
        public void ScaleTransform(float sx, float sy) { baseGraphics.ScaleTransform(sx, sy); }
        public void ScaleTransform(float sx, float sy, MatrixOrder order) { baseGraphics.ScaleTransform(sx, sy, order); }
        public void SetClip(System.Drawing.Graphics g) { baseGraphics.SetClip(g); }
        public void SetClip(GraphicsPath path) { baseGraphics.SetClip(path); }
        public void SetClip(Rectangle rectangle) { baseGraphics.SetClip(rectangle); }
        public void SetClip(RectangleF rectangle) { baseGraphics.SetClip(rectangle); }
        public void SetClip(System.Drawing.Graphics g, CombineMode combineMode) { baseGraphics.SetClip(g, combineMode); }
        public void SetClip(GraphicsPath path, CombineMode combineMode) { baseGraphics.SetClip(path, combineMode); }
        public void SetClip(Rectangle rectangle, CombineMode combineMode) { baseGraphics.SetClip(rectangle, combineMode); }
        public void SetClip(RectangleF rectangle, CombineMode combineMode) { baseGraphics.SetClip(rectangle, combineMode); }
        public void SetClip(Region region, CombineMode combineMode) { baseGraphics.SetClip(region, combineMode); }
        public void TransformPoints(CoordinateSpace destinationSpace, CoordinateSpace sourceSpace, Point[] pts) { baseGraphics.TransformPoints(destinationSpace, sourceSpace, pts); }
        public void TransformPoints(CoordinateSpace destinationSpace, CoordinateSpace sourceSpace, PointF[] pts) { baseGraphics.TransformPoints(destinationSpace, sourceSpace, pts); }
        public void TranslateClip(float dx, float dy) { baseGraphics.TranslateClip(dx, dy); }
        public void TranslateClip(int dx, int dy) { baseGraphics.TranslateClip(dx, dy); }
        public void TranslateTransform(float dx, float dy) { baseGraphics.TranslateTransform(dx, dy); }
        public void TranslateTransform(float dx, float dy, MatrixOrder order) { baseGraphics.TranslateTransform(dx, dy, order); }
    }
}
