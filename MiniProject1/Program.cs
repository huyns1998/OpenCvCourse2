using OpenCvSharp;
using System;

namespace MiniProject1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideoCapture cap = new VideoCapture(0);

            while(true)
            {
                Mat frame = GetFrame(cap, 1.0);
                Cv2.ImShow("frame", Sketch(frame));

                int c = Cv2.WaitKey(10);
                if(c == 13)
                {
                    break;
                }
            }
            Cv2.DestroyAllWindows();
        }

        private static Mat Sketch(Mat image)
        {
            Mat grayScale = new Mat();
            Cv2.CvtColor(image, grayScale, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(grayScale, grayScale, new Size(5, 5), 1.5, 1.5);
            Cv2.Canny(grayScale, grayScale, 50, 200, 5, true);
            return grayScale;   
        }
        private static Mat GetFrame(VideoCapture cap, double scalingFactor)
        {
            Mat frame = new Mat();
            bool ret = cap.Read(frame);

            Cv2.Resize(frame, frame, new Size(), scalingFactor, scalingFactor, InterpolationFlags.Nearest);

            return frame;
        }
    }
}
