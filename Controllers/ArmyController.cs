using BornfightTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BornfightTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArmyController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> BeginBattle([FromQuery, Required] int army1, [FromQuery, Required] int army2)
        {
            if(army1 < 1 || army2 < 1)
            {
                return BadRequest("Invalid parameters, armies have to have soldiers.");
            }

            bool isBattleOver = false;
            Armies armies = new Armies(army1, army2);
            
            List<string> log = new List<string>();
            log.Add("Battle begins! Army1 has " + army1 + " soldiers. Army2 has" + army2 + " soldiers.");

            int numberOfClashes = 0;
            while (!isBattleOver)
            {
                numberOfClashes++;

                (armies, log) = Armies.Clash(armies, log);

                log.Add("Clash #" + numberOfClashes + " is over. Army1 has " + armies.Army1 + " soldiers remaining. Army2 has " + armies.Army2 + " soldiers remaining.");
                if (armies.Army1 < 1 || armies.Army2 < 1)
                {
                    isBattleOver = true;
                }
            }


            if (armies.Army2 < 1)
            {
                log.Add("Army1 won!");
                return Ok(log);
            }  
            else if (armies.Army1 < 1)
            {
                log.Add("Army2 won!");
                return Ok(log);
            }
            else
            {
                log.Add("Something bad happened:/");
                return Ok(log);
            }
        }
    }
}
