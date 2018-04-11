﻿namespace VKM.Admin.Services.Interfaces
{
    public interface IAuthorizationService
    {
        string Authorize(string userName, string password);
        void Register(string userName, string password, string confirmPassword);
    }
}