﻿namespace MyStock.Domain;

public class Person : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }

    public string Address { get; set; }
    public string Phone { get; set; }
}
