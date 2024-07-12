using Microsoft.EntityFrameworkCore;
using req_tracker_back.Data;
using req_tracker_back.Models;

namespace req_tracker_back.Repositories
{
    public class UsersRepository(RTContext context)
    {
        private readonly RTContext _context = context;

        public async Task<User?> GetUser(string login)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login);
        }

        public IEnumerable<User> GetAll(string? filter)
        {
            IQueryable<User> query = _context.Users;

            if (filter is not null)
            {
                string filterToLower = filter.ToLower();
                query = query.Where(p => p.Nickname.ToLower().Contains(filterToLower));
            }

            return query.ToList();
        }
    }
}