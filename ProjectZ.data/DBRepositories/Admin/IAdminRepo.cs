using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.Admin
{
    public interface IAdminRepo
    {
        Task<int> DeleteallSubCAtegory(int id);
        Task<int> DeletePosters(int id);

    }
}
