using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodedHomes.DataContracts;
using CodedHomes.Models;

namespace CodedHomes.Data
{
    public class ApplicationUnit : IDisposable
    {

        private DataContext _context = new DataContext();

        private IRepository<Home> _homes = null;
        private IRepository<User> _users = null;

        public IRepository<Home> Homes
        {
            get
            {
                if (_homes == null)
                {
                    _homes = new HomesRepository(_context);
                }
                return _homes;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UsersRepository(_context);
                }
                return _users;
            }
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
