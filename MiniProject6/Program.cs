using OpenCvSharp;
using System;
using System.IO;

namespace MiniProject6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/image_template.jpg");
            VideoCapture cap = new VideoCapture(0);
            Mat image_template = Cv2.ImRead(fileName);
            int matches;
            int width, height, top_left_x, top_left_y, bottom_right_x, bottom_right_y;   
            while(true)
            {
                var frame = GetFrame(cap, 1);

                width = frame.Width;    
                height = frame.Height;

                top_left_x = width / 3;
                top_left_y = height / 2 + height / 4;
                bottom_right_x = (width / 3) * 2;
                bottom_right_y = height/2-height / 4;

                Cv2.Rectangle(frame, new Point(top_left_x, top_left_y), new Point(bottom_right_x, bottom_right_y), 255, 3);

                int cropX = top_left_x;
                int cropY = bottom_right_y;
                int cropWidth = bottom_right_x - top_left_x;
                int cropHeight = top_left_y - bottom_right_y;

                // Ensure the region to crop falls within the bounds of the original image
                if (cropX < 0)
                {
                    cropX = 0;
                }
                if (cropY < 0)
                {
                    cropY = 0;
                }
                if (cropX + cropWidth > frame.Width)
                {
                    cropWidth = frame.Width - cropX;
                }
                if (cropY + cropHeight > frame.Height)
                {
                    cropHeight = frame.Height - cropY;
                }

                // Crop the image using the defined region
                Mat cropped = new Mat(frame, new Rect(cropX, cropY, cropWidth, cropHeight));

                Cv2.Flip(frame, frame, FlipMode.Y);

                matches = ORB_Detector(cropped, image_template);

                string output_string = "Matches = " + matches;
                Cv2.PutText(frame, output_string, new Point(50, 450), HersheyFonts.HersheyComplex, 2, new Scalar(250, 0 ,150), 2);

                Cv2.ImShow("cropped", cropped);
                Cv2.ImShow("frame", frame);
                //Cv2.ImShow("Color detector", res);

                var c = Cv2.WaitKey(10);
                if (c == 27)
                {
                    break;
                }
            }
            Cv2.DestroyAllWindows();
        }
        private static Mat GetFrame(VideoCapture cap, double scalingFactor)
        {
            Mat frame = new Mat();
            bool ret = cap.Read(frame);

            Cv2.Resize(frame, frame, new Size(), scalingFactor, scalingFactor, InterpolationFlags.Nearest);

            return frame;
        }
        private static int ORB_Detector(Mat newImage, Mat templateImage)
        {
            Mat image1 = new Mat();
            Cv2.CvtColor(newImage, image1, ColorConversionCodes.BGR2GRAY);

            ORB orb = ORB.Create(1000, 1.2f);

            KeyPoint[] keyPoints1;
            Mat des1 = new Mat();
            KeyPoint[] keyPoints2;
            Mat des2 = new Mat();
            orb.DetectAndCompute(image1, null, out keyPoints1, des1);
            orb.DetectAndCompute(templateImage, null, out keyPoints2, des2);

            //DescriptorMatcher bf = DescriptorMatcher.Create("BruteForce-Hamming");
            BFMatcher bf = new BFMatcher(NormTypes.Hamming, crossCheck: true);
            // perform a two-way matching with k=2
            DMatch[] matches = bf.Match(des1, des2);

            return matches.Length;
        }
    }
}
