﻿using System;
namespace PatientAnalyticsMaui.Models.Payload;

public class LoginPayload
{
  public LoginPayload(string username, string password)
  {
    Username = username;
    Password = password;
  }

  public string Username { get; private set; }
  public string Password { get; private set; }
}

