#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace DebuggingChallenge.Models;

public class User
{
    [Required(ErrorMessage = "Name must be provided")]
    public string Name { get; set; }

    public string? Location { get; set; }
}