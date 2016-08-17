using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidaCamara.DIS.Negocio
{
    public class Correo
    {

        enum PrioridadCorreo
        {

            Alta = 1,

            Baja = 2,

            Normal = 2,
        }

        private string _Para;

        private string _De;

        private string _Asunto;

        private string _Cuerpo;

        private string _CC;

        private string _CCO;

        private string _Archivo;

        private System.Net.Mail.Attachment _Adjuntar;

        public System.Net.Mail.Attachment Adjuntar
        {
            get
            {
                return _Adjuntar;
            }
            set
            {
                _Adjuntar = value;
            }
        }

        public string Archivo
        {
            get
            {
                return _Archivo;
            }
            set
            {
                _Archivo = value;
            }
        }

        public string CCO
        {
            get
            {
                return _CCO;
            }
            set
            {
                _CCO = value;
            }
        }

        public string CC
        {
            get
            {
                return _CC;
            }
            set
            {
                _CC = value;
            }
        }

        public string Cuerpo
        {
            get
            {
                return _Cuerpo;
            }
            set
            {
                _Cuerpo = value;
            }
        }

        public string Asunto
        {
            get
            {
                return _Asunto;
            }
            set
            {
                _Asunto = value;
            }
        }

        public string De
        {
            get
            {
                return _De;
            }
            set
            {
                _De = value;
            }
        }

        public string Para
        {
            get
            {
                return _Para;
            }
            set
            {
                _Para = value;
            }
        }
    }
}
