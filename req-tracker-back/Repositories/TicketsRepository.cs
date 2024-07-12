using Microsoft.EntityFrameworkCore;
using req_tracker_back.Data;
using req_tracker_back.Models;

namespace req_tracker_back.Repositories
{
    public class TicketsRepository(RTContext context)
    {
        private readonly RTContext _context = context;

        public IEnumerable<Ticket> GetAll(string? filter)
        {
            IQueryable<Ticket> query = _context.Tickets;

            if (filter is not null)
            {
                string filterToLower = filter.ToLower();
                query = query
                    .Where(req => req.Observer.Nickname.ToLower().Contains(filterToLower)
                    || req.Executor.Nickname.ToLower().Contains(filterToLower)
                    || req.Status.Name.ToLower().Contains(filterToLower)
                    || req.Text.ToLower().Contains(filterToLower));
            }

            query = query.Include(p => p.Status)
                    .Include(p => p.Observer)
                    .Include(p => p.Executor);

            return query.OrderBy(p => p.Id).ToList();
        }

        public Ticket GetById(int id)
        {
            try
            {
                return _context.Tickets.Include(p => p.Status)
                    .Include(p => p.Observer)
                    .Include(p => p.Executor)
                    .First(req => req.Id == id);                    
            }
            catch
            {
                throw new ArgumentException("Заявка не найдена!");
            }
        }

        public int Create(Ticket request)
        {
            _context.Attach(request);
            _context.SaveChanges();
            return request.Id;
        }

        public void Delete(int id)
        {
            _context.Tickets
                .Where(req => req.Id == id)
                .ExecuteDelete();
        }

        public void Update(Ticket value)
        {
            _context.Tickets.Attach(value);
            _context.Entry(value).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            return _context.Status;
        }
    }
}