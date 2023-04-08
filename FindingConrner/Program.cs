using OpenCvSharp;
using System;
using System.IO;
using System.Security.Cryptography;

namespace FindingConrner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/corner.PNG");

            Mat img = Cv2.ImRead(fileName);
            Mat gray = new Mat();
            //Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

            //Point2f[] corners = Cv2.GoodFeaturesToTrack(gray, 100, 0.01, 15, null, 3, false, 0.04);

            //foreach (Point2f corner in corners)
            //{
            //    Cv2.Circle(img, corner, 3, Scalar.Red, 3);
            //}

            //Cv2.ImShow("img", img);




            // Convert to grayscale
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

            Mat floatImage = new Mat();
            gray.ConvertTo(floatImage, MatType.CV_32FC1);

            // Apply cornerHarris
            Mat corners = new Mat();
            Cv2.CornerHarris(floatImage, corners, 2, 3, 0.04);

            Mat kernel = Mat.Ones(new Size(5, 5), MatType.CV_8UC1);

            Cv2.Dilate(corners, corners, kernel, iterations: 2);
            Cv2.ImShow("corners", corners);

            double minValue, maxValue;
            Point minLocation, maxLocation;
            Cv2.MinMaxLoc(corners, out minValue, out maxValue, out minLocation, out maxLocation);

            double threshold = 0.025 * maxValue;
            Mat thresholded = new Mat();
            Cv2.Threshold(corners, thresholded, threshold, 255, ThresholdTypes.Binary);

            Cv2.ImShow("thresholded", thresholded);

            Scalar red = new Scalar(0, 0, 255);
            Mat redPixels = new Mat(img.Size(), img.Type(), red);
            thresholded.ConvertTo(thresholded, MatType.CV_8UC1);
            Cv2.BitwiseAnd(redPixels, redPixels, img, thresholded);

            Cv2.ImShow("Result", img);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();    
        }
    }
}
