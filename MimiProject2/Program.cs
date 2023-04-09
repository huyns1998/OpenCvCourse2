using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace MimiProject2
{
    internal class Program
    {
        public const int TRIANGLE = 0;
        public const int RECTANGLE = 1;
        public const int SQUARE = 2;
        public const int STAR = 3;
        public const int CIRCLE = 4;
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/shape.PNG");

            Mat img = Cv2.ImRead(fileName, ImreadModes.Color);

            Point[][] contours = GetContour(img);
            foreach (Point[] contour in contours)
            {
                Point[] contourNew = Cv2.ApproxPolyDP(contour, 0.01 * Cv2.ArcLength(contour, true), true);
                if(contourNew.Length == 3)
                {
                    PutText(img, contour, "Triangle");
                }
                else if(contourNew.Length == 3) 
                {
                    Rect rect = Cv2.BoundingRect(contour);
                    if(Math.Abs(rect.Width - rect.Height) > 4)
                    {
                        PutText(img, contour, "Rectangle2");
                    }
                    else
                    {
                        PutText(img, contour, "Square1");
                    }
                }
                else if(contourNew.Length == 10) 
                {
                    PutText(img, contour, "Star");
                }
                else if(contourNew.Length > 14)
                {
                    PutText(img, contour, "Circle");
                }
                Cv2.ImShow("img", img);
                Cv2.WaitKey(0);
            }
            Cv2.WaitKey(2);
            Cv2.DestroyAllWindows();
        }

        static Point[][] GetContour(Mat img)
        {
            Mat grayImg = new Mat();
            Cv2.CvtColor(img, grayImg, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(grayImg, grayImg, new Size(5, 5), 1.5, 1.5);
            Cv2.Threshold(grayImg, grayImg, 127, 255, ThresholdTypes.Binary);
            HierarchyIndex[] hIndx;
            Point[][] contours;
            Cv2.FindContours(grayImg, out contours, out hIndx, RetrievalModes.List, ContourApproximationModes.ApproxNone);
            return contours;
        }

        static void PutText(Mat img, Point[] contour, string text)
        {
            Moments M = Cv2.Moments(contour);
            double cx = M.M10 / M.M00;
            double cy = M.M01 / M.M00;
            Cv2.PutText(img, text, new Point(cx - 50, cy), HersheyFonts.HersheySimplex, 1, new Scalar(0, 170, 0), 1);
        }

        void IdentityShape(Point[] contour)
        {
            
        }
    }
}
//c1
//c2
//c3