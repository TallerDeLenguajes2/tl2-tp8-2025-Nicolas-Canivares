
using Microsoft.Data.Sqlite;

namespace TiendaDB;

public class ProductoRepository
{
    private string _connectionString = "Data Source=DB/Tienda.db;";

    //Metodo para agregar un nuevo producto
    public bool addNewProducto(Productos producto)
    {

        using (var connection = new SqliteConnection(_connectionString))
        {

            connection.Open();
            string sql = "INSERT INTO Productos (Descripcion, Precio) Values (@desc, @prec);";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@desc", producto.Descripcion);
                command.Parameters.AddWithValue("@prec", producto.Precio);

                var filasInsertadas = command.ExecuteNonQuery();

                if (filasInsertadas > 0)
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

    //Metodo para modificar un producto existente

    public bool updateProducto(int idProducto, Productos productos)
    {

        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            string sql = "UPDATE Productos SET Descripcion = @desc, precio = @prec WHERE idProducto = @id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@desc", productos.Descripcion);
                command.Parameters.AddWithValue("@prec", productos.Precio);

                command.Parameters.AddWithValue("@id", idProducto);


                var filasModificadas = command.ExecuteNonQuery();

                if (filasModificadas > 0)
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

    public List<Productos> getProductos()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT idProducto,Descripcion,precio FROM Productos";
            using (var command = new SqliteCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var productos = new List<Productos>();
                    while (reader.Read())
                    {
                        var producto = new Productos();
                        //Carga de producto
                        producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                        producto.Descripcion = reader["Descripcion"].ToString();
                        producto.Precio = Convert.ToInt32(reader["Precio"]);

                        productos.Add(producto);

                    }
                    return productos;
                }
            }
        }

    }

    public Productos getProductosById(int idProducto)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT idProducto,Descripcion,precio FROM Productos where idProducto = @idProd";
            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idProd", idProducto);
                using (var reader = command.ExecuteReader())
                {
                    var productoBuscado = new Productos();

                    if (reader.Read())
                    {
                        
                        
                        //Carga de producto
                        productoBuscado.IdProducto = Convert.ToInt32(reader["idProducto"]);
                        productoBuscado.Descripcion = reader["Descripcion"].ToString();
                        productoBuscado.Precio = Convert.ToInt32(reader["Precio"]);

                        return productoBuscado;
                    }

                    return productoBuscado;

                }
            }
        }

    }

    public bool deleteProductoById(int idProducto)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {

            connection.Open();

            var sql = "DELETE FROM Productos WHERE idProducto = @idProd";

            using (var command = new SqliteCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@idProd", idProducto);



                if (command.ExecuteNonQuery() > 0)
                {
                    return true;
                }

                return false;
            }
        }
    }

}