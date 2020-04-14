using Lab1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1.Data
{
    public class CustomUserValidator : UserValidator<User>
    {
        public override Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            User us = manager.Users.Where(c => c.Email == user.Email).FirstOrDefault();
            if(us != null && us != user)
            {
                errors.Add(new IdentityError
                {
                    Description = "This email is used"
                });
            }

            if (user.Email.ToLower().EndsWith("@spam.com"))
            {
                errors.Add(new IdentityError
                {
                    Description = "This domain is in the spam database. Choose another email service"
                });
            }
            if (user.UserName.Contains("admin"))
            {
                errors.Add(new IdentityError
                {
                    Description = "User email must not contain the word 'admin'"
                });
            }

            if(!IsValidEmail(user.Email)  || !isValid1(user.Email))
            {
                errors.Add(new IdentityError
                {
                    Description = "Incorrect email"
                });
            }


            User u = manager.Users.Where(s => s.Nick == user.Nick).FirstOrDefault();
            if(u != null && u != user)
            {
                errors.Add(new IdentityError
                {
                    Description = "This user name is occupied"
                });
            }
            else
            {
                if (user.Nick.Length < 4)
                {
                    errors.Add(new IdentityError
                    {
                        Description = "User name must have minimum 4 characters"
                    });
                }
                if (user.Nick.Length > 50)
                {
                    errors.Add(new IdentityError
                    {
                        Description = "User name must have maximum 50 characters"
                    });
                }
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        private bool isValid1(string email)
        {
            
            int i = 0;
            while(email[i] != '@')
            {
                if (!((email[i] >= '0' && email[i] <= '9') || (email[i] >= 'a' && email[i] <= 'z') || (email[i] >= 'A' && email[i] <= 'Z') || email[i] == '.' || email[i] == '-' || email[i] == '_'))
                    return false;
                else 
                {
                    if (email[i] == '.' && email[i + 1] == '.')
                        return false;
                    if (email[i] == '.' && email[i + 1] == '-')
                        return false;
                    if (email[i] == '.' && email[i + 1] == '_')
                        return false;
                    if (email[i] == '-' && email[i + 1] == '-')
                        return false;
                    if (email[i] == '-' && email[i + 1] == '.')
                        return false;
                    if (email[i] == '-' && email[i + 1] == '_')
                        return false;
                    if (email[i] == '_' && email[i + 1] == '_')
                        return false;
                    if (email[i] == '_' && email[i + 1] == '.')
                        return false;
                    if (email[i] == '_' && email[i + 1] == '-')
                        return false;
                }
                i++;
            }
            return true;
        }
    }

}
