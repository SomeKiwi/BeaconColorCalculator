﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace BeaconColorCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    class RGB
    {
        byte red;
        byte green;
        byte blue;

        public RGB(byte r, byte g, byte b)
        {
            red = r;
            green = g;
            blue = b;
        }
        public byte GetRed()
        {
            return red;
        }
        public byte GetGreen()
        {
            return green;
        }
        public byte GetBlue()
        {
            return blue;
        }
        public static RGB FromHex(string hex)
        {
            return new(ColorTranslator.FromHtml(hex).R, ColorTranslator.FromHtml(hex).G, ColorTranslator.FromHtml(hex).B);
        }
        public override string ToString()
        {
            return $"{red}, {green}, {blue}";
        }
        public static RGB FindDifference(RGB rgb1, RGB rgb2)
        {
            return new RGB(Convert.ToByte(Math.Abs(Convert.ToInt32(rgb2.red) - Convert.ToInt32(rgb1.red))), Convert.ToByte(Math.Abs(Convert.ToInt32(rgb2.green) - Convert.ToInt32(rgb1.green))), Convert.ToByte(Math.Abs(Convert.ToInt32(rgb2.blue) - Convert.ToInt32(rgb1.blue))));
        }
        public static int FindTotalDifference(RGB rgb1, RGB rgb2)
        {
            return FindDifference(rgb1, rgb2).red + FindDifference(rgb1, rgb2).green + FindDifference(rgb1, rgb2).blue;
        }
        public static byte Abs(byte input)
        {
            return Convert.ToByte(Math.Abs(Convert.ToInt32(input)));
        }

        static string DecToHexa(int n)
        {

            // char array to store
            // hexadecimal number
            char[] hexaDeciNum = new char[2];

            // Counter for hexadecimal
            // number array
            int i = 0;
            while (n != 0)
            {

                // Temporary variable to
                // store remainder
                int temp = 0;

                // Storing remainder in
                // temp variable.
                temp = n % 16;

                // Check if temp < 10
                if (temp < 10)
                {
                    hexaDeciNum[i] = (char)(temp + 0x30);
                    i++;
                }
                else
                {
                    hexaDeciNum[i] = (char)(temp + 0x41 - 10);
                    i++;
                }
                n = n / 16;
            }
            string hexCode = "";

            if (i == 2)
            {
                hexCode += hexaDeciNum[1];
                hexCode += hexaDeciNum[0];
            }
            else if (i == 1)
            {
                hexCode = "0";
                hexCode += hexaDeciNum[0];
            }
            else if (i == 0)
                hexCode = "00";

            // Return the equivalent
            // hexadecimal color code
            return hexCode;
        }

        // Function to convert the
        // RGB code to Hex color code
        public static string ToHex(RGB rgb)
        {
            if ((rgb.red >= 0 && rgb.red <= 255) &&
                (rgb.green >= 0 && rgb.green <= 255) &&
                (rgb.blue >= 0 && rgb.blue <= 255))
            {
                string hexCode = "#";
                hexCode += DecToHexa(rgb.red);
                hexCode += DecToHexa(rgb.green);
                hexCode += DecToHexa(rgb.blue);

                return hexCode;
            }

            // The hex color code doesn't exist
            else
                return "-1";
        }
        public static RGB FindAverage(RGB[] rGBs)
        {
            int totalR = 0;
            int totalG = 0;
            int totalB = 0;

            for (int i = 0; i < rGBs.Length; i++)
            {
                totalR += rGBs[i].GetRed();
                totalG += rGBs[i].GetGreen();
                totalB += rGBs[i].GetBlue();
            }
            return new(Convert.ToByte(totalR / rGBs.Length), Convert.ToByte(totalG / rGBs.Length), Convert.ToByte(totalB / rGBs.Length));
        }
    }

    class Glass
    {
        string type;
        RGB color;

        public Glass(string t, RGB c)
        {
            type = t;
            color = c;
        }

        static Glass[] glasses = 
        { 
            new("black", RGB.FromHex("#1D1D21")),
            new("red", RGB.FromHex("#B02E26")),
            new("green", RGB.FromHex("#5E7C16")),
            new("brown", RGB.FromHex("#835432")),
            new("blue", RGB.FromHex("#3C44AA")),
            new("purple", RGB.FromHex("#8932B8")),
            new("cyan", RGB.FromHex("#169C9C")),
            new("lightGray", RGB.FromHex("#9D9D97")),
            new("gray", RGB.FromHex("#474F52")),
            new("pink", RGB.FromHex("#F38BAA")),
            new("lime", RGB.FromHex("#80C71F")),
            new("yellow", RGB.FromHex("#FED83D")),
            new("lightBlue", RGB.FromHex("#3AB3DA")),
            new("magenta", RGB.FromHex("#C74EBD")),
            new("orange", RGB.FromHex("#F9801D")),
            new("white", RGB.FromHex("#F9FFFE"))
        };
        public RGB GetColor()
        {
            return color;
        }

        public new string GetType()
        {
            return type;
        }

        // Step 1: Ziel R/G/B mit allen Glass-Blöcken vergleichen
        // Step 2: Gucken wo die kleinste Differenz ist.
        // Step 3: 
        // Step 4: 
        // Step 5:

        public static Glass FindClosestGlass(RGB target, int maxGlasses) //maxGlasses can be a maximum of 6 since the color does not change after 6 glass blocks
        {
            int bestInt = 0;
            int bestDifference = 765;
            int difference;
            for (int i = 0; i < 16; i++)
            {
                difference = RGB.FindTotalDifference(target, glasses[i].color);
                if (difference < bestDifference)
                {
                    bestInt = i;
                    bestDifference = difference;
                }
            }
            return glasses[bestInt];
        }

        public static RGB FindBestCombination(RGB target)
        {
            int glass1 = 0;
            int glass2 = 0;
            int glass3 = 0;
            int glass4 = 0;
            int glass5 = 0;
            int glass6 = 0;

            Glass[] currentComb = new Glass[6];
            int bestDifference = 765;
            RGB bestColor = new RGB(0, 0, 0);
            int difference = 0;
            RGB color = new RGB(0, 0, 0);
            
            for (int i = 0; (i < Math.Pow(16, 6) / 2) ||(bestDifference == 0); i++)
            {
                currentComb[0] = glasses[glass1];
                currentComb[1] = glasses[glass2];
                currentComb[2] = glasses[glass3];
                currentComb[3] = glasses[glass4];
                currentComb[4] = glasses[glass5];
                currentComb[5] = glasses[glass6];

                color = FindAverageColor(currentComb);
                difference = RGB.FindTotalDifference(target, color);

                if (difference < bestDifference)
                {
                    bestDifference = difference;
                    bestColor = color;
                }
            }
            return bestColor;
        }

        public static RGB FindAverageColor(Glass[] glasses) //Glasses can be a maximum of 6 since the color does not change after 6 glass blocks
        {
            RGB[] colors = new RGB[glasses.Length];

            for (int i = 0; i < glasses.Length; i++)
            {
                colors[i] = glasses[i].color;
            }
            return RGB.FindAverage(colors);
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RGB target = RGB.FromHex("#FF4500");
            RGB output = Glass.FindBestCombination(target);
            lbl_output.Content = output.ToString();
            lbl_output.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            lbl_output.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(output.GetRed(), output.GetGreen(), output.GetBlue()));
        }
    }
}