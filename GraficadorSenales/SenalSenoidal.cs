using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSenales
{
   class SenalSenoidal : Senal
    {
        public double Amplitud { get; set; }
        public double Fase { get; set; }
        public double Frecuencia { get; set; }

        
        public SenalSenoidal()
        {
            Amplitud = 1.0;
            Fase = 0.0;
            Frecuencia = 1.0;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;

        }

        public SenalSenoidal(double amplitud, double fase, double frecuencia)
        {
            Amplitud = amplitud;
            Fase = fase;
            Frecuencia = frecuencia;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }

        //Override va a buscar si el padre tiene la misma variable, y si existe, el padre es la clase Senal
        override public double evaluar(double tiempo)
        {
            double resultado;
            resultado = Amplitud * Math.Sin(((2 * Math.PI * Frecuencia) * tiempo) + Fase);
            return resultado;


        }

    }
}

