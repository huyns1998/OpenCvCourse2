using OpenCvSharp;

internal class Program
{
    private static void Main(string[] args)
    {
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        //string eye_clasifier = Path.Combine(projectDirectory, "Models/haarcascade_eye.xml");
        //string face_clasifier = Path.Combine(projectDirectory, "Models/haarcascade_frontalface_alt2.xml");
        string eyeglasses_clasifier = Path.Combine(projectDirectory, "Models/haarcascade_eye_tree_eyeglasses.xml");

        //CascadeClassifier faceCascade = new CascadeClassifier(face_clasifier);
        CascadeClassifier glassesCascade = new CascadeClassifier(eyeglasses_clasifier);

        string f = Path.Combine(projectDirectory, "Images/glasses.jpg");

        Mat image = Cv2.ImRead(f);

        Mat gray = new Mat();
        Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);
        // Detect objects using the cascade classifier
        Rect[] faces = glassesCascade.DetectMultiScale(gray, 1.1, 6, HaarDetectionTypes.ScaleImage, new Size(15, 15));

        // Draw rectangles around the detected faces with glasses
        foreach (Rect face in faces)
        {
            Cv2.Rectangle(image, face, Scalar.Red, 2);
            //Mat face_gray = gray.SubMat(face);
            //Rect[] eyes_glasses = glassesCascade.DetectMultiScale(gray, 1.1, 3, HaarDetectionTypes.ScaleImage, new Size(30, 30));
            //if(eyes_glasses.Length != 0)
            //{
            //    foreach (Rect eye in eyes_glasses)
            //    {
            //        Cv2.Rectangle(image, eye, Scalar.Red, 2);
            //    }
            //} 
        }

        // Display the result
        Cv2.ImShow("Result", image);
        Cv2.ImShow("Result1", gray);
        Cv2.WaitKey(0);
    }
}