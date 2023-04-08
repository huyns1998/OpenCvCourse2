using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContourDetection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/Hand.PNG");

            Mat img = Cv2.ImRead(fileName);
            Mat gray = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(gray, gray, new Size(5, 5), 1.5, 1.5);
            Mat edge = new Mat();
            //Cv2.Canny(gray, edge, 20, 50, 3, true);
            Cv2.Threshold(gray, edge, 230, 255, ThresholdTypes.Binary);
            Cv2.ImShow("edge", edge);
            Cv2.ImShow("gray", gray);

            Point[][] contours;
            HierarchyIndex[] hIndx;
            Cv2.FindContours(edge, out contours, out hIndx, RetrievalModes.List, ContourApproximationModes.ApproxNone);
            //Cv2.DrawContours(img, contours, -1, new Scalar(0, 128, 0), thickness: 2);
            //Cv2.ImShow("img", img);

            Mat blankImage = new Mat(img.Rows, img.Cols, MatType.CV_8UC3, Scalar.All(0));
            //Cv2.DrawContours(blankImage, contours, -1, new Scalar(0, 128, 0), thickness: 2);
            Cv2.ImShow("blankImage", blankImage);

            contours = contours.OrderBy(x => Cv2.ContourArea(x)).ToArray();
            //foreach (Point[] contour in contours)
            //{
            //    Point[][] p = { contour };
            //    Cv2.DrawContours(blankImage, p, -1, new Scalar(0, 128, 0), thickness: 2);
            //    Cv2.ImShow("contourSorted", blankImage);
            //    Cv2.WaitKey(0);
            //}

            //foreach (Point[] contour in contours)
            //{
            //    Rect rect = Cv2.BoundingRect(contour);
            //    Cv2.Rectangle(img, rect, new Scalar(0, 128, 0), thickness: 2);
            //    Cv2.ImShow("Bounding rect", img);
            //}


            //List<Point[]> ps = new List<Point[]>();
            //foreach (Point[] contour in contours)
            //{
            //    double accuracy = 0.005 * Cv2.ArcLength(contour, true);
            //    Point[] approx = Cv2.ApproxPolyDP(contour, accuracy, true);
            //    ps.Add(approx);
            //}
            //Mat approxs = new Mat(img.Rows, img.Cols, MatType.CV_8UC3, Scalar.All(0));
            //Cv2.DrawContours(approxs, ps, -1, new Scalar(0, 200, 0), thickness: 2);
            //Cv2.ImShow("approx", approxs);

            List<Point[]> hulls = new List<Point[]>();
            foreach(Point[] contour in contours)
            {
                Point[] hull = Cv2.ConvexHull(contour); 
                hulls.Add(hull);
                Cv2.DrawContours(img, hulls, -1, new Scalar(0, 200, 0), thickness: 2);
                Cv2.ImShow("img", img);
            }

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
