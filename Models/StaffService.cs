using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.DiaSymReader;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace ADONET.Models
{
    public class StaffService:IStaff
    {

        private readonly string _connectionString;
        public StaffService( IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }
        
        public async Task AddStaff(Staffs staff )
        {
            using var con =new SqlConnection(_connectionString );
            using var cmd = new SqlCommand("AddStaff", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = staff.StaffID;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = staff.StaffName;
            cmd.Parameters.Add("@email", System.Data.SqlDbType.NChar).Value = staff.StaffEmail;
            cmd.Parameters.Add("@phone",System.Data.SqlDbType.VarChar).Value= staff.StaffPhone;
            cmd.Parameters.Add("@address", System.Data.SqlDbType.VarChar).Value = staff.staffAddress;

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            
               
        }

        public async Task DeleteStaff(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("Deletestaff", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id;
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task<IEnumerable<Staffs>> Getallstaffs()
        {
            var staffs = new List<Staffs>();
            using var con = new SqlConnection(_connectionString);
            // using var cmd = new SqlCommand("SELECT * FROM Staffs", con);
            using var cmd = new SqlCommand("SHOWEMPLOYEE", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
                               var staff = new Staffs
                {
                    StaffID = reader.GetInt32(0),
                    StaffName = reader.GetString(1),
                    StaffEmail = reader.GetString(2),
                    StaffPhone = reader.GetString(3),
                    staffAddress = reader.GetString(4)
                };
                staffs.Add(staff);
            }
            return staffs;


        }
        public async Task<Staffs> Getstaffbyid(int id)
        {
            var staff = new Staffs();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GETEMPBYID", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id;
            

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
               staff=new Staffs
                {
                    StaffID = reader.GetInt32(0),
                    StaffName = reader.GetString(1),
                    StaffEmail = reader.GetString(2),
                    StaffPhone = reader.GetString(3),
                    staffAddress = reader.GetString(4)
                };
            }
            return staff;
        }
        public async Task Updatestaff(Staffs staff)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("Updatestaff", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = staff.StaffID;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.Char).Value = staff.StaffName;
            cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = staff.StaffEmail;
            cmd.Parameters.Add("@phone", System.Data.SqlDbType.VarChar).Value = staff.StaffPhone;
            cmd.Parameters.Add("@address", System.Data.SqlDbType.VarChar).Value = staff.staffAddress;

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
