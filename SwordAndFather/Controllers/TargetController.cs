using Microsoft.AspNetCore.Mvc;
using SwordAndFather.Data;
using SwordAndFather.Models;

namespace SwordAndFather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddTarget(CreateTargetRequest createRequest)
        {
            var repository = new TargetRepository(); // has add target method that takes in whats required to create a target

            var newTarget = repository.AddTarget(createRequest.Name, 
                                 createRequest.Location,
                                 createRequest.FitnessLevel,
                                 createRequest.UserId);

            return Created($"/api/target/{newTarget.Id}", newTarget); // returning out the result of that add to the database
        }

    }
}