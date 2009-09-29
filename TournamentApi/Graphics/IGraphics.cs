//-----------------------------------------------------------------------
// <copyright file="IGraphics.cs" company="(none)">
//  Copyright (c) 2009 John Gietzen
//
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
//  BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
//  ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE
// </copyright>
// <author>John Gietzen</author>
//-----------------------------------------------------------------------

namespace Tournaments.Graphics
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;

    /// <summary>
    /// Represents the interface of a graphics object.
    /// </summary>
    /// <remarks>Build from metatdata.</remarks>
    public interface IGraphics
    {
        /// <summary>Gets or sets a System.Drawing.Region that limits the drawing region of this System.Drawing.Graphics.</summary>
        /// <returns>A System.Drawing.Region that limits the portion of this System.Drawing.Graphics that is currently available for drawing.</returns>
        Region Clip { get; set; }

        /// <summary>Gets a System.Drawing.RectangleF structure that bounds the clipping region of this System.Drawing.Graphics.</summary>
        /// <returns>A System.Drawing.RectangleF structure that represents a bounding rectangle for the clipping region of this System.Drawing.Graphics.</returns>
        RectangleF ClipBounds { get; }

        /// <summary>Gets or sets a value that specifies how composited images are drawn to this System.Drawing.Graphics.</summary>
        /// <returns>This property specifies a member of the System.Drawing.Drawing2D.CompositingMode enumeration.</returns>
        CompositingMode CompositingMode { get; set; }

        /// <summary>Gets or sets the rendering quality of composited images drawn to this System.Drawing.Graphics.</summary>
        /// <returns>This property specifies a member of the System.Drawing.Drawing2D.CompositingQuality enumeration.</returns>
        CompositingQuality CompositingQuality { get; set; }

        /// <summary>Gets the horizontal resolution of this System.Drawing.Graphics.</summary>
        /// <returns>The value, in dots per inch, for the horizontal resolution supported by this System.Drawing.Graphics.</returns>
        float DpiX { get; }

        /// <summary>Gets the vertical resolution of this System.Drawing.Graphics.</summary>
        /// <returns>The value, in dots per inch, for the vertical resolution supported by this System.Drawing.Graphics.</returns>
        float DpiY { get; }

        /// <summary>Gets or sets the interpolation mode associated with this System.Drawing.Graphics.</summary>
        /// <returns>One of the System.Drawing.Drawing2D.InterpolationMode values.</returns>
        InterpolationMode InterpolationMode { get; set; }

        /// <summary>Gets a value indicating whether the clipping region of this System.Drawing.Graphics is empty.</summary>
        /// <returns>true if the clipping region of this System.Drawing.Graphics is empty; otherwise, false.</returns>
        bool IsClipEmpty { get; }

        /// <summary>Gets a value indicating whether the visible clipping region of this System.Drawing.Graphics is empty.</summary>
        /// <returns>true if the visible portion of the clipping region of this System.Drawing.Graphics is empty; otherwise, false.</returns>
        bool IsVisibleClipEmpty { get; }

        /// <summary>Gets or sets the scaling between world units and page units for this System.Drawing.Graphics.</summary>
        /// <returns>This property specifies a value for the scaling between world units and page units for this System.Drawing.Graphics.</returns>
        float PageScale { get; set; }

        /// <summary>Gets or sets the unit of measure used for page coordinates in this System.Drawing.Graphics.</summary>
        /// <returns>One of the System.Drawing.GraphicsUnit values other than System.Drawing.GraphicsUnit.World.</returns>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">System.Drawing.Graphics.PageUnit is set to System.Drawing.GraphicsUnit.World, which is not a physical unit.</exception>
        GraphicsUnit PageUnit { get; set; }

        /// <summary>Gets or sets a value specifying how pixels are offset during rendering of this System.Drawing.Graphics.</summary>
        /// <returns>This property specifies a member of the System.Drawing.Drawing2D.PixelOffsetMode enumeration</returns>
        PixelOffsetMode PixelOffsetMode { get; set; }

        /// <summary>Gets or sets the rendering origin of this System.Drawing.Graphics for dithering and for hatch brushes.</summary>
        /// <returns>A System.Drawing.Point structure that represents the dither origin for 8-bits-per-pixel and 16-bits-per-pixel dithering and is also used to set the origin for hatch brushes.</returns>
        Point RenderingOrigin { get; set; }

        /// <summary>Gets or sets the rendering quality for this System.Drawing.Graphics.</summary>
        /// <returns>One of the System.Drawing.Drawing2D.SmoothingMode values.</returns>
        SmoothingMode SmoothingMode { get; set; }

        /// <summary>Gets or sets the gamma correction value for rendering text.</summary>
        /// <returns>The gamma correction value used for rendering antialiased and ClearType text.</returns>
        int TextContrast { get; set; }

        /// <summary>Gets or sets the rendering mode for text associated with this System.Drawing.Graphics.</summary>
        /// <returns>One of the System.Drawing.Text.TextRenderingHint values.</returns>
        TextRenderingHint TextRenderingHint { get; set; }

        /// <summary>Gets or sets a copy of the geometric world transformation for this System.Drawing.Graphics.</summary>
        /// <returns>A copy of the System.Drawing.Drawing2D.Matrix that represents the geometric world transformation for this System.Drawing.Graphics.</returns>
        Matrix Transform { get; set; }

        /// <summary>Gets the bounding rectangle of the visible clipping region of this System.Drawing.Graphics.</summary>
        /// <returns>A System.Drawing.RectangleF structure that represents a bounding rectangle for the visible clipping region of this System.Drawing.Graphics.</returns>
        RectangleF VisibleClipBounds { get; }

        /// <summary>Adds a comment to the current System.Drawing.Imaging.Metafile.</summary>
        /// <param name="data">Array of bytes that contains the comment.</param>
        void AddMetafileComment(byte[] data);

        /// <summary>Saves a graphics container with the current state of this System.Drawing.Graphics and opens and uses a new graphics container.</summary>
        /// <returns>This method returns a System.Drawing.Drawing2D.GraphicsContainer that represents the state of this System.Drawing.Graphics at the time of the method call.</returns>
        GraphicsContainer BeginContainer();

        /// <summary>Saves a graphics container with the current state of this System.Drawing.Graphics and opens and uses a new graphics container with the specified scale transformation.</summary>
        /// <param name="destination">System.Drawing.Rectangle structure that, together with the source parameter, specifies a scale transformation for the container.</param>
        /// <param name="source">System.Drawing.Rectangle structure that, together with the destination parameter, specifies a scale transformation for the container.</param>
        /// <param name="unit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure for the container.</param>
        /// <returns>This method returns a System.Drawing.Drawing2D.GraphicsContainer that represents the state of this System.Drawing.Graphics at the time of the method call.</returns>
        GraphicsContainer BeginContainer(Rectangle destination, Rectangle source, GraphicsUnit unit);

        /// <summary>Saves a graphics container with the current state of this System.Drawing.Graphics and opens and uses a new graphics container with the specified scale transformation.</summary>
        /// <param name="destination">System.Drawing.RectangleF structure that, together with the source parameter, specifies a scale transformation for the new graphics container.</param>
        /// <param name="source">System.Drawing.RectangleF structure that, together with the destination parameter, specifies a scale transformation for the new graphics container.</param>
        /// <param name="unit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure for the container.</param>
        /// <returns>This method returns a System.Drawing.Drawing2D.GraphicsContainer that represents the state of this System.Drawing.Graphics at the time of the method call.</returns>
        GraphicsContainer BeginContainer(RectangleF destination, RectangleF source, GraphicsUnit unit);

        /// <summary>Clears the entire drawing surface and fills it with the specified background color.</summary>
        /// <param name="color">System.Drawing.Color structure that represents the background color of the drawing surface.</param>
        void Clear(Color color);

        /// <summary>Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the System.Drawing.Graphics.</summary>
        /// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
        /// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <exception cref="System.ComponentModel.Win32Exception">The operation failed.</exception>
        void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize);

        /// <summary>Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the System.Drawing.Graphics.</summary>
        /// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
        /// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <param name="copyPixelOperation">One of the System.Drawing.CopyPixelOperation values.</param>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">copyPixelOperation is not a member of System.Drawing.CopyPixelOperation.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">The operation failed.</exception>
        void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation);

        /// <summary>Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the System.Drawing.Graphics.</summary>
        /// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <exception cref="System.ComponentModel.Win32Exception">The operation failed.</exception>
        void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize);

        /// <summary>Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the System.Drawing.Graphics.</summary>
        /// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle</param>
        /// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <param name="copyPixelOperation">One of the System.Drawing.CopyPixelOperation values.</param>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">copyPixelOperation is not a member of System.Drawing.CopyPixelOperation.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">The operation failed.</exception>
        void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation);

        /// <summary>Releases all resources used by this System.Drawing.Graphics.</summary>
        void Dispose();

        /// <summary>Draws an arc representing a portion of an ellipse specified by a System.Drawing.Rectangle structure.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the arc.</param>
        /// <param name="rectangle">System.Drawing.RectangleF structure that defines the boundaries of the ellipse.</param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawArc(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle);

        /// <summary>Draws an arc representing a portion of an ellipse specified by a System.Drawing.RectangleF structure.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the arc.</param>
        /// <param name="rectangle">System.Drawing.RectangleF structure that defines the boundaries of the ellipse.</param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.</param>
        /// <exception cref="System.ArgumentNullException">pen is null</exception>
        void DrawArc(Pen pen, RectangleF rectangle, float startAngle, float sweepAngle);

        /// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the arc.</param>
        /// <param name="ellipseX">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
        /// <param name="ellipseY">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
        /// <param name="width">Width of the rectangle that defines the ellipse.</param>
        /// <param name="height">Height of the rectangle that defines the ellipse.</param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawArc(Pen pen, float ellipseX, float ellipseY, float width, float height, float startAngle, float sweepAngle);

        /// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the arc.</param>
        /// <param name="ellipseX">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
        /// <param name="ellipseY">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
        /// <param name="width">Width of the rectangle that defines the ellipse.</param>
        /// <param name="height">Height of the rectangle that defines the ellipse.</param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawArc(Pen pen, int ellipseX, int ellipseY, int width, int height, int startAngle, int sweepAngle);

        /// <summary>Draws a Bézier spline defined by four System.Drawing.Point structures.</summary>
        /// <param name="pen">System.Drawing.Pen structure that determines the color, width, and style of the curve.</param>
        /// <param name="pt1">System.Drawing.Point structure that represents the starting point of the curve.</param>
        /// <param name="pt2">System.Drawing.Point structure that represents the first control point for the curve.</param>
        /// <param name="pt3">System.Drawing.Point structure that represents the second control point for the curve.</param>
        /// <param name="pt4">System.Drawing.Point structure that represents the ending point of the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4);

        /// <summary>Draws a Bézier spline defined by four System.Drawing.PointF structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the curve.</param>
        /// <param name="pt1">System.Drawing.PointF structure that represents the starting point of the curve.</param>
        /// <param name="pt2">System.Drawing.PointF structure that represents the first control point for the curve.</param>
        /// <param name="pt3">System.Drawing.PointF structure that represents the second control point for the curve.</param>
        /// <param name="pt4">System.Drawing.PointF structure that represents the ending point of the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4);

        /// <summary>Draws a Bézier spline defined by four ordered pairs of coordinates that represent points.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the curve.</param>
        /// <param name="x1">The x-coordinate of the starting point of the curve.</param>
        /// <param name="y1">The y-coordinate of the starting point of the curve.</param>
        /// <param name="x2">The x-coordinate of the first control point of the curve.</param>
        /// <param name="y2">The y-coordinate of the first control point of the curve.</param>
        /// <param name="x3">The x-coordinate of the second control point of the curve.</param>
        /// <param name="y3">The y-coordinate of the second control point of the curve.</param>
        /// <param name="x4">The x-coordinate of the ending point of the curve.</param>
        /// <param name="y4">The y-coordinate of the ending point of the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4);

        /// <summary>Draws a series of Bézier splines from an array of System.Drawing.Point structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the curve.</param>
        /// <param name="points">Array of System.Drawing.Point structures that represent the points that determine the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawBeziers(Pen pen, Point[] points);

        /// <summary>Draws a series of Bézier splines from an array of System.Drawing.PointF structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the curve.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that represent the points that determine the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawBeziers(Pen pen, PointF[] points);

        /// <summary>Draws a closed cardinal spline defined by an array of System.Drawing.Point structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.Point structures that define the spline.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawClosedCurve(Pen pen, Point[] points);

        /// <summary>Draws a closed cardinal spline defined by an array of System.Drawing.PointF structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that define the spline.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawClosedCurve(Pen pen, PointF[] points);

        /// <summary>Draws a closed cardinal spline defined by an array of System.Drawing.Point structures using a specified tension.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.Point structures that define the spline.</param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
        /// <param name="fillMode">Member of the System.Drawing.Drawing2D.FillMode enumeration that determines how the curve is filled. This parameter is required but ignored.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillMode);

        /// <summary>Draws a closed cardinal spline defined by an array of System.Drawing.PointF structures using a specified tension.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that define the spline.</param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
        /// <param name="fillMode">Member of the System.Drawing.Drawing2D.FillMode enumeration that determines how the curve is filled. This parameter is required but is ignored.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillMode);

        /// <summary>Draws a cardinal spline through a specified array of System.Drawing.Point structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.Point structures that define the spline.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawCurve(Pen pen, Point[] points);

        /// <summary>Draws a cardinal spline through a specified array of System.Drawing.PointF structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that define the spline.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawCurve(Pen pen, PointF[] points);

        /// <summary>Draws a cardinal spline through a specified array of System.Drawing.Point structures using a specified tension.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.Point structures that define the spline.</param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawCurve(Pen pen, Point[] points, float tension);

        /// <summary>Draws a cardinal spline through a specified array of System.Drawing.PointF structures using a specified tension.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that represent the points that define the curve.</param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawCurve(Pen pen, PointF[] points, float tension);

        /// <summary>Draws a cardinal spline through a specified array of System.Drawing.PointF structures. The drawing begins offset from the beginning of the array.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that define the spline.</param>
        /// <param name="offset">Offset from the first element in the array of the points parameter to the starting point in the curve.</param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments);

        /// <summary>Draws a cardinal spline through a specified array of System.Drawing.Point structures using a specified tension.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.Point structures that define the spline.</param>
        /// <param name="offset">Offset from the first element in the array of the points parameter to the starting point in the curve.</param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension);

        /// <summary>Draws a cardinal spline through a specified array of System.Drawing.PointF structures using a specified tension. The drawing begins offset from the beginning of the array.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and height of the curve.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that define the spline.</param>
        /// <param name="offset">Offset from the first element in the array of the points parameter to the starting point in the curve.</param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension);

        /// <summary>Draws an ellipse specified by a bounding System.Drawing.Rectangle structure.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the ellipse.</param>
        /// <param name="rectangle">System.Drawing.Rectangle structure that defines the boundaries of the ellipse.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawEllipse(Pen pen, Rectangle rectangle);

        /// <summary>Draws an ellipse defined by a bounding System.Drawing.RectangleF.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the ellipse.</param>
        /// <param name="rectangle">System.Drawing.RectangleF structure that defines the boundaries of the ellipse.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawEllipse(Pen pen, RectangleF rectangle);

        /// <summary>Draws an ellipse defined by a bounding rectangle specified by a pair of coordinates, a height, and a width.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the ellipse.</param>
        /// <param name="ellipseX">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
        /// <param name="ellipseY">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawEllipse(Pen pen, float ellipseX, float ellipseY, float width, float height);

        /// <summary>Draws an ellipse defined by a bounding rectangle specified by a pair of coordinates, a height, and a width.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the ellipse.</param>
        /// <param name="ellipseX">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
        /// <param name="ellipseY">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawEllipse(Pen pen, int ellipseX, int ellipseY, int width, int height);

        /// <summary>Draws the image represented by the specified System.Drawing.Icon within the area specified by a System.Drawing.Rectangle structure.</summary>
        /// <param name="icon">System.Drawing.Icon to draw.</param>
        /// <param name="targetRect">System.Drawing.Rectangle structure that specifies the location and size of the resulting image on the display surface. The image contained in the icon parameter is scaled to the dimensions of this rectangular area.</param>
        /// <exception cref="System.ArgumentNullException">icon is null.</exception>
        void DrawIcon(Icon icon, Rectangle targetRect);

        /// <summary>Draws the image represented by the specified System.Drawing.Icon at the specified coordinates.</summary>
        /// <param name="icon">System.Drawing.Icon to draw.</param>
        /// <param name="iconX">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="iconY">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">icon is null.</exception>
        void DrawIcon(Icon icon, int iconX, int iconY);

        /// <summary>Draws the image represented by the specified System.Drawing.Icon without scaling the image.</summary>
        /// <param name="icon">System.Drawing.Icon to draw.</param>
        /// <param name="targetRect">System.Drawing.Rectangle structure that specifies the location and size of the resulting image. The image is not scaled to fit this rectangle, but retains its original size. If the image is larger than the rectangle, it is clipped to fit inside it.</param>
        /// <exception cref="System.ArgumentNullException">icon is null.</exception>
        void DrawIconUnstretched(Icon icon, Rectangle targetRect);

        /// <summary>Draws the specified System.Drawing.Image, using its original physical size, at the specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="point">System.Drawing.Point structure that represents the location of the upper-left corner of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Point point);

        /// <summary>Draws the specified System.Drawing.Image at the specified location and with the specified shape and size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Point[] destinationPoints);

        /// <summary>Draws the specified System.Drawing.Image, using its original physical size, at the specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="point">System.Drawing.PointF structure that represents the upper-left corner of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, PointF point);

        /// <summary>Draws the specified System.Drawing.Image at the specified location and with the specified shape and size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, PointF[] destinationPoints);

        /// <summary>Draws the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="rectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle rectangle);

        /// <summary>Draws the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="rectangle">System.Drawing.RectangleF structure that specifies the location and size of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, RectangleF rectangle);

        /// <summary>Draws the specified System.Drawing.Image, using its original physical size, at the specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, float imageX, float imageY);

        /// <summary>Draws the specified image, using its original physical size, at the location specified by a coordinate pair.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, int imageX, int imageY);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.RectangleF structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit);

        /// <summary>Draws the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="width">Width of the drawn image.</param>
        /// <param name="height">Height of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, float imageX, float imageY, float width, float height);

        /// <summary>Draws a portion of an image at a specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the System.Drawing.Image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, float imageX, float imageY, RectangleF sourceRectangle, GraphicsUnit sourceUnit);

        /// <summary>Draws the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="width">Width of the drawn image.</param>
        /// <param name="height">Height of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, int imageX, int imageY, int width, int height);

        /// <summary>Draws a portion of an image at a specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, int imageX, int imageY, Rectangle sourceRectangle, GraphicsUnit sourceUnit);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <param name="callback">System.Drawing.Graphics.DrawImageAbort delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort) method according to application-determined criteria.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, Graphics.DrawImageAbort callback);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <param name="callback">System.Drawing.Graphics.DrawImageAbort delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort) method according to application-determined criteria.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, Graphics.DrawImageAbort callback);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <param name="callback">System.Drawing.Graphics.DrawImageAbort delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32) method according to application-determined criteria.</param>
        /// <param name="callbackData">Value specifying additional data for the System.Drawing.Graphics.DrawImageAbort delegate to use when checking whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32) method.</param>
        void DrawImage(Image image, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, Graphics.DrawImageAbort callback, int callbackData);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the image object to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used by the sourceRectangle parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <param name="callback">System.Drawing.Graphics.DrawImageAbort delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32) method according to application-determined criteria.</param>
        /// <param name="callbackData">Value specifying additional data for the System.Drawing.Graphics.DrawImageAbort delegate to use when checking whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32) method.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, Graphics.DrawImageAbort callback, int callbackData);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceLeft">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceTop">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceWidth">Width of the portion of the source image to draw.</param>
        /// <param name="sourceHeight">Height of the portion of the source image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used to determine the source rectangle.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceLeft">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceTop">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceWidth">Width of the portion of the source image to draw.</param>
        /// <param name="sourceHeight">Height of the portion of the source image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used to determine the source rectangle.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceLeft">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceTop">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceWidth">Width of the portion of the source image to draw.</param>
        /// <param name="sourceHeight">Height of the portion of the source image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used to determine the source rectangle.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceLeft">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceTop">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceWidth">Width of the portion of the source image to draw.</param>
        /// <param name="sourceHeight">Height of the portion of the source image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used to determine the source rectangle.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceLeft">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceTop">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceWidth">Width of the portion of the source image to draw.</param>
        /// <param name="sourceHeight">Height of the portion of the source image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used to determine the source rectangle.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <param name="callback">System.Drawing.Graphics.DrawImageAbort delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort) method according to application-determined criteria.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, Graphics.DrawImageAbort callback);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceLeft">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceTop">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceWidth">Width of the portion of the source image to draw.</param>
        /// <param name="sourceHeight">Height of the portion of the source image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used to determine the source rectangle.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for image.</param>
        /// <param name="callback">System.Drawing.Graphics.DrawImageAbort delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort) method according to application-determined criteria.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, Graphics.DrawImageAbort callback);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceLeft">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceTop">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceWidth">Width of the portion of the source image to draw.</param>
        /// <param name="sourceHeight">Height of the portion of the source image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used to determine the source rectangle.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <param name="callback">System.Drawing.Graphics.DrawImageAbort delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr) method according to application-determined criteria.</param>
        /// <param name="callbackData">Value specifying additional data for the System.Drawing.Graphics.DrawImageAbort delegate to use when checking whether to stop execution of the DrawImage method.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, float sourceLeft, float sourceTop, float sourceWidth, float sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, Graphics.DrawImageAbort callback, IntPtr callbackData);

        /// <summary>Draws the specified portion of the specified System.Drawing.Image at the specified location and with the specified size.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
        /// <param name="sourceLeft">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceTop">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
        /// <param name="sourceWidth">Width of the portion of the source image to draw.</param>
        /// <param name="sourceHeight">Height of the portion of the source image to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the units of measure used to determine the source rectangle.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies recoloring and gamma information for the image object.</param>
        /// <param name="callback">System.Drawing.Graphics.DrawImageAbort delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr) method according to application-determined criteria.</param>
        /// <param name="callbackData">Value specifying additional data for the System.Drawing.Graphics.DrawImageAbort delegate to use when checking whether to stop execution of the DrawImage method.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImage(Image image, Rectangle destinationRectangle, int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, GraphicsUnit sourceUnit, ImageAttributes imageAttributes, Graphics.DrawImageAbort callback, IntPtr callbackData);

        /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="point">System.Drawing.Point structure that specifies the upper-left corner of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImageUnscaled(Image image, Point point);

        /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="rectangle">System.Drawing.Rectangle that specifies the upper-left corner of the drawn image. The X and Y properties of the rectangle specify the upper-left corner. The Width and Height properties are ignored.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImageUnscaled(Image image, Rectangle rectangle);

        /// <summary>Draws the specified image using its original physical size at the location specified by a coordinate pair.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImageUnscaled(Image image, int x, int y);

        /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
        /// <param name="image">System.Drawing.Image to draw.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="width">Width is not used.</param>
        /// <param name="height">Height is not used.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImageUnscaled(Image image, int x, int y, int width, int height);

        /// <summary>Draws the specified image without scaling and clips it, if necessary, to fit in the specified rectangle.</summary>
        /// <param name="image">The System.Drawing.Image to draw.</param>
        /// <param name="rectangle">The System.Drawing.Rectangle in which to draw the image.</param>
        /// <exception cref="System.ArgumentNullException">image is null.</exception>
        void DrawImageUnscaledAndClipped(Image image, Rectangle rectangle);

        /// <summary>Draws a line connecting two System.Drawing.Point structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the line.</param>
        /// <param name="pt1">System.Drawing.Point structure that represents the first point to connect.</param>
        /// <param name="pt2">System.Drawing.Point structure that represents the second point to connect.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawLine(Pen pen, Point pt1, Point pt2);

        /// <summary>Draws a line connecting two System.Drawing.PointF structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the line.</param>
        /// <param name="pt1">System.Drawing.PointF structure that represents the first point to connect.</param>
        /// <param name="pt2">System.Drawing.PointF structure that represents the second point to connect.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawLine(Pen pen, PointF pt1, PointF pt2);

        /// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the line.</param>
        /// <param name="startX">The x-coordinate of the first point.</param>
        /// <param name="startY">The y-coordinate of the first point.</param>
        /// <param name="endX">The x-coordinate of the second point.</param>
        /// <param name="endY">The y-coordinate of the second point.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawLine(Pen pen, float startX, float startY, float endX, float endY);

        /// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the line.</param>
        /// <param name="startX">The x-coordinate of the first point.</param>
        /// <param name="startY">The y-coordinate of the first point.</param>
        /// <param name="endX">The x-coordinate of the second point.</param>
        /// <param name="endY">The y-coordinate of the second point.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawLine(Pen pen, int startX, int startY, int endX, int endY);

        /// <summary>Draws a series of line segments that connect an array of System.Drawing.Point structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the line segments.</param>
        /// <param name="points">Array of System.Drawing.Point structures that represent the points to connect.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawLines(Pen pen, Point[] points);

        /// <summary>Draws a series of line segments that connect an array of System.Drawing.PointF structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the line segments.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that represent the points to connect.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawLines(Pen pen, PointF[] points);

        /// <summary>Draws a System.Drawing.Drawing2D.GraphicsPath.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the path.</param>
        /// <param name="path">System.Drawing.Drawing2D.GraphicsPath to draw.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- path is null.</exception>
        void DrawPath(Pen pen, GraphicsPath path);

        /// <summary>Draws a pie shape defined by an ellipse specified by a System.Drawing.Rectangle structure and two radial lines.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the pie shape.</param>
        /// <param name="rectangle">System.Drawing.Rectangle structure that represents the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
        /// <param name="sweepAngle">Angle measured in degrees clockwise from the startAngle parameter to the second side of the pie shape.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawPie(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle);

        /// <summary>Draws a pie shape defined by an ellipse specified by a System.Drawing.RectangleF structure and two radial lines.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the pie shape.</param>
        /// <param name="rectangle">System.Drawing.RectangleF structure that represents the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
        /// <param name="sweepAngle">Angle measured in degrees clockwise from the startAngle parameter to the second side of the pie shape.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawPie(Pen pen, RectangleF rectangle, float startAngle, float sweepAngle);

        /// <summary>Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial lines.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the pie shape.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
        /// <param name="sweepAngle">Angle measured in degrees clockwise from the startAngle parameter to the second side of the pie shape.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawPie(Pen pen, float imageX, float imageY, float width, float height, float startAngle, float sweepAngle);

        /// <summary>Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial lines.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the pie shape.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
        /// <param name="sweepAngle">Angle measured in degrees clockwise from the startAngle parameter to the second side of the pie shape.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawPie(Pen pen, int imageX, int imageY, int width, int height, int startAngle, int sweepAngle);

        /// <summary>Draws a polygon defined by an array of System.Drawing.Point structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the polygon.</param>
        /// <param name="points">Array of System.Drawing.Point structures that represent the vertices of the polygon.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawPolygon(Pen pen, Point[] points);

        /// <summary>Draws a polygon defined by an array of System.Drawing.PointF structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the polygon.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that represent the vertices of the polygon.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- points is null.</exception>
        void DrawPolygon(Pen pen, PointF[] points);

        /// <summary>Draws a rectangle specified by a System.Drawing.Rectangle structure.</summary>
        /// <param name="pen">A System.Drawing.Pen that determines the color, width, and style of the rectangle.</param>
        /// <param name="rectangle">A System.Drawing.Rectangle structure that represents the rectangle to draw.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawRectangle(Pen pen, Rectangle rectangle);

        /// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
        /// <param name="pen">A System.Drawing.Pen that determines the color, width, and style of the rectangle.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
        /// <param name="width">The width of the rectangle to draw.</param>
        /// <param name="height">The height of the rectangle to draw.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawRectangle(Pen pen, float imageX, float imageY, float width, float height);

        /// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the rectangle.</param>
        /// <param name="imageX">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
        /// <param name="imageY">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
        /// <param name="width">Width of the rectangle to draw.</param>
        /// <param name="height">Height of the rectangle to draw.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.</exception>
        void DrawRectangle(Pen pen, int imageX, int imageY, int width, int height);

        /// <summary>Draws a series of rectangles specified by System.Drawing.Rectangle structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the outlines of the rectangles.</param>
        /// <param name="rects">Array of System.Drawing.Rectangle structures that represent the rectangles to draw.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- rects is null.</exception>
        void DrawRectangles(Pen pen, Rectangle[] rects);

        /// <summary>Draws a series of rectangles specified by System.Drawing.RectangleF structures.</summary>
        /// <param name="pen">System.Drawing.Pen that determines the color, width, and style of the outlines of the rectangles.</param>
        /// <param name="rects">Array of System.Drawing.RectangleF structures that represent the rectangles to draw.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- rects is null.</exception>
        void DrawRectangles(Pen pen, RectangleF[] rects);

        /// <summary>Draws the specified text string at the specified location with the specified System.Drawing.Brush and System.Drawing.Font objects.</summary>
        /// <param name="s">String to draw.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="brush">System.Drawing.Brush that determines the color and texture of the drawn text.</param>
        /// <param name="point">System.Drawing.PointF structure that specifies the upper-left corner of the drawn text.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- s is null.</exception>
        void DrawString(string s, Font font, Brush brush, PointF point);

        /// <summary>Draws the specified text string in the specified rectangle with the specified System.Drawing.Brush and System.Drawing.Font objects.</summary>
        /// <param name="s">String to draw.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="brush">System.Drawing.Brush that determines the color and texture of the drawn text.</param>
        /// <param name="layoutRectangle">System.Drawing.RectangleF structure that specifies the location of the drawn text.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- s is null.</exception>
        void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle);

        /// <summary>Draws the specified text string at the specified location with the specified System.Drawing.Brush and System.Drawing.Font objects.</summary>
        /// <param name="s">String to draw.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="brush">System.Drawing.Brush that determines the color and texture of the drawn text.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn text.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn text.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- s is null.</exception>
        void DrawString(string s, Font font, Brush brush, float x, float y);

        /// <summary>Draws the specified text string at the specified location with the specified System.Drawing.Brush and System.Drawing.Font objects using the formatting attributes of the specified System.Drawing.StringFormat.</summary>
        /// <param name="s">String to draw.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="brush">System.Drawing.Brush that determines the color and texture of the drawn text.</param>
        /// <param name="point">System.Drawing.PointF structure that specifies the upper-left corner of the drawn text.</param>
        /// <param name="format">System.Drawing.StringFormat that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- s is null.</exception>
        void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format);

        /// <summary>Draws the specified text string in the specified rectangle with the specified System.Drawing.Brush and System.Drawing.Font objects using the formatting attributes of the specified System.Drawing.StringFormat.</summary>
        /// <param name="s">String to draw.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="brush">System.Drawing.Brush that determines the color and texture of the drawn text.</param>
        /// <param name="layoutRectangle">System.Drawing.RectangleF structure that specifies the location of the drawn text.</param>
        /// <param name="format">System.Drawing.StringFormat that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- s is null.</exception>
        void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format);

        /// <summary>Draws the specified text string at the specified location with the specified System.Drawing.Brush and System.Drawing.Font objects using the formatting attributes of the specified System.Drawing.StringFormat.</summary>
        /// <param name="s">String to draw.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="brush">System.Drawing.Brush that determines the color and texture of the drawn text.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn text.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn text.</param>
        /// <param name="format">System.Drawing.StringFormat that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- s is null.</exception>
        void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format);

        /// <summary>Closes the current graphics container and restores the state of this System.Drawing.Graphics to the state saved by a call to the System.Drawing.Graphics.BeginContainer() method.</summary>
        /// <param name="container">System.Drawing.Drawing2D.GraphicsContainer that represents the container this method restores.</param>
        void EndContainer(GraphicsContainer container);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.Point structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, Point destinationPoint, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.PointF structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, PointF destinationPoint, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records of the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records of the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.RectangleF structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.Point structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, Point destinationPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.PointF structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, PointF destinationPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records of the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records of the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.RectangleF structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.Point structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, Point destinationPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.Point structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.PointF structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, PointF destinationPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.PointF structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records in the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structures that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records of the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records of a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records of the specified System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.RectangleF structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records of a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.RectangleF structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.Point structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.PointF structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records of a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records of a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.RectangleF structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="sourceUnit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit sourceUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.Point structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="unit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, Point destinationPoint, Rectangle sourceRectangle, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.Point structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="unit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, Point[] destinationPoints, Rectangle sourceRectangle, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoint">System.Drawing.PointF structure that specifies the location of the upper-left corner of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="unit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, PointF destinationPoint, RectangleF sourceRectangle, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records in a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationPoints">Array of three System.Drawing.PointF structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="unit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, PointF[] destinationPoints, RectangleF sourceRectangle, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records of a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.Rectangle structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.Rectangle structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="unit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, Rectangle destinationRectangle, Rectangle sourceRectangle, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Sends the records of a selected rectangle from a System.Drawing.Imaging.Metafile, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
        /// <param name="metafile">System.Drawing.Imaging.Metafile to enumerate.</param>
        /// <param name="destinationRectangle">System.Drawing.RectangleF structure that specifies the location and size of the drawn metafile.</param>
        /// <param name="sourceRectangle">System.Drawing.RectangleF structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
        /// <param name="unit">Member of the System.Drawing.GraphicsUnit enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the sourceRectangle parameter contains.</param>
        /// <param name="callback">System.Drawing.Graphics.EnumerateMetafileProc delegate that specifies the method to which the metafile records are sent.</param>
        /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass System.IntPtr.Zero for this parameter.</param>
        /// <param name="imageAttributes">System.Drawing.Imaging.ImageAttributes that specifies image attribute information for the drawn image.</param>
        void EnumerateMetafile(Metafile metafile, RectangleF destinationRectangle, RectangleF sourceRectangle, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttributes);

        /// <summary>Updates the clip region of this System.Drawing.Graphics to exclude the area specified by a System.Drawing.Rectangle structure.</summary>
        /// <param name="rectangle">System.Drawing.Rectangle structure that specifies the rectangle to exclude from the clip region.</param>
        void ExcludeClip(Rectangle rectangle);

        /// <summary>Updates the clip region of this System.Drawing.Graphics to exclude the area specified by a System.Drawing.Region.</summary>
        /// <param name="region">System.Drawing.Region that specifies the region to exclude from the clip region.</param>
        void ExcludeClip(Region region);

        /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of System.Drawing.Point structures.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.Point structures that define the spline.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillClosedCurve(Brush brush, Point[] points);

        /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of System.Drawing.PointF structures.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that define the spline.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillClosedCurve(Brush brush, PointF[] points);

        /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of System.Drawing.Point structures using the specified fill mode.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.Point structures that define the spline.</param>
        /// <param name="fillMode">Member of the System.Drawing.Drawing2D.FillMode enumeration that determines how the curve is filled.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillClosedCurve(Brush brush, Point[] points, FillMode fillMode);

        /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of System.Drawing.PointF structures using the specified fill mode.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that define the spline.</param>
        /// <param name="fillMode">Member of the System.Drawing.Drawing2D.FillMode enumeration that determines how the curve is filled.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillClosedCurve(Brush brush, PointF[] points, FillMode fillMode);

        /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of System.Drawing.Point structures using the specified fill mode and tension.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.Point structures that define the spline.</param>
        /// <param name="fillMode">Member of the System.Drawing.Drawing2D.FillMode enumeration that determines how the curve is filled.</param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillClosedCurve(Brush brush, Point[] points, FillMode fillMode, float tension);

        /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of System.Drawing.PointF structures using the specified fill mode and tension.</summary>
        /// <param name="brush">A System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that define the spline.</param>
        /// <param name="fillMode">Member of the System.Drawing.Drawing2D.FillMode enumeration that determines how the curve is filled.</param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillClosedCurve(Brush brush, PointF[] points, FillMode fillMode, float tension);

        /// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a System.Drawing.Rectangle structure.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="rectangle">System.Drawing.Rectangle structure that represents the bounding rectangle that defines the ellipse.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillEllipse(Brush brush, Rectangle rectangle);

        /// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a System.Drawing.RectangleF structure.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="rectangle">System.Drawing.RectangleF structure that represents the bounding rectangle that defines the ellipse.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillEllipse(Brush brush, RectangleF rectangle);

        /// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillEllipse(Brush brush, float x, float y, float width, float height);

        /// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillEllipse(Brush brush, int x, int y, int width, int height);

        /// <summary>Fills the interior of a System.Drawing.Drawing2D.GraphicsPath.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="path">System.Drawing.Drawing2D.GraphicsPath that represents the path to fill.</param>
        /// <exception cref="System.ArgumentNullException">pen is null.  -or- path is null.</exception>
        void FillPath(Brush brush, GraphicsPath path);

        /// <summary>Fills the interior of a pie section defined by an ellipse specified by a System.Drawing.RectangleF structure and two radial lines.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="rectangle">System.Drawing.Rectangle structure that represents the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to the second side of the pie section.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillPie(Brush brush, Rectangle rectangle, float startAngle, float sweepAngle);

        /// <summary>Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a height, and two radial lines.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to the second side of the pie section.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle);

        /// <summary>Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a height, and two radial lines.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to the second side of the pie section.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle);

        /// <summary>Fills the interior of a polygon defined by an array of points specified by System.Drawing.Point structures.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.Point structures that represent the vertices of the polygon to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillPolygon(Brush brush, Point[] points);

        /// <summary>Fills the interior of a polygon defined by an array of points specified by System.Drawing.PointF structures.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that represent the vertices of the polygon to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillPolygon(Brush brush, PointF[] points);

        /// <summary>Fills the interior of a polygon defined by an array of points specified by System.Drawing.Point structures using the specified fill mode.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.Point structures that represent the vertices of the polygon to fill.</param>
        /// <param name="fillMode">Member of the System.Drawing.Drawing2D.FillMode enumeration that determines the style of the fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillPolygon(Brush brush, Point[] points, FillMode fillMode);

        /// <summary>Fills the interior of a polygon defined by an array of points specified by System.Drawing.PointF structures using the specified fill mode.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="points">Array of System.Drawing.PointF structures that represent the vertices of the polygon to fill.</param>
        /// <param name="fillMode">Member of the System.Drawing.Drawing2D.FillMode enumeration that determines the style of the fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- points is null.</exception>
        void FillPolygon(Brush brush, PointF[] points, FillMode fillMode);

        /// <summary>Fills the interior of a rectangle specified by a System.Drawing.Rectangle structure.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="rectangle">System.Drawing.Rectangle structure that represents the rectangle to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillRectangle(Brush brush, Rectangle rectangle);

        /// <summary>Fills the interior of a rectangle specified by a System.Drawing.RectangleF structure.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="rectangle">System.Drawing.RectangleF structure that represents the rectangle to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillRectangle(Brush brush, RectangleF rectangle);

        /// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
        /// <param name="width">Width of the rectangle to fill.</param>
        /// <param name="height">Height of the rectangle to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillRectangle(Brush brush, float x, float y, float width, float height);

        /// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
        /// <param name="width">Width of the rectangle to fill.</param>
        /// <param name="height">Height of the rectangle to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillRectangle(Brush brush, int x, int y, int width, int height);

        /// <summary>Fills the interiors of a series of rectangles specified by System.Drawing.Rectangle structures.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="rects">Array of System.Drawing.Rectangle structures that represent the rectangles to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillRectangles(Brush brush, Rectangle[] rects);

        /// <summary>Fills the interiors of a series of rectangles specified by System.Drawing.RectangleF structures.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="rects">Array of System.Drawing.RectangleF structures that represent the rectangles to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.</exception>
        void FillRectangles(Brush brush, RectangleF[] rects);

        /// <summary>Fills the interior of a System.Drawing.Region.</summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="region">System.Drawing.Region that represents the area to fill.</param>
        /// <exception cref="System.ArgumentNullException">brush is null.  -or- region is null.</exception>
        void FillRegion(Brush brush, Region region);

        /// <summary>Forces execution of all pending graphics operations and returns immediately without waiting for the operations to finish.</summary>
        void Flush();

        /// <summary>Forces execution of all pending graphics operations with the method waiting or not waiting, as specified, to return before the operations finish.</summary>
        /// <param name="intention">Member of the System.Drawing.Drawing2D.FlushIntention enumeration that specifies whether the method returns immediately or waits for any existing operations to finish.</param>
        void Flush(FlushIntention intention);

        /// <summary>Gets the handle to the device context associated with this System.Drawing.Graphics.</summary>
        /// <returns>Handle to the device context associated with this System.Drawing.Graphics.</returns>
        IntPtr GetHdc();

        /// <summary>Gets the nearest color to the specified System.Drawing.Color structure.</summary>
        /// <param name="color">System.Drawing.Color structure for which to find a match.</param>
        /// <returns>A System.Drawing.Color structure that represents the nearest color to the one specified with the color parameter.</returns>
        Color GetNearestColor(Color color);

        /// <summary>Updates the clip region of this System.Drawing.Graphics to the intersection of the current clip region and the specified System.Drawing.Rectangle structure.</summary>
        /// <param name="rectangle">System.Drawing.Rectangle structure to intersect with the current clip region.</param>
        void IntersectClip(Rectangle rectangle);

        /// <summary>Updates the clip region of this System.Drawing.Graphics to the intersection of the current clip region and the specified System.Drawing.RectangleF structure.</summary>
        /// <param name="rectangle">System.Drawing.RectangleF structure to intersect with the current clip region.</param>
        void IntersectClip(RectangleF rectangle);

        /// <summary>Updates the clip region of this System.Drawing.Graphics to the intersection of the current clip region and the specified System.Drawing.Region.</summary>
        /// <param name="region">System.Drawing.Region to intersect with the current region.</param>
        void IntersectClip(Region region);

        /// <summary>Indicates whether the specified System.Drawing.Point structure is contained within the visible clip region of this System.Drawing.Graphics.</summary>
        /// <param name="point">System.Drawing.Point structure to test for visibility.</param>
        /// <returns>true if the point specified by the point parameter is contained within the visible clip region of this System.Drawing.Graphics; otherwise, false.</returns>
        bool IsVisible(Point point);

        /// <summary>Indicates whether the specified System.Drawing.PointF structure is contained within the visible clip region of this System.Drawing.Graphics.</summary>
        /// <param name="point">System.Drawing.PointF structure to test for visibility.</param>
        /// <returns>true if the point specified by the point parameter is contained within the visible clip region of this System.Drawing.Graphics; otherwise, false.</returns>
        bool IsVisible(PointF point);

        /// <summary>Indicates whether the rectangle specified by a System.Drawing.Rectangle structure is contained within the visible clip region of this System.Drawing.Graphics.</summary>
        /// <param name="rectangle">System.Drawing.Rectangle structure to test for visibility.</param>
        /// <returns>true if the rectangle specified by the rectangle parameter is contained within the visible clip region of this System.Drawing.Graphics; otherwise, false.</returns>
        bool IsVisible(Rectangle rectangle);

        /// <summary>Indicates whether the rectangle specified by a System.Drawing.RectangleF structure is contained within the visible clip region of this System.Drawing.Graphics.</summary>
        /// <param name="rectangle">System.Drawing.RectangleF structure to test for visibility.</param>
        /// <returns>true if the rectangle specified by the rectangle parameter is contained within the visible clip region of this System.Drawing.Graphics; otherwise, false.</returns>
        bool IsVisible(RectangleF rectangle);

        /// <summary>Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this System.Drawing.Graphics.</summary>
        /// <param name="x">The x-coordinate of the point to test for visibility.</param>
        /// <param name="y">The y-coordinate of the point to test for visibility.</param>
        /// <returns>true if the point defined by the x and y parameters is contained within the visible clip region of this System.Drawing.Graphics; otherwise, false.</returns>
        bool IsVisible(float x, float y);

        /// <summary>Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this System.Drawing.Graphics.</summary>
        /// <param name="x">The x-coordinate of the point to test for visibility.</param>
        /// <param name="y">The y-coordinate of the point to test for visibility.</param>
        /// <returns>true if the point defined by the x and y parameters is contained within the visible clip region of this System.Drawing.Graphics; otherwise, false.</returns>
        bool IsVisible(int x, int y);

        /// <summary>Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the visible clip region of this System.Drawing.Graphics.</summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
        /// <param name="width">Width of the rectangle to test for visibility.</param>
        /// <param name="height">Height of the rectangle to test for visibility.</param>
        /// <returns>true if the rectangle defined by the x, y, width, and height parameters is contained within the visible clip region of this System.Drawing.Graphics; otherwise, false.</returns>
        bool IsVisible(float x, float y, float width, float height);

        /// <summary>Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the visible clip region of this System.Drawing.Graphics.</summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
        /// <param name="width">Width of the rectangle to test for visibility.</param>
        /// <param name="height">Height of the rectangle to test for visibility.</param>
        /// <returns>true if the rectangle defined by the x, y, width, and height parameters is contained within the visible clip region of this System.Drawing.Graphics; otherwise, false.</returns>
        bool IsVisible(int x, int y, int width, int height);

        /// <summary>Gets an array of System.Drawing.Region objects, each of which bounds a range of character positions within the specified string.</summary>
        /// <param name="text">String to measure.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="layoutRect">System.Drawing.RectangleF structure that specifies the layout rectangle for the string.</param>
        /// <param name="stringFormat">System.Drawing.StringFormat that represents formatting information, such as line spacing, for the string.</param>
        /// <returns>This method returns an array of System.Drawing.Region objects, each of which bounds a range of character positions within the specified string.</returns>
        Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat);

        /// <summary>Measures the specified string when drawn with the specified System.Drawing.Font.</summary>
        /// <param name="text">String to measure.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the size, in the units specified by the System.Drawing.Graphics.PageUnit property, of the string specified by the text parameter as drawn with the font parameter.</returns>
        SizeF MeasureString(string text, Font font);

        /// <summary>Measures the specified string when drawn with the specified System.Drawing.Font.</summary>
        /// <param name="text">String to measure.</param>
        /// <param name="font">System.Drawing.Font that defines the format of the string.</param>
        /// <param name="width">Maximum width of the string in pixels.</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the size, in the units specified by the System.Drawing.Graphics.PageUnit property, of the string specified in the text parameter as drawn with the font parameter.</returns>
        SizeF MeasureString(string text, Font font, int width);

        /// <summary>Measures the specified string when drawn with the specified System.Drawing.Font within the specified layout area.</summary>
        /// <param name="text">String to measure.</param>
        /// <param name="font">System.Drawing.Font defines the text format of the string.</param>
        /// <param name="layoutArea">System.Drawing.SizeF structure that specifies the maximum layout area for the text.</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the size, in the units specified by the System.Drawing.Graphics.PageUnit property, of the string specified by the text parameter as drawn with the font parameter.</returns>
        SizeF MeasureString(string text, Font font, SizeF layoutArea);

        /// <summary>Measures the specified string when drawn with the specified System.Drawing.Font and formatted with the specified System.Drawing.StringFormat.</summary>
        /// <param name="text">String to measure.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="width">Maximum width of the string.</param>
        /// <param name="format">System.Drawing.StringFormat that represents formatting information, such as line spacing, for the string.</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the size, in the units specified by the System.Drawing.Graphics.PageUnit property, of the string specified in the text parameter as drawn with the font parameter and the stringFormat parameter.</returns>
        SizeF MeasureString(string text, Font font, int width, StringFormat format);

        /// <summary>Measures the specified string when drawn with the specified System.Drawing.Font and formatted with the specified System.Drawing.StringFormat.</summary>
        /// <param name="text">String to measure.</param>
        /// <param name="font">System.Drawing.Font defines the text format of the string.</param>
        /// <param name="origin">System.Drawing.PointF structure that represents the upper-left corner of the string.</param>
        /// <param name="stringFormat">System.Drawing.StringFormat that represents formatting information, such as line spacing, for the string.</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the size, in the units specified by the System.Drawing.Graphics.PageUnit property, of the string specified by the text parameter as drawn with the font parameter and the stringFormat parameter.</returns>
        SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat);

        /// <summary>Measures the specified string when drawn with the specified System.Drawing.Font and formatted with the specified System.Drawing.StringFormat.</summary>
        /// <param name="text">String to measure.</param>
        /// <param name="font">System.Drawing.Font defines the text format of the string.</param>
        /// <param name="layoutArea">System.Drawing.SizeF structure that specifies the maximum layout area for the text.</param>
        /// <param name="stringFormat">System.Drawing.StringFormat that represents formatting information, such as line spacing, for the string.</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the size, in the units specified by the System.Drawing.Graphics.PageUnit property, of the string specified in the text parameter as drawn with the font parameter and the stringFormat parameter.</returns>
        SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat);

        /// <summary>Measures the specified string when drawn with the specified System.Drawing.Font and formatted with the specified System.Drawing.StringFormat.</summary>
        /// <param name="text">String to measure.</param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <param name="layoutArea">System.Drawing.SizeF structure that specifies the maximum layout area for the text.</param>
        /// <param name="stringFormat">System.Drawing.StringFormat that represents formatting information, such as line spacing, for the string.</param>
        /// <param name="charactersFitted">Number of characters in the string.</param>
        /// <param name="linesFilled">Number of text lines in the string.</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the size of the string, in the units specified by the System.Drawing.Graphics.PageUnit property, of the text parameter as drawn with the font parameter and the stringFormat parameter.</returns>
        SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled);

        /// <summary>Multiplies the world transformation of this System.Drawing.Graphics and specified the System.Drawing.Drawing2D.Matrix.</summary>
        /// <param name="matrix">4x4 System.Drawing.Drawing2D.Matrix that multiplies the world transformation.</param>
        void MultiplyTransform(Matrix matrix);

        /// <summary>Multiplies the world transformation of this System.Drawing.Graphics and specified the System.Drawing.Drawing2D.Matrix in the specified order.</summary>
        /// <param name="matrix">4x4 System.Drawing.Drawing2D.Matrix that multiplies the world transformation.</param>
        /// <param name="order">Member of the System.Drawing.Drawing2D.MatrixOrder enumeration that determines the order of the multiplication.</param>
        void MultiplyTransform(Matrix matrix, MatrixOrder order);

        /// <summary>Releases a device context handle obtained by a previous call to the System.Drawing.Graphics.GetHdc() method of this System.Drawing.Graphics.</summary>
        void ReleaseHdc();

        /// <summary>Releases a device context handle obtained by a previous call to the System.Drawing.Graphics.GetHdc() method of this System.Drawing.Graphics.</summary>
        /// <param name="hdc">Handle to a device context obtained by a previous call to the System.Drawing.Graphics.GetHdc() method of this System.Drawing.Graphics.</param>
        void ReleaseHdc(IntPtr hdc);

        /// <summary>Releases a handle to a device context.</summary>
        /// <param name="hdc">Handle to a device context.</param>
        void ReleaseHdcInternal(IntPtr hdc);

        /// <summary>Resets the clip region of this System.Drawing.Graphics to an infinite region.</summary>
        void ResetClip();

        /// <summary>Resets the world transformation matrix of this System.Drawing.Graphics to the identity matrix.</summary>
        void ResetTransform();

        /// <summary>Restores the state of this System.Drawing.Graphics to the state represented by a System.Drawing.Drawing2D.GraphicsState.</summary>
        /// <param name="graphicsState">System.Drawing.Drawing2D.GraphicsState that represents the state to which to restore this System.Drawing.Graphics.</param>
        void Restore(GraphicsState graphicsState);

        /// <summary>Applies the specified rotation to the transformation matrix of this System.Drawing.Graphics.</summary>
        /// <param name="angle">Angle of rotation in degrees.</param>
        void RotateTransform(float angle);

        /// <summary>Applies the specified rotation to the transformation matrix of this System.Drawing.Graphics in the specified order.</summary>
        /// <param name="angle">Angle of rotation in degrees.</param>
        /// <param name="order">Member of the System.Drawing.Drawing2D.MatrixOrder enumeration that specifies whether the rotation is appended or prepended to the matrix transformation.</param>
        void RotateTransform(float angle, MatrixOrder order);

        /// <summary>Saves the current state of this System.Drawing.Graphics and identifies the saved state with a System.Drawing.Drawing2D.GraphicsState.</summary>
        /// <returns>This method returns a System.Drawing.Drawing2D.GraphicsState that represents the saved state of this System.Drawing.Graphics.</returns>
        GraphicsState Save();

        /// <summary>Applies the specified scaling operation to the transformation matrix of this System.Drawing.Graphics by prepending it to the object's transformation matrix.</summary>
        /// <param name="sx">Scale factor in the x direction.</param>
        /// <param name="sy">Scale factor in the y direction.</param>
        void ScaleTransform(float sx, float sy);

        /// <summary>Applies the specified scaling operation to the transformation matrix of this System.Drawing.Graphics in the specified order.</summary>
        /// <param name="sx">Scale factor in the x direction.</param>
        /// <param name="sy">Scale factor in the y direction.</param>
        /// <param name="order">Member of the System.Drawing.Drawing2D.MatrixOrder enumeration that specifies whether the scaling operation is prepended or appended to the transformation matrix.</param>
        void ScaleTransform(float sx, float sy, MatrixOrder order);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the Clip property of the specified System.Drawing.Graphics.</summary>
        /// <param name="g">System.Drawing.Graphics from which to take the new clip region.</param>
        void SetClip(Graphics g);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the specified System.Drawing.Drawing2D.GraphicsPath.</summary>
        /// <param name="path">System.Drawing.Drawing2D.GraphicsPath that represents the new clip region.</param>
        void SetClip(GraphicsPath path);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the rectangle specified by a System.Drawing.Rectangle structure.</summary>
        /// <param name="rectangle">System.Drawing.Rectangle structure that represents the new clip region.</param>
        void SetClip(Rectangle rectangle);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the rectangle specified by a System.Drawing.RectangleF structure.</summary>
        /// <param name="rectangle">System.Drawing.RectangleF structure that represents the new clip region.</param>
        void SetClip(RectangleF rectangle);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the result of the specified combining operation of the current clip region and the System.Drawing.Graphics.Clip property of the specified System.Drawing.Graphics.</summary>
        /// <param name="g">System.Drawing.Graphics that specifies the clip region to combine.</param>
        /// <param name="combineMode">Member of the System.Drawing.Drawing2D.CombineMode enumeration that specifies the combining operation to use.</param>
        void SetClip(Graphics g, CombineMode combineMode);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the result of the specified operation combining the current clip region and the specified System.Drawing.Drawing2D.GraphicsPath.</summary>
        /// <param name="path">System.Drawing.Drawing2D.GraphicsPath to combine.</param>
        /// <param name="combineMode">Member of the System.Drawing.Drawing2D.CombineMode enumeration that specifies the combining operation to use.</param>
        void SetClip(GraphicsPath path, CombineMode combineMode);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the result of the specified operation combining the current clip region and the rectangle specified by a System.Drawing.Rectangle structure.</summary>
        /// <param name="rectangle">System.Drawing.Rectangle structure to combine.</param>
        /// <param name="combineMode">Member of the System.Drawing.Drawing2D.CombineMode enumeration that specifies the combining operation to use.</param>
        void SetClip(Rectangle rectangle, CombineMode combineMode);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the result of the specified operation combining the current clip region and the rectangle specified by a System.Drawing.RectangleF structure.</summary>
        /// <param name="rectangle">System.Drawing.RectangleF structure to combine.</param>
        /// <param name="combineMode">Member of the System.Drawing.Drawing2D.CombineMode enumeration that specifies the combining operation to use.</param>
        void SetClip(RectangleF rectangle, CombineMode combineMode);

        /// <summary>Sets the clipping region of this System.Drawing.Graphics to the result of the specified operation combining the current clip region and the specified System.Drawing.Region.</summary>
        /// <param name="region">System.Drawing.Region to combine.</param>
        /// <param name="combineMode">Member from the System.Drawing.Drawing2D.CombineMode enumeration that specifies the combining operation to use.</param>
        void SetClip(Region region, CombineMode combineMode);

        /// <summary>Transforms an array of points from one coordinate space to another using the current world and page transformations of this System.Drawing.Graphics.</summary>
        /// <param name="destinationSpace">Member of the System.Drawing.Drawing2D.CoordinateSpace enumeration that specifies the destination coordinate space.</param>
        /// <param name="sourceSpace">Member of the System.Drawing.Drawing2D.CoordinateSpace enumeration that specifies the source coordinate space.</param>
        /// <param name="pts">Array of System.Drawing.Point structures that represents the points to transformation.</param>
        void TransformPoints(CoordinateSpace destinationSpace, CoordinateSpace sourceSpace, Point[] pts);

        /// <summary>Transforms an array of points from one coordinate space to another using the current world and page transformations of this System.Drawing.Graphics.</summary>
        /// <param name="destinationSpace">Member of the System.Drawing.Drawing2D.CoordinateSpace enumeration that specifies the destination coordinate space.</param>
        /// <param name="sourceSpace">Member of the System.Drawing.Drawing2D.CoordinateSpace enumeration that specifies the source coordinate space.</param>
        /// <param name="pts">Array of System.Drawing.PointF structures that represent the points to transform.</param>
        void TransformPoints(CoordinateSpace destinationSpace, CoordinateSpace sourceSpace, PointF[] pts);

        /// <summary>Translates the clipping region of this System.Drawing.Graphics by specified amounts in the horizontal and vertical directions.</summary>
        /// <param name="dx">The x-coordinate of the translation.</param>
        /// <param name="dy">The y-coordinate of the translation.</param>
        void TranslateClip(float dx, float dy);

        /// <summary>Translates the clipping region of this System.Drawing.Graphics by specified amounts in the horizontal and vertical directions.</summary>
        /// <param name="dx">The x-coordinate of the translation.</param>
        /// <param name="dy">The y-coordinate of the translation.</param>
        void TranslateClip(int dx, int dy);

        /// <summary>Changes the origin of the coordinate system by prepending the specified translation to the transformation matrix of this System.Drawing.Graphics.</summary>
        /// <param name="dx">The x-coordinate of the translation.</param>
        /// <param name="dy">The y-coordinate of the translation.</param>
        void TranslateTransform(float dx, float dy);

        /// <summary>Changes the origin of the coordinate system by applying the specified translation to the transformation matrix of this System.Drawing.Graphics in the specified order.</summary>
        /// <param name="dx">The x-coordinate of the translation.</param>
        /// <param name="dy">The y-coordinate of the translation.</param>
        /// <param name="order">Member of the System.Drawing.Drawing2D.MatrixOrder enumeration that specifies whether the translation is prepended or appended to the transformation matrix.</param>
        void TranslateTransform(float dx, float dy, MatrixOrder order);
    }
}
