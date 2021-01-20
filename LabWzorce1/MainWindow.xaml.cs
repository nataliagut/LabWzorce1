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
        public List<GlassProductBuilder> orderList;
        public GlassProduct glassAdditions;
        public Glasses glasses;
        public ContactLenses contactLenses;
        public GlassProductDefectCommand glassAddDefectCommand;
        public MainWindow()
        {
            InitializeComponent();
            orderList = new List<GlassProductBuilder>();
        }
        #region Logic
        void CreateGlassProduct(object sender, RoutedEventArgs e)
        {
            TypeComboBox.Visibility = Visibility.Visible;
            labelChooseType.Visibility = Visibility.Visible;
            Button srcButton = e.Source as Button;
            string eventBtnName = srcButton.Name;
            RenderTypes(eventBtnName);
            GlassesProductBtn.IsEnabled = false;
            ContactLensesBtn.IsEnabled = false;
        }
        void OkButtonLensesClick(object sender, RoutedEventArgs e)
        {
            var selectedType = TypeComboBox.Text;
            if (!string.IsNullOrEmpty(selectedType))
            {
                OkButtonLenses.IsEnabled = false;
                TypeComboBox.IsEnabled = false;
                ShowHiddenCLElements();
                string product = "lenses";
                VisionExpress visionExpress = new VisionExpress();
                switch (selectedType)
                {
                    case "Multifocal":
                        GlassProductBuilder builderMulti = new MultifocalBuilder(product);
                        visionExpress.ConstructContactLenses(builderMulti);
                        contactLenses = builderMulti._ContactLenses;
                        break;
                    case "Progressive":
                        GlassProductBuilder builderProg = new ProgressiveBuilder(product);
                        visionExpress.ConstructContactLenses(builderProg);
                        contactLenses = builderProg._ContactLenses;
                        break;
                    case "Antireflective":
                        GlassProductBuilder builderAnti = new AntireflectiveBuilder(product);
                        visionExpress.ConstructContactLenses(builderAnti);
                        contactLenses = builderAnti._ContactLenses;
                        break;

                }
                glassAdditions = new GlassProduct();
                glassAddDefectCommand = new GlassProductDefectCommand();
                RenderCheckBox();
                FillColorsOptions();
                RefreshCurrentOrderList();
            }
        }
        void OkButtonGlassesClick(object sender, RoutedEventArgs e)
        {
            var selectedType = TypeComboBox.Text;
            if (!string.IsNullOrEmpty(selectedType))
            {
                OkButtonGlasses.IsEnabled = false;
                TypeComboBox.IsEnabled = false;
                ShowHiddenGlassesElements();
                string product = "glasses";
                VisionExpress visionExpress = new VisionExpress();
                switch (selectedType)
                {
                    case "Multifocal":
                        GlassProductBuilder builderMulti = new MultifocalBuilder(product);
                        visionExpress.ConstructGlasses(builderMulti);
                        glasses = builderMulti.Glasses;
                        break;
                    case "Progressive":
                        GlassProductBuilder builderProg = new ProgressiveBuilder(product);
                        visionExpress.ConstructGlasses(builderProg);
                        glasses = builderProg.Glasses;
                        break;
                    case "Antireflective":
                        GlassProductBuilder builderAnti = new AntireflectiveBuilder(product);
                        visionExpress.ConstructGlasses(builderAnti);
                        glasses = builderAnti.Glasses;
                        break;

                }
                glassAdditions = new GlassProduct();
                glassAddDefectCommand = new GlassProductDefectCommand();
                RenderCheckBox();
                FillRimsOptions();
                RefreshCurrentOrderList();
            }
        }

        void RenderTypes(string eventBtnName)
        {
            var multi = new ComboBoxItem();
            multi.Content = "Multifocal";
            var anti = new ComboBoxItem();
            anti.Content = "Antireflective";
            var prog = new ComboBoxItem();
            prog.Content = "Progressive";

            TypeComboBox.Items.Add(multi);
            TypeComboBox.Items.Add(anti);
            TypeComboBox.Items.Add(prog);
            if (eventBtnName == "GlassesProductBtn")
                OkButtonGlasses.Visibility = Visibility.Visible;
            else
                OkButtonLenses.Visibility = Visibility.Visible;
        }
        void RenderCheckBox()
        {
            List<string> options = new List<string>() { "UV filter", "Polarized", "For computer filter" };

            foreach (string t in options)
            {
                var ch = new CheckBox();
                ch.Checked += Ch_Checked;
                ch.Unchecked += Ch_Unchecked;
                ch.Content = t;
                optionsST.Children.Add(ch);
            }
            //foreach (string t in Enum.GetNames(typeof(FiltersEnum)))
            //{
            //    var ch = new CheckBox();
            //    ch.Checked += Ch_Checked;
            //    ch.Unchecked += Ch_Unchecked;
            //    ch.Content = t;
            //    optionsST.Children.Add(ch);
            //}
        }
        void Ch_Unchecked(object sender, RoutedEventArgs e)
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
        void Ch_Checked(object sender, RoutedEventArgs e)
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
            RefreshCurrentOrderList();
        }

        void BtnChangeDefect_Click(object sender, RoutedEventArgs e)
        {
            glassAddDefectCommand.Undo();
            RefreshCurrentOrderList();
            textBoxLeft.IsEnabled = true;
            textBoxRight.IsEnabled = true;
            BtnSaveDefect.IsEnabled = true;
            BtnUndoDefect.Visibility = Visibility.Hidden;
            BtnSaveDefect.Visibility = Visibility.Visible;
        }
        void BtnSaveDefect_Click(object sender, RoutedEventArgs e)
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
                BtnUndoDefect.Visibility = Visibility.Visible;
                BtnSaveDefect.Visibility = Visibility.Hidden;
                var defect = new DefectValue(defectL, defectR);
                glassAddDefectCommand.DefectCmd(glassAdditions, WhatToDoEnum.AddElement, defect);
                glassAddDefectCommand.Call();
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
                if (selectedRims != "Standard")
                    glasses.Price -= 100;
            }
            RefreshCurrentOrderList();
        }

        void FillColorsOptions()
        {
            ColorsComboBox.SelectedIndex = 0;
            foreach (string name in Enum.GetNames(typeof(ContactLensesColorsEnum)))
            {
                var color = new ComboBoxItem();
                color.Content = name;
                ColorsComboBox.Items.Add(color);
            }
            SaveColor.Click += new RoutedEventHandler(SaveColorBtnClick);
        }
        void ChangeColorBtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsComboBox.SelectedIndex = 0;
            contactLenses.colorPrice = 0;
            contactLenses.cLColor = ContactLensesColorsEnum.Standard;

            RefreshCurrentOrderList();
            ColorsComboBox.IsEnabled = true;
            ChangeColorBtn.Visibility = Visibility.Hidden;
            SaveColor.Visibility = Visibility.Visible;
            SaveColor.IsEnabled = true;
        }
        void SaveColorBtnClick(object sender, RoutedEventArgs e)
        {
            var selectedColor = ColorsComboBox.Text;
            if (!string.IsNullOrEmpty(selectedColor))
            {
                ChangeColorBtn.Visibility = Visibility.Visible;
                SaveColor.Visibility = Visibility.Hidden;
                ColorsComboBox.IsEnabled = false;
                switch (selectedColor)
                {
                    case "Standard":
                        contactLenses.cLColor = ContactLensesColorsEnum.Standard;
                        contactLenses.colorPrice = 0;
                        break;
                    case "Gray":
                        contactLenses.cLColor = ContactLensesColorsEnum.Gray;
                        contactLenses.colorPrice = 30;
                        break;
                    case "Red":
                        contactLenses.cLColor = ContactLensesColorsEnum.Red;
                        contactLenses.colorPrice = 40;
                        break;
                    case "Green":
                        contactLenses.cLColor = ContactLensesColorsEnum.Green;
                        contactLenses.colorPrice = 35;
                        break;
                    case "Blue":
                        contactLenses.cLColor = ContactLensesColorsEnum.Blue;
                        contactLenses.colorPrice = 40;
                        break;
                    case "Yellow":
                        contactLenses.cLColor = ContactLensesColorsEnum.Yellow;
                        contactLenses.colorPrice = 20;
                        break;
                    case "Pink":
                        contactLenses.cLColor = ContactLensesColorsEnum.Pink;
                        contactLenses.colorPrice = 30;
                        break;
                }
            }
            RefreshCurrentOrderList();
        }
