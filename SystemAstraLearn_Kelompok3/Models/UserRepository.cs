using System.Data.SqlClient;

namespace SystemAstraLearn_Kelompok3.Models
{
    public class UserRepository
    {

        //alamat koneksi database
        private readonly string _connectingString;

        //koneksi sql
        private readonly SqlConnection _connection;

        //constructor kelas yang akan kita gunakan untuk mengsetup connection string
        public UserRepository(IConfiguration configuration)
        {
            //mengambil konfigurasi connection string dari appsettings.json
            _connectingString = configuration.GetConnectionString("DefaultCOnnection");

            //koneksi sql menggunakan connection string
            _connection = new SqlConnection(_connectingString);
        }

        public User getDataByUsername_Password(string username)
        {
            User user = new User();
            try
            {
                string query = "SELECT * FROM tb_pengguna WHERE username = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", username);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user.IdPengguna = Convert.ToInt32(reader["id_pengguna"].ToString());
                    user.NamaLengkap = reader["nama_lengkap"].ToString();
                    user.Username = reader["username"].ToString();
                    user.HakAkses = reader["hak_akses"].ToString();
                }
                else
                {
                    // Set IdPengguna to 0 if user not found
                    user.IdPengguna = 0;
                }
                _connection.Close();

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return user;
        }

    }
}
