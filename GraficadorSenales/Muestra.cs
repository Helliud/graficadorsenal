using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSenales
{
    class Muestra
    {
        //El instante del tiempo en el que fue tomada la muestra
        public double X { get; set; }

        //El valor de esa muestra en ese instante
        public double Y { get; set; }

        //Constructor que inicializa valores
        public Muestra(double x, double y)
        {
            X = x;
            Y = y;

        }

        //Constructor sin parametro
        public Muestra()
        {
            X = 0.0;
            Y = 0.0;
        }

    }
}
