using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Menu
            Console.WriteLine("Please select type of filter You want to apply: ");
            Console.WriteLine("1. Sharpen");
            Console.WriteLine("2. Blur");
            Console.WriteLine("3. Edge detection v1");
            Console.WriteLine("4. Edge detection v2");
            Console.WriteLine("5. Emboss");
            Console.Write("6. Top sobel\n>> ");
            // Get choice
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Processing image . . .");

            // Opening .jpg file
            //string folderPath = (@"C:\Users\pazer\GoogleDrive\Studia\Sem4\SystemySztucznejInteligencji\lab2\photos");
            Bitmap sourceImage = new Bitmap(@"C:\Users\pazer\GoogleDrive\Studia\Sem4\SystemySztucznejInteligencji\filtrGrafika\photos\input.jpg");

            // Saving dimensions
            int imageHeight = sourceImage.Height;
            int imageWidth = sourceImage.Width;

            // Creating blank bitmap for new image
            Bitmap targetImage = new Bitmap(imageWidth, imageHeight);

            // Defining kernel size
            const int kernelSize = 3;

            // Array for each set of 3x3 pixel neighbours
            Color[,] neighboursArray = new Color[kernelSize, kernelSize];

            // For each row on pixel in image
            for (int i = 1; i < imageWidth - 1; i++)
            {
                // For each column of pixels in image
                for (int j = 1; j < imageHeight - 1; j++)
                {
                    // For each of 3 rows of pixel neighbours
                    for (int a = 0; a < kernelSize; a++)
                    {
                        // For each of 3 columns of pixel neighbours
                        for (int b = 0; b < kernelSize; b++)
                        {
                            // Each neighbour is added to array
                            Color pixelColor = sourceImage.GetPixel(i + a - 1, j + b - 1);
                            neighboursArray[a, b] = pixelColor;
                        }
                    }
                    // Each array of neighbours is calculated to get new value of pixel
                    Color newValue = edgeDetectionFilter2(neighboursArray, choice);
                    // New value is saved to output image
                    targetImage.SetPixel(i, j, newValue);
                }
            }
            
            // Saving file to original folder
            targetImage.Save(@"C:\Users\pazer\GoogleDrive\Studia\Sem4\SystemySztucznejInteligencji\filtrGrafika\photos\output.jpg");
            // Displaying image in default Windows Photo Viewer
            System.Diagnostics.Process.Start(@"C:\Users\pazer\GoogleDrive\Studia\Sem4\SystemySztucznejInteligencji\filtrGrafika\photos\output.jpg");
        }
        static Color edgeDetectionFilter2(Color[,] pixelArray, int filter)
        {
            // Creating matrix of kernel values
            double[,] filterWeightMatrix = new double[3, 3];
            if(filter == 1) 
                filterWeightMatrix = new double[3, 3]{
                {0, -1, 0},
                {-1, 5, -1},
                {0, -1, 0}};
            else if(filter == 2)
                filterWeightMatrix = new double[3, 3]{
                {0.0625, 0.125, 0.0625},
                {0.125, 0.25, 0.125},
                {0.06251, 0.125, 0.0625}};
            else if (filter == 3)
                filterWeightMatrix = new double[3, 3]{
                {1, 0, -1},
                {-0, 0, -0},
                {-1, -0, 1}};
            else if (filter == 4)
                filterWeightMatrix = new double[3, 3]{
                {-1, -1, -1},
                {-1, 8, -1},
                {-1, -1, -1}};
            else if (filter == 5)
                filterWeightMatrix = new double[3, 3]{
                {-2, -1, 0},
                {-1, 1, -1},
                {0, -1, 2}};
            else if (filter == 6)
                filterWeightMatrix = new double[3, 3]{
                {1, 2, 1},
                {0, 0, 0},
                {-1, -2, -1}};

            Color outputPixelValue = new Color();
            
            // RBG values calculating as double type for precision purposes
            double red = 0;
            double green = 0;
            double blue = 0;

            // Convolution
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    red += pixelArray[j, i].R * filterWeightMatrix[i, j];
                    green += pixelArray[j, i].G * filterWeightMatrix[i, j];
                    blue += pixelArray[j, i].B * filterWeightMatrix[i, j];
                }
            }

            // Checking if any value is out of range 0-255
            if (red > 255) red = 255;
            if (red < 0) red = 0;
            if (green > 255) green = 255;
            if (green < 0) green = 0;
            if (blue > 255) blue = 255;
            if (blue < 0) blue = 0;

            outputPixelValue = Color.FromArgb((int)red, (int)green, (int)blue);
            return outputPixelValue;
        }

       
    }
}
