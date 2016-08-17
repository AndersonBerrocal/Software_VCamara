using System;
using System.Collections.Generic;
using System.Data;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bRegistroDatoVC 
    {
        String[] mes = { "MES DEVEGUE", " ENE ", " FEB ", " MAR ", " ABR ", " MAY ", " JUN ", " JUL", " AGO ", " SEP ", " OCT ", " NOV ", " DIC ", " TOTALIZADO"};
        String[] column = { "MES-DEVENGUE", "ID", "MES-ABONO", "PRIMA-ESTIMADA" };
        public Int32 SetInsertarDatoM(List<eDatoM> o)
        {
            dSqlRegistroDatoVC dm = new dSqlRegistroDatoVC();
            return dm.SetInsertarDatoM(o);
        }
        public Int32 SetActualizarDatoM(eDatoM o) {
            dSqlRegistroDatoVC dm = new dSqlRegistroDatoVC();
            return dm.SetActualizarDatoM(o);
        }
        public Int32 SetEliminarDatoM(eDatoM o)
        {
            dSqlRegistroDatoVC dm = new dSqlRegistroDatoVC();
            return dm.SetEliminarDatoM(o);
        }
        public DataTable GetSelectDatoMGrid(eDatoM o) {
            int totalContrato;
            dSqlRegistroDatoVC dr = new dSqlRegistroDatoVC();
            DataTable dtm = dr.GetSelectDatoMGrid(o);
            DataTable dtedit = new DataTable();
            if (dtm.Rows.Count > 0)
            {
                eContratoVC ecn = new eContratoVC();
                ecn._inicio = 0;
                ecn._fin = 1000000;
                ecn._orderby = "IDE_CONTRATO ASC";
                ecn._nro_Contrato = o._nro_Contrato;
                ecn._estado = "A";

                bContratoVC bcn = new bContratoVC();
                List<eContratoVC> list = bcn.GetSelecionarContrato(ecn, out totalContrato);
                DateTime inicio_contrato = list[0]._fec_Ini_Vig;
                DateTime fin_contrato = list[0]._fec_Fin_Vig;

                Int32 mes_vigente = inicio_contrato.Month;

                for (int c = 0; c < 4; c++) {
                    dtedit.Columns.Add(column[c]);
                }
                for (int r = 0; r < dtm.Rows.Count; r++) {
                    if(mes_vigente > 12){
                        mes_vigente = 1;
                    }
                    Object[] row = new Object[dtedit.Columns.Count];
                    row[0] = SetCalcularMesDevengueString(inicio_contrato.Year, inicio_contrato.Month, r, mes_vigente);
                    for (int cl = 1; cl < dtedit.Columns.Count; cl++) {
                        if (cl == 1)
                            row[cl] = dtm.Rows[r][cl - 1];
                        else
                            row[cl] = String.Format(o._Formato_Moneda, Convert.ToDecimal(dtm.Rows[r][cl - 1]));

                    }
                    dtedit.Rows.Add(row);
                    mes_vigente++;
                }
            }
            return dtedit;
        }
        public DataTable GetSelectdatoM(eDatoM ent,out String totalContable,out String totalDevengue) {
            Int32 total_column;
            dSqlRegistroDatoVC dm = new dSqlRegistroDatoVC();
            var dt = dm.GetSelectDatoM(ent, out total_column);

            DataTable piramide = new DataTable();
            if (dt.Rows.Count != 0)
            {
                //variables
                //String[] cabecera = {"INICIO","FIN","AÑO VIGENTE","MES DEVENGUE","MES CONTABLE","MONTO ABONADO","MTO PRIMA EST."};
                DataTable dtHidden = new DataTable();
                DateTime fec_fin_vig = Convert.ToDateTime(dt.Rows[0]["FEC_FIN"]);
                DateTime fec_ini_vig = Convert.ToDateTime(dt.Rows[0]["FEC_INI"]);

                //funcion de llenado de columnas dinamicas
                int anio_inicio, anio_fin;
                int mes_inicio, mes_fin,mes_devengue_inicial;
                int meses_del_contrat = (1 + CalcularMesesDeDiferencia(fec_ini_vig, fec_fin_vig));

                //int total
                anio_inicio = fec_ini_vig.Year;
                anio_fin = fec_fin_vig.Year;

                mes_fin = fec_fin_vig.Month;
                mes_inicio = fec_ini_vig.Month;

                //FOR PARA CALCULAR COLUMNAS DE LA TABLA
                for (int cl = 0; cl < total_column; cl++) {
                    if (mes_inicio > 12) {
                        mes_inicio = 1;
                    }

                    if (cl == 0)
                    {
                        piramide.Columns.Add(mes[0]);
                        dtHidden.Columns.Add("0");
                    }
                    piramide.Columns.Add(SetCalcularMesDevengueString(anio_inicio, fec_ini_vig.Month, cl, mes_inicio));
                    dtHidden.Columns.Add(SetCalculaMesDevengue(anio_inicio, fec_ini_vig.Month, cl, mes_inicio));
                    mes_inicio++;
                }
                piramide.Columns.Add(mes[13]);
                dtHidden.Columns.Add(mes[13]);

                object[] obj = new object[piramide.Columns.Count];

                /* Llena Grilla con datos resumen */
                mes_devengue_inicial = fec_ini_vig.Month;
                int nregistro = 0;
                /*for (int o = 1; o < (meses_del_contrat + 2); o++)
                {
                    obj[o] = String.Format(ent._Formato_Moneda, 0.00m);
                }*/
                int Max_filas = meses_del_contrat;
                for (int nfila = 0; nfila < Max_filas; nfila++)
                {
                    if (nregistro < dt.Rows.Count)
                    {
                        if(mes_devengue_inicial > 12)
                               mes_devengue_inicial = 1;

                        obj[0] = SetCalcularMesDevengueString(fec_ini_vig.Year, fec_ini_vig.Month, nfila, mes_devengue_inicial);
                        Decimal costo_fila = 0.00m;
                        for (int ncolumna = 1; ncolumna < (piramide.Columns.Count -1); ncolumna++)
                        {
                            if (nregistro < dt.Rows.Count)
                            {
                                var columna1 = dt.Rows[nregistro][4].ToString();
                                var columna2 = dtHidden.Columns[ncolumna].ToString();

                                if (dt.Rows[nregistro][4].ToString().Equals(dtHidden.Columns[ncolumna].ToString()))
                                {
                                    obj[ncolumna] = String.Format(ent._Formato_Moneda, Convert.ToDecimal(dt.Rows[nregistro][5]));
                                    obj[piramide.Columns.Count - 1] = String.Format(ent._Formato_Moneda, costo_fila += Convert.ToDecimal(dt.Rows[nregistro][5]));
                                    nregistro++;
                                }
                                else
                                {
                                    obj[ncolumna] = String.Format(ent._Formato_Moneda, 0.00m);
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                    else
                    {
                        break;
                    }
                    //if (nregistro >= dt.Rows.Count) {break; }

                    piramide.Rows.Add(obj);
                    mes_devengue_inicial++;
                }
                for (int rt = 1; rt < piramide.Columns.Count; rt++)
                {
                    obj[0] = "TOT ABONO";
                    Decimal suma = 0;
                    for (int ct = 0; ct < piramide.Rows.Count; ct++)
                    {
                        suma += Convert.ToDecimal(piramide.Rows[ct][rt]);
                    }
                    obj[rt] = String.Format(ent._Formato_Moneda, suma);
                }

                piramide.Rows.Add(obj);

                //calcular variable total del mes
                Decimal mes_devengue = 0;
                Decimal mes_contable = 0;
                String condicion_mes_vigente;
                if (ent._mes_Vigente < 10)
                {
                    condicion_mes_vigente = Convert.ToString(ent._anio_Vigente + "0" + ent._mes_Vigente);
                }
                else {
                    condicion_mes_vigente = Convert.ToString(ent._anio_Vigente +""+ ent._mes_Vigente);
                }

                for (int l = 0; l < dt.Rows.Count; l++) {
                    if (Convert.ToString(condicion_mes_vigente) == Convert.ToString(dt.Rows[l][4]))
                    {
                        mes_devengue += Convert.ToDecimal(dt.Rows[l][5]);
                    }
                }
                for (int f = 0; f < dt.Rows.Count; f++) {
                    if (Convert.ToString(condicion_mes_vigente) == Convert.ToString(dt.Rows[f][3]))
                    {
                        mes_contable += Convert.ToDecimal(dt.Rows[f][5]);
                    }
                }
                    //añadiendo valor a las variables de salida
                totalContable = String.Format(ent._Formato_Moneda, mes_contable);
                totalDevengue = String.Format(ent._Formato_Moneda, mes_devengue);
            }
            else {
                totalContable = "0";
                totalDevengue = "0";
            }
          return piramide;
        }
        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
        }
        private String SetCalculaMesDevengue(Int32 anio_devengue_ini, Int32 mes_devengue_ini, Int32 index_row, Int32 mes_actual)
        {
            String devengue = "";
            Int32 anio_devengue = (mes_devengue_ini + index_row) - 1;
            if (mes_actual < 10)
            {
                devengue = anio_devengue_ini + Convert.ToInt32(anio_devengue / 12) + "0" + mes_actual;
            }
            else
            {
                devengue = anio_devengue_ini + Convert.ToInt32(anio_devengue / 12) + "" + mes_actual;
            }
            return devengue;
        }
        private String SetCalcularMesDevengueString(Int32 anio_devengue_ini, Int32 mes_devengue_ini, Int32 index_row, Int32 mes_actual)
        {
            String res_column = "";
            Int32 anio_devengue = (mes_devengue_ini + index_row) - 1;
            if (mes_actual < 10)
            {
                res_column = mes[mes_actual] +" "+Convert.ToInt32(anio_devengue_ini + Convert.ToInt32(anio_devengue / 12));
            }
            else
            {
                res_column = mes[mes_actual]+" "+Convert.ToInt32(anio_devengue_ini + Convert.ToInt32(anio_devengue / 12));
            }
            return res_column;
        }

        public List<eDatoM> listMesDevengue(eDatoM datoM)
        {
            return new dSqlRegistroDatoVC().listMesDevengue(datoM);
        }
    }
}