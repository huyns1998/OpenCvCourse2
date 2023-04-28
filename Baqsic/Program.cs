using OpenCvSharp;
using System;
using System.IO;

namespace Baqsic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/google.PNG");

            Mat image = Cv2.ImRead(fileName);
            Cv2.Resize(image, image, new Size(), 0.5, 0.5);
            //Mat grayImage = new Mat();
            //Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);

            //Mat M = Mat.Ones(grayImage.Width, grayImage.Height, MatType.CV_8UC1) * 75;
            //Mat dark = new Mat();   
            //Cv2.Subtract(grayImage, M, dark);
            //Cv2.ImShow("grayImage", grayImage);
            //Cv2.ImShow("dark", dark);


            //Mat square = new Mat(new Size(300, 300), MatType.CV_8UC1, Scalar.All(0));
            //Cv2.Rectangle(square, new Point(50, 50), new Point(250, 250), Scalar.All(255), -1);
            //Cv2.ImShow("square", square);

            //Mat ellipse = new Mat(new Size(300, 300), MatType.CV_8UC1, Scalar.All(0));
            //Cv2.Ellipse(ellipse, new Point(150, 150), new Size(150, 150), 30, 0, 180, 255, -1);
            //Cv2.ImShow("ellipse", ellipse);

            //Mat and = new Mat();
            //Cv2.BitwiseAnd(square, ellipse, and);
            //Cv2.ImShow("and", and);



            //Cv2.ImShow("image", image);
            //Mat bilDst1 = new Mat();
            //Mat bilDst2 = new Mat();
            //Mat bilDst3 = new Mat();
            //Mat bilDst4 = new Mat();
            //Mat medianBlur = new Mat();
            //Cv2.BilateralFilter(image, bilDst1, 9, 75, 75);
            //Cv2.BilateralFilter(image, bilDst2, 9, 75, 2);
            Cv2.MedianBlur(image, medianBlur, 7);
            Cv2.ImShow("bilDst1", bilDst1);
            Cv2.ImShow("bilDst2", bilDst2);
            Cv2.ImShow("medianBlur", medianBlur1);


            float[] data = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            Mat M = new Mat(3, 3, MatType.CV_32FC1, data);
            Mat sharpen = new Mat();
            Cv2.Filter2D(image, sharpen, MatType.CV_8UC3, M);
            Cv2.ImShow("image", image);
            Cv2.ImShow("sharpen", sharpen);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();    
        }
    }
}
