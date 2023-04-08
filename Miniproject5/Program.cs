using OpenCvSharp;
using System;
using System.IO;

namespace Miniproject5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileNameShape = Path.Combine(projectDirectory, "Images/shape.PNG");
            string fileNameTemplate = Path.Combine(projectDirectory, "Images/template.PNG");

            Mat image = Cv2.ImRead(fileNameShape);
            Mat template = Cv2.ImRead(fileNameTemplate);

            Mat grayImage = new Mat();

            Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGRA2GRAY);
            Cv2.CvtColor(template, template, ColorConversionCodes.BGRA2GRAY);

            Mat result = new Mat();

            Cv2.MatchTemplate(grayImage, template, result, TemplateMatchModes.SqDiff);

            Point minloc = new Point();
            Point maxloc = new Point(); 

            Cv2.MinMaxLoc(result, out minloc, out maxloc);

            double w = template.Width;
            double h = template.Height; 

            Point top_left = minloc;
            Point bottomRight = new Point(top_left.X + w, top_left.Y + h);

            Cv2.Rectangle(image, top_left, bottomRight, Scalar.Red, 3);

            Cv2.ImShow("image", image);
            Cv2.ImShow("result", result);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();    

        }
    }
}
