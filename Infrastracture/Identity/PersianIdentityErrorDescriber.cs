using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Identity
{
  public  class PersianIdentityErrorDescriber:IdentityErrorDescriber
    {

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code =nameof(DuplicateEmail),Description=$"ایمیل {email} در سیستم موجود میباشد"};
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code =nameof(DuplicateUserName),Description=$"نام کاربری {userName} در سیستم موجود میباشد"};         
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError { Code = nameof(InvalidEmail), Description = $"ایمیل {email} معتبر نمیباشد" };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError { Code = nameof(InvalidToken), Description = "توکن امنیتی معتبر نمیباشد" };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError { Code = nameof(InvalidUserName), Description = $"نام کاربری {userName} معتبر نمیباشد" };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError { Code = nameof(PasswordMismatch), Description = "رمزعبور صحیح نمیباشد" };

        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "رمزعبور شامل اعداد میباشد" };

        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "رمزعبور شامل حروف کوچک میباشد" };

        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "رمزعبور شامل علامت های غیر الفبایی میباشد" };

        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return base.PasswordRequiresUniqueChars(uniqueChars);
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "رمزعبور شامل حروف بزرگ میباشد" };

        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = nameof(PasswordTooShort), Description = "رمزعبور کوتاه میباشد" };

        }
    }
}
