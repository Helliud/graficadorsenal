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

namespace GraficadorSenales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            plnGrafica.Points.Add(new Point(0, 6));
            plnGrafica.Points.Add(new Point(100, 50));
            plnGrafica.Points.Add(new Point(150, 46));
            plnGrafica.Points.Add(new Point(155, 70));
            plnGrafica.Points.Add(new Point(300, 100));
            plnGrafica.Points.Add(new Point(433, 68));
            plnGrafica.Points.Add(new Point(533, 100));
            plnGrafica.Points.Add(new Point(633, 50));
            plnGrafica.Points.Add(new Point(763, 68));
            plnGrafica.Points.Add(new Point(1003, 68));

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
