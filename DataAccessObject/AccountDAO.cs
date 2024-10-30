using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class AccountDAO
    {
        private SilverJewelry2023DbContext _context;
        private static AccountDAO instance;

        public AccountDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public List<BranchAccount> GetBranchAccounts()
        {
            return _context.BranchAccounts.ToList();
        }

        public BranchAccount GetBranchAccount(String email, String password)
        {
            return _context.BranchAccounts.SingleOrDefault(m => m.EmailAddress.Equals(email) && m.AccountPassword.Equals(password));

        }

        public BranchAccount GetAccountByID(int accountID)
        {
            return _context.BranchAccounts.SingleOrDefault(m => m.AccountId.Equals(accountID));

        }
        public async Task<BranchAccount> GetAccountByEmailLogin(string email, params Expression<Func<BranchAccount, object>>[] includeTables)
        {
            // Sử dụng Where thay vì SingleOrDefault để giữ nguyên kiểu IQueryable
            var query = _context.BranchAccounts.Where(x => x.EmailAddress == email);

            // Kiểm tra nếu có các bảng cần Include
            if (includeTables != null)
            {
                foreach (var includeTable in includeTables)
                {
                    query = query.Include(includeTable);
                }
            }

            // Trả về bản ghi đầu tiên hoặc null (FirstOrDefaultAsync) một cách bất đồng bộ
            return await query.FirstOrDefaultAsync();
        }
        public void Update(BranchAccount account)
        {
            _context.BranchAccounts.Update(account);
        }

        public void Add(BranchAccount account)
        {
            _context.BranchAccounts.Add(account);
        }
    
        public void Delete(BranchAccount account)
        {
             _context.BranchAccounts.Remove(account);
        }

        public async Task<BranchAccount?> addAccount(BranchAccount account)
        {
            BranchAccount account1 = this.GetAccountByID(account.AccountId);
            if (account1 == null)
            {
                try
                {
                    using var hmac = new HMACSHA512(); // Tạo HMAC với khóa ngẫu nhiên
                    account.AccountPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(account.AccountPassword)));

                    // Lưu khóa vào cơ sở dữ liệu (có thể cần thêm cột cho khóa này)
                    account.HmacKey = Convert.ToBase64String(hmac.Key); // Giả sử bạn có thuộc tính HmacKey trong model

                    // Thêm account vào database
                    _context.BranchAccounts.Add(account);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ nếu cần
                }
            }

            return account1;
        }


        public async Task<BranchAccount?> UpdateAccount(BranchAccount account)
        {
            BranchAccount account1 = this.GetAccountByID(account.AccountId);
            if (account1 != null)
            {
                try
                {
                    _context.Entry(account1).CurrentValues.SetValues(account);
                    _context.SaveChanges();
                    return account1;
                }
                catch (Exception ex)
                {
                }
            }
            return null;
        }

        public async Task<bool> RemoveAccount(int accountId)
        {
            bool result = false;
            BranchAccount account1 = this.GetAccountByID(accountId);
            if (account1 != null)
            {
                try
                {
                    _context.BranchAccounts.Remove(account1);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

    }
}
