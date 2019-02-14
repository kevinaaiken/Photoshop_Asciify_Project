using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Photoshop__Asciify_Project
{
    class BitmapAscii
    {
        //fields
        string sBlack;
        string sVDGray;
        string sDGray;
        string sGray;
        string sLGray;
        string sWhite;
        

        //methods
        public string Asciitize(Bitmap bitmap1, int kernel_x, int kernel_y, string string1, string string2, string string3, string string4, string string5, string string6)
        {
            //method accepts bitmap and returns string containing ascii text

            //variables
            int new_y;
            int new_x;
            List<Color> lst_color = new List<Color>();
            string pic_message = "";
            double color_num = 0.0;
            
            sBlack = string1;
            sVDGray = string2;
            sDGray = string3;
            sGray = string4;
            sLGray = string5;
            sWhite = string6;
            
            //origin x & y are positions on entire bitmap
            //kernel x & y are positions within the kernel
            //new_x and new_y are calculated in case the kernel extends past the bitmap edge
            
            for (int origin_y = 0; origin_y < bitmap1.Height; origin_y += kernel_y)
            {

                if (origin_y + kernel_y < bitmap1.Height)
                {
                    new_y = kernel_y + origin_y;  //if kernel is within edge of bitmap
                }else{
                    new_y = kernel_y - ((origin_y + kernel_y) - bitmap1.Height);  //if kernel extends past edge of bitmap
                }//end if kernel goes past edge of bitmap

                for (int origin_x = 0; origin_x < bitmap1.Width; origin_x += kernel_x)
                {

                    if (origin_x + kernel_x < bitmap1.Width)
                    {
                        new_x = kernel_x + origin_x;  //if kernel is within edge of bitmap
                    }else{
                        new_x = kernel_x - ((origin_x + kernel_x) - bitmap1.Width);  //if kernel extends past edge of bitmap
                }//end if kernel goes past edge of bitmap

                    for (int ky = origin_y; ky < new_y; ky ++)
                    {
                        for (int kx = origin_x;kx < new_x; kx ++)
                        {

                            lst_color.Add(bitmap1.GetPixel(kx, ky));  //add all pixels within the kernel to a list
                        }//end for kernel x
                    }//end for kernel y

                    color_num = AverageColor(lst_color);  //get average color of each pixel
                    lst_color.Clear();  //clear old list for next kernel
                    pic_message += GrayToString(color_num);
                }//end for origin x
                
                pic_message += "\n";
            }//end for origin y

            return pic_message;
        }//end asciitize method


        public Bitmap GrayBitmap(Bitmap bitmap1)
        {
            //returns grayscale bitmap for 2nd picturebox

            double graycolor = 0.0;

            for (int y = 0; y < bitmap1.Height; y ++){
                for (int x = 0; x < bitmap1.Width; x ++){

                    Color pixel = bitmap1.GetPixel(x, y);
                    graycolor = AveragePixel(pixel.R, pixel.G, pixel.B);  //avg pixel takes Luminosity of the pixels

                    pixel = Color.FromArgb((int)graycolor, (int)graycolor, (int)graycolor);
                    bitmap1.SetPixel(x, y, pixel);

                }//end for x
            }//end for y

            return bitmap1;
        }//end gray bitmap method


        public double AverageColor(List<Color> list1)
        {
            //accepts a list of colors and returns a normalized value (0-1) of the grey value calculated from the colors RGB value
            double avg_color = 0.0;

            foreach (Color pixel in list1)
            {
                avg_color += AveragePixel(pixel);  //pass pixel.R, pixel.G, pixel.B to access AveragePixel's overload
            }
            
            return avg_color / list1.Count;
        }//end averaged color


        public double AveragePixel(Color color1)
        {
            //this method should accept a color instance and return a normalized value calculated from the RGB values
            //Lumonisity Algorithm == 0.21 R + .72 G + 0.07 B

            double answer = 0.0;
            answer = (0.21 * color1.R) + (0.72 * color1.G) + (0.07 * color1.B);

            return answer;
        }//end average pixel


        public double AveragePixel(int int1, int int2, int int3)
        {
            //this method should accept color components and return a normalized value calculated from the RGB values
            //Lumonisity Algorithm == 0.21 R + .72 G + 0.07 B

            double answer = 0.0;
            answer = (0.21 * int1) + (0.72 * int2) + (0.07 * int3);

            return answer;
        }//end average pixel


        string GrayToString(double double1)
        {
            //accepts a normalized value (0-1) and return a string containing the ascii 
            string answer = "";
            double1 = double1 / 255.0;

            if (double1 > 0.9){
                answer = sWhite;
            }else if (double1 >= 0.7){
                answer = sLGray;
            }else if (double1 >= 0.5){
                answer = sGray;
            }else if (double1 >= 0.3){
                answer = sDGray;
            }else if (double1 >= 0.1){
                answer = sVDGray;
            }else{ //if (double1 <= 0.1)
                answer = sBlack;
            }//end if statement
            
            return answer;
        }//end gray to string method
        

    }//class
}//name
