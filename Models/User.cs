using System;
namespace project_api_poo_ii.Models

public class User
{
    private integer Id { get; }
    private string firstName { get; set; }
    private string lastName { get; set; }
    private string Name { get; } = firstName + ' ' + lastName
    private string email { get; set; }
    private string telephone { get; set; }
    private DateTime birthDate { get; set; }
    private string document { get; set; }
}