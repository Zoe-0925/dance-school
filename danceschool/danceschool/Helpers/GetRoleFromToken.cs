using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace danceschool.Helpers
{
    public static class AuthHelper
    {
        public static async Task<string> GetRoleFromTokenAsync(string token)
        {
            FirebaseToken decoded = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            object isAdmin;
            decoded.Claims.TryGetValue("admin", out isAdmin);
            object isStudent;
            decoded.Claims.TryGetValue("student", out isStudent);
            if (isAdmin != null){
                Console.WriteLine("Role: admin");
                return "admin";
          }
           else{
                Console.WriteLine("Role: student");
                return "student";
            }
        }
    }
}
