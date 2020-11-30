using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



namespace LabWzorce1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<BoolStringClass> TheList { get; set; }
        public List<GlassesBuilder> orderList;
        public GlassProduct glassAdditions;
        //public VisionExpress visionexpress;
        public Glasses glasses;
        public MainWindow()
        {
            InitializeComponent();
            orderList = new List<GlassesBuilder>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TypeComboBox.Visibility = Visibility.Visible;
            labelChooseType.Visibility = Visibility.Visible;
            CreateGlasses();
            GlassesProductBtn.IsEnabled = false;

            //create visibility func
        }
        private void CreateGlasses()
        {
            var multi = new ComboBoxItem();
            multi.Content = "Multifocal";
            var anti = new ComboBoxItem();
            anti.Content = "Antireflective";
            var prog = new ComboBoxItem();
            //prog.PreviewMouseLeftButtonDown
            prog.Content = "Progressive";

            TypeComboBox.Items.Add(multi);
            TypeComboBox.Items.Add(anti);
            TypeComboBox.Items.Add(prog);

            var continueButton = new Button();
            continueButton.Content = "OK";
            continueButton.HorizontalAlignment = HorizontalAlignment.Left;
            continueButton.Margin = new Thickness(672, 236, 0, 0);
            continueButton.VerticalAlignment = VerticalAlignment.Top;
            continueButton.Height = 22;
            continueButton.Width = 44;
            continueButton.Name = "OkButtonClick";
            continueButton.Click += new RoutedEventHandler(OkButtonClick);
            mainGrid.Children.Add(continueButton);
        }
        void ShowHiddenGlassesElements()
        {
            RimsComboBox.Visibility = Visibility.Visible;
            labelChooseRims.Visibility = Visibility.Visible;
            textBoxLeft.Visibility = Visibility.Visible;
            textBoxRight.Visibility = Visibility.Visible;
            labelSetDefect.Visibility = Visibility.Visible;
            labelRight.Visibility = Visibility.Visible; 
            labelLeft.Visibility = Visibility.Visible;
            BtnSaveDefect.Visibility = Visibility.Visible;
            OptionList.Visibility = Visibility.Visible; 
        }
        void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedType = TypeComboBox.Text;
            if (!string.IsNullOrEmpty(selectedType))
            {
                TypeComboBox.IsEnabled = false;
                ShowHiddenGlassesElements();
                VisionExpress visionExpress = new VisionExpress();
                switch (selectedType)
                {
                    case "Multifocal":
                        GlassesBuilder builderMulti = new MultifocalBuilder();
                        visionExpress.ConstructGlasses(builderMulti);
                        glasses = builderMulti.Glasses;
                        break;
                    case "Progressive":
                        GlassesBuilder builderProg = new ProgressiveBuilder();
                        visionExpress.ConstructGlasses(builderProg);
                        glasses = builderProg.Glasses;
                        break;
                    case "Antireflective":
                        GlassesBuilder builderAnti = new AntireflectiveBuilder();
                        visionExpress.ConstructGlasses(builderAnti);
                        glasses = builderAnti.Glasses;
                        break;

                }
                glassAdditions = new GlassProduct();
                FillOptionList();
                FillRimsOptions();
            }

        }
        void FillRimsOptions()
        {
            RimsComboBox.Items.Insert(0, "Standard");
            RimsComboBox.SelectedIndex = 0;
            var gucciRims = new ComboBoxItem();
            gucciRims.Content = "Gucci rims";
            var armaniRims = new ComboBoxItem();
            armaniRims.Content = "Armani rims";
            var diorRims = new ComboBoxItem();
            diorRims.Content = "Dior rims";

            RimsComboBox.Items.Add(gucciRims);
            RimsComboBox.Items.Add(armaniRims);
            RimsComboBox.Items.Add(diorRims);

            //var saveRimsBtn = new Button();
            //saveRimsBtn.Content = "OK";
            //saveRimsBtn.HorizontalAlignment = HorizontalAlignment.Left;
            //saveRimsBtn.Margin = new Thickness(562, 495, 0, 0);
            //saveRimsBtn.VerticalAlignment = VerticalAlignment.Top;
            //saveRimsBtn.Height = 22;
            //saveRimsBtn.Width = 59;
            //saveRimsBtn.Name = "Saverims";
            SaveRims.Visibility = Visibility.Visible;
            SaveRims.Click += new RoutedEventHandler(SaveRimsBtnClick);
            //mainGrid.Children.Add(saveRimsBtn);
        }
        void SaveRimsBtnClick(object sender, RoutedEventArgs e)
        {
            SaveRims.IsEnabled = false;
            //zdezaktywowac ten button ktory wywolal funkcje
            var selectedRims = RimsComboBox.Text;
            if (!string.IsNullOrEmpty(selectedRims))
            {
                RimsComboBox.IsEnabled = false;
                switch (selectedRims)
                {
                    case "Gucci rims":
                        var gucciRims = new Rims(RimTypesEnum.Gucci);
                        glasses.Rims = gucciRims.Name;
                        glasses.RimsPrice = gucciRims.Price;
                        glasses.Price += gucciRims.Price;
                        break;
                    case "Armani rims":
                        var armaniRims = new Rims(RimTypesEnum.Armani);
                        glasses.Rims = armaniRims.Name;
                        glasses.RimsPrice = armaniRims.Price;
                        glasses.Price += armaniRims.Price;
                        break;
                    case "Dior rims":
                        var diorRims = new Rims(RimTypesEnum.Dior);
                        glasses.Rims = diorRims.Name;
                        glasses.RimsPrice = diorRims.Price;
                        glasses.Price += diorRims.Price;
                        break;
                }
                if(selectedRims != "Standard")
                    glasses.Price -= 100;
            }
        }
        void FillOptionList()
        {
            TheList = new ObservableCollection<BoolStringClass>();
            TheList.Add(new BoolStringClass { TheText = "UV filter"});
            TheList.Add(new BoolStringClass { TheText = "Polarized"});
            TheList.Add(new BoolStringClass { TheText = "For computer filter"});
            this.DataContext = this;
        }
        void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;
            string checkedContent = chkZone.Content.ToString();
            switch (checkedContent)
            {
                //case "Gucci rims":
                //    var gucciRims = new Rims(RimTypesEnum.Gucci);
                //    glasses.Rims = gucciRims.Name;
                //    glasses.RimsPrice = gucciRims.Price;
                //    glasses.Price += gucciRims.Price;
                //    break;
                //case "Armani rims":
                //    var armaniRims = new Rims(RimTypesEnum.Armani);
                //    glasses.Rims = armaniRims.Name;
                //    glasses.RimsPrice = armaniRims.Price;
                //    glasses.Price += armaniRims.Price;
                //    break;
                //case "Dior rims":
                //    var diorRims = new Rims(RimTypesEnum.Dior);
                //    glasses.Rims = diorRims.Name;
                //    glasses.RimsPrice = diorRims.Price;
                //    glasses.Price += diorRims.Price;
                //    break;
                case "UV filter":
                    var uvFilter = new Filters(FiltersEnum.UV);
                    glassAdditions.AddElement(uvFilter);
                    break;
                case "Polarized":
                    var polarFilter = new Filters(FiltersEnum.Polarized);
                    glassAdditions.AddElement(polarFilter);
                    break;
                case "For computer filter":
                    var compFilter = new Filters(FiltersEnum.ForComputer);
                    glassAdditions.AddElement(compFilter);
                    break;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
            //save order btn
        {
            glassAdditions.AddToGlasses(glasses);
            MessageBox.Show(glasses.AdditionList.Count + "");
            foreach (IComposite el in glasses.AdditionList)
            {
                MessageBox.Show(el.Name + " " + el.Price);
                glasses.Price += el.Price;
            }
            //glasses.Price += glasses.RimsPrice;
            //glasses.Price += glasses.LensesPrice;
            MessageBox.Show(glasses.Lenses);
            MessageBox.Show(glasses.LensesPrice.ToString()+"lenses price");
            MessageBox.Show(glasses.Rims.ToString());
            MessageBox.Show(glasses.RimsPrice.ToString()+"Rims price");
            MessageBox.Show(glasses.Price.ToString() +"total price");


            //private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
            //{
            //    CheckBox chkZone = (CheckBox)sender;
            //    ZoneText.Text = "Selected Zone Name= " + chkZone.Content.ToString();
            //    ZoneValue.Text = "Selected Zone Value= " + chkZone.Tag.ToString();
            //}

        }

        private void BtnSaveDefect_Click(object sender, RoutedEventArgs e)
        {
            double defectL, defectR;
            bool isDoubleLeft = Double.TryParse(textBoxLeft.Text, out defectL);
            bool isDoubleRight = Double.TryParse(textBoxRight.Text, out defectR);
            bool flag1 = false;
            bool flag2 = false;
            if (isDoubleLeft && defectL <= 30 && defectL >= -30)
            {
                textBoxLeft.IsEnabled = false;
                flag1 = true;
            }
            else
            {
                MessageBox.Show("Wrong value of left defect, type value between -30 and 30");
                textBoxLeft.Text = "";
            }
            if (isDoubleRight && defectR <= 30 && defectR >= -30)
            {
                textBoxRight.IsEnabled = false;
                flag2 = true;
            }
            else
            {
                MessageBox.Show("Wrong value of right defect, type value between -30 and 30");
                textBoxRight.Text = "";
            }
            if (flag1 && flag2)
            {
                BtnSaveDefect.IsEnabled = false;
                var defect = new DefectValue(defectL, defectR);
                MessageBox.Show(defect.Name +" " +defect.Price);
                glassAdditions.AddElement(defect);
            }
        }

    }
    public class BoolStringClass
    {
        public string TheText { get; set; }
    }
}
