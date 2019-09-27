
using System;
using System.IO;
using System.Drawing;
namespace POO_Entrega2
{
    public static class CopyPicture
    {
        public static void CopyAPicture(string sourceFileName, string pictureName)
        {
            string targetDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            File.Copy(sourceFileName, targetDirectory);
        }
    }
}
