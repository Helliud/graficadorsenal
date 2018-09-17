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
    }
}
