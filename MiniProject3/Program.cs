using OpenCvSharp;
using System;
using System.IO;

namespace MiniProject3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/blob.PNG");

            Mat img = Cv2.ImRead(fileName);

            KeyPoint[] keyPoints = FilterEllipse(img);  

            Mat output = new Mat();

            Cv2.DrawKeypoints(img, keyPoints, output, new Scalar(0, 0, 255), DrawMatchesFlags.DrawRichKeypoints);

            Cv2.ImShow("output", output);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        static KeyPoint[] FilterCircle(Mat img)
        {
            Cv2.CvtColor(img, img, ColorConversionCodes.BGRA2GRAY);
            SimpleBlobDetector.Params parameters = new SimpleBlobDetector.Params();

            //Filter by area
            parameters.FilterByArea = true;
            parameters.MinArea = 0;
            parameters.MaxArea = 10000;

            //Filter by color
            parameters.FilterByColor = true;
            parameters.BlobColor = 0;

            //Filter by Circularity
            parameters.FilterByCircularity = true;
            parameters.MinCircularity = 0.9F;
            parameters.MaxCircularity = 1;

            //Filter by Convexity
            parameters.FilterByConvexity = false;
            parameters.MinConvexity = 0.2F;
            parameters.MaxConvexity = 1;

            SimpleBlobDetector detector = SimpleBlobDetector.Create(parameters);

            KeyPoint[] keyPoints = detector.Detect(img);

            return keyPoints;
        }

        static KeyPoint[] FilterEllipse(Mat img)
        {
            Cv2.CvtColor(img, img, ColorConversionCodes.BGRA2GRAY);
            SimpleBlobDetector.Params parameters = new SimpleBlobDetector.Params();

            //Filter by area
            parameters.FilterByArea = true;
            parameters.MinArea = 0;
            parameters.MaxArea = 10000;

            //Filter by color
            parameters.FilterByColor = true;
            parameters.BlobColor = 0;

            //Filter by Circularity
            parameters.FilterByCircularity = false;
            parameters.MinCircularity = 0.9F;
            parameters.MaxCircularity = 1;

            //Filter by Convexity
            parameters.FilterByConvexity = false;
            parameters.MinConvexity = 0.2F;
            parameters.MaxConvexity = 1;

            parameters.FilterByInertia= true;  
            parameters.MaxInertiaRatio = 0.3F;
            parameters.MinInertiaRatio = 0;

            SimpleBlobDetector detector = SimpleBlobDetector.Create(parameters);

            KeyPoint[] keyPoints = detector.Detect(img);

            return keyPoints;
        }
    }
}
