using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Entrega_3
{
    public partial class FotoApp : Form
    {
        public FotoApp()
        {
            InitializeComponent();
            ImportSubMenuPanel.Visible = false;
            SearchSubMenuPanel.Visible = false;
            PicturesSubMenuPanel.Visible = false;
            SlideShowSubMenuPanel.Visible = false;
            EditSubMenuPanel.Visible = false;
        }

        //METHODS--------------------------------------------------------------------------------------------------------------------------------------

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
        void saveAs()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog(); // create a new save file dialog object
                sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                ImageFormat format = ImageFormat.Png;// you want to store it in by default format
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string ext = Path.GetExtension(sfd.FileName);
                    switch (ext)
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }
                    pbShowPictures.Image.Save(sfd.FileName, format);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No image loaded, first upload image ");
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
                        Image file = Image.FromFile(fi.FullName.ToString());
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
            StaticPicture.Update();
            InfoLabel.Text = StaticPicture.GetInfo();
            pbShowPictures.Image = StaticPicture.BmpCopy;
            UpdateFeatures();
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
        bool b = true;
        void UpdateListBox()
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
            b = false;
            StaticPicture.Update();
        }
        Color SelectColor()
        {
            Color color;
            colorDialog1.ShowDialog();
            color = colorDialog1.Color;

            return color;
        }
        void UpdateListBoxFeatures()
        {
            PeopleListBoxColection.Items.Clear();
            foreach (string name in StaticAlbum.ShowPeopleNames())
            {
                PeopleListBoxColection.Items.Add(name);
            }

            LabelListBoxColection.Items.Clear();
            foreach (string name in StaticAlbum.ShowLabelNames())
            {
                LabelListBoxColection.Items.Add(name);
            }

            PictureLabelListBox.Items.Clear();
            foreach (string name in StaticPicture.ShowLabelListPic())
            {
                PictureLabelListBox.Items.Add(name);
            }

            PicturePeopleListBox.Items.Clear();
            foreach (string name in StaticPicture.ShowPersonListPic())
            {
                PicturePeopleListBox.Items.Add(name);
            }

            TagPeopleListBox.Items.Clear();
            foreach (string name in StaticPicture.ShowPersonListPic())
            {
                TagPeopleListBox.Items.Add(name);
            }
        }
        void UpdatePeopoleListBox()
        {
            TagPeopleListBox.Items.Clear();
            PeopleListBoxColection.Items.Clear();
            foreach (Person p in StaticAlbum.peopleList)
            {
                PeopleListBoxColection.Items.Add(p.Name);
                TagPeopleListBox.Items.Add(p.Name);
            }
        }
        void UpdateLabelListBox()
        {
            LabelListBoxColection.Items.Clear();
            foreach (Label lbl in StaticAlbum.labelList)
            {
                LabelListBoxColection.Items.Add(lbl.Name);
            }
        }
        void UpdateNPA()
        {
            StaticPicture.namePic = textBoxPictureName.Text;
            StaticPicture.photographer = textBoxPicturePhotographer.Text;
            StaticPicture.adress = textBoxPeopleAdress.Text;
            StaticPicture.Update();
            UpdateListBoxFeatures();
            Update();
        }
        void UpdateFeatures()
        {
            textBoxPictureName.Text = StaticPicture.namePic;
            textBoxPicturePhotographer.Text = StaticPicture.photographer;
            textBoxPeopleAdress.Text = StaticPicture.adress;
            lblAspectRatioLabel.Text = StaticPicture.aspectRatio;
            lblResolutionLabel.Text = StaticPicture.resolution;
            lblSaturationLabel.Text = StaticPicture.saturation;
            StaticPicture.Update();
        }
        void UpdateListPicturebox()
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
        void UpdatePictureListBox()
        {
            lbPictureListBox.Items.Clear();
            foreach (string name in StaticAlbum.ShowNames())
            {
                lbPictureListBox.Items.Add(name);
            }
        }

        //TOP MENU EVENTS------------------------------------------------------------------------------------------------------------------------------

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            TopMenu.Width = ActiveForm.Width;
            btnRestore.Visible = true;
            btnMaximize.Visible = false;
        }
        private void BtnRestore_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            TopMenu.Width = ActiveForm.Width;
            btnRestore.Visible = false;
            btnMaximize.Visible = true;
        }
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            if (SidebarGradient.Width == 270)
            {
                SidebarGradient.Width = 55;
                Left.Width = 74;
                LayoutPanelLeft.Width = 81;
                Separator.Width = 53;
                MenuIcon1.Visible = true;
                //SubMenuHandling
                ImportSubMenuPanel.Visible = false;
                SearchSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
            }
            else
            {
                SidebarGradient.Width = 270;
                Left.Width = 293;
                LayoutPanelLeft.Width = 300;
                Separator.Width = 258;
                MenuIcon1.Visible = false;
                //SubMenuHandling
                ImportSubMenuPanel.Visible = false;
                SearchSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;

            }
        }

        //SIDEBAR MENU EVENTS--------------------------------------------------------------------------------------------------------------------------

        private void BtnImport_Click(object sender, EventArgs e)
        {
            if (SidebarGradient.Width == 270)
            {
                SearchSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                if (ImportSubMenuPanel.Visible)
                {
                    ImportSubMenuPanel.Visible = false;
                }
                else
                {
                    ImportSubMenuPanel.Location = new Point(302, 86);
                    ImportSubMenuPanel.Visible = true;
                }
            }
            else
            {
                SearchSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                if (ImportSubMenuPanel.Visible)
                {
                    ImportSubMenuPanel.Visible = false;
                }
                else
                {
                    ImportSubMenuPanel.Location = new Point(84, 86);
                    ImportSubMenuPanel.Visible = true;
                }
            }
        }
        private void BtnPictures_Click(object sender, EventArgs e)
        {
            UpdatePictureListBox();
            if (SidebarGradient.Width == 270)
            {
                SearchSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
                ImportSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                if (PicturesSubMenuPanel.Visible)
                {
                    PicturesSubMenuPanel.Visible = false;
                }
                else
                {
                    PicturesSubMenuPanel.Location = new Point(302, 86);
                    PicturesSubMenuPanel.Visible = true;
                }
            }
            else
            {
                SearchSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
                ImportSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                if (PicturesSubMenuPanel.Visible)
                {
                    PicturesSubMenuPanel.Visible = false;
                }
                else
                {
                    PicturesSubMenuPanel.Location = new Point(84, 86);
                    PicturesSubMenuPanel.Visible = true;
                }
            }
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (SidebarGradient.Width == 270)
            {
                //TextBox field reset
                //txtSearchAspect1.text = "Aspect"; txtSearchAspect2.text = "Ratio"; txtSearchMark.text = "Mark"; txtSearchResolution1.text = "Resolution";
                //txtSearchResolution2.text = "Resolution"; txtSearchSaturation.text = "Saturation"; txtSearchValue.text = "Value";

                ImportSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                if (SearchSubMenuPanel.Visible)
                {
                    SearchSubMenuPanel.Visible = false;
                }
                else
                {
                    SearchSubMenuPanel.Location = new Point(302, 86);
                    SearchSubMenuPanel.Visible = true;
                }
            }
            else
            {
                //TextBox field reset
                //txtSearchAspect1.text = "Aspect"; txtSearchAspect2.text = "Ratio"; txtSearchMark.text = "Mark"; txtSearchResolution1.text = "Resolution";
                //txtSearchResolution2.text = "Resolution"; txtSearchSaturation.text = "Saturation"; txtSearchValue.text = "Value";

                ImportSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                if (SearchSubMenuPanel.Visible)
                {
                    SearchSubMenuPanel.Visible = false;
                }
                else
                {
                    SearchSubMenuPanel.Location = new Point(84, 86);
                    SearchSubMenuPanel.Visible = true;
                }
            }
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            UpdateListPicturebox();
            if (SidebarGradient.Width == 270)
            {
                ImportSubMenuPanel.Visible = false;
                SearchSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                if (EditSubMenuPanel.Visible)
                {
                    EditSubMenuPanel.Visible = false;
                }
                else
                {
                    EditSubMenuPanel.Location = new Point(302, 86);
                    EditSubMenuPanel.Visible = true;
                }
            }
            else
            {
                ImportSubMenuPanel.Visible = false;
                SearchSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                SlideShowSubMenuPanel.Visible = false;
                if (EditSubMenuPanel.Visible)
                {
                    EditSubMenuPanel.Visible = false;
                }
                else
                {
                    EditSubMenuPanel.Location = new Point(84, 86);
                    EditSubMenuPanel.Visible = true;
                }
            }
        }
        private void BtnSlideShow_Click(object sender, EventArgs e)
        {
            if (SidebarGradient.Width == 270)
            {
                ImportSubMenuPanel.Visible = false;
                SearchSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
                if (SlideShowSubMenuPanel.Visible)
                {
                    SlideShowSubMenuPanel.Visible = false;
                }
                else
                {
                    SlideShowSubMenuPanel.Location = new Point(302, 86);
                    SlideShowSubMenuPanel.Visible = true;
                }
            }
            else
            {
                ImportSubMenuPanel.Visible = false;
                SearchSubMenuPanel.Visible = false;
                PicturesSubMenuPanel.Visible = false;
                EditSubMenuPanel.Visible = false;
                if (SlideShowSubMenuPanel.Visible)
                {
                    SlideShowSubMenuPanel.Visible = false;
                }
                else
                {
                    SlideShowSubMenuPanel.Location = new Point(84, 86);
                    SlideShowSubMenuPanel.Visible = true;
                }
            }
        }
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
        private void BtnSaveAs_Click(object sender, EventArgs e)
        {
            saveAs();
        }
        private void BtnUndo_Click(object sender, EventArgs e)
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

        //IMPORT SUBMENU EVENTS------------------------------------------------------------------------------------------------------------------------

        private void BtnImportPicture_Click(object sender, EventArgs e)
        {
            openPicture();
        }

        private void BtnImportFolder_Click(object sender, EventArgs e)
        {
            importFolder();
        }

        //EDIT SIDEBAR MENU EVENTS---------------------------------------------------------------------------------------------------------------------

        private void BtnMultiSelect_Click(object sender, EventArgs e)
        {
            UpdateListPicturebox();
            MultiSelectPanel.Visible = true;
            ColorFiltersPanel.Visible = false;
            ToolsPanel.Visible = false;
            FunnyPanel.Visible = false;
            FeaturesPanel.Visible = false;
        }
        private void BtnColorFilters_Click(object sender, EventArgs e)
        {
            MultiSelectPanel.Visible = false;
            ColorFiltersPanel.Visible = true;
            ToolsPanel.Visible = false;
            FunnyPanel.Visible = false;
            FeaturesPanel.Visible = false;
        }
        private void BtnTools_Click(object sender, EventArgs e)
        {
            MultiSelectPanel.Visible = false;
            ColorFiltersPanel.Visible = false;
            ToolsPanel.Visible = true;
            FunnyPanel.Visible = false;
            FeaturesPanel.Visible = false;
        }
        private void BtnFunny_Click(object sender, EventArgs e)
        {
            MultiSelectPanel.Visible = false;
            ColorFiltersPanel.Visible = false;
            ToolsPanel.Visible = false;
            FunnyPanel.Visible = true;
            FeaturesPanel.Visible = false;
        }
        private void BtnFeatures_Click(object sender, EventArgs e)
        {
            MultiSelectPanel.Visible = false;
            ColorFiltersPanel.Visible = false;
            ToolsPanel.Visible = false;
            FunnyPanel.Visible = false;
            FeaturesPanel.Visible = true;
        }

        //EDIT MULTI SELECT EVENTS--------------------------------------------------------------------------------------------------------------------------

        private void AllPicsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = AllPicsListBox.SelectedItem.ToString();
                Picture pic = StaticAlbum.GetPicture(name);
                LittlePictureBox.Image = pic.Bitmap;
                SelectedPicListBox.Items.Add(AllPicsListBox.SelectedItem);
                StaticAlbum.AddSelecctedPic(name);
            }
            catch (NullReferenceException)
            {

            }
        }
        private void SelectedPicListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = SelectedPicListBox.SelectedItem.ToString();
                Picture pic = StaticAlbum.GetPicture(name);
                LittlePictureBox.Image = pic.Bitmap;
                StaticAlbum.DelatePic(SelectedPicListBox.SelectedIndex);
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
        private void DeleteSelectedPicBtn_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateListPicturebox();
                StaticAlbum.DelatePic(SelectedPicListBox.SelectedIndex);
                UpdateListBox();
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        //EDIT COLORFILTER EVENTS----------------------------------------------------------------------------------------------------------------------

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
        private void BtnPersonalisedColor_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Tools.AplyColor(SelectColor(), StaticPicture.BmpCopy);
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
        }

        //-----RGB SCROLLS-----//
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

        //EDIT TOOLS EVENTS----------------------------------------------------------------------------------------------------------------------------

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

        //EDIT FUNNY EVENTS----------------------------------------------------------------------------------------------------------------------------

        private void RandomCollageBtn_Click(object sender, EventArgs e)
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
            catch (ArgumentNullException)
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
            catch (ArgumentNullException)
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
            catch (ArgumentNullException)
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
            catch (ArgumentNullException)
            {
                MessageBox.Show("No picture loaded");

            }
        }
        private void OltTimesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.PixelArt(StaticPicture.BmpCopy, Convert.ToInt32(numericUpDownPixelArt.Value));
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No picture loaded");

            }
        }
        private void AutoMosaicBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.AutoMosaic(StaticPicture.BmpCopy, Convert.ToInt32(numericUpDownAutoMosaic.Value));
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No picture loaded");

            }
        }
        private void MosaicBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.Mosaic(StaticPicture.BmpCopy, StaticAlbum.GetSelectedBitmaps(), Convert.ToInt32(numericUpDownAutoMosaic.Value));
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No picture loaded");

            }
        }
        private void BtnMosaic2_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.BmpCopy = Filters.TrapMosaic(StaticPicture.BmpCopy, StaticAlbum.GetSelectedBitmaps(), Convert.ToInt32(numericUpDownMosaic2.Value));
                Update();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No picture loaded");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No picture loaded");

            }
        }

        //EDIT FEATURES EVENTS-------------------------------------------------------------------------------------------------------------------------

            //-----EDIT FEATURES TOPMENU EVENTS-----//
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateListBox();
                UpdateListBoxFeatures();
            }
            catch (NullReferenceException)
            {

            }
            CreatePanel.Visible = true;
            LabelsPanel.Visible = false;
            PicturePanel.Visible = false;
            TagPanel.Visible = false;
        }

        private void BtnLabels_Click(object sender, EventArgs e)
        {
            
            try
            {
                UpdateListBox();
                UpdateListBoxFeatures();
            }
            catch (NullReferenceException)
            {

            }
            CreatePanel.Visible = false;
            LabelsPanel.Visible = true;
            PicturePanel.Visible = false;
            TagPanel.Visible = false;
        }

        private void BtnPicture_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateListBox();
                UpdateListBoxFeatures();
            }
            catch (NullReferenceException)
            {

            }
            CreatePanel.Visible = false;
            LabelsPanel.Visible = false;
            PicturePanel.Visible = true;
            TagPanel.Visible = false;
        }

        private void BtnTag_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateListBox();
                UpdateListBoxFeatures();
            }
            catch (NullReferenceException)
            {

            }
            CreatePanel.Visible = false;
            LabelsPanel.Visible = false;
            PicturePanel.Visible = false;
            TagPanel.Visible = true;
        }
            //------EDIT FEATURES CREATE EVENTS------//
        private void BtnCreateNewLabel_Click(object sender, EventArgs e)
        {
            Label lbl = new Label(textBoxNameLabel.Text);
            StaticAlbum.AddNewLabel(lbl);
            try
            {
                UpdateListBoxFeatures();
            }
            catch (NullReferenceException)
            {

            }
        }

        private void BtnCreateNewPerson_Click(object sender, EventArgs e)
        {
            string sex = "";
            if (MaleRadioButton1.Checked)
            {
                sex = "Male";
            }
            if (FemaleRadioButton2.Checked)
            {
                sex = "Female";
            }

            Person p = new Person(textBoxNameNewPerson.Text, sex);
            p.DateOfBirth = dateTimePicker1.Value;
            StaticAlbum.AddNewPerson(p);
            lblPersoninfo3.Text = p.GetPersonInfo();
            try
            {
                UpdateListBoxFeatures();
            }
            catch (NullReferenceException)
            {

            }
            UpdatePeopoleListBox();
        }
            //-----EDIT FEATURES LABELS EVENTS-----//
        private void PeopleListBoxColection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = PeopleListBoxColection.SelectedItem.ToString();
            Person p = StaticAlbum.SearchPerson(name);
            lblPersonInfo.Text = p.GetPersonInfo();
        }
        private void DelateLabelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticAlbum.DelateLabel(LabelListBoxColection.SelectedIndex);
                UpdateLabelListBox();
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
        private void BtnAddToActualpic_Click(object sender, EventArgs e)
        {
            try
            {
                StaticPicture.AddLabel(StaticAlbum.SearchLabel(LabelListBoxColection.SelectedItem.ToString()));
            }
            catch (NullReferenceException)
            {

            }
        }
        private void DelatePersonBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticAlbum.DelatePerson(PeopleListBoxColection.SelectedIndex);
                UpdatePeopoleListBox();
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
            //-----EDIT FEATURES PICTURE EVENTS------//
        private void BtnChangefeatures_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateNPA();
            }
            catch (NullReferenceException)
            {

            }
        }
        private void BtnDelateLabelOfPeople_Click(object sender, EventArgs e)
        {
            try
            {
                int index = PictureLabelListBox.SelectedIndex;
                StaticPicture.DelateLabel(index);
                try
                {
                    UpdateListBoxFeatures();
                }
                catch (NullReferenceException)
                {

                }
                Update();
            }
            catch (NullReferenceException)
            {

            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
        private void BtnDelatePersonOfPicture_Click(object sender, EventArgs e)
        {
            try
            {
                int index = PicturePeopleListBox.SelectedIndex;
                StaticPicture.DelatePerson(index);
                try
                {
                    UpdateListBoxFeatures();
                }
                catch (NullReferenceException)
                {

                }
                Update();
            }
            catch (NullReferenceException)
            {

            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
            //-----EDIT FEATURES TAG EVENTS-----//
        private void TagPersonBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string name = TagPeopleListBox.SelectedItem.ToString();
                Person p = StaticAlbum.SearchPerson(name);
                int X = Convert.ToInt32(lblXCoordenadas.Text);
                int Y = Convert.ToInt32(lblYCoordenadas.Text);
                StaticPicture.AddPerson(p, X, Y);
                Coordenada co = new Coordenada(X, Y, p);
                try
                {
                    StaticPicture.BmpCopy = Filters.TagPerson(co, StaticPicture.BmpCopy, Convert.ToInt32(numericUpDownFaceSize.Value));
                    Update();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("No picture loaded");
                }
            }
            catch (NullReferenceException)
            {

            }
        }
        private void TagPeopleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = TagPeopleListBox.SelectedItem.ToString();
            Person p = StaticAlbum.SearchPerson(name);
            lblPersonInfo2.Text = p.GetPersonInfo();
        }

        //PICTURE SUBMENU EVENTS-----------------------------------------------------------------------------------------------------------------------

        private void LbPictureListBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                lbPictureListBox.Items.Clear();
                foreach (string name in StaticAlbum.ShowNames())
                {
                    lbPictureListBox.Items.Add(name);
                }
            }
            catch (NullReferenceException)
            {

            }
        }
        private void LbPictureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = lbPictureListBox.SelectedItem.ToString();
                Picture pic = StaticAlbum.GetPicture(name);
                StaticPicture.CopyActualPicture(pic);
                Update();
            }
            catch (NullReferenceException)
            {

            }
        }

        //SEARCH SUBMENU EVENTS------------------------------------------------------------------------------------------------------------------------

        private void LbSearchresulltListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = lbSearchresulltListbox.SelectedItem.ToString();
                Picture pic = StaticAlbum.GetPicture(name);
                StaticPicture.CopyActualPicture(pic);
                Update();
            }
            catch (NullReferenceException)
            {

            }
        }
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            lbSearchresulltListbox.Items.Clear();
            string Search = txtbSearch.Text;
            string SearchMesage = Busqueda.Buscar(Search).Item1;
            List<Picture> SearchResult = Busqueda.Buscar(Search).Item2;
            PictureList Results = new PictureList();
            Results.AddListOfPics(SearchResult);
            foreach (string name in Results.ShowNames())
            {
                lbSearchresulltListbox.Items.Add(name);
            }
            if (SearchMesage != "")
            {
                MessageBox.Show(SearchMesage);
            }
        }

        //SLIDESHOW SUBMENU EVENTS

        private void RandomSlideShowBtn_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                return;
            }
            timer1.Enabled = true;
        }
        int cont = 0;
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

        private void PbShowPictures_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            lblXCoordenadas.Text = coordinates.X.ToString();
            lblYCoordenadas.Text = coordinates.Y.ToString();
        }
    }
}
