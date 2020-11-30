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
            //visionexpress = new VisionExpress();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateGlasses();
        }
        private void CreateGlasses()
        {
            var multi = new ComboBoxItem();
            multi.Content = "Multifocal";
            var anti = new ComboBoxItem();
            anti.Content = "Antireflective";
            var prog = new ComboBoxItem();
            prog.Content = "Progressive";

            ComboBox1.Items.Add(multi);
            ComboBox1.Items.Add(anti);
            ComboBox1.Items.Add(prog);

            var continueButton = new Button();
            continueButton.Content = "Get additions";
            continueButton.HorizontalAlignment = HorizontalAlignment.Left;
            continueButton.Margin = new Thickness(759, 265, 0, 0);
            continueButton.VerticalAlignment = VerticalAlignment.Top;
            continueButton.Height = 46;
            continueButton.Width = 139;
            continueButton.Name = "NextButton";
            continueButton.Click += new RoutedEventHandler(NextButtonClick);
            mainGrid.Children.Add(continueButton);
        }
        void NextButtonClick(object sender, RoutedEventArgs e)
        {

            var selectedType = ComboBox1.Text;
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

        }
        void FillOptionList()
        {
            //var checkGucciRims = new CheckBox();
            //checkGucciRims.Content = "Gucci rims";
            //OptionList.Items.Add(checkGucciRims);
            TheList = new ObservableCollection<BoolStringClass>();
            TheList.Add(new BoolStringClass { TheText = "Gucci rims"});
            TheList.Add(new BoolStringClass { TheText = "Armani rims"});
            TheList.Add(new BoolStringClass { TheText = "UV filter"});
            TheList.Add(new BoolStringClass { TheText = "Polarized"});
            this.DataContext = this;
        }
        void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;
            string checkedContent = chkZone.Content.ToString();
            switch (checkedContent)
            {
                case "Gucci rims":
                    var gucciRims = new Rims(RimTypesEnum.Gucci);
                    glasses.Rims = gucciRims.Name;
                    glasses.RimsPrice = gucciRims.Price;
                    glasses.Price += gucciRims.Price;
                    glasses.Price -= 100;
                    break;
                case "UV filter":
                    var uvFilter = new Filters(FiltersEnum.UV);
                    glasses.Price += uvFilter.Price;
                    break;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (IComposite el in glasses.AdditionList)
            {
                MessageBox.Show(el.Name + "added el");
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

    }
    public class BoolStringClass
    {
        public string TheText { get; set; }
    }
}
