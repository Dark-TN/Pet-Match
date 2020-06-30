using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DataLayer.Aplicacion
{
    public class BaseDeDatos
    {
        public static DataSet EjecutaProcedimientoAlmacenado(string pmtProcedimientoAlmacenado,
           Hashtable pmtParametros, String pmtCadenaConexion)
        {
            IEnumerator enumerator = null;
            using (SqlConnection cn = new SqlConnection(pmtCadenaConexion))
            {
                cn.Open();


                SqlCommand cmd = new SqlCommand(pmtProcedimientoAlmacenado, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (pmtParametros != null)
                {
                    try
                    {
                        enumerator = pmtParametros.Keys.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            if (RuntimeHelpers.GetObjectValue(enumerator.Current) != null)
                            {
                                string Col = RuntimeHelpers.GetObjectValue(enumerator.Current).ToString();
                                cmd.Parameters.Add(new SqlParameter(Col, RuntimeHelpers.GetObjectValue(pmtParametros[Col])));
                            }
                        }
                    }
                    finally
                    {
                        if (enumerator is IDisposable)
                        {
                            (enumerator as IDisposable).Dispose();
                        }
                    }
                }

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    ds.Tables.Add(dt);
                    cn.Dispose();
                }
                catch (Exception e)
                {
                    throw e;
                }
                return ds;
            }
        }


        public static DataSet EjecutaStore(string storeName, List<SqlParameter> parameters, string strConnection)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmm = new SqlCommand();
            DataSet dts = new DataSet();
            SqlDataAdapter dta = null;
            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandText = storeName;
            cmm.Connection = cnn;

            if (parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    cmm.Parameters.Add(item);

                }
            }




            try
            {
                cnn.Open();
                dta = new SqlDataAdapter(cmm);
                dta.Fill(dts);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }

            return dts;
        }

        
    }
}
