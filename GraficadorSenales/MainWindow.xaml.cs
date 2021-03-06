﻿using System;
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
            Senal segundaSenal;

            //Declarar las opciones para senalar en el vombo
            switch (cbTipoSenal.SelectedIndex)
            {

                case 0:
                    double amplitud = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion.Children[0])).txtFrecuencia.Text);

                    senal = new SenalSenoidal(amplitud, fase, frecuencia);
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

            //Para la segunda senal
            switch (cbTipoSenal_Copy.SelectedIndex)
            {

                case 0:
                    double amplitud = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion_Copy.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion_Copy.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSenoidal)(stackPanelConfiguracion_Copy.Children[0])).txtFrecuencia.Text);

                    segundaSenal = new SenalSenoidal(amplitud, fase, frecuencia);
                    break;

                case 1: segundaSenal = new Rampa(); break;
                case 2: segundaSenal = new Signo(); break;
                case 3: segundaSenal = new Parabolica(); break;
                case 4:
                    double alfa = double.Parse(((ConfiguracionExponencial)(stackPanelConfiguracion_Copy.Children[0])).txtAlfa.Text);

                    segundaSenal = new SenalExponencial(alfa);
                    break;
                default:
                    segundaSenal = null;
                    break;
            }

            senal.TiempoInicial = tiempoInicial;
            senal.TiempoFinal = tiempoFinal;
            senal.FrecuenciaMuestreo = frecuenciaMuestreo;
            senal.construirSenalDigital();


            //segunda senal
            segundaSenal.TiempoInicial = tiempoInicial;
            segundaSenal.TiempoFinal = tiempoFinal;
            segundaSenal.FrecuenciaMuestreo = frecuenciaMuestreo;
            segundaSenal.construirSenalDigital();


            //Escalar
            double factorEscala = double.Parse(txtFactorEscalaAmplitud.Text);
            senal.escalar(factorEscala);

            //Desplazamiento Y
            double factorDesplazamientoY = double.Parse(txtDesplazamientoY.Text);
            senal.desplazamientoY(factorDesplazamientoY);

            //Truncar
            double Umbral = double.Parse(txtTruncar.Text);
            senal.Truncar(Umbral);

            senal.actualizarAmplitudMaxima();

            plnGrafica.Points.Clear();


            if (senal != null)
            {


                //Recorrer una coleccion o arreglo
                foreach (Muestra muestra in senal.Muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width, (muestra.Y / senal.AmplitudMaxima * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
                }

                lblAmplitudMaxY.Text = senal.AmplitudMaxima.ToString("F");
                lblAmplitudNegY.Text = "-" + senal.AmplitudMaxima.ToString("F");

            }



            plnEjeX.Points.Clear();
            //Punto del principio
            plnEjeX.Points.Add(new Point(0, (scrContenedor.Height / 2)));
            //Punto del final
            plnEjeX.Points.Add(new Point(tiempoFinal + scrContenedor.Width, scrContenedor.Height / 2));

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


        private void tbTruncar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void chbDesplazamientoY_Checked(object sender, RoutedEventArgs e)
        {
            if (chbDesplazamientoY.IsChecked == true)
            {
                // If checked, do not allow items to be dragged onto the form.  
                txtDesplazamientoY.IsEnabled = true;
            }

            else
            {
                txtDesplazamientoY.IsEnabled = false;
            }
        }

        private void chbDesplazamientoY_Checked2(object sender, RoutedEventArgs e)
        {
            if (chbDesplazamientoY2.IsChecked == true)
            {
                // If checked, do not allow items to be dragged onto the form.  
                txtDesplazamientoY2.IsEnabled = true;
            }

            else
            {
                txtDesplazamientoY2.IsEnabled = false;
            }
        }

        private void chbEscala_Checked(object sender, RoutedEventArgs e)
        {
            if (chbEscala.IsChecked == true)
            {
                // If checked, do not allow items to be dragged onto the form.  
                txtFactorEscalaAmplitud.IsEnabled = true;
            }

            else
            {
                txtFactorEscalaAmplitud.IsEnabled = false;
            }
        }

        private void chbEscala_Checked2(object sender, RoutedEventArgs e)
        {
            if (chbEscala2.IsChecked == true)
            {
                // If checked, do not allow items to be dragged onto the form.  
                txtFactorEscalaAmplitud2.IsEnabled = true;
            }

            else
            {
                txtFactorEscalaAmplitud2.IsEnabled = false;
            }
        }


        private void chbTruncar_Checked(object sender, RoutedEventArgs e)
        {
            if (chbTruncar.IsChecked == true)
            {
                // If checked, do not allow items to be dragged onto the form.  
                txtTruncar.IsEnabled = true;
            }

            else
            {
                txtTruncar.IsEnabled = false;
            }
        }

        private void chbTruncar_Checked2(object sender, RoutedEventArgs e)
        {
            if (chbTruncar2.IsChecked == true)
            {
                // If checked, do not allow items to be dragged onto the form.  
                txtTruncar2.IsEnabled = true;
            }

            else
            {
                txtTruncar2.IsEnabled = false;
            }
        }





        private void cbTipoSenal_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stackPanelConfiguracion_Copy.Children.Clear();
            switch (cbTipoSenal_Copy.SelectedIndex)
            {
                case 0:
                    stackPanelConfiguracion_Copy.Children.Add(new ConfiguracionSenoidal());
                    break;

                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    stackPanelConfiguracion_Copy.Children.Add(new ConfiguracionExponencial());
                    break;

                    /*<ComboBoxItem Content="Senal Senoidal"/>
            <ComboBoxItem Content="Senal Rampa"/>
            <ComboBoxItem Content="Senal Signo"/>
            <ComboBoxItem Content="Senal Parabolica"/>
            <ComboBoxItem Content="Senal Exponencial"/>*/

            }
        }
    }
}


