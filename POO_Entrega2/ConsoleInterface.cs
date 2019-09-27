using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace POO_Entrega2
{
    public class ConsoleInterface
    {


        public ConsoleInterface()
        {

        }

        public void program()
        {
            while (true)
            {
                Console.WriteLine("Importar             [1]");
                Console.WriteLine("Ver Imagenes         [2]");
                Console.WriteLine("Buscar Imagenes      [3]");
                Console.WriteLine("Buscar por Etiquetas [4]");
                Console.WriteLine("Editar imagen        [5]");
                Console.WriteLine("Salir del programa   [0]");
                string key = Console.ReadLine();
                if (key == "0")
                {
                    break;
                }
                switch (key)
                {
                    case "1": //importar archivos
                        Console.WriteLine("Imagen           [1]");
                        Console.WriteLine("Carpetas         [2]");
                        Console.WriteLine("Volver           [0]");
                        string key1 = Console.ReadLine();
                        switch (key1)
                        {
                            case "1":
                                Console.Write("Ingrese direccion de la imagen");
                                string path1 = Console.ReadLine();
                                Console.WriteLine("Ingrese nombre de la imagen");
                                string name = Console.ReadLine();
                                try
                                {
                                    CopyPicture.CopyAPicture(path1, name);
                                    Bitmap bitmap = new Bitmap(path1);
                                    Picture img = new Picture(bitmap);
                                    TemporalPictureList.AddPic(img);

                                }
                                catch (IOException copyError)
                                {
                                    Console.WriteLine(copyError.Message);
                                }
                                catch (UnauthorizedAccessException permissionError)
                                {
                                    Console.WriteLine(permissionError.Message);
                                    Console.WriteLine("File doesn't exists");
                                }
                                break;
                            case "2":
                                Console.WriteLine("Ingrese direccion de la carpeta");
                                try
                                {
                                    CopyFolder.CopyFolders(Console.ReadLine());
                                }
                                catch (DirectoryNotFoundException NotFound)
                                {
                                    Console.WriteLine(NotFound.Message);
                                }
                                break;
                            case "0":
                                break;
                            default:
                                Console.WriteLine($"Error: {key1} not implemented");
                                break;
                        }
                        break;
                    case "2": // FALTA AGREGAR FUNCIONALIDAD DE MOSTRAR IMAGENES POR BITMAP
                        int counter = 0;

                        foreach (string name in TemporalPictureList.ShowNames())
                        {
                            counter++;
                            Console.WriteLine($"[{counter}] {name}");
                        }
                        break;
                    case "3": //COMPLETADO  mostrar caracteristicas de la imagen

                        Console.Write("Ingrese el nombre de la imagen: ");
                        string name1 = Console.ReadLine();

                        try
                        {
                            foreach (string p in TemporalPictureList.ShowPicture(name1))
                            {
                                Console.WriteLine(p);
                            }

                        }
                        catch (KeyNotFoundException KeyNotFound)
                        {
                            Console.WriteLine(KeyNotFound.Message);
                        }
                        break;
                    /*
                case "4"://BUSQUEDA DE ETIQUETAS/personas

                    Console.Write("Ingrese las etiquetas por la cual quiere filtrar separadas por un enter: ");
                    Console.WriteLine("para finalizar lista pulse enter");
                    List<string> searchs = new List<string>();
                    while (true)
                    {
                        string label = Console.ReadLine();
                        if (label == "")
                        {

                            break;
                        }
                        searchs.Add(label);
                    }

                    int c = 0;
                    List<Picture> search = new List<Picture>();
                    TemporalPictureList temporalpiclist = new TemporalPictureList();
                    List<string> notFound = new List<string>();
                    foreach (string lb in searchs)
                    {

                        if (c == 0)
                        {
                            search = pictureList.Search(lb);
                            if (search.Count == 0)
                            {
                                notFound.Add(lb);
                                continue;
                            }

                            temporalpiclist.AddListOfPics(search);
                            c++;
                        }
                        search = temporalpiclist.Search(lb);
                        if (search.Count == 0)
                        {
                            notFound.Add(lb);
                            continue;
                        }
                        temporalpiclist = new TemporalPictureList();
                        temporalpiclist.AddListOfPics(search);
                    }
                    search = temporalpiclist.Copy();

                    if (c == 0)
                    {
                        Console.WriteLine("Etiquetas invalidas"); ;
                    }
                    else
                    {
                        foreach (Picture i in search)
                        {
                            Console.WriteLine(i.namePic);
                        }
                        foreach (string i in notFound)
                        {
                            Console.WriteLine($"Etiqueta de {i} no ha sido encontrada");
                        }

                    }
                    break;
                    */
                    case "5":// editar imagen
                        Console.Write("Ingrese el nombre de la imagen a editar: ");
                        string name3 = Console.ReadLine();
                        try
                        {
                            foreach (string p in TemporalPictureList.ShowPicture(name3))
                            {
                                Console.WriteLine(p);
                            }

                        }
                        catch (KeyNotFoundException KeyNotFound)
                        {
                            Console.WriteLine(KeyNotFound.Message);

                            break;
                        }

                        Boolean bo = true;

                        while (bo)
                        {
                            Console.WriteLine("Editar             ");
                            Console.WriteLine("Nombre             [1]");
                            Console.WriteLine("Location           [2]");
                            Console.WriteLine("Photographer       [3]");
                            Console.WriteLine("Adress             [4]");
                            Console.WriteLine("Saturation         [5]");
                            Console.WriteLine("Resolution         [6]");
                            Console.WriteLine("Aspect Ratio       [7]");
                            Console.WriteLine("Label              [8]");
                            Console.WriteLine("Etiquetados        [9]");
                            Console.WriteLine("Volver             [0]");
                            string name2 = Console.ReadLine();
                            if (name2 == "0")
                            {
                                break;
                            }
                            Console.WriteLine("Ingrese nuevo valor: ");
                            string val = Console.ReadLine();
                            switch (name2)
                            {
                                case "0":
                                    bo = false;
                                    break;
                                case "1"://Name
                                    TemporalPictureList.album[name3].namePic = val;
                                    break;
                                case "2"://Location
                                    TemporalPictureList.album[name3].location = val;
                                    break;
                                case "3"://Photographer
                                    TemporalPictureList.album[name3].photographer = val;
                                    break;
                                case "4"://Adress  
                                    TemporalPictureList.album[name3].adress = val;
                                    break;
                                case "5"://Saturation 
                                    TemporalPictureList.album[name3].saturation = val;
                                    break;
                                case "6"://Resolution   
                                    TemporalPictureList.album[name3].resolution = val;
                                    break;
                                case "7"://Aspect Ratio
                                    TemporalPictureList.album[name3].adress = val;
                                    break;
                                case "8"://Label   
                                    TemporalPictureList.album[name3].NewLabel(val);
                                    break;
                                case "9"://Etiquetados    
                                    TemporalPictureList.album[name3].AddPerson(new Person(val));
                                    break;

                                default:
                                    break;
                            }
                            try
                            {
                                foreach (string p in TemporalPictureList.ShowPicture(name3))
                                {
                                    Console.WriteLine(p);
                                }

                            }
                            catch (KeyNotFoundException KeyNotFound)
                            {
                                Console.WriteLine(KeyNotFound.Message);
                            }


                        }

                        break;
                    case "z"://genera imagenes para poder probar el programa
                        Person p1 = new Person("pedro", "male");
                        Person p2 = new Person("juan", "male");
                        Person p3 = new Person("diego", "male");
                        Picture pic1 = new Picture("img1.1", "playa", "antonio", "00.00.0");
                        TemporalPictureList.AddPic(pic1);
                        Picture pic2 = new Picture("img2.1", "monte", "antonio", "00.00.0");
                        TemporalPictureList.AddPic(pic2);
                        Picture pic3 = new Picture("img3.1", "monte", "juan", "00.00.0");
                        TemporalPictureList.AddPic(pic3);
                        Picture pic4 = new Picture("img4.1", "playa", "juan", "00.00.0");
                        pic4.persons.Add(new Person("catalina", "female"));
                        pic4.persons.Add(p1);
                        TemporalPictureList.AddPic(pic4);
                        Picture pic5 = new Picture("img5.1", "playa", "pedro", "00.00.0");
                        pic5.AddPerson(p1);
                        pic5.AddPerson(p2);
                        pic5.AddPerson(p3);
                        TemporalPictureList.AddPic(pic5);
                        TemporalPictureList.Default();
                        break;

                    default:
                        Console.WriteLine($"Error: {key} not implemented");
                        break;
                }
            }
        }
    }
}

