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



        }

        private void txtTiempoFinal_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtTiempoInicial_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtMuestreo.Text);

            SenalSenoidal senal = new SenalSenoidal(amplitud, fase, frecuencia);


            double periodoMuestreo = 1 / frecuenciaMuestreo;
            plnGrafica.Points.Clear();


            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestral = senal.evaluar(i);

                if (Math.Abs(valorMuestral) > senal.AmplitudMaxima)
                {
                    senal.AmplitudMaxima = Math.Abs(valorMuestral);

                }

                senal.Muestras.Add(new Muestra(i, valorMuestral));

            }

            //Recorrer una coleccion o arreglo
            foreach(Muestra muestra in senal.Muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.X* scrContenedor.Width, (muestra.Y* ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));


            }
        }

        /*-------------------------------------*/
        private void btnGraficasRampa_click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txtTiempoInicial.Text); //TODAS LAS SENALES OCUPAN ESTAS 3 VARIABLES
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtMuestreo.Text);

            Rampa senalRampa = new Rampa();


            double periodoMuestreo = 1 / frecuenciaMuestreo;
            plnGrafica.Points.Clear();


            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestral = senalRampa.evaluar(i);

                if (Math.Abs(valorMuestral) > senalRampa.AmplitudMaxima)
                {
                    senalRampa.AmplitudMaxima = Math.Abs(valorMuestral);

                }

                senalRampa.Muestras.Add(new Muestra(i, valorMuestral));

            }

            //Recorrer una coleccion o arreglo
            foreach (Muestra muestra in senalRampa.Muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.X * scrContenedor.Width, (muestra.Y * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));


            }
        }
    }
}

   

