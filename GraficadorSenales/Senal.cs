using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSenales
{
    abstract class Senal
    {
        public List<Muestra> Muestras { get; set; }
        public double AmplitudMaxima { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaMuestreo { get; set; }


        public abstract double evaluar(double tiempo);

        public void construirSenalDigital()
        {
            double periodoMuestreo = 1 / FrecuenciaMuestreo;

            for (double i = TiempoInicial; i <= TiempoFinal; i += periodoMuestreo)
            {
                double valorMuestral = evaluar(i);

                Muestras.Add(new Muestra(i, valorMuestral));
                if (Math.Abs(valorMuestral) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(valorMuestral);

                }



            }
        }

        public void escalar(double factor)
        {
            foreach (Muestra muestra in Muestras)
            {
                muestra.Y = muestra.Y * factor;
            }
        }

        public void desplazamientoY(double factor)
        {
            foreach (Muestra muestra in Muestras)
            {
                muestra.Y = muestra.Y + factor;
            }
        }

        public void actualizarAmplitudMaxima()
        {
            AmplitudMaxima = 0;
            foreach (Muestra muestra in Muestras)
            {
                if (Math.Abs(muestra.Y) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(muestra.Y);
                }

            }
        }

        public void Truncar(double n)
        {
            foreach (Muestra muestra in Muestras)
            {
               if(muestra.Y > n)
                {
                    muestra.Y = n;
                }
               
               else if (muestra.Y < -n)
                {
                    muestra.Y = -n;
                }
            }

        }
    }
}