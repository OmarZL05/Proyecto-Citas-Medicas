using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GMC
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Nodos.inicializarListas();

            //// Ejecucion de la interfaz grafica
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuGeneral());
             
        }

    }

    

}
