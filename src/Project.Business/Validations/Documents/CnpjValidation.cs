using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validations.Documents
{
    public class CnpjValidation
    {
        public const int SizeCnpj = 14;

        public static bool Validate(string cnpj)
        {
            var cnpjNumbers = Utils.OnlyNumbers(cnpj);

            if (!HasValideSize(cnpjNumbers)) return false;
            return !HasRepeatedDigit(cnpjNumbers) && HasValideDigits(cnpjNumbers);
        }

        private static bool HasValideSize(string value)
        {
            return value.Length == SizeCnpj;
        }

        private static bool HasRepeatedDigit(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValideDigits(string value)
        {
            var number = value.Substring(0, SizeCnpj - 2);

            var verifyingDigit = new VerifyingDigit(number).WithMultipliersOf(2, 9).Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalculateDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculateDigit();
            
            return string.Concat(firstDigit, secondDigit) == value.Substring(SizeCnpj - 2, 2);
        }
    }
}
