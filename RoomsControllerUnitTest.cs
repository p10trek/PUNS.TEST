using PunsApi.Controllers;
using PunsApi.Data;
using PunsApi.Requests.Games;
using PunsApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PUNS.TESTS
{
    public class RoomsIntegrationTest : TestServerDependent
    {
        private readonly RoomsController controller;
        private readonly GamesController games_controller;
        private IRoomsService service;
        private IGamesService game_service;
        private readonly AppDbContext dbContext;
        private IAuthenticationService _authenticationService;
        private AuthenticationController authController;
        public RoomsIntegrationTest(TestServerFixture fixture) : base(fixture)
        {
            game_service = fixture.GetService<IGamesService>();
            service = fixture.GetService<IRoomsService>();
            dbContext = fixture.GetService<AppDbContext>();
            games_controller = new GamesController(game_service);
            controller = new RoomsController(service);
            _authenticationService = GetService<IAuthenticationService>();
            authController = new AuthenticationController(_authenticationService);
        }
        [Fact]
        public async Task IntegrationTest()
        {
            var loginResponse = await authController.Login(new PunsApi.Requests.Authentication.LoginRequest 
            { 
                Email = "p10trek@o2.pl",
                Password = "pwsz00145tfe_!a" 
            });
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)loginResponse).StatusCode == 200);
            var createRoomRespose = await controller.Create(new PunsApi.Requests.Rooms.CreateRoomRequest
            {
                PlayerMaxCount = 5,
                PlayerMinCount = 2,
                RoomName = "Testowy Pokoj"
            });
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)createRoomRespose).StatusCode == 200);
            var createGameResponse = await games_controller.Create(new CreateGameRequest { GameName = "Gra Testowa" });
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)createRoomRespose).StatusCode == 200);
            var fetchGameResponse = await games_controller.FetchGame();
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)fetchGameResponse).StatusCode == 200);
        }
    }
}
