using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/petname")]
public class PetNameController : ControllerBase
{
    private static readonly string[] DogNames = { "Renato", "Charles", "Lance", "Felix", "Rohann" };
    private static readonly string[] CatNames = { "Poosee", "Whitey", "Blackie", "Powsi", "Pspsps" };
    private static readonly string[] BirdNames = { "Twitter", "Birdie", "Cheerp", "Raven", "Maya" };

    [HttpPost("generate")]
    public IActionResult Generate([FromBody] PetNameRequest request)
    {
        string[] names;

        switch (request.AnimalType.ToLower())
        {
            case "dog":
                names = DogNames;
                break;
            case "cat":
                names = CatNames;
                break;
            case "bird":
                names = BirdNames;
                break;
            default:
                return BadRequest(new { error = "Invalid animal type." });
        }

        var random = new Random();
        string petName = names[random.Next(names.Length)] +
            (request.TwoPart == true ? " " + names[random.Next(names.Length)] : "");

        return Ok(new { name = petName });
    }
}

public class PetNameRequest
{
    public string AnimalType { get; set; }
    public bool? TwoPart { get; set; }
}
