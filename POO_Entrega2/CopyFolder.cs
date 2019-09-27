using System;
using System.IO;
namespace POO_Entrega2
{
    public static class CopyFolder
    {
        public static void CopyFolders(string sourceDirectory)
        {
            string targetDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            Copy(sourceDirectory, targetDirectory);
            Console.WriteLine("\r\nCopy finished");

            void Copy(string SourceDirectory, string TargetDirectory)
            {
                var diSource = new DirectoryInfo(SourceDirectory);
                var diTarget = new DirectoryInfo(TargetDirectory);

                CopyAll(diSource, diTarget);
            }

            void CopyAll(DirectoryInfo source, DirectoryInfo target)
            {
                Directory.CreateDirectory(target.FullName);

                // Copy each file into the new directory.
                foreach (FileInfo fi in source.GetFiles())
                {
                    Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);

                }

                // Copy each subdirectory using recursion.
                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);
                    CopyAll(diSourceSubDir, nextTargetSubDir);
                }
            }
        }
    }
}