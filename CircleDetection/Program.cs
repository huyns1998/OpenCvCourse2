using OpenCvSharp;
using System;
using System.IO;

namespace CircleDetection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/candy.PNG");

            Mat img = Cv2.ImRead(fileName);
            Mat gray = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(gray, gray, new Size(5, 5), 1.5, 1.5);
            //Cv2.ImShow("gray", gray);
            Mat canny = new Mat();
            Cv2.Canny(gray, canny, 150, 200, 5, true);
            CircleSegment[] circles = Cv2.HoughCircles(gray, HoughMethods.Gradient, 1, 100, 46, 50, 0, 0);
           
            foreach(CircleSegment circle in circles) 
            {
                Cv2.Circle(img, circle.Center, (int)circle.Radius, Scalar.Blue);
            }

            Cv2.ImShow("img", img);
            Cv2.ImShow("canny", canny);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();    
        }
    }
}
