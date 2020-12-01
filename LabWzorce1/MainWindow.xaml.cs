using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

            OkButton.Visibility = Visibility.Visible;
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
            CurrentOrderLabel.Visibility = Visibility.Visible;
            CurrentOrderList.Visibility = Visibility.Visible;
            saveOrderBtn.Visibility = Visibility.Visible;
            ClearOrderBtn.Visibility = Visibility.Visible;
        }
        void HideCurrentOrderElements()
        {
            labelChooseType.Visibility = Visibility.Hidden;
            TypeComboBox.Visibility = Visibility.Hidden;
            SaveRims.Visibility = Visibility.Hidden;
            OkButton.Visibility = Visibility.Hidden;
            RimsComboBox.Visibility = Visibility.Hidden;
            labelChooseRims.Visibility = Visibility.Hidden;
            textBoxLeft.Visibility = Visibility.Hidden;
            textBoxRight.Visibility = Visibility.Hidden;
            labelSetDefect.Visibility = Visibility.Hidden;
            labelRight.Visibility = Visibility.Hidden;
            labelLeft.Visibility = Visibility.Hidden;
            BtnSaveDefect.Visibility = Visibility.Hidden;
            OptionList.Visibility = Visibility.Hidden;
            CurrentOrderLabel.Visibility = Visibility.Hidden;
            CurrentOrderList.Visibility = Visibility.Hidden;
            saveOrderBtn.Visibility = Visibility.Hidden;
            ClearOrderBtn.Visibility = Visibility.Hidden;
        }
        void ClearCurrentComboBoxValues()
        {
            TypeComboBox.Items.Clear();
            RimsComboBox.Items.Clear();
        }
        void BtnsReenable()
        {
            GlassesProductBtn.IsEnabled = true;
            OkButton.IsEnabled = true;
            TypeComboBox.IsEnabled = true;
            SaveRims.IsEnabled = true;
            RimsComboBox.IsEnabled = true;
            BtnSaveDefect.IsEnabled = true;
            textBoxRight.IsEnabled = true;
            textBoxRight.Text = "";
            textBoxLeft.IsEnabled = true;
            textBoxLeft.Text = "";

        }
        void OkButtonClick(object sender, RoutedEventArgs e)
        {
            FillOptionList();
            var selectedType = TypeComboBox.Text;
            if (!string.IsNullOrEmpty(selectedType))
            {
                OkButton.IsEnabled = false;
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
                FillRimsOptions();
                RefreshCurrentOrderList();
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

            SaveRims.Visibility = Visibility.Visible;
            SaveRims.Click += new RoutedEventHandler(SaveRimsBtnClick);
        }
        void SaveRimsBtnClick(object sender, RoutedEventArgs e)
        {
            SaveRims.IsEnabled = false;
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
            RefreshCurrentOrderList();
        }
        void FillOptionList()
        {
            TheList = new ObservableCollection<BoolStringClass>();
            TheList.Add(new BoolStringClass { TheText = "UV filter"});
            TheList.Add(new BoolStringClass { TheText = "Polarized"});
            TheList.Add(new BoolStringClass { TheText = "For computer filter"});
            this.DataContext = this;
            //foreach(CheckBox x in OptionList.Items)
            //{
            //    x.IsChecked = false;
            //}
                
        }
        void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;
            string checkedContent = chkZone.Content.ToString();
            switch (checkedContent)
            {
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
            //chkZone.IsEnabled = false;
            RefreshCurrentOrderList();
        }
        void CheckBoxZone_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;
            string checkedContent = chkZone.Content.ToString();
            switch (checkedContent)
            {
                case "UV filter":
                    var uvFilter = new Filters(FiltersEnum.UV);
                    glassAdditions.RemoveElement(uvFilter);
                    break;
                case "Polarized":
                    var polarFilter = new Filters(FiltersEnum.Polarized);
                    glassAdditions.RemoveElement(polarFilter);
                    break;
                case "For computer filter":
                    var compFilter = new Filters(FiltersEnum.ForComputer);
                    glassAdditions.RemoveElement(compFilter);
                    break;
            }
            RefreshCurrentOrderList();
        }

        void ResetCurrentValues()
        {
            ClearCurrentComboBoxValues();
            BtnsReenable();
            HideCurrentOrderElements();
            glasses = null;
            glassAdditions = null;
            orderList = null;
            TheList = null;
        }
        void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            ResetCurrentValues();
        }
        void SaveOrderBtn(object sender, RoutedEventArgs e)
        {
            glassAdditions.AddToGlasses(glasses);
            foreach (IComposite el in glasses.AdditionList)
            {
                glasses.Price += el.Price;
            }
            string path = @"Test.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Orders:");
                    sw.WriteLine("-------------");
                }
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("type:" + glasses._type);
                sw.WriteLine("lenses type:" + glasses.Lenses);
                sw.WriteLine("lenses price:" + glasses.LensesPrice);
                sw.WriteLine("Rims:" + glasses.Rims);
                sw.WriteLine("Rims price:" + glasses.RimsPrice);
                foreach (IComposite el in glasses.AdditionList)
                {
                    sw.WriteLine(el.Name + " " + el.Price);
                }
                sw.WriteLine($"Total price: {glasses.Price}"); 
                sw.WriteLine("-------------");

            }
            ResetCurrentValues();
        }
        void RefreshCurrentOrderList()
        {
            CurrentOrderList.Items.Clear();
            var glassesType = new TextBlock();
            glassesType.Text = $"Glasses type: {glasses._type}";
            var lensesType = new TextBlock();
            lensesType.Text = $"Lenses type: {glasses.Lenses}, {glasses.LensesPrice}";
            var rimsType = new TextBlock();
            rimsType.Text = $"Rims type: {glasses.Rims}, {glasses.RimsPrice}";

            CurrentOrderList.Items.Add(glassesType);
            CurrentOrderList.Items.Add(lensesType);
            CurrentOrderList.Items.Add(rimsType);
            double tempPrice = 0;
            foreach (IComposite el in glassAdditions.AddedProducts)
            {
                var temp = new TextBlock();
                temp.Text = $"{el.Name}, {el.Price}";
                tempPrice += el.Price;
                CurrentOrderList.Items.Add(temp);
            }
            var totalPrice = new TextBlock();
            totalPrice.Text = $"Total price: {glasses.Price + tempPrice}";
            CurrentOrderList.Items.Add(totalPrice);

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
                glassAdditions.AddElement(defect);
                RefreshCurrentOrderList();
            }
        }

    }
    public class BoolStringClass
    {
        public string TheText { get; set; }
    }
}
