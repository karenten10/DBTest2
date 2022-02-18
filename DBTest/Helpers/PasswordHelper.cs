using InspectionBlazor.AdapterModels;
using InspectionBlazor.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InspectionBlazor.Helpers
{
    public static class PasswordHelper
    {
        public static void GetPasswordSHA(PersonAdapterModel model)
        {
            if (string.IsNullOrEmpty(model.Salt))
                model.Salt = Guid.NewGuid().ToString();
            string assemblyPassword = $"{model.PasswordPlainText}-{model.Salt}@";
            SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(assemblyPassword));
            StringBuilder builder = new StringBuilder();
            for (int j = 0; j < bytes.Length; j++)
            {
                builder.Append(bytes[j].ToString("x2"));
            }
            model.Password = builder.ToString();
        }
        public static (string salt, string password) GetPasswordSHA(string salt, string password)
        {
            if (string.IsNullOrEmpty(salt))
                salt = Guid.NewGuid().ToString();
            string assemblyPassword = $"{password}-{salt}@";
            SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(assemblyPassword));
            StringBuilder builder = new StringBuilder();
            for (int j = 0; j < bytes.Length; j++)
            {
                builder.Append(bytes[j].ToString("x2"));
            }
            password = builder.ToString();
            return (salt, password);
        }
    }
}
