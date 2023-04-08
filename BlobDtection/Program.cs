using OpenCvSharp;
using OpenCvSharp.XFeatures2D;
using System;
using System.IO;

namespace BlobDtection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/blob.PNG");

            Mat img = Cv2.ImRead(fileName);

            Cv2.CvtColor(img, img, ColorConversionCodes.BGRA2GRAY);

            SimpleBlobDetector.Params parameters = new SimpleBlobDetector.Params();

            //Filter by area
            parameters.FilterByArea = true;
            parameters.MinArea= 0;
            parameters.MaxArea = 1000000;

            //Filter by color
            parameters.FilterByColor = true;
            parameters.BlobColor = 0;

            //Filter by Circularity
            parameters.FilterByCircularity = false;
            parameters.MinCircularity = 0.0F;
            parameters.MaxCircularity = 1;

            //Filter by Convexity
            parameters.FilterByConvexity = true;
            parameters.MinConvexity = 0.2F;
            parameters.MaxConvexity = 1;
            parameters.MinDistBetweenBlobs = 0;

            
            SimpleBlobDetector detector = SimpleBlobDetector.Create(parameters);

            KeyPoint[] keyPoints = detector.Detect(img);

            Mat output = new Mat();

            Cv2.DrawKeypoints(img, keyPoints, output, new Scalar(0, 0, 255), DrawMatchesFlags.DrawRichKeypoints);

            Cv2.ImShow("Output", output);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();    
        }
    }
}
