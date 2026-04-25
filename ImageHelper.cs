using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace Inflow
{
    internal static class ImageHelper
    {
        /// Creates a dimmed (grayed-out) version of the original image.
        /// Used for displaying unselected stars

        public static Image CreateDimmedStar(Image original)
        {
            if (original == null) return null;

            Bitmap dimmed = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(dimmed))
            {
                float[][] matrix = {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.4f, 0},
                    new float[] {0, 0, 0, 0, 1}
                };
                using (var attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(new ColorMatrix(matrix));
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                        0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return dimmed;
        }

    }
}
