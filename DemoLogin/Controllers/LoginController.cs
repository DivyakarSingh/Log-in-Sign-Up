using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoLogin.Models;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Net.Http;
using System.Web.Script.Serialization;// Add this using directive // Make sure you have this using directive for List<T>

namespace DemoLogin.Controllers
{
    public class LoginController : Controller
    {
        string connectionString = @"Data Source=DIVYAKAR;Initial Catalog=DivDB;Integrated Security=true";
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult SignUp()
        {

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Home()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }


            return View();

        }







        [HttpPost]
        public ActionResult Index(User u)
        {

            if (ModelState.IsValid == true)
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string quary = "SELECT username,password,saltkey FROM login WHERE username=@Username";

                    SqlCommand sqlCmd = new SqlCommand(quary, sqlCon);

                    sqlCmd.Parameters.AddWithValue("@username", u.username);

                    SqlDataReader sdr = sqlCmd.ExecuteReader();

                    if (sdr.Read())
                    {
                        string storedHashedPassword = sdr["password"].ToString();
                        string storedSalt = sdr["saltkey"].ToString();
                        PasswordHasher passwordHasher = new PasswordHasher();

                        string hashedPasswordToCheck = passwordHasher.HashPassword(u.password, storedSalt);

                        if (storedHashedPassword == hashedPasswordToCheck)
                        {
                            Session["user"] = u.username;
                            return RedirectToAction("Home", "Login");
                        }
                        else
                        {

                            ViewBag.ErrorMessage = "Crediential Incorrect";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User not found";
                    }
                }
            }
            return View();



            //return View();

            //ViewData["SuccessMessage"] = "<script>alert('LogIn Successful')</script>";
            //ModelState.Clear();


        }
        public class PasswordHasher
        {
            public string GenerateSalt()
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    byte[] saltBytes = new byte[16];
                    rng.GetBytes(saltBytes);
                    return Convert.ToBase64String(saltBytes);
                }
            }

            public string HashPassword(string password, string salt)
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
                    byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                    return Convert.ToBase64String(hashBytes);
                }
            }

        }


        [HttpPost]
        public ActionResult SignUp(SignUp u)
        {
            if (ModelState.IsValid == true)
            {

                PasswordHasher passwordHasher = new PasswordHasher();
                string salt = passwordHasher.GenerateSalt();
                string hashedPassword = passwordHasher.HashPassword(u.Password, salt);
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string query = "Insert into login(Username,password,saltkey) values(@Username,@Password,@salt)";
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.Parameters.AddWithValue("@Username", u.Name);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    int a = cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    if (a != 0)
                    {

                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ViewBag.ErrorMessege("Enter the data");
                    }


                }

                //ViewData["SuccessMessage"] = "<script>alert('SignUp Successful')</script>";
                //ModelState.Clear();
            }
            return View();
        }

      

    }
}

