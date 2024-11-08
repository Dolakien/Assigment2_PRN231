﻿using BusinessObject.Models;
using Repository.Contract.Request;
using Repository.Contract.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAccountRepo
    {
        public Task<LoginResponse> CheckLogin(LoginRequest loginRequest);
        public BranchAccount GetBranchAccount(string email, string password);
        public Task<BranchAccount?> addAccount(CreateAccountRequest account);
        public bool removeAccount(int accountId);
        public bool updateAccount(BranchAccount account);
        public BranchAccount GetBranchAccountById(int id);
        public List<BranchAccount> GetAccounts();
    }
}
