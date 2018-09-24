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
           
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtMuestreo.Text);

            Senal senal;

            //Declarar las opciones para senalar en el vombo
            switch (cbTipoSenal.SelectedIndex)
            {
                
                case 0:
                   double amplitud = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion.Children[0])).txtFrecuencia.Text);

                    senal = new SenalSenoidal(5, 0, 8);
                    break;

                case 1: senal = new Rampa(); break;
                case 2: senal = new Signo(); break;
                case 3: senal = new Parabolica(); break;
                case 4:  
                        double alfa = double.Parse(((ConfiguracionExponencial)(stackPanelConfiguracion.Children[0])).txtAlfa.Text);

                    senal = new SenalExponencial(alfa);
                    break;
                default:
                    senal = null;
                    break;
            }

            senal.TiempoInicial = tiempoInicial;
            senal.TiempoFinal = tiempoFinal;
            senal.FrecuenciaMuestreo = frecuenciaMuestreo;

            senal.construirSenalDigital();
                
            plnGrafica.Points.Clear();


            if (senal != null)
            {


                //Recorrer una coleccion o arreglo
                foreach (Muestra muestra in senal.Muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width, (muestra.Y / senal.AmplitudMaxima * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
                }

                lblAmplitudMaxY.Text = senal.AmplitudMaxima.ToString();
                lblAmplitudNegY.Text = "-" + senal.AmplitudMaxima.ToString();

            }



            plnEjeX.Points.Clear();
            //Punto del principio
            plnEjeX.Points.Add(new Point(0,  (scrContenedor.Height / 2)));
            //Punto del final
            plnEjeX.Points.Add(new Point(tiempoFinal + scrContenedor.Width , scrContenedor.Height/2));

            plnEjeY.Points.Clear();
            //Punto del principio
            plnEjeY.Points.Add(new Point((0 - tiempoInicial) * scrContenedor.Width, (senal.AmplitudMaxima * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            //Punto del final
            plnEjeY.Points.Add(new Point((0 - tiempoInicial) * scrContenedor.Width, (-senal.AmplitudMaxima * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));

            
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

        private void btnGraficasSigno_click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txtTiempoInicial.Text); //TODAS LAS SENALES OCUPAN ESTAS 3 VARIABLES
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtMuestreo.Text);

            Signo senalSigno = new Signo();


            double periodoMuestreo = 1 / frecuenciaMuestreo;
            plnGrafica.Points.Clear();


            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestral = senalSigno.evaluar(i);


                senalSigno.Muestras.Add(new Muestra(i, valorMuestral));

            }

            //Recorrer una coleccion o arreglo
            foreach (Muestra muestra in senalSigno.Muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.X * scrContenedor.Width, (muestra.Y * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));


            }
        }

        private void btnParabolico_Click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txtTiempoInicial.Text); //TODAS LAS SENALES OCUPAN ESTAS 3 VARIABLES
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtMuestreo.Text);

            Parabolica senalParabolica = new Parabolica();


            double periodoMuestreo = 1 / frecuenciaMuestreo;
            plnGrafica.Points.Clear();


            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestral = senalParabolica.evaluar(i);


                senalParabolica.Muestras.Add(new Muestra(i, valorMuestral));

            }

            //Recorrer una coleccion o arreglo
            foreach (Muestra muestra in senalParabolica.Muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.X * scrContenedor.Width, (muestra.Y * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));


            }

        }

        private void cbTipoSenal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            stackPanelConfiguracion.Children.Clear();
            switch (cbTipoSenal.SelectedIndex)
            {
                case 0: //Senoidal
                 
                    stackPanelConfiguracion.Children.Add(new ConfiguracionSenoidal());
                    break;

                case 1: // Rampa
                    //Nomas se limpia, pero arriba ya se hizo
                    break;

                case 4:
                    stackPanelConfiguracion.Children.Add(new ConfiguracionExponencial());
                    break;
                default:
                    break;

            }
        }
    }
}

   

