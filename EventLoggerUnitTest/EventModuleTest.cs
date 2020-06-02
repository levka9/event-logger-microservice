using System;
using System.Collections.Generic;
using System.Text;
using Nancy.Testing;
using Nancy;
using Xunit;
using System.Threading.Tasks;
using EventLogger.Entities;
using EventLogger.Models.Responses;
using Newtonsoft.Json;

namespace EventLoggerUnitTest
{
    public class EventModuleTest
    {

        Browser sut;

        public EventModuleTest()
        {
            var bootstrapper = new DefaultNancyBootstrapper();

            this.sut = new Browser(
                bootstrapper,
                defaultsTo => defaultsTo.Accept("application/json"));
        }

        [Fact]
        public async Task GetEventTest()
        {
            var actual = await sut.Get("/event/5");
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
        }

        [Fact]
        public async Task RegisterNewEvent()
        {
            var expected = new Event()
            {
                EventTypeId = 2,
                UserId = "14",
                AppicationName = "test1111",
                ProccessId = "1111"
            };

            var jsonExpected = JsonConvert.SerializeObject(expected);

            var addEventResponse = await sut.Post("/event/add", with =>
            {
                with.JsonBody(expected);
            });

            var eventAddReponse = addEventResponse.Body.DeserializeJson<EventAddReponse>();

            var actual = await sut.Get($"/event/{eventAddReponse.EventId}");

            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
            Assert.Equal(expected.UserId, actual.Body.DeserializeJson<Event>().UserId);
        }
    }
}
