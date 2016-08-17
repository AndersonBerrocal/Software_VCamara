using System;
using VidaCamara.DIS.Negocio;

namespace VidaCamara.DIS.Helpers
{
    public class ValidaRegla
    {
        public int validacionDeCampos(Regla r, string CampoActual)
        {
            if (r.TipoCampo.Equals("DATETIME"))
                return paValidaFecha(CampoActual);
            else
                return 0;
        }

        public static int paValidaFecha(string valor,DateTime inicio = new DateTime()) {
            try
            {
                if (valor.Length != 8)
                    return 0;
                else {
                    var p = valor.Substring(0,3);
                    var datetime = new DateTime(Convert.ToInt32(valor.Substring(0,4)),Convert.ToInt32(valor.Substring(4,2)),Convert.ToInt32(valor.Substring(6,2)));
                    DateTime.Parse(datetime.ToString());
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int pa_valida_Numero7x2(string valor) {
            try
            {
                double.Parse(valor);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int pa_valida_CUSPP(string valor) {
            try
            {
                var parse = 0;
                if (Int32.TryParse(valor.Substring(1, 6), out parse) == true || Int32.TryParse(valor.Substring(12, 1), out parse) == true || Int32.TryParse(valor.Substring(7, 5), out parse) == false)
                    return 1;
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
