using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSenales
{
    class Signo
    {

        public List<Muestra> Muestras { get; set; }
        public double AmplitudMaxima { get; set; }

        public Signo()
        {
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;

        }

        public double evaluar(double tiempo)
        {
            double resultado;

            if (tiempo == 0)
            {
                resultado = 0;
            }

            else if (tiempo < 0)
            {
                resultado = -1;
            }

            else
            {
               resultado = 1;

            }

            return resultado;
        }
    }
}


