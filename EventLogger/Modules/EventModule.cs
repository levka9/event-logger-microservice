using Nancy;
using System;
using System.Linq;
using EventLogger.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventLogger.Entities;
using Microsoft.EntityFrameworkCore;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Microservices.Helper;

namespace EventLogger.Modules
{
    public class EventModule : NancyModule
    {
        EventLoggerContext dbContext;

        public EventModule(EventLoggerContext DbContext) : base("event")
        {
            this.dbContext = DbContext;
            
            base.Get("/{eventId:long}", async (parameters, _) =>
            {
                return await this.Get(parameters);
            });

            base.Get("/getByUserId/{userId:long}", async (parameters, _) =>
            {
                return await this.GetByUserId(parameters);
            });

            base.Post("add", async (parameters, _) =>
            {
                long response = await Add(parameters);
                return Response.AsJson<long>(response);                 
            });
        }

        private async Task<string> Get(dynamic parameters)
        {
            var eventId = (long)parameters.eventId;

            var eventLog = await dbContext.Event.Include(x => x.EventDetails)
                                                .Include(x => x.EventType)
                                                .FirstOrDefaultAsync(x => x.Id == eventId)
                                                .ConfigureAwait(false);


            return await JsonConvertHelper.GetIgnoreLooping(eventLog).ConfigureAwait(false);
        }

        private async Task<string> GetByUserId(dynamic parameters)
        {
            var userId = (long)parameters.userId;

            var users = await dbContext.Event.Include(x => x.EventDetails)
                                             .Include(x => x.EventType)
                                             .Where(x => x.UserId == userId.ToString())
                                             .OrderByDescending(x => x.CreatedDate)
                                             .Take(50)
                                             .ToListAsync()
                                             .ConfigureAwait(false);

            return await JsonConvertHelper.GetIgnoreLooping(users).ConfigureAwait(false);
        }

        private async Task<long> Add(dynamic parameters)
        {
            var eventLog = this.Bind<Event>();

            //TODO: Get userId from token;
            var userId = 12;

            eventLog.UserId = userId.ToString();

            try
            {
                await dbContext.Event.AddAsync(eventLog).ConfigureAwait(false);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            return eventLog.Id;
        }
    }
}