#endregion
        #region Interface clearing methods
        void ShowHiddenGlassesElements()
        {
            RimsComboBox.Visibility = Visibility.Visible;
            labelChooseRims.Visibility = Visibility.Visible;
            SaveRims.Visibility = Visibility.Visible;
            ShowCommonElements();
        }
        void ShowHiddenCLElements()
        {
            ColorsComboBox.Visibility = Visibility.Visible;
            labelChooseCLColor.Visibility = Visibility.Visible;
            SaveColor.Visibility = Visibility.Visible;
            ShowCommonElements();
        }
        void ShowCommonElements()
        {
            optionsST.Visibility = Visibility.Visible;
            textBoxLeft.Visibility = Visibility.Visible;
            textBoxRight.Visibility = Visibility.Visible;
            labelSetDefect.Visibility = Visibility.Visible;
            labelRight.Visibility = Visibility.Visible;
            labelLeft.Visibility = Visibility.Visible;
            BtnSaveDefect.Visibility = Visibility.Visible;
            CurrentOrderLabel.Visibility = Visibility.Visible;
            CurrentOrderList.Visibility = Visibility.Visible;
            saveOrderBtn.Visibility = Visibility.Visible;
            ClearOrderBtn.Visibility = Visibility.Visible;
        }
        void HideCurrentOrderElements()
        {
            ChangeColorBtn.Visibility = Visibility.Hidden;
            optionsST.Visibility = Visibility.Hidden;
            BtnUndoDefect.Visibility = Visibility.Hidden;
            labelChooseType.Visibility = Visibility.Hidden;
            TypeComboBox.Visibility = Visibility.Hidden;
            OkButtonGlasses.Visibility = Visibility.Hidden;
            OkButtonLenses.Visibility = Visibility.Hidden;
            RimsComboBox.Visibility = Visibility.Hidden;
            labelChooseRims.Visibility = Visibility.Hidden;
            SaveRims.Visibility = Visibility.Hidden;
            ColorsComboBox.Visibility = Visibility.Hidden;
            labelChooseCLColor.Visibility = Visibility.Hidden;
            SaveColor.Visibility = Visibility.Hidden;
            textBoxLeft.Visibility = Visibility.Hidden;
            textBoxRight.Visibility = Visibility.Hidden;
            labelSetDefect.Visibility = Visibility.Hidden;
            labelRight.Visibility = Visibility.Hidden;
            labelLeft.Visibility = Visibility.Hidden;
            BtnSaveDefect.Visibility = Visibility.Hidden;
            CurrentOrderLabel.Visibility = Visibility.Hidden;
            CurrentOrderList.Visibility = Visibility.Hidden;
            saveOrderBtn.Visibility = Visibility.Hidden;
            ClearOrderBtn.Visibility = Visibility.Hidden;
        }
        void ClearCurrentComboBoxValues()
        {
            ColorsComboBox.Items.Clear();
            TypeComboBox.Items.Clear();
            RimsComboBox.Items.Clear();
        }
        void BtnsReenable()
        {
            ContactLensesBtn.IsEnabled = true;
            GlassesProductBtn.IsEnabled = true;
            OkButtonGlasses.IsEnabled = true;
            OkButtonLenses.IsEnabled = true;
            TypeComboBox.IsEnabled = true;
            SaveRims.IsEnabled = true;
            RimsComboBox.IsEnabled = true;
            SaveColor.IsEnabled = true;
            ColorsComboBox.IsEnabled = true;
            BtnSaveDefect.IsEnabled = true;
            textBoxRight.IsEnabled = true;
            textBoxRight.Text = "";
            textBoxLeft.IsEnabled = true;
            textBoxLeft.Text = "";
        }
        void ResetCurrentValues()
        {
            ClearCurrentComboBoxValues();
            BtnsReenable();
            HideCurrentOrderElements();
            contactLenses = null;
            glasses = null;
            glassAdditions = null;
            orderList = null;
            optionsST.Children.Clear();
            SaveRims.Click -= SaveRimsBtnClick;
        }
        void RefreshCurrentOrderList()
        {
            CurrentOrderList.Items.Clear();
            if (glasses != null)
            {
                var glassesType = new TextBlock();
                glassesType.Text = $"Glasses type: {glasses._type}";
                var lensesType = new TextBlock();
                lensesType.Text = $"Lenses type: {glasses.Lenses}, price: {glasses.LensesPrice:c}";
                var rimsType = new TextBlock();
                rimsType.Text = $"Rims type: {glasses.Rims}, price: {glasses.RimsPrice:c}";
                CurrentOrderList.Items.Add(glassesType);
                CurrentOrderList.Items.Add(lensesType);
                CurrentOrderList.Items.Add(rimsType);
            }
            else
            {
                var cLType = new TextBlock();
                cLType.Text = $"Contact lenses type: {contactLenses._type}";
                var basePrice = new TextBlock();
                basePrice.Text = $"Base price: {contactLenses.Price:c}";
                var clColor = new TextBlock();
                clColor.Text = $"Color: {contactLenses.cLColor}";
                var clColorPrice = new TextBlock();
                CurrentOrderList.Items.Add(cLType);
                CurrentOrderList.Items.Add(basePrice);
                CurrentOrderList.Items.Add(clColor);
                if (contactLenses.cLColor.ToString() != "Standard")
                {
                    clColorPrice.Text = $"Color price: {contactLenses.colorPrice:c}";
                    CurrentOrderList.Items.Add(clColorPrice);
                }
            }
            double tempPrice = 0;
            foreach (IComposite el in glassAdditions.AddedProducts)
            {
                var temp = new TextBlock();
                var price = el.Price;
                if (glasses == null)
                    price /= 100;
                temp.Text = $"{el.Name}, price: {price:c}";
                tempPrice += price;
                CurrentOrderList.Items.Add(temp);
            }
            var totalPrice = new TextBlock();
            if (glasses != null)
                totalPrice.Text = $"Total price: {(glasses.Price + tempPrice):c}";
            else
                totalPrice.Text = $"Total price: {(contactLenses.Price + +contactLenses.colorPrice + tempPrice):c}";
            CurrentOrderList.Items.Add(totalPrice);
        }
        #endregion
        #region Order methods
        void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            ResetCurrentValues();
        }
        void SaveOrderBtn(object sender, RoutedEventArgs e)
        {
            double baseCLPrice = 0;
            if (contactLenses != null)
                baseCLPrice = contactLenses.Price;
            if (glasses != null)
            {
                glassAdditions.AddToGlasses(glasses);
                foreach (IComposite el in glasses.AdditionList)
                {
                    glasses.Price += el.Price;
                }
            }
            else
            {
                glassAdditions.AddToContactLenses(contactLenses);
                foreach (IComposite el in contactLenses.AdditionList)
                    contactLenses.Price += el.Price / 100;
            }
            string path = @"Test.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Orders:");
                    sw.WriteLine("-------------");
                }
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                if (glasses != null)
                {
                    sw.WriteLine("Glasses");
                    sw.WriteLine("type: " + glasses._type);
                    sw.WriteLine("lenses type: " + glasses.Lenses);
                    sw.WriteLine($"lenses price: {glasses.LensesPrice:c}");
                    sw.WriteLine("Rims: " + glasses.Rims);
                    sw.WriteLine($"Rims price: {glasses.RimsPrice:c}");
                    foreach (IComposite el in glasses.AdditionList)
                        sw.WriteLine($"{el.Name}, price: {el.Price:c}");
                    sw.WriteLine($"Total price: {glasses.Price:c}");
                    sw.WriteLine("-------------");
                }
                else
                {
                    sw.WriteLine("Contact lenses");
                    sw.WriteLine("type: " + contactLenses._type);
                    sw.WriteLine($"base price: {baseCLPrice:c}");
                    sw.WriteLine("color: " + contactLenses.cLColor);
                    if (contactLenses.cLColor.ToString() != "Standard")
                        sw.WriteLine($"color price: {contactLenses.colorPrice:c}");
                    foreach (IComposite el in contactLenses.AdditionList)
                        sw.WriteLine($"{el.Name}, price: {(el.Price / 100):c}");
                    sw.WriteLine($"Total price: {contactLenses.Price + contactLenses.colorPrice:c}");
                    sw.WriteLine("-------------");
                }

            }
            ResetCurrentValues();
        }
        #endregion
    }
}
