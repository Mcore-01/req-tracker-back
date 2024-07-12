using Microsoft.AspNetCore.SignalR;
using req_tracker_back.Data;
using System.Collections.Concurrent;

namespace req_tracker_back.SignalR
{
    public class TicketHud(RTContext context) : Hub
    {
        private readonly RTContext _context = context;
        private static ConcurrentDictionary<int, string> userByTicket = new ();
        
        public async Task LockAccessTicket(int ticketId)
        {
            var userCon = Context.ConnectionId;
            if (userByTicket.ContainsKey(ticketId))
            {
                await Clients.Caller.SendAsync("ErrorEditTicket", $"Заявка с номером {ticketId} уже редактируется!");
            }
            else
            {
                var ticket = _context.Tickets.FirstOrDefault(p => p.Id == ticketId);
                if (ticket is not null)
                {
                    ticket.IsLocked = true;
                    await _context.SaveChangesAsync();
                     
                    userByTicket.TryAdd(ticketId, userCon);

                    await Clients.Caller.SendAsync("EditTicket", ticketId);
                    await Clients.All.SendAsync("UpdateTicketStatuses", $"Заявка с номером: {ticketId}. Редактируется");
                }
            }
        }

        public async Task UnLockAccessTicket(int ticketId)
        {
            if (userByTicket.ContainsKey(ticketId) && userByTicket.TryRemove(ticketId, out _))
            {
                var ticket = _context.Tickets.FirstOrDefault(p => p.Id == ticketId);
                if (ticket is not null)
                {
                    ticket.IsLocked = false;
                    await _context.SaveChangesAsync();
                }

                await Clients.All.SendAsync("UpdateTicketStatuses", $"Заявка с номером: {ticketId}. Обновлена");
                await Clients.All.SendAsync("NotifyAboutTicketUpdate", ticketId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var pair = userByTicket.First(p => p.Value == Context.ConnectionId);
                await UnLockAccessTicket(pair.Key);
            }
            catch { }
        }

        public async Task SendMessageAboutAdding(int ticketId)
        {
            await Clients.All.SendAsync("NotifyAboutTicketCreate", ticketId);
        }
    }
}
