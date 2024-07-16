using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.GetInfo
{
    public interface IGetInfoRepo
    {
        Task<int> GetById(int id);
    }
}
