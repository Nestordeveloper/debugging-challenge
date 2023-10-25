using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DebuggingChallenge.Models;

namespace DebuggingChallenge.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Hint on a few errors: pay close attention to how things are named

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("user/create")]
    public IActionResult CreateUser(User newUser)
    {
        if (ModelState.IsValid)
        {
            HttpContext.Session.SetString("Name", newUser.Name);
            if (newUser.Location != null)
            {
                HttpContext.Session.SetString("Location", $"{newUser.Location}");
            }
            else
            {
                HttpContext.Session.SetString("Location", "Undisclosed");
            }
            return RedirectToAction("Generator");
        }
        else
        {
            return View("Index");
        }
    }

    [HttpGet("generator")]
    public IActionResult Generator()
    {
        if (HttpContext.Session.GetString("Name") == null)
        {
            return RedirectToAction("Index");
        }
        if (HttpContext.Session.GetString("Passcode") == null)
        {
            GeneratePasscode();
        }
        return View();
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    // Hint: Something isn't right here...
    // Se cambia el nombre del cshtml a Generator, se pudo haber resuelto de dos formas
    // Cambiando el nombre de los archivos o de la ruta correspondiente a las solicitudes.
    [HttpPost("generate/new")]
    public IActionResult GenerateNew()
    {
        GeneratePasscode(); // Se agrega la función de generar el código, ya que antes no estaba dentro de la solicitud
        return RedirectToAction("Generator");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public void GeneratePasscode()
    {
        string passcode = "";
        string CharOptions = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string NumOptions = "0123456789";
        Random rand = new Random();
        for (int i = 1; i < 15; i++)
        {
            int odds = rand.Next(2);
            if (odds == 0)
            {
                passcode += CharOptions[rand.Next(CharOptions.Length)];
            }
            else
            {
                passcode += NumOptions[rand.Next(NumOptions.Length)];
            }
        }
        HttpContext.Session.SetString("Passcode", passcode);
    }
}
