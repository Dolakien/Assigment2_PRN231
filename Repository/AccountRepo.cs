using BusinessObject.Models;
using DataAccessObject;
using Microsoft.EntityFrameworkCore;
using Repository.Contract.Request;
using Repository.Contract.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public AccountRepo(IJwtProvider jwtProvider, IMapper mapper) {
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<BranchAccount?> addAccount(CreateAccountRequest account)
        {
            var account1 = _mapper.Map<BranchAccount>(account);
            var result = await AccountDAO.Instance.addAccount(account1);
            if (result != null)
            {
                return result;
            }
            return result;
        }

        public async Task<LoginResponse> CheckLogin(LoginRequest loginRequest)
        {
            var result = await AccountDAO.Instance.GetAccountByEmailLogin(loginRequest.email, m => m.Role);
            if (result == null)
            {
                throw new Exception("Not found account with this Email!");
            }

            // Khôi phục khóa từ cơ sở dữ liệu
            var key = Convert.FromBase64String(result.HmacKey); // Giả sử bạn đã lưu khóa trong model

            using var hmac = new HMACSHA512(key); // Sử dụng khóa đã lưu
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.password));
            var computedHashString = Convert.ToBase64String(computeHash);

            // So sánh hash của mật khẩu đầu vào với AccountPassword
            if (computedHashString != result.AccountPassword)
            {
                throw new Exception("Password is invalid!");
            }
            else
            {
                var jwtToken = _jwtProvider.GenerateToken(result);
                var loginResponse = new LoginResponse
                {
                    AccountId = result.AccountId,
                    FullName = result.FullName,
                    JwtToken = jwtToken,
                    RoleName = result.Role.RoleName,
                };
                return loginResponse;
            }
        }




        public BranchAccount GetBranchAccount(string email, string password)
            => AccountDAO.Instance.GetBranchAccount(email, password);

        public async Task<bool> removeAccount(BranchAccount account)
        {
            throw new NotImplementedException();
        }

        public async Task<BranchAccount?> updateAccount(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}
