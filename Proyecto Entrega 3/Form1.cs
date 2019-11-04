using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Entrega_3
{
    public partial class Form1 : Form
    {
        int cont = 0;
        public Form1()
        {
            InitializeComponent();
        }


        //Metodos internos --------------------------------------------------------------------------------------------------------------------------------------------------


        void saveActualImage()
        {
            StaticPicture.BmpCopy.Save(StaticPicture.namePic);
        }
        void openPicture()
        {
            Image file;
            DialogResult dr = ImportPictureFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(ImportPictureFileDialog.FileName);
                Bitmap bmp = new Bitmap(file.Width, file.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(file, 0, 0, file.Width, file.Height);
                string name = ImportPictureFileDialog.FileName.Split(@"\"[0]).Last<string>().ToString();
                Picture pic = new Picture(name, bmp, ImportPictureFileDialog.FileName.ToString());
                StaticAlbum.AddPic(pic);
                UpdatePic(pic);    
                string target = AppDomain.CurrentDomain.BaseDirectory + "Saves";
                var targetDirectory = new DirectoryInfo(target);
                Directory.CreateDirectory(targetDirectory.FullName);
                FileInfo fi = new FileInfo(ImportPictureFileDialog.FileName);
                fi.CopyTo(Path.Combine(targetDirectory.FullName, fi.Name), true);
            }
        }
        void importFolder()
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                string targetDirectory = AppDomain.CurrentDomain.BaseDirectory + "Saves";
                Copy(FBD.SelectedPath, targetDirectory);
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
                        Image file = Image.FromFile(fi.FullName. ToString());
                        Bitmap bmp = new Bitmap(file.Width, file.Height);
                        Graphics g = Graphics.FromImage(bmp);
                        g.DrawImage(file, 0, 0, file.Width, file.Height);
                        string name = fi.Name.ToString();
                        Picture pic = new Picture(name, bmp, ImportPictureFileDialog.FileName.ToString());
                        StaticAlbum.AddPic(pic);
                        fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                        
                    }
                    
                }
            }
        }
        void UpdatePic(Picture pic)
        {
            StaticPicture.CopyActualPicture(pic);
            Update();
        }
        void Update()
        {
            InfoLabel.Text = StaticPicture.GetInfo(); 
            pbShowPictures.Image = (StaticPicture.BmpCopy);
        }
        void UpdateMatrixColor()
        {

        }


        //EVENTS-----------------------------------------------------------------------------------------------------------------------------------------
   

        //IMPORT ONE PICTURE
        private void BtnImportPicture_Click(object sender, EventArgs e)
        {
            openPicture();
        }
        //IMPORT FOLDER
        private void BtnImportFolder_Click_1(object sender, EventArgs e)
        {
            importFolder();
        }
        //SAVE PICTURE
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                saveActualImage();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        //UNDO
        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.Bmp, "Nothing");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }


        //COLOR FILTERS----------------------------------------------------------------------------------------------------------------------------------


        private void GreyScaleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "greyscale");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }
        private void GreenBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "green");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void SepiaBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "sepia");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void LightBlueBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "lightblue");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void RainbowBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = ColorFilter.Rainbow(StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }
        private void BlueBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "blue");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }


        }
        private void RedBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "red");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }
        private void PurpleBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "purple");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }
        private void OrangeBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "orange");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }
        private void FrozenBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "frozen");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }

        private void YellowBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "yellow");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }
        private void NegativeBtn_Click(object sender, EventArgs e)
        {


            try
            {
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "negative");
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }
        private void OldStyleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = ColorFilter.Rainbow(StaticPicture.BmpCopy);
                StaticPicture.BmpCopy = Filters.Rotate90(StaticPicture.BmpCopy);
                StaticPicture.BmpCopy = ColorFilter.Rainbow(StaticPicture.BmpCopy);
                StaticPicture.BmpCopy = Filters.Rotate90(StaticPicture.BmpCopy);
                StaticPicture.BmpCopy = Filters.Rotate90(StaticPicture.BmpCopy);
                StaticPicture.BmpCopy = ColorFilter.Transform(StaticPicture.BmpCopy, "sepia");
                StaticPicture.BmpCopy = Filters.Rotate90(StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }


        //TOOL FILTERS----------------------------------------------------------------------------------------------------------------------------------


        private void RotateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.Rotate90(StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
           
           
        }
        private void RotateXbtn_Click(object sender, EventArgs e)
        {
            
            
            try
            {
                StaticPicture.BmpCopy = Filters.RotateFlipX(StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.RotateFlipY(StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }

        private void MirrorXBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = Filters.FlipX(StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }

        private void MirrorYBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.FlipY(StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }

        private void MirrorXYBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.FlipXY(StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }

        private void SizeBtn_Click(object sender, EventArgs e)
        { 
            try
            {
                StaticPicture.BmpCopy = Filters.ReSize(StaticPicture.BmpCopy, Decimal.ToInt32(HeightUpDown.Value), Decimal.ToInt32(WidthUpDown.Value));
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }

        }

        private void RescaleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.PercentageReSize(StaticPicture.BmpCopy, Decimal.ToInt32(ReduceUpDown.Value));
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }


        //FUNNY FILTERS----------------------------------------------------------------------------------------------------------------------------------


        private void OltTimesBtn_Click(object sender, EventArgs e)
        {

        }
        private void RandomCollageBtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.Collage(StaticAlbum.GetSelectedBitmaps());
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void RandomBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = Filters.RandomCollage(StaticAlbum.GetSelectedBitmaps());
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void FibonachiBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = Filters.FibonachiCollage(StaticAlbum.GetSelectedBitmaps());
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void SideUnionBtn_Click(object sender, EventArgs e)
        {

            try
            {
                StaticPicture.BmpCopy = Filters.Combine(StaticAlbum.GetSelectedBitmaps());
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void MosaicBtn_Click(object sender, EventArgs e)
        {

        }
        private void OnePicCollageBtn_Click(object sender, EventArgs e)
        {

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------//

        private void RandomSlideShowBtn_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                return;
            }
            timer1.Enabled = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LbPictureListBox_MouseHover(object sender, EventArgs e)
        {
            lbPictureListBox.Items.Clear();
            foreach (string name in StaticAlbum.ShowNames())
            {
                lbPictureListBox.Items.Add(name);
            }
        }
        private void AllPicsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = AllPicsListBox.SelectedItem.ToString();
            Picture pic = StaticAlbum.GetPicture(name);
            LittlePictureBox.Image = pic.Bitmap;
            SelectedPicListBox.Items.Add(AllPicsListBox.SelectedItem);
            StaticAlbum.AddSelecctedPic(name);
        }
        private void AllPicsListBox_MouseHover(object sender, EventArgs e)
        {
            AllPicsListBox.Items.Clear();
            foreach (string name in StaticAlbum.ShowNames())
            {
                AllPicsListBox.Items.Add(name);
            }
            SelectedPicListBox.Items.Clear();
            foreach (string name in StaticAlbum.ShowSelectedNames())
            {
                SelectedPicListBox.Items.Add(name);
            }
        }
        private void SelectedPicListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StaticAlbum.DelatePic(SelectedPicListBox.SelectedIndex);
            
        }
        private void LbPictureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string name = lbPictureListBox.SelectedItem.ToString();
            Picture pic = StaticAlbum.GetPicture(name);
            StaticPicture.CopyActualPicture(pic);
            Update();
        }
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            lbSearchresulltListbox.Items.Clear();
            string Search = textBoxSearch.Text;
            string SearchMesage = Busqueda.Buscar(Search).Item1;
            List<Picture> SearchResult = Busqueda.Buscar(Search).Item2;
            PictureList Results = new PictureList();
            Results.AddListOfPics(SearchResult);
            foreach (string name in Results.ShowNames())
            {
                lbSearchresulltListbox.Items.Add(name);
            }
            if (SearchMesage != "Resultados no encontrados")
            {
                MessageBox.Show(SearchMesage);
            }
        }
        private void LbSearchresulltListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = lbSearchresulltListbox.SelectedItem.ToString();
            Picture pic = StaticAlbum.GetPicture(name);
            StaticPicture.CopyActualPicture(pic);
            Update();
        }
        void UpdateColorMatrix()
        {
            try
            {
                int r = RedBar.Value;
                int g = GreenBar.Value;
                int b = BlueBar.Value;
                StaticPicture.BmpCopy = Tools.AplyColor(Color.FromArgb(r, g, b), StaticPicture.Bmp);
                Update();
            }

            catch (NullReferenceException)
            {

            }
        }
        private void RedBar_Scroll(object sender, EventArgs e)
        {
            UpdateColorMatrix();
            RedValuesLbl.Text = RedBar.Value.ToString();
        }
        private void GreenBar_Scroll(object sender, EventArgs e)
        {
            UpdateColorMatrix();
            GreenValuesLbl.Text = GreenBar.Value.ToString();
        }
        private void BlueBar_Scroll(object sender, EventArgs e)
        {
            UpdateColorMatrix();
            BlueValuesLbl.Text = BlueBar.Value.ToString();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Tools.SlideShowRandomCollage(StaticAlbum.GetSelectedBitmaps(), StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void FusionSlideShowBtn_Click(object sender, EventArgs e)
        {
            if (FusionTimer.Enabled)
            {
                FusionTimer.Enabled = false;
                return;
            }
            try
            {
                cont = 0;
                StaticPicture.Bmp = Filters.Combine(StaticAlbum.GetSelectedBitmaps());
                Update();
                FusionTimer.Enabled = true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }
        private void FusionTimer_Tick(object sender, EventArgs e)
        {
            StaticPicture.BmpCopy = Tools.SlideShowFusion(StaticPicture.Bmp, cont);
            cont++;
            Update();
        }
    }
}
