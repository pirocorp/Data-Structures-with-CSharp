﻿using System;

public class Point2D : IComparable<Point2D>
{
    public double X { get; }

    public double Y { get; }

    public Point2D(double x, double y)
    {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return $"({this.X}, {this.Y})";
    }

    public override bool Equals(object obj)
    {
        if (obj == this) return true;
        if (obj == null) return false;
        if (obj.GetType() != this.GetType()) return false; 
        var that = (Point2D)obj;
        return this.X == that.X && this.Y == that.Y;
    }

    public override int GetHashCode()
    {
        var hashX = this.X.GetHashCode();
        var hashY = this.Y.GetHashCode();

        return 31 * hashX + hashY;
    }

    public int CompareTo(Point2D that)
    {
        if (this.Y < that.Y) return -1;
        if (this.Y > that.Y) return +1;
        if (this.X < that.X) return -1;
        if (this.X > that.X) return +1;

        return 0;
    }
}