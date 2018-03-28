using System;
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

namespace CollectibleCardGame.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для FractionPage.xaml
    /// </summary>
    public partial class FractionPage : UserControl
    {
        public FractionPage()
        {
            InitializeComponent();
        }


        public string Fract
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Fract", typeof(string), typeof(FractionPage), null);


        // Обычное свойство .NET
        private string fractionName = null;
        public string FractionName
        {
            get { return fractionName; }
            set
            {
                fractionName = value;
                // Передаем в метку текущее значение
                Fraction.Text = FractionName;
            }
        }

        private string pathPicture=null;
        public string PathPicture
        {
            get { return pathPicture; }
            set
            {
                pathPicture = value;
                // Передаем в метку текущее значение
                AboutPicture.Source = new ImageSourceConverter().ConvertFromString(PathPicture) as ImageSource;
            }
        }

        private string fractionInfo = null;
        public string FractionInfo
        {
            get { return fractionInfo; }
            set
            {
                fractionName = value;
                // Передаем в метку текущее значение
                AboutTextBlock.Text = FractionInfo;
            }
        }

       
    }
}
