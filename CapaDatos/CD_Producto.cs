using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProducto, Nombre, Descripcion, PrecioUnidad, Cantidad, Vencimiento, Estado from PRODUCTO");


                    SqlCommand cmd = new SqlCommand(query.ToString(), objConexion);
                    cmd.CommandType = CommandType.Text;

                    objConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                //Codigo = dr["Codigo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                //objCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DescripcionCategoria"].ToString() },

                                PrecioUnidad = Convert.ToDouble(dr["PrecioUnidad"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                Vencimiento = Convert.ToDateTime(dr["Vencimiento"]),
                                //Vencimiento = dtpFechaVencimiento.Value,
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                    //throw;
                }
            }
            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje) //parametro de entrada Producto y string mensaje de salida
        {
            int idProductogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarProducto", objConexion);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    //cmd.Parameters.AddWithValue("IdCategoria", obj.objCategoria.IdCategoria);

                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    objConexion.Open();

                    cmd.ExecuteNonQuery();

                    idProductogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idProductogenerado = 0;
                Mensaje = ex.Message;
                //throw;
            }
            return idProductogenerado;
        }



        public bool Editar(Producto obj, out string Mensaje) //parametro de entrada Producto y string mensaje de salida
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ModificarProducto", objConexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    //cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);

                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    objConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
                //throw;
            }


            return respuesta;
        }


        public bool Eliminar(Producto obj, out string Mensaje) //parametro de entrada Producto y string mensaje de salida
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            //try
            //{
            //    using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            //    {
            //        SqlCommand cmd = new SqlCommand("SP_EliminarProducto", objConexion);
            //        cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
            //        cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
            //        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        objConexion.Open();

            //        cmd.ExecuteNonQuery();

            //        respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
            //        Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    respuesta = false;
            //    Mensaje = ex.Message;
            //    //throw;
            //}
            return respuesta;
        }
    }
}
