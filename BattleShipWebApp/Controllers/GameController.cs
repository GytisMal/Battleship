using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace BattleShipWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public GameController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public Game Get()
        {
            return new Game(10, 10);
        }

        [HttpGet]
        [Route("[action]")]
        public BoardDTO GetUserGame(string userName)
        {
            if (GameFlow.Games.ContainsKey(userName))
            {
                return new BoardDTO()
                {
                    CurrentBoard = GameFlow.Games[userName].battleBoard.CurrentBoard,                       
                    ShipsCount = GameFlow.Games[userName].battleBoard.Ships.Where(ship => ship.isHit == false).ToList().Count
                };
            }
            else
                return null ;
        }

        [HttpPost]
        [Route("[action]")]
        public bool StartNewGame([FromBody] LoginDTO login)
        {
            GameFlow.StartNewGame(login.UserName, login.BoardSize, login.ShipsCount);
            if (GameFlow.Games.ContainsKey(login.UserName))
                return true;
            else
                return false;
        }

        [HttpPost]
        [Route("[action]")]
        public AttackReturnDTO Attack([FromBody] AttackPerformDTO attack)
        {
            if (GameFlow.Games.ContainsKey(attack.UserName))
            {
                Game game = GameFlow.Games[attack.UserName];
                AttackReturnDTO dto = new AttackReturnDTO();
                try
                {
                    CellType attackResult = game.battleBoard.Attack(attack.X, attack.Y);
                    dto.CellType = (int)attackResult;
                    dto.Information = attackResult.ToString();
                    dto.Ships = game.battleBoard.Ships.Where(ship => ship.isHit == false).ToList().Count;
                }
                catch(Exception ex) 
                {
                    dto.Information = ex.Message;
                }
                return dto;
            }
            else
                return new AttackReturnDTO() { Information = $"Game for user {attack.UserName} does not exists. Start New Game." };
        }
    }
}
