using OpenCvSharp;
using System;
using System.IO;

namespace SIFTq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/Effel.PNG");

            Mat image = Cv2.ImRead(fileName);
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

            // Create ORB object
            var orb = ORB.Create();

            // Detect keypoints
            KeyPoint[] keypoints;
            Mat descriptors = new Mat();
            orb.DetectAndCompute(gray, null, out keypoints, descriptors);

            // Draw keypoints
            Mat imgWithKeypoints = new Mat();
            Cv2.DrawKeypoints(image, keypoints, imgWithKeypoints);

            // Display image with keypoints
            Cv2.ImShow("Image with Keypoints", imgWithKeypoints);
            Cv2.WaitKey(0);
        }
    }
}
