using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Plugins
{
    public enum GUIAlign
    {
        Top,
        TopLeft,
        Left,
        BottomLeft,
        Bottom,
        BottomRight,
        Right,
        Center
    }

    public static class GUIPlus
    {

        public static Rect LayoutRect(float percentWidth, float percentHeight, GUIAlign align)
        {
            float width = Screen.width * percentWidth;
            float height = Screen.height * percentHeight;
            return new Rect(GetLeft(width, align), GetTop(height, align), width, height);
        }

        private static float GetLeft(float width, GUIAlign align)
        {
            switch (align)
            {
                case GUIAlign.Right:
                case GUIAlign.BottomRight:
                    return Screen.width - width;
                case GUIAlign.Center:
                case GUIAlign.Bottom:
                    return (Screen.width - width) / 2;
                default:
                    return 0;
            }
        }

        private static float GetTop(float height, GUIAlign align)
        {
            switch (align)
            {
                case GUIAlign.Bottom:
                case GUIAlign.BottomRight:
                    return Screen.height - height;
                case GUIAlign.Center:
                    return (Screen.height - height) / 2;
                default:
                    return 0;
            }
        }
    }
}
