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
                            p.FechaCreacion
                            FROM Presupuestos AS p 
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
                    }
                    return presupuesto;
                }
            }
        }

    }

    public bool updatePresupuesto(Presupuestos presupuesto)
    {

        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = "UPDATE Presupuestos SET NombreDestinatario = @Nomb, FechaCreacion = @Fech WHERE idPresupuesto = @Id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Nomb", presupuesto.NombreDestinatario);
                command.Parameters.AddWithValue("@Fech", presupuesto.FechaCreacion1);
                command.Parameters.AddWithValue("@Id", presupuesto.IdPresupuesto);

                var filasInsertadas = command.ExecuteNonQuery();

                if (filasInsertadas > 0)
                {
                    return true;
                }

                return false;

            }
        }

    }

    public bool deletePresupuesto(int id)
    {
    
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string sql1 = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";
                using (var command = new SqliteCommand(sql1, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }

                string sql2 = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";

                using (var command = new SqliteCommand(sql2, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    var filasInsertadas = command.ExecuteNonQuery();
                    if (filasInsertadas > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

//REVISAR ---------------------
            public Presupuestos getDetallesPresupuesto(int id)
    {
        using (var conexion = new SqliteConnection(_connectionString))
        {
            conexion.Open();

            string consulta = @"
                SELECT  p.IdPresupuesto, p.NombreDestinatario, p.FechaCreacion, pd.IdProducto, pd.Cantidad, pr.Descripcion, pr.Precio
                FROM Presupuestos p
                LEFT JOIN PresupuestosDetalle pd USING (IdPresupuesto)
                LEFT JOIN Productos pr USING (IdProducto)
                WHERE p.IdPresupuesto = @Id;";

            var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@Id", id));

            using var reader = comando.ExecuteReader();

            Presupuestos? presupuesto = null;
            var detalles = new List<PresupuestoDetalle>();

            while (reader.Read())
            {
                
                if (presupuesto == null)
                {
                    presupuesto = new Presupuestos();
                    presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();

                    var iFecha = reader.GetOrdinal("FechaCreacion");
                    presupuesto.FechaCreacion1 = Convert.ToDateTime(reader.GetDateTime(iFecha));
                    presupuesto.Detalle = detalles;
                }

                if (!reader.IsDBNull(reader.GetOrdinal("IdProducto")))
                {
                    var producto = new Productos(
                        Convert.ToInt32(reader["IdProducto"]),
                        reader["Descripcion"]?.ToString(),
                        Convert.ToInt32(reader["Precio"])
                    );

                    var detalle = new PresupuestoDetalle(
                        producto,
                        Convert.ToInt32(reader["Cantidad"])
                    );

                    detalles.Add(detalle);
                }
            }

            return presupuesto;
        }
    }
    
    public bool agregarDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        using (var conexion = new SqliteConnection(_connectionString))
        {
            conexion.Open();
            string sql = "INSERT INTO PresupuestosDetalle (IdPresupuesto, IdProducto, Cantidad) VALUES (@IdPre, @IdPro, @Cant)";

            var command = new SqliteCommand(sql, conexion);

            command.Parameters.Add(new SqliteParameter("@IdPre", idPresupuesto));
            command.Parameters.Add(new SqliteParameter("@IdPro", idProducto));
            command.Parameters.Add(new SqliteParameter("@Cant", cantidad));

            int filas = command.ExecuteNonQuery();
            if (filas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}



