using Microsoft.Data.Sqlite;

namespace TiendaDB;


public class PresupuestosRepository
{
    private string _connectionString = "Data Source=DB/Tienda.db;";

    //Metodo para agregar un nuevo presupuesto
    public bool addNewPresupuesto(Presupuestos presupuesto)
    {

        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@Nomb, @Fech);";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Nomb", presupuesto.NombreDestinatario);
                command.Parameters.AddWithValue("@Fech", presupuesto.FechaCreacion1);

                var filasInsertadas = command.ExecuteNonQuery();

                if (filasInsertadas > 0)
                {
                    return true;
                }

                return false;

            }
        }

    }

    //Metodo para listar todos los presupuestos
    public List<Presupuestos> getPresupuestos()
    {

        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM presupuestos;";

            using (var command = new SqliteCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var presupuestos = new List<Presupuestos>();

                    while (reader.Read())
                    {
                        var presupuesto = new Presupuestos();

                        //Carga del presupuesto
                        presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                        presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                        presupuesto.FechaCreacion1 = Convert.ToDateTime(reader["FechaCreacion"]);

                        presupuestos.Add(presupuesto);
                    }
                    return presupuestos;
                }
            }
        }

    }


    //Metodo para listar los presupuestos por ID
    public Presupuestos getPresupuestosById(int idPresupuesto)
    {

        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = @"SELECT 
                            p.idPresupuesto, 
                            p.NombreDestinatario, 
                            p.FechaCreacion, 
                            pr.idProducto, 
                            pr.Descripcion    AS Producto, 
                            pr.Precio, 
                            pd.Cantidad, 
                            ROUND(pr.Precio * pd.Cantidad, 2) AS Subtotal 
                            FROM Presupuestos AS p 
                            JOIN PresupuestosDetalle AS pd ON pd.idPresupuesto = p.idPresupuesto 
                            JOIN Productos AS pr ON pr.idProducto = pd.idProducto 
                            WHERE p.idPresupuesto = @id";
            using (var command = new SqliteCommand(sql, connection))
            {

                command.Parameters.AddWithValue("@id", idPresupuesto);

                using (var reader = command.ExecuteReader())
                {
                    var presupuesto = new Presupuestos();

                    while (reader.Read())
                    {
                        
                        //Carga del presupuesto
                        presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                        presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                        presupuesto.FechaCreacion1 = Convert.ToDateTime(reader["FechaCreacion"]);

                        presupuesto.Detalle = new List<PresupuestoDetalle>();

                        var detalle = new PresupuestoDetalle(); 
                        var producto = new Productos();

                        producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                        producto.Descripcion = reader["Producto"].ToString();
                        producto.Precio = Convert.ToInt32(reader["Precio"]);

                        detalle.Producto = producto;
                        detalle.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                        presupuesto.Detalle.Add(detalle);

                    }
                    return presupuesto;
                }
            }
        }

    }
}



