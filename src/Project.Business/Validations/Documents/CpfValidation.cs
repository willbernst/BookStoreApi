﻿using System;
using System.Linq;

namespace Project.Business.Validations.Documents
{
    public class CpfValidation
    {
        public const int SizeCpf = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumbers = Utils.OnlyNumbers(cpf);

            if (!ValidSize(cpfNumbers)) return false;
            return !HasRepitedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
        }

        private static bool ValidSize(string value)
        {
            return value.Length == SizeCpf;
        }

        private static bool HasRepitedDigits(string value)
        {
            string[] invalidNumbers =
        {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, SizeCpf - 2);
            var verifyingDigit = new VerifyingDigit(number).WithMultipliersOf(2, 11).Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalculateDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(SizeCpf - 2, 2);
        }
    }
}
