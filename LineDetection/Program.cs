using OpenCvSharp;
using System;
using System.IO;
using System.Security.Cryptography;

namespace LineDetection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/straight.PNG");

            Mat image = Cv2.ImRead(fileName);
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);
            Mat edges = new Mat();
            Cv2.Canny(gray, edges, 20, 100, 3, true);
            Cv2.ImShow("edges", edges);

            //LineSegmentPolar[] lines = Cv2.HoughLines(edges, 1,Math.PI / 180, 120);
            LineSegmentPoint[] lines = Cv2.HoughLinesP(edges, 1, Math.PI / 180, 120);
            //int p = 1000;
            //foreach (LineSegmentPolar line in lines)
            //{
            //    double rho = line.Rho, theta = line.Theta;
            //    Point pt1 = new Point(), pt2 = new Point();
            //    double a = Math.Cos(theta), b = Math.Sin(theta);
            //    double x0 = a * rho, y0 = b * rho;
            //    pt1.X = (int)(x0 + p * (-b));
            //    pt1.Y = (int)(y0 + p * (a));
            //    pt2.X = (int)(x0 - p * (-b));
            //    pt2.Y = (int)(y0 - p * (a));
            //    Cv2.Line(image, pt1, pt2, Scalar.Red, 3, LineTypes.AntiAlias, 0);
            //}

            foreach( LineSegmentPoint line in lines)
            {
                Cv2.Line(image, new Point(line.P1.X, line.P1.Y), new Point(line.P2.X, line.P2.Y), Scalar.Red, 3, LineTypes.AntiAlias, 0);
            }

            Cv2.ImShow("image", image);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
