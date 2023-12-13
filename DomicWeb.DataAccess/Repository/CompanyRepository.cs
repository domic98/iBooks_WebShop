using DomicWeb.DataAccess.Data;
using DomicWeb.DataAccess.Repository.IRepository;
using DomicWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomicWeb.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {

        private ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }


        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
