using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSenales
{
    class SenalSenoidal
    {
        public double Amplitud { get; set; }
        public double Fase { get; set; }
        public double Frecuencia { get; set; }

        public SenalSenoidal()
        {
            Amplitud = 1.0;
            Fase = 0.0;
            Frecuencia = 1.0;
        }

        public SenalSenoidal(double amplitud, double fase, double frecuencia)
        {
            Amplitud = amplitud;
            Fase = fase;
            Frecuencia = frecuencia;
        }

        public double evaluar(double tiempo)
        {
            double resultado;
            resultado = Amplitud * Math.Sin(((2 * Math.PI * Frecuencia) * tiempo) + Fase);
            return resultado;


        }

    }
}

