﻿using UnityEngine;

namespace Assets.Source.Hex
{
    public static class HexMetrics
    {
        public const float OuterRadius = 10f;
        public const float InnerRadius = OuterRadius*0.866025404f;

        public static readonly Vector3[] Corners =
        {
            new Vector3(0f, 0f, OuterRadius),                   // Top
            new Vector3(InnerRadius, 0f, 0.5f*OuterRadius),     // Top - Right
            new Vector3(InnerRadius, 0f, -0.5f*OuterRadius),    // Bottom - Right
            new Vector3(0f, 0f, -OuterRadius),                  // Bottom 
            new Vector3(-InnerRadius, 0f, -0.5f*OuterRadius),   // Bottom - Left
            new Vector3(-InnerRadius, 0f, 0.5f*OuterRadius),    // Top - Left
        };

        public static int Width, Height;
        public static int MaxElevation = 100;

        public static Vector3 GetCornerByVertexPosition(HexVertexPosition position)
        {
            return Corners[(int) position];
        }
    }

    public enum HexVertexPosition
    {
        Top = 0,
        TopRight = 1,
        BottomRight = 2,
        Bottom = 3,
        BottomLeft = 4,
        TopLeft = 5
    }
}