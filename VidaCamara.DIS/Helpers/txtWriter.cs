using System;
using System.IO;
using System.Text;

namespace VidaCamara.DIS.Helpers
{
    public class txtWriter
    {
        public bool writer(StringBuilder content)
        {
            try
            {
                using (var file = new StreamWriter(@"C:\text\prueba1.txt"))
                {
                    file.Write(content);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
