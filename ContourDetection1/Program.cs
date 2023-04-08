using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContourDetection1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/matchshape2.PNG");
            string fileNameTemplate = Path.Combine(projectDirectory, "Images/triangle_template.png");
            Mat img = Cv2.ImRead(fileName, ImreadModes.Color);
            Mat imgTemplate = Cv2.ImRead(fileNameTemplate, ImreadModes.Color);

            Mat grayTemplate = new Mat();
            Cv2.CvtColor(imgTemplate, grayTemplate, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(grayTemplate, grayTemplate, new Size(5, 5), 1.5, 1.5);
            Cv2.Threshold(grayTemplate, grayTemplate, 127, 255, ThresholdTypes.Binary);

            HierarchyIndex[] hIndxTemplate;
            Point[][] contoursTemplate;
            Cv2.FindContours(grayTemplate, out contoursTemplate, out hIndxTemplate, RetrievalModes.List, ContourApproximationModes.ApproxSimple);
            //Cv2.DrawContours(imgTemplate, contoursTemplate, -1, new Scalar(0, 127, 0), thickness: 2);
            //Cv2.ImShow("imgTemplate", imgTemplate);


            Mat imgGray = new Mat();
            Cv2.CvtColor(img, imgGray, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(imgGray, imgGray, new Size(5, 5), 1.5, 1.5);
            Cv2.Threshold(imgGray, imgGray, 127, 255, ThresholdTypes.Binary);

            HierarchyIndex[] hIndx;
            Point[][] contours;
            Cv2.FindContours(imgGray, out contours, out hIndx, RetrievalModes.List, ContourApproximationModes.ApproxSimple);
            //Cv2.DrawContours(img, contours, -1, new Scalar(0, 127, 0), thickness: 2);

            //List<Point[]> ps = new List<Point[]>();
            //foreach (Point[] contour in contours)
            //{
            //    double accuracy = 0.01 * Cv2.ArcLength(contour, true);
            //    Point[] approx = Cv2.ApproxPolyDP(contour, accuracy, true);
            //    ps.Add(approx);
            //}

            contoursTemplate.OrderBy(x => Cv2.ContourArea(x));
            contours.OrderBy(x => Cv2.ContourArea(x));

            Point[] template_contour = contoursTemplate[0];
            List<Point[]> closestContours = new List<Point[]>();
            double min = 10;
            int index = 0;
            int good_index = 0;
            Mat blankImage = new Mat(img.Rows, img.Cols, MatType.CV_8UC3, Scalar.All(0));
            foreach (Point[] contour in contours)
            {
                double match = Cv2.MatchShapes(contour, template_contour, ShapeMatchModes.I1, 0);
                Cv2.DrawContours(blankImage, new Point[][] { contour }, -1, new Scalar(0, 127, 0), thickness: 2);
                //Cv2.ImShow("blankImage", blankImage);
                //Cv2.WaitKey(0);
                if (min > match)
                {
                    min = match;
                    good_index = index;
                }
                index++;
            }
            closestContours.Add(contours.ElementAt(good_index));
            Cv2.DrawContours(img, closestContours, -1, new Scalar(0, 127, 0), thickness: 2);
            Cv2.DrawContours(imgTemplate, new Point[][] { template_contour }, -1, new Scalar(0, 127, 0), thickness: 2);

            Cv2.ImShow("img", img);
            Cv2.ImShow("imgTemplate", imgTemplate);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
