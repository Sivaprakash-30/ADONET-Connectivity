namespace ADONET.Models
{
    public interface IStaff
    {
        Task< IEnumerable<Staffs> > Getallstaffs();
        Task<Staffs> Getstaffbyid(int id);

       Task AddStaff(Staffs staff);
        Task Updatestaff(Staffs staff);
         Task DeleteStaff(int id);


        

    }
}
