#region License

/*
 
Copyright (c) 2012 Danko Kozar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 
*/

#endregion License

using System;
using UnityEngine;

namespace eDriven.Core.Geom
{
    /// <summary>
    /// The class representing a point in 2-D space
    /// </summary>
    /// <remarks>Coded by Danko Kozar</remarks>
    public class Point : ICloneable
    {
        #region Properties

        /// <summary>
        /// Zero point
        /// </summary>
        public static Point Zero = new Point();

        /// <summary>
        /// X coordinate
        /// </summary>
        public float X;

        /// <summary>
        /// Y coordinate
        /// </summary>
        public float Y;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Point()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds two points
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Point Add(Point p)
        {
            return new Point(X + p.X, Y + p.Y);
        }

        /// <summary>
        /// Subtracts two points
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Point Subtract(Point p)
        {
            return new Point(X - p.X, Y - p.Y);
        }

        /// <summary>
        /// Converts Vector2 to Point
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Point FromVector2(Vector2 v)
        {
            return new Point(v.x, v.y);
        }

        /// <summary>
        /// Converts Point to Vector2
        /// </summary>
        /// <returns></returns>
        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public override string ToString()
        {
            return string.Format("Point[{0}, {1}]", X, Y);
        }

        #endregion

        #region Equals

        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other)) return false;
            //if (ReferenceEquals(this, other)) return true;
            return other.X == X && other.Y == Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            //if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Point)) return false;
            return Equals((Point) obj);
        }

        // override object.Equals
        //public override bool Equals(object obj)
        //{
        //    if (obj == null || GetType() != obj.GetType())
        //    {
        //        return false;
        //    }

        //    Point p = (Point)obj;

        //    return p.X == X && p.Y == Y;

        //}

        // override object.GetHashCode
        //public override int GetHashCode()
        //{
        //    // TODO: write your implementation of GetHashCode() here
        //    return base.GetHashCode();
        //}

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return new Point(X, Y);
        }

        #endregion
    }
}