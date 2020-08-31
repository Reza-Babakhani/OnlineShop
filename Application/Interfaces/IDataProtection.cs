using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
  public  interface IDataProtection
    {
        string Encrypt(string text, string keyString);

        string Decrypt(string cipherText, string keyString);
    }
}
