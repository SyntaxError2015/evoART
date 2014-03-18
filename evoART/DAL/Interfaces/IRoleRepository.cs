﻿using System;
using evoART.Models;

namespace evoART.DAL.Interfaces
{
    interface IRoleRepository : IStandardInterface, IDisposable
    {
        string[] GetRoleNames();

        void Insert(AccountModels.Role role);

        void Delete(int roleId);

        void Update(AccountModels.Role role);
    }
}
